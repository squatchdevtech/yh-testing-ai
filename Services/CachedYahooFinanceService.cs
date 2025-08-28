using System.Diagnostics;
using WebApiProject.DTOs;
using WebApiProject.Services;

namespace WebApiProject.Services;

/// <summary>
/// Decorator service that adds caching to the Yahoo Finance service
/// Implements the decorator pattern to avoid modifying the original service
/// </summary>
public class CachedYahooFinanceService : IYahooFinanceService
{
    private readonly IYahooFinanceService _originalService;
    private readonly ICacheService _cacheService;
    private readonly ILogger<CachedYahooFinanceService> _logger;

    public CachedYahooFinanceService(
        IYahooFinanceService originalService,
        ICacheService cacheService,
        ILogger<CachedYahooFinanceService> logger)
    {
        _originalService = originalService;
        _cacheService = cacheService;
        _logger = logger;
    }

    public async Task<Result<QuoteResponse>> GetQuoteAsync(QuoteRequest request)
    {
        var stopwatch = Stopwatch.StartNew();
        var requestId = Guid.NewGuid().ToString();
        var symbols = request.GetSymbolsArray();
        var cacheHits = 0;
        var totalSymbols = symbols.Length;

        try
        {
            // Check cache for each symbol
            var cachedQuotes = new List<QuoteData>();
            var symbolsToFetch = new List<string>();

            foreach (var symbol in symbols)
            {
                var cachedQuote = await _cacheService.GetCachedQuoteAsync(symbol, request.Region);
                if (cachedQuote != null)
                {
                    cachedQuotes.Add(cachedQuote);
                    cacheHits++;
                    _logger.LogDebug("Cache hit for symbol {Symbol} in region {Region}", symbol, request.Region);
                }
                else
                {
                    symbolsToFetch.Add(symbol);
                    _logger.LogDebug("Cache miss for symbol {Symbol} in region {Region}", symbol, request.Region);
                }
            }

            // If all symbols were cached, return from cache
            if (symbolsToFetch.Count == 0)
            {
                var response = new QuoteResponse
                {
                    Symbols = symbols,
                    Region = request.Region,
                    Language = request.Language,
                    Quotes = cachedQuotes.ToArray(),
                    Timestamp = DateTime.UtcNow
                };

                stopwatch.Stop();
                await LogApiRequest(requestId, "/api/yh/quote", "GET", request.Symbols, 
                    request.Region, request.Language, 200, (int)stopwatch.ElapsedMilliseconds, true);

                _logger.LogInformation("All {Count} symbols served from cache for region {Region}", 
                    totalSymbols, request.Region);

                return Result<QuoteResponse>.Success(response);
            }

            // Fetch remaining symbols from API
            var remainingRequest = new QuoteRequest
            {
                Symbols = string.Join(",", symbolsToFetch),
                Region = request.Region,
                Language = request.Language
            };

            var apiResult = await _originalService.GetQuoteAsync(remainingRequest);
            
            if (apiResult.IsSuccess)
            {
                var allQuotes = new List<QuoteData>();
                allQuotes.AddRange(cachedQuotes);
                allQuotes.AddRange(apiResult.Value!.Quotes);

                // Cache the new quotes
                foreach (var quote in apiResult.Value.Quotes)
                {
                    await _cacheService.SaveQuoteAsync(quote, request.Region);
                }

                var response = new QuoteResponse
                {
                    Symbols = symbols,
                    Region = request.Region,
                    Language = request.Language,
                    Quotes = allQuotes.ToArray(),
                    Timestamp = DateTime.UtcNow
                };

                stopwatch.Stop();
                await LogApiRequest(requestId, "/api/yh/quote", "GET", request.Symbols, 
                    request.Region, request.Language, 200, (int)stopwatch.ElapsedMilliseconds, false);

                _logger.LogInformation("Quote request completed: {CacheHits} from cache, {ApiCalls} from API for region {Region}", 
                    cacheHits, symbolsToFetch.Count, request.Region);

                return Result<QuoteResponse>.Success(response);
            }
            else
            {
                stopwatch.Stop();
                await LogApiRequest(requestId, "/api/yh/quote", "GET", request.Symbols, 
                    request.Region, request.Language, 500, (int)stopwatch.ElapsedMilliseconds, false);

                return apiResult;
            }
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            await LogApiRequest(requestId, "/api/yh/quote", "GET", request.Symbols, 
                request.Region, request.Language, 500, (int)stopwatch.ElapsedMilliseconds, false);

            _logger.LogError(ex, "Error in cached quote service for symbols {Symbols}", request.Symbols);
            throw;
        }
    }

    public async Task<Result<TrendingResponse>> GetTrendingAsync(TrendingRequest request)
    {
        var stopwatch = Stopwatch.StartNew();
        var requestId = Guid.NewGuid().ToString();

        try
        {
            // Check if we have cached trending data for this region
            var cachedTrendingStocks = await GetCachedTrendingStocksAsync(request.Region);
            
            if (cachedTrendingStocks != null && cachedTrendingStocks.Any())
            {
                var response = new TrendingResponse
                {
                    Region = request.Region,
                    Stocks = cachedTrendingStocks.ToArray(),
                    Timestamp = DateTime.UtcNow,
                    Count = cachedTrendingStocks.Count
                };

                stopwatch.Stop();
                await LogApiRequest(requestId, "/api/yh/trending", "GET", null, 
                    request.Region, "en", 200, (int)stopwatch.ElapsedMilliseconds, true);

                _logger.LogInformation("Trending stocks served from cache for region {Region}", request.Region);
                return Result<TrendingResponse>.Success(response);
            }

            // Fetch from API if not cached
            var apiResult = await _originalService.GetTrendingAsync(request);
            
            if (apiResult.IsSuccess)
            {
                // Cache all trending stocks as a group
                await _cacheService.SaveTrendingStocksAsync(apiResult.Value!.Stocks, request.Region);

                stopwatch.Stop();
                await LogApiRequest(requestId, "/api/yh/trending", "GET", null, 
                    request.Region, "en", 200, (int)stopwatch.ElapsedMilliseconds, false);

                _logger.LogInformation("Trending stocks fetched from API and cached for region {Region}", request.Region);
                return apiResult;
            }
            else
            {
                stopwatch.Stop();
                await LogApiRequest(requestId, "/api/yh/trending", "GET", null, 
                    request.Region, "en", 500, (int)stopwatch.ElapsedMilliseconds, false);

                return apiResult;
            }
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            await LogApiRequest(requestId, "/api/yh/trending", "GET", null, 
                request.Region, "en", 500, (int)stopwatch.ElapsedMilliseconds, false);

            _logger.LogError(ex, "Error in cached trending service for region {Region}", request.Region);
            throw;
        }
    }

    public string[] GetSupportedRegions()
    {
        // This method doesn't need caching, delegate to original service
        return _originalService.GetSupportedRegions();
    }

    // Helper method to get cached trending stocks for a region
    private async Task<List<TrendingStock>?> GetCachedTrendingStocksAsync(string region)
    {
        try
        {
            return await _cacheService.GetCachedTrendingStocksForRegionAsync(region);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving cached trending stocks for region {Region}", region);
            return null;
        }
    }

    // Helper method to log API requests
    private async Task LogApiRequest(string requestId, string endpoint, string method, 
        string? symbols, string region, string language, int statusCode, int responseTimeMs, bool cacheHit)
    {
        try
        {
            await _cacheService.SaveApiRequestAsync(requestId, endpoint, method, 
                symbols, region, language, statusCode, responseTimeMs, cacheHit);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error logging API request");
        }
    }
}
