using Microsoft.AspNetCore.Mvc;
using WebApiProject.Services;
using WebApiProject.DTOs;

namespace WebApiProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class YHController : ControllerBase
{
    private readonly IYahooFinanceService _yahooFinanceService;
    private readonly IAwsParameterStoreService _parameterStore;
    private readonly ILogger<YHController> _logger;
    private const string ApiKeyParameterName = "/WebApiProject/YfApi/ApiKey";

    public YHController(
        IYahooFinanceService yahooFinanceService, 
        IAwsParameterStoreService parameterStore,
        ILogger<YHController> logger)
    {
        _yahooFinanceService = yahooFinanceService;
        _parameterStore = parameterStore;
        _logger = logger;
    }

    [HttpGet("quote")]
    public async Task<IActionResult> GetQuote([FromQuery] string symbols, [FromQuery] string? region = "US", [FromQuery] string? lang = "en")
    {
        // Create request DTO
        var request = new QuoteRequest
        {
            Symbols = symbols ?? string.Empty,
            Region = region ?? "US",
            Language = lang ?? "en"
        };

        // Validate request
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _yahooFinanceService.GetQuoteAsync(request);
        
        return result.IsSuccess
            ? Ok(result.Value!)
            : MapErrorToActionResult(result.Error!);
    }

    [HttpPost("quote")]
    public async Task<IActionResult> GetQuotePost([FromBody] QuoteRequest request)
    {
        // Validate request
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _yahooFinanceService.GetQuoteAsync(request);
        
        return result.IsSuccess
            ? Ok(result.Value!)
            : MapErrorToActionResult(result.Error!);
    }

    [HttpGet("trending/{region}")]
    public async Task<IActionResult> GetTrending(string region)
    {
        // Create request DTO
        var request = new TrendingRequest
        {
            Region = region ?? string.Empty
        };

        // Validate request
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _yahooFinanceService.GetTrendingAsync(request);
        
        return result.IsSuccess
            ? Ok(result.Value!)
            : MapErrorToActionResult(result.Error!);
    }

    [HttpGet("health")]
    public async Task<IActionResult> Health()
    {
        // Check if Parameter Store has the API key
        var parameterStoreResult = await _parameterStore.GetParameterAsync(ApiKeyParameterName);
        var hasParameterStoreKey = parameterStoreResult.IsSuccess;
        
        var supportedRegions = _yahooFinanceService.GetSupportedRegions();
        
        var healthResponse = new HealthResponse
        {
            Service = "YH Controller",
            Status = "Healthy",
            ApiKeyConfigured = hasParameterStoreKey,
            ApiKeySource = hasParameterStoreKey ? "AWS Parameter Store" : "Configuration Fallback",
            ParameterStorePath = ApiKeyParameterName,
            SupportedRegions = supportedRegions,
            Timestamp = DateTime.UtcNow,
            Message = "YH Finance API integration is ready",
            AvailableEndpoints = new[]
            {
                "GET /api/yh/quote?symbols={symbols}&region={region}&lang={lang}",
                "POST /api/yh/quote",
                "GET /api/yh/trending/{region}",
                "GET /api/yh/market-summary/{region}",
                "GET /api/yh/currency-exchange/{fromCurrency}/{toCurrency}",
                "GET /api/yh/crypto?symbols={symbols}&currency={currency}",
                "POST /api/yh/bulk-quotes",
                "GET /api/yh/capabilities",
                "GET /api/yh/health"
            }
        };

        return Ok(healthResponse);
    }

    /// <summary>
    /// Gets market summary for a specific region
    /// </summary>
    /// <param name="region">Market region</param>
    /// <param name="lang">Response language</param>
    /// <returns>Market summary with indices and sector data</returns>
    [HttpGet("market-summary/{region}")]
    public Task<IActionResult> GetMarketSummary(
        string region, 
        [FromQuery] string? lang = "en")
    {
        var request = new MarketSummaryRequest
        {
            Region = region ?? string.Empty,
            Language = lang ?? "en"
        };

        if (!ModelState.IsValid)
        {
            return Task.FromResult<IActionResult>(BadRequest(ModelState));
        }

        // For now, return a mock response since this endpoint isn't in the base API
        // This would typically call a service method
        var mockResponse = CreateMockMarketSummary(request.Region, request.Language);
        return Task.FromResult<IActionResult>(Ok(mockResponse));
    }

    /// <summary>
    /// Gets currency exchange rate between two currencies
    /// </summary>
    /// <param name="fromCurrency">Source currency code</param>
    /// <param name="toCurrency">Target currency code</param>
    /// <returns>Exchange rate information</returns>
    [HttpGet("currency-exchange/{fromCurrency}/{toCurrency}")]
    public async Task<IActionResult> GetCurrencyExchange(
        string fromCurrency, 
        string toCurrency)
    {
        var request = new CurrencyExchangeRequest
        {
            FromCurrency = fromCurrency?.ToUpperInvariant() ?? string.Empty,
            ToCurrency = toCurrency?.ToUpperInvariant() ?? string.Empty
        };

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Create the symbol for Yahoo Finance format (e.g., EURUSD=X)
        var exchangeSymbol = $"{request.FromCurrency}{request.ToCurrency}=X";
        
        var quoteRequest = new QuoteRequest
        {
            Symbols = exchangeSymbol,
            Region = "US",
            Language = "en"
        };

        var result = await _yahooFinanceService.GetQuoteAsync(quoteRequest);
        
        if (result.IsFailure)
        {
            return MapErrorToActionResult(result.Error!);
        }

        var quote = result.Value!.Quotes.FirstOrDefault();
        var exchangeResponse = new CurrencyExchangeResponse
        {
            FromCurrency = request.FromCurrency,
            ToCurrency = request.ToCurrency,
            Symbol = exchangeSymbol,
            ExchangeRate = quote?.RegularMarketPrice,
            Change = quote?.RegularMarketChange,
            ChangePercent = quote?.RegularMarketChangePercent,
            LastUpdate = quote?.RegularMarketTime,
            Timestamp = DateTime.UtcNow,
            ErrorMessage = result.Value.ErrorMessage
        };

        return Ok(exchangeResponse);
    }

    /// <summary>
    /// Gets cryptocurrency quotes
    /// </summary>
    /// <param name="symbols">Comma-separated crypto symbols</param>
    /// <param name="currency">Base currency for comparison</param>
    /// <returns>Cryptocurrency quote data</returns>
    [HttpGet("crypto")]
    public async Task<IActionResult> GetCrypto(
        [FromQuery] string symbols,
        [FromQuery] string? currency = "USD")
    {
        var request = new CryptoRequest
        {
            Symbols = symbols ?? string.Empty,
            Currency = currency ?? "USD"
        };

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!request.HasValidSymbolFormat())
        {
            return BadRequest("Invalid symbols format");
        }

        // Convert crypto symbols to Yahoo Finance format (e.g., BTC-USD)
        var cryptoSymbols = request.GetSymbolsArray()
            .Select(symbol => symbol.EndsWith("-USD") ? symbol : $"{symbol}-{request.Currency}")
            .ToArray();

        var quoteRequest = new QuoteRequest
        {
            Symbols = string.Join(",", cryptoSymbols),
            Region = "US",
            Language = "en"
        };

        var result = await _yahooFinanceService.GetQuoteAsync(quoteRequest);
        
        if (result.IsFailure)
        {
            return MapErrorToActionResult(result.Error!);
        }

        var cryptoData = result.Value!.Quotes.Select(quote => new CryptoData
        {
            Symbol = quote.Symbol,
            Name = quote.ShortName ?? quote.Symbol,
            Price = quote.RegularMarketPrice,
            Change = quote.RegularMarketChange,
            ChangePercent = quote.RegularMarketChangePercent,
            Currency = quote.Currency ?? request.Currency,
            MarketCap = quote.MarketCap,
            Volume24h = quote.RegularMarketVolume,
            High24h = quote.RegularMarketDayHigh,
            Low24h = quote.RegularMarketDayLow,
            MarketState = quote.MarketState,
            LastUpdate = quote.RegularMarketTime
        }).ToArray();

        var cryptoResponse = new CryptoResponse
        {
            Symbols = request.GetSymbolsArray(),
            Currency = request.Currency,
            CryptoQuotes = cryptoData,
            Timestamp = DateTime.UtcNow,
            ErrorMessage = result.Value.ErrorMessage
        };

        return Ok(cryptoResponse);
    }

    /// <summary>
    /// Processes bulk quote requests for multiple symbol groups
    /// </summary>
    /// <param name="request">Bulk quote request with symbol groups</param>
    /// <returns>Organized quote data by groups</returns>
    [HttpPost("bulk-quotes")]
    public async Task<IActionResult> GetBulkQuotes([FromBody] BulkQuoteRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var quoteGroups = new List<QuoteGroup>();
        var totalSymbols = 0;
        var successfulQuotes = 0;
        var failedQuotes = 0;
        var globalErrors = new List<string>();

        foreach (var symbolGroup in request.SymbolGroups)
        {
            totalSymbols += symbolGroup.Symbols.Length;
            
            var quoteRequest = new QuoteRequest
            {
                Symbols = string.Join(",", symbolGroup.Symbols),
                Region = request.Region,
                Language = request.Language
            };

            var result = await _yahooFinanceService.GetQuoteAsync(quoteRequest);
            
            if (result.IsSuccess)
            {
                var groupQuotes = result.Value!.Quotes;
                successfulQuotes += groupQuotes.Length;
                failedQuotes += symbolGroup.Symbols.Length - groupQuotes.Length;

                var quoteGroup = new QuoteGroup
                {
                    GroupName = symbolGroup.GroupName,
                    Quotes = groupQuotes,
                    SuccessCount = groupQuotes.Length,
                    ErrorCount = symbolGroup.Symbols.Length - groupQuotes.Length,
                    Errors = result.Value.ErrorMessage != null 
                        ? new[] { result.Value.ErrorMessage } 
                        : Array.Empty<string>()
                };

                quoteGroups.Add(quoteGroup);
            }
            else
            {
                failedQuotes += symbolGroup.Symbols.Length;
                globalErrors.Add($"Group '{symbolGroup.GroupName}': {result.Error!.Message}");

                var errorGroup = new QuoteGroup
                {
                    GroupName = symbolGroup.GroupName,
                    Quotes = Array.Empty<QuoteData>(),
                    SuccessCount = 0,
                    ErrorCount = symbolGroup.Symbols.Length,
                    Errors = new[] { result.Error.Message }
                };

                quoteGroups.Add(errorGroup);
            }
        }

        var bulkResponse = new BulkQuoteResponse
        {
            Region = request.Region,
            Language = request.Language,
            QuoteGroups = quoteGroups.ToArray(),
            Timestamp = DateTime.UtcNow,
            TotalSymbols = totalSymbols,
            SuccessfulQuotes = successfulQuotes,
            FailedQuotes = failedQuotes,
            Errors = globalErrors.ToArray()
        };

        return Ok(bulkResponse);
    }

    /// <summary>
    /// Gets API capabilities and supported features
    /// </summary>
    /// <returns>Comprehensive API capability information</returns>
    [HttpGet("capabilities")]
    public IActionResult GetCapabilities()
    {
        var capabilities = new CapabilitiesResponse
        {
            AvailableEndpoints = new[]
            {
                new ApiEndpoint
                {
                    Path = "/api/yh/quote",
                    Method = "GET",
                    Description = "Get real-time stock quotes",
                    Parameters = new[] { "symbols", "region", "lang" },
                    Examples = new[] { "/api/yh/quote?symbols=AAPL,GOOGL&region=US&lang=en" }
                },
                new ApiEndpoint
                {
                    Path = "/api/yh/quote",
                    Method = "POST",
                    Description = "Get real-time stock quotes via JSON payload",
                    Parameters = new[] { "QuoteRequest body" },
                    Examples = new[] { "POST with { \"symbols\": \"AAPL,GOOGL\", \"region\": \"US\" }" }
                },
                new ApiEndpoint
                {
                    Path = "/api/yh/trending/{region}",
                    Method = "GET",
                    Description = "Get trending stocks for a region",
                    Parameters = new[] { "region" },
                    Examples = new[] { "/api/yh/trending/US" }
                },
                new ApiEndpoint
                {
                    Path = "/api/yh/market-summary/{region}",
                    Method = "GET",
                    Description = "Get market summary for a region",
                    Parameters = new[] { "region", "lang" },
                    Examples = new[] { "/api/yh/market-summary/US?lang=en" }
                },
                new ApiEndpoint
                {
                    Path = "/api/yh/currency-exchange/{fromCurrency}/{toCurrency}",
                    Method = "GET",
                    Description = "Get currency exchange rates",
                    Parameters = new[] { "fromCurrency", "toCurrency" },
                    Examples = new[] { "/api/yh/currency-exchange/USD/EUR" }
                },
                new ApiEndpoint
                {
                    Path = "/api/yh/crypto",
                    Method = "GET",
                    Description = "Get cryptocurrency quotes",
                    Parameters = new[] { "symbols", "currency" },
                    Examples = new[] { "/api/yh/crypto?symbols=BTC,ETH&currency=USD" }
                },
                new ApiEndpoint
                {
                    Path = "/api/yh/bulk-quotes",
                    Method = "POST",
                    Description = "Process bulk quote requests",
                    Parameters = new[] { "BulkQuoteRequest body" },
                    Examples = new[] { "POST with grouped symbol requests" }
                }
            },
            SupportedRegions = _yahooFinanceService.GetSupportedRegions(),
            SupportedLanguages = new[] { "en", "fr", "de", "it", "es", "zh" },
            SupportedAssetTypes = new[] { "EQUITY", "ETF", "MUTUAL_FUND", "INDEX", "CURRENCY", "CRYPTOCURRENCY" },
            RateLimits = new RateLimit
            {
                RequestsPerMinute = 100,
                RequestsPerHour = 2000,
                RequestsPerDay = 10000,
                BurstLimit = 10
            },
            Timestamp = DateTime.UtcNow,
            ApiVersion = "1.0.0"
        };

        return Ok(capabilities);
    }

    private static MarketSummaryResponse CreateMockMarketSummary(string region, string language)
    {
        // This is a mock implementation - in a real scenario, this would call the Yahoo Finance API
        // or have its own service implementation
        return new MarketSummaryResponse
        {
            Region = region.ToUpper(),
            Language = language,
            Indices = new[]
            {
                new MarketIndex
                {
                    Symbol = region.ToUpper() == "US" ? "^GSPC" : "^FTSE",
                    Name = region.ToUpper() == "US" ? "S&P 500" : "FTSE 100",
                    Price = 4500.50m,
                    Change = 25.75m,
                    ChangePercent = 0.58m,
                    Currency = region.ToUpper() == "US" ? "USD" : "GBP",
                    MarketState = "REGULAR",
                    MarketTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
                }
            },
            Sectors = new[]
            {
                new MarketSector
                {
                    Name = "Technology",
                    Performance = 2.1m,
                    ChangePercent = 1.95m,
                    TopStocks = new[] { "AAPL", "GOOGL", "MSFT" }
                },
                new MarketSector
                {
                    Name = "Healthcare",
                    Performance = 0.8m,
                    ChangePercent = 0.75m,
                    TopStocks = new[] { "JNJ", "PFE", "UNH" }
                }
            },
            Timestamp = DateTime.UtcNow
        };
    }

    private IActionResult MapErrorToActionResult(Error error)
    {
        var statusCode = GetStatusCodeFromError(error);
        var errorResponse = CreateErrorResponse(error);
        return StatusCode(statusCode, errorResponse);
    }

    private int GetStatusCodeFromError(Error error) => error.Code switch
    {
        "VALIDATION_REQUIRED" or "VALIDATION_INVALID" or "VALIDATION_FORMAT" => 400,
        "DATA_NOT_FOUND" => 404,
        "NETWORK_TIMEOUT" => 408,
        "NETWORK_REQUEST_FAILED" => 502,
        "NETWORK_CONNECTION_FAILED" => 503,
        "CONFIG_MISSING_API_KEY" or "CONFIG_INVALID" => 500,
        "DATA_PARSE_ERROR" => 502,
        _ => 500
    };

    private ErrorResponse CreateErrorResponse(Error error)
    {
        _logger.LogWarning("Request failed with error: {ErrorCode} - {ErrorMessage}", error.Code, error.Message);

        return new ErrorResponse
        {
            Error = new ErrorDetails
            {
                Code = error.Code,
                Message = error.Message
            },
            Timestamp = DateTime.UtcNow
        };
    }
}
