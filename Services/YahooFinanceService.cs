using System.Text.Json;
using WebApiProject.DTOs;
using WebApiProject.Services.Mappers;

namespace WebApiProject.Services;

public class YahooFinanceService : IYahooFinanceService
{
    private readonly HttpClient _httpClient;
    private readonly IAwsParameterStoreService _parameterStore;
    private readonly IConfiguration _configuration;
    private readonly ILogger<YahooFinanceService> _logger;
    
    private const string YfApiBaseUrl = "https://yfapi.net";
    private const string ApiKeyParameterName = "/WebApiProject/YfApi/ApiKey";
    
    private static readonly string[] ValidRegions = { "US", "AU", "CA", "FR", "DE", "HK", "IT", "ES", "GB", "IN" };

    public YahooFinanceService(
        HttpClient httpClient, 
        IAwsParameterStoreService parameterStore, 
        IConfiguration configuration,
        ILogger<YahooFinanceService> logger)
    {
        _httpClient = httpClient;
        _parameterStore = parameterStore;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<Result<QuoteResponse>> GetQuoteAsync(QuoteRequest request)
    {
        // Validate request
        if (!request.HasValidSymbolFormat())
        {
            return DomainErrors.Validation.InvalidFormat("symbols");
        }

        if (!IsValidRegion(request.Region))
        {
            return DomainErrors.Validation.InvalidValue(nameof(request.Region), request.Region);
        }

        var symbols = request.GetSymbolsArray();
        _logger.LogInformation("Fetching quotes for symbols: {Symbols}, region: {Region}, lang: {Language}", 
            string.Join(",", symbols), request.Region, request.Language);

        var apiKeyResult = await SetApiKeyHeaderAsync();
        if (apiKeyResult.IsFailure)
        {
            return Result.Failure<QuoteResponse>(apiKeyResult.Error!);
        }

        var queryParams = $"symbols={Uri.EscapeDataString(request.Symbols)}&region={request.Region}&lang={request.Language}";
        var url = $"{YfApiBaseUrl}/v6/finance/quote?{queryParams}";

        try
        {
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("YF API returned {StatusCode} for symbols {Symbols}", 
                    response.StatusCode, string.Join(",", symbols));
                return DomainErrors.Network.RequestFailed((int)response.StatusCode);
            }

            var content = await response.Content.ReadAsStringAsync();
            
            try
            {
                using var jsonDocument = JsonDocument.Parse(content);
                return YahooFinanceMapper.MapToQuoteResponse(jsonDocument, symbols, request.Region, request.Language);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON parsing error when processing data for symbols {Symbols}", string.Join(",", symbols));
                return DomainErrors.Data.ParseError("JSON response");
            }
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP request error when fetching data for symbols {Symbols}", string.Join(",", symbols));
            return DomainErrors.Network.ConnectionFailed();
        }
        catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
        {
            _logger.LogError(ex, "Timeout when fetching data for symbols {Symbols}", string.Join(",", symbols));
            return DomainErrors.Network.Timeout();
        }
    }

    public async Task<Result<TrendingResponse>> GetTrendingAsync(TrendingRequest request)
    {
        if (!IsValidRegion(request.Region))
        {
            return DomainErrors.Validation.InvalidValue(nameof(request.Region), request.Region);
        }

        _logger.LogInformation("Fetching trending stocks for region: {Region}", request.Region);

        var apiKeyResult = await SetApiKeyHeaderAsync();
        if (apiKeyResult.IsFailure)
        {
            return Result.Failure<TrendingResponse>(apiKeyResult.Error!);
        }

        var url = $"{YfApiBaseUrl}/v1/finance/trending/{request.Region.ToUpper()}";

        try
        {
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("YF API returned {StatusCode} for trending in region {Region}", 
                    response.StatusCode, request.Region);
                return DomainErrors.Network.RequestFailed((int)response.StatusCode);
            }

            var content = await response.Content.ReadAsStringAsync();
            
            try
            {
                using var jsonDocument = JsonDocument.Parse(content);
                return YahooFinanceMapper.MapToTrendingResponse(jsonDocument, request.Region);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON parsing error when processing trending data for region {Region}", request.Region);
                return DomainErrors.Data.ParseError("JSON response");
            }
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP request error when fetching trending for region {Region}", request.Region);
            return DomainErrors.Network.ConnectionFailed();
        }
        catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
        {
            _logger.LogError(ex, "Timeout when fetching trending for region {Region}", request.Region);
            return DomainErrors.Network.Timeout();
        }
    }

    public bool IsValidRegion(string region)
    {
        return ValidRegions.Contains(region?.ToUpper());
    }

    public string[] GetSupportedRegions()
    {
        return ValidRegions.ToArray();
    }

    private async Task<string?> GetApiKeyAsync()
    {
        // First try Parameter Store (production)
        var parameterResult = await _parameterStore.GetParameterAsync(ApiKeyParameterName);
        
        if (parameterResult.IsSuccess)
        {
            return parameterResult.Value;
        }

        // Fallback to configuration (development)
        var configApiKey = _configuration["YfApi:ApiKey"];
        return configApiKey;
    }

    private async Task<Result<Unit>> SetApiKeyHeaderAsync()
    {
        var apiKey = await GetApiKeyAsync();
        if (!string.IsNullOrEmpty(apiKey))
        {
            _httpClient.DefaultRequestHeaders.Remove("X-API-KEY");
            _httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey);
            return Result.Success(Unit.Value);
        }
        else
        {
            _logger.LogWarning("No API key found in Parameter Store or configuration");
            return DomainErrors.Configuration.MissingApiKey();
        }
    }
}
