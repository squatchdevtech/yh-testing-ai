using Microsoft.EntityFrameworkCore;
using WebApiProject.Data;
using WebApiProject.Data.Entities;
using WebApiProject.DTOs;

namespace WebApiProject.Services;

public interface ICacheService
{
    Task<QuoteData?> GetCachedQuoteAsync(string symbol, string region);
    Task<TrendingStock?> GetCachedTrendingStockAsync(string symbol, string region);
    Task<List<TrendingStock>?> GetCachedTrendingStocksForRegionAsync(string region);
    Task SaveQuoteAsync(QuoteData quote, string region);
    Task SaveTrendingStockAsync(TrendingStock trendingStock, string region);
    Task SaveTrendingStocksAsync(IEnumerable<TrendingStock> trendingStocks, string region, int? jobTimestamp = null);
    Task<bool> IsCacheValidAsync(string symbol, string region, string dataType);
    Task SaveApiRequestAsync(string requestId, string endpoint, string method, 
        string? symbols, string region, string language, int? statusCode, int? responseTimeMs, bool cacheHit);
    Task SaveHealthMetricAsync(string serviceName, string status, bool apiKeyConfigured, 
        string? apiKeySource, string? parameterStorePath, string? supportedRegions, string? message, int? responseTimeMs);
}

public class CacheService : ICacheService
{
    private readonly YahooFinanceDbContext _context;
    private readonly ILogger<CacheService> _logger;

    public CacheService(YahooFinanceDbContext context, ILogger<CacheService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<QuoteData?> GetCachedQuoteAsync(string symbol, string region)
    {
        try
        {
            var cachedQuote = await _context.StockQuotes
                .Where(q => q.Symbol == symbol && 
                           q.Region == region && 
                           q.CacheValidUntil > DateTime.UtcNow)
                .OrderByDescending(q => q.QuoteTimestamp)
                .FirstOrDefaultAsync();

            if (cachedQuote == null)
                return null;

            // Convert entity to DTO
            return new QuoteData
            {
                Symbol = cachedQuote.Symbol,
                RegularMarketPrice = cachedQuote.RegularMarketPrice,
                RegularMarketChange = cachedQuote.RegularMarketChange,
                RegularMarketChangePercent = cachedQuote.RegularMarketChangePercent,
                RegularMarketTime = cachedQuote.QuoteTimestamp,
                RegularMarketDayHigh = cachedQuote.RegularMarketDayHigh,
                RegularMarketDayLow = cachedQuote.RegularMarketDayLow,
                RegularMarketVolume = cachedQuote.RegularMarketVolume,
                RegularMarketPreviousClose = cachedQuote.RegularMarketPreviousClose,
                Currency = cachedQuote.Currency,
                MarketState = cachedQuote.MarketState,
                ShortName = cachedQuote.ShortName,
                LongName = cachedQuote.LongName,
                Exchange = cachedQuote.Exchange,
                ExchangeTimezoneName = cachedQuote.ExchangeTimezoneName,
                ExchangeTimezoneShortName = cachedQuote.ExchangeTimezoneShortName,
                QuoteType = cachedQuote.QuoteType,
                MarketCap = cachedQuote.MarketCap,
                SharesOutstanding = cachedQuote.SharesOutstanding,
                BookValue = cachedQuote.BookValue,
                PriceToBook = cachedQuote.PriceToBook,
                FiftyTwoWeekLow = cachedQuote.FiftyTwoWeekLow,
                FiftyTwoWeekHigh = cachedQuote.FiftyTwoWeekHigh,
                FiftyDayAverage = cachedQuote.FiftyDayAverage,
                TwoHundredDayAverage = cachedQuote.TwoHundredDayAverage,
                TrailingPE = cachedQuote.TrailingPE,
                ForwardPE = cachedQuote.ForwardPE,
                DividendYield = cachedQuote.DividendYield,
                TrailingAnnualDividendYield = cachedQuote.TrailingAnnualDividendYield,
                Beta = cachedQuote.Beta
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving cached quote for {Symbol} in {Region}", symbol, region);
            return null;
        }
    }

    public async Task<TrendingStock?> GetCachedTrendingStockAsync(string symbol, string region)
    {
        try
        {
            var cachedTrending = await _context.TrendingStocks
                .Where(t => t.Symbol == symbol && 
                           t.Region == region && 
                           t.CacheValidUntil > DateTime.UtcNow)
                .OrderByDescending(t => t.JobTimestamp)
                .FirstOrDefaultAsync();

            if (cachedTrending == null)
                return null;

            // Convert entity to DTO
            return new DTOs.TrendingStock
            {
                Symbol = cachedTrending.Symbol,
                ShortName = cachedTrending.ShortName,
                LongName = cachedTrending.LongName,
                RegularMarketPrice = cachedTrending.RegularMarketPrice,
                RegularMarketChange = cachedTrending.RegularMarketChange,
                RegularMarketChangePercent = cachedTrending.RegularMarketChangePercent,
                Currency = cachedTrending.Currency,
                MarketState = cachedTrending.MarketState,
                Exchange = cachedTrending.Exchange,
                QuoteType = cachedTrending.QuoteType
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving cached trending stock for {Symbol} in {Region}", symbol, region);
            return null;
        }
    }

    public async Task SaveQuoteAsync(QuoteData quote, string region)
    {
        try
        {
            var stockQuote = new StockQuote
            {
                Symbol = quote.Symbol,
                Region = region,
                QuoteTimestamp = quote.RegularMarketTime ?? DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                RegularMarketPrice = quote.RegularMarketPrice,
                RegularMarketChange = quote.RegularMarketChange,
                RegularMarketChangePercent = quote.RegularMarketChangePercent,
                RegularMarketTime = quote.RegularMarketTime,
                RegularMarketDayHigh = quote.RegularMarketDayHigh,
                RegularMarketDayLow = quote.RegularMarketDayLow,
                RegularMarketVolume = quote.RegularMarketVolume,
                RegularMarketPreviousClose = quote.RegularMarketPreviousClose,
                Currency = quote.Currency,
                MarketState = quote.MarketState,
                ShortName = quote.ShortName,
                LongName = quote.LongName,
                Exchange = quote.Exchange,
                ExchangeTimezoneName = quote.ExchangeTimezoneName,
                ExchangeTimezoneShortName = quote.ExchangeTimezoneShortName,
                QuoteType = quote.QuoteType,
                MarketCap = quote.MarketCap,
                SharesOutstanding = quote.SharesOutstanding,
                BookValue = quote.BookValue,
                PriceToBook = quote.PriceToBook,
                FiftyTwoWeekLow = quote.FiftyTwoWeekLow,
                FiftyTwoWeekHigh = quote.FiftyTwoWeekHigh,
                FiftyDayAverage = quote.FiftyDayAverage,
                TwoHundredDayAverage = quote.TwoHundredDayAverage,
                TrailingPE = quote.TrailingPE,
                ForwardPE = quote.ForwardPE,
                DividendYield = quote.DividendYield,
                TrailingAnnualDividendYield = quote.TrailingAnnualDividendYield,
                Beta = quote.Beta,
                CacheValidUntil = DateTime.UtcNow.AddMinutes(15), // 15 minutes cache
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.StockQuotes.Add(stockQuote);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Cached quote for {Symbol} in {Region}", quote.Symbol, region);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving quote to cache for {Symbol} in {Region}", quote.Symbol, region);
        }
    }

    public async Task SaveTrendingStockAsync(DTOs.TrendingStock trendingStock, string region)
    {
        try
        {
            var trendingEntity = new TrendingStock
            {
                Region = region,
                Symbol = trendingStock.Symbol,
                ShortName = trendingStock.ShortName,
                LongName = trendingStock.LongName,
                RegularMarketPrice = trendingStock.RegularMarketPrice,
                RegularMarketChange = trendingStock.RegularMarketChange,
                RegularMarketChangePercent = trendingStock.RegularMarketChangePercent,
                Currency = trendingStock.Currency,
                MarketState = trendingStock.MarketState,
                Exchange = trendingStock.Exchange,
                QuoteType = trendingStock.QuoteType,
                JobTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                CacheValidUntil = DateTime.UtcNow.AddMinutes(30), // 30 minutes cache
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.TrendingStocks.Add(trendingEntity);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Cached trending stock for {Symbol} in {Region}", trendingStock.Symbol, region);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving trending stock to cache for {Symbol} in {Region}", trendingStock.Symbol, region);
        }
    }

    public async Task<bool> IsCacheValidAsync(string symbol, string region, string dataType)
    {
        try
        {
            switch (dataType.ToLower())
            {
                case "quote":
                    return await _context.StockQuotes
                        .AnyAsync(q => q.Symbol == symbol && 
                                      q.Region == region && 
                                      q.CacheValidUntil > DateTime.UtcNow);
                case "trending":
                    return await _context.TrendingStocks
                        .AnyAsync(t => t.Symbol == symbol && 
                                      t.Region == region && 
                                      t.CacheValidUntil > DateTime.UtcNow);
                default:
                    return false;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking cache validity for {Symbol} in {Region}", symbol, region);
            return false;
        }
    }

    // Additional methods for other data types
    public async Task SaveApiRequestAsync(string requestId, string endpoint, string method, 
        string? symbols, string region, string language, int? statusCode, int? responseTimeMs, bool cacheHit)
    {
        try
        {
            var apiRequest = new ApiRequest
            {
                RequestId = requestId,
                Endpoint = endpoint,
                Method = method,
                Symbols = symbols,
                Region = region,
                Language = language,
                RequestTimestamp = DateTime.UtcNow,
                ResponseTimestamp = DateTime.UtcNow,
                StatusCode = statusCode,
                ResponseTimeMs = responseTimeMs,
                CacheHit = cacheHit,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.ApiRequests.Add(apiRequest);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving API request to database");
        }
    }

    public async Task SaveHealthMetricAsync(string serviceName, string status, bool apiKeyConfigured, 
        string? apiKeySource, string? parameterStorePath, string? supportedRegions, string? message, int? responseTimeMs)
    {
        try
        {
            var healthMetric = new ApiHealthMetric
            {
                ServiceName = serviceName,
                Status = status,
                ApiKeyConfigured = apiKeyConfigured,
                ApiKeySource = apiKeySource,
                ParameterStorePath = parameterStorePath,
                SupportedRegions = supportedRegions,
                Message = message,
                ResponseTimeMs = responseTimeMs,
                CreatedAt = DateTime.UtcNow
            };

            _context.ApiHealthMetrics.Add(healthMetric);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving health metric to database");
        }
    }

    public async Task<List<TrendingStock>?> GetCachedTrendingStocksForRegionAsync(string region)
    {
        try
        {
            var cachedTrending = await _context.TrendingStocks
                .Where(t => t.Region == region && 
                           t.CacheValidUntil > DateTime.UtcNow)
                .OrderBy(t => t.TrendingRank)
                .ThenByDescending(t => t.JobTimestamp)
                .Take(50) // Limit to top 50 trending stocks
                .ToListAsync();

            if (!cachedTrending.Any())
                return null;

            // Convert entities to DTOs
            return cachedTrending.Select(t => new DTOs.TrendingStock
            {
                Symbol = t.Symbol,
                ShortName = t.ShortName,
                LongName = t.LongName,
                RegularMarketPrice = t.RegularMarketPrice,
                RegularMarketChange = t.RegularMarketChange,
                RegularMarketChangePercent = t.RegularMarketChangePercent,
                Currency = t.Currency,
                MarketState = t.MarketState,
                Exchange = t.Exchange,
                QuoteType = t.QuoteType
            }).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving cached trending stocks for region {Region}", region);
            return null;
        }
    }

    public async Task SaveTrendingStocksAsync(IEnumerable<DTOs.TrendingStock> trendingStocks, string region, int? jobTimestamp = null)
    {
        try
        {
            var timestamp = jobTimestamp ?? DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var rank = 1;

            foreach (var stock in trendingStocks)
            {
                var trendingEntity = new TrendingStock
                {
                    Region = region,
                    Symbol = stock.Symbol,
                    ShortName = stock.ShortName,
                    LongName = stock.LongName,
                    RegularMarketPrice = stock.RegularMarketPrice,
                    RegularMarketChange = stock.RegularMarketChange,
                    RegularMarketChangePercent = stock.RegularMarketChangePercent,
                    Currency = stock.Currency,
                    MarketState = stock.MarketState,
                    Exchange = stock.Exchange,
                    QuoteType = stock.QuoteType,
                    JobTimestamp = timestamp,
                    TrendingRank = rank++,
                    CacheValidUntil = DateTime.UtcNow.AddMinutes(30), // 30 minutes cache
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.TrendingStocks.Add(trendingEntity);
            }

            await _context.SaveChangesAsync();
            _logger.LogInformation("Cached {Count} trending stocks for region {Region}", trendingStocks.Count(), region);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving trending stocks to cache for region {Region}", region);
        }
    }
}
