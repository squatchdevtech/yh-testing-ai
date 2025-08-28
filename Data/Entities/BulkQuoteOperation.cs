using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiProject.Data.Entities;

[Table("bulk_quote_operations")]
public class BulkQuoteOperation
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [Column("operation_id")]
    [StringLength(36)]
    public string OperationId { get; set; } = string.Empty;

    [Column("region")]
    [StringLength(2)]
    public string Region { get; set; } = "US";

    [Column("language")]
    [StringLength(2)]
    public string Language { get; set; } = "en";

    [Required]
    [Column("total_symbols")]
    public int TotalSymbols { get; set; }

    [Column("successful_quotes")]
    public int SuccessfulQuotes { get; set; }

    [Column("failed_quotes")]
    public int FailedQuotes { get; set; }

    [Column("errors")]
    public string? Errors { get; set; } // JSON string

    [Column("processing_time_ms")]
    public int? ProcessingTimeMs { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
}
```

```csharp:Data/Entities/BulkQuoteGroup.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiProject.Data.Entities;

[Table("bulk_quote_groups")]
public class BulkQuoteGroup
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [Column("operation_id")]
    [StringLength(36)]
    public string OperationId { get; set; } = string.Empty;

    [Required]
    [Column("group_name")]
    [StringLength(100)]
    public string GroupName { get; set; } = string.Empty;

    [Required]
    [Column("symbols")]
    public string Symbols { get; set; } = string.Empty; // JSON string

    [Column("quotes")]
    public string? Quotes { get; set; } // JSON string

    [Column("success_count")]
    public int SuccessCount { get; set; }

    [Column("error_count")]
    public int ErrorCount { get; set; }

    [Column("errors")]
    public string? Errors { get; set; } // JSON string

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}
```

```csharp:Data/Entities/ApiHealthMetric.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiProject.Data.Entities;

[Table("api_health_metrics")]
public class ApiHealthMetric
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [Column("service_name")]
    [StringLength(100)]
    public string ServiceName { get; set; } = string.Empty;

    [Required]
    [Column("status")]
    [StringLength(20)]
    public string Status { get; set; } = string.Empty;

    [Column("api_key_configured")]
    public bool ApiKeyConfigured { get; set; }

    [Column("api_key_source")]
    [StringLength(50)]
    public string? ApiKeySource { get; set; }

    [Column("parameter_store_path")]
    [StringLength(200)]
    public string? ParameterStorePath { get; set; }

    [Column("supported_regions")]
    public string? SupportedRegions { get; set; } // JSON string

    [Column("message")]
    public string? Message { get; set; }

    [Column("response_time_ms")]
    public int? ResponseTimeMs { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}
```

```csharp:Data/Entities/ApiRequest.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiProject.Data.Entities;

[Table("api_requests")]
public class ApiRequest
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [Column("request_id")]
    [StringLength(36)]
    public string RequestId { get; set; } = string.Empty;

    [Required]
    [Column("endpoint")]
    [StringLength(100)]
    public string Endpoint { get; set; } = string.Empty;

    [Required]
    [Column("method")]
    [StringLength(10)]
    public string Method { get; set; } = string.Empty;

    [Column("symbols")]
    public string? Symbols { get; set; }

    [Column("region")]
    [StringLength(2)]
    public string Region { get; set; } = "US";

    [Column("language")]
    [StringLength(2)]
    public string Language { get; set; } = "en";

    [Column("request_timestamp")]
    public DateTime RequestTimestamp { get; set; }

    [Column("response_timestamp")]
    public DateTime? ResponseTimestamp { get; set; }

    [Column("status_code")]
    public int? StatusCode { get; set; }

    [Column("response_time_ms")]
    public int? ResponseTimeMs { get; set; }

    [Column("cache_hit")]
    public bool CacheHit { get; set; }

    [Column("user_agent")]
    public string? UserAgent { get; set; }

    [Column("ip_address")]
    [StringLength(45)]
    public string? IpAddress { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
}
```

```csharp:Data/Entities/TrendingStock.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiProject.Data.Entities;

[Table("trending_stocks")]
public class TrendingStock
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [Column("region")]
    [StringLength(2)]
    public string Region { get; set; } = string.Empty;

    [Required]
    [Column("symbol")]
    [StringLength(20)]
    public string Symbol { get; set; } = string.Empty;

    [Column("short_name")]
    [StringLength(100)]
    public string? ShortName { get; set; }

    [Column("long_name")]
    public string? LongName { get; set; }

    [Column("regular_market_price")]
    [Column(TypeName = "decimal(20,6)")]
    public decimal? RegularMarketPrice { get; set; }

    [Column("regular_market_change")]
    [Column(TypeName = "decimal(20,6)")]
    public decimal? RegularMarketChange { get; set; }

    [Column("regular_market_change_percent")]
    [Column(TypeName = "decimal(10,4)")]
    public decimal? RegularMarketChangePercent { get; set; }

    [Column("currency")]
    [StringLength(3)]
    public string? Currency { get; set; }

    [Column("market_state")]
    [StringLength(20)]
    public string? MarketState { get; set; }

    [Column("exchange")]
    [StringLength(20)]
    public string? Exchange { get; set; }

    [Column("quote_type")]
    [StringLength(20)]
    public string? QuoteType { get; set; }

    [Column("job_timestamp")]
    public long? JobTimestamp { get; set; }

    [Column("start_interval")]
    public long? StartInterval { get; set; }

    [Column("trending_rank")]
    public int? TrendingRank { get; set; }

    [Column("cache_valid_until")]
    public DateTime? CacheValidUntil { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
}
```

```csharp:Data/Entities/MarketIndex.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiProject.Data.Entities;

[Table("market_indices")]
public class MarketIndex
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [Column("region")]
    [StringLength(2)]
    public string Region { get; set; } = string.Empty;

    [Required]
    [Column("symbol")]
    [StringLength(20)]
    public string Symbol { get; set; } = string.Empty;

    [Required]
    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Column("price")]
    [Column(TypeName = "decimal(20,6)")]
    public decimal? Price { get; set; }

    [Column("change_value")]
    [Column(TypeName = "decimal(20,6)")]
    public decimal? ChangeValue { get; set; }

    [Column("change_percent")]
    [Column(TypeName = "decimal(10,4)")]
    public decimal? ChangePercent { get; set; }

    [Column("currency")]
    [StringLength(3)]
    public string? Currency { get; set; }

    [Column("market_state")]
    [StringLength(20)]
    public string? MarketState { get; set; }

    [Column("market_time")]
    public long? MarketTime { get; set; }

    [Column("cache_valid_until")]
    public DateTime? CacheValidUntil { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
}
```

```csharp:Data/Entities/MarketSector.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiProject.Data.Entities;

[Table("market_sectors")]
public class MarketSector
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [Column("region")]
    [StringLength(2)]
    public string Region { get; set; } = string.Empty;

    [Required]
    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Column("performance")]
    [Column(TypeName = "decimal(10,4)")]
    public decimal? Performance { get; set; }

    [Column("change_percent")]
    [Column(TypeName = "decimal(10,4)")]
    public decimal? ChangePercent { get; set; }

    [Column("top_stocks")]
    public string? TopStocks { get; set; } // JSON string

    [Column("cache_valid_until")]
    public DateTime? CacheValidUntil { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
}
```

```csharp:Data/Entities/CurrencyExchangeRate.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiProject.Data.Entities;

[Table("currency_exchange_rates")]
public class CurrencyExchangeRate
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [Column("from_currency")]
    [StringLength(3)]
    public string FromCurrency { get; set; } = string.Empty;

    [Required]
    [Column("to_currency")]
    [StringLength(3)]
    public string ToCurrency { get; set; } = string.Empty;

    [Required]
    [Column("symbol")]
    [StringLength(20)]
    public string Symbol { get; set; } = string.Empty;

    [Column("exchange_rate")]
    [Column(TypeName = "decimal(20,6)")]
    public decimal? ExchangeRate { get; set; }

    [Column("change_value")]
    [Column(TypeName = "decimal(20,6)")]
    public decimal? ChangeValue { get; set; }

    [Column("change_percent")]
    [Column(TypeName = "decimal(10,4)")]
    public decimal? ChangePercent { get; set; }

    [Column("last_update")]
    public long? LastUpdate { get; set; }

    [Column("cache_valid_until")]
    public DateTime? CacheValidUntil { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
}
```

```csharp:Data/Entities/CryptocurrencyData.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiProject.Data.Entities;

[Table("cryptocurrency_data")]
public class CryptocurrencyData
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [Column("symbol")]
    [StringLength(20)]
    public string Symbol { get; set; } = string.Empty;

    [Column("name")]
    [StringLength(100)]
    public string? Name { get; set; }

    [Column("price")]
    [Column(TypeName = "decimal(20,6)")]
    public decimal? Price { get; set; }

    [Column("change_value")]
    [Column(TypeName = "decimal(20,6)")]
    public decimal? ChangeValue { get; set; }

    [Column("change_percent")]
    [Column(TypeName = "decimal(10,4)")]
    public decimal? ChangePercent { get; set; }

    [Column("currency")]
    [StringLength(3)]
    public string Currency { get; set; } = "USD";

    [Column("market_cap")]
    public long? MarketCap { get; set; }

    [Column("volume_24h")]
    public long? Volume24h { get; set; }

    [Column("high_24h")]
    [Column(TypeName = "decimal(20,6)")]
    public decimal? High24h { get; set; }

    [Column("low_24h")]
    [Column(TypeName = "decimal(20,6)")]
    public decimal? Low24h { get; set; }

    [Column("market_state")]
    [StringLength(20)]
    public string? MarketState { get; set; }

    [Column("last_update")]
    public long? LastUpdate { get; set; }

    [Column("cache_valid_until")]
    public DateTime? CacheValidUntil { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
}
```

```csharp:Services/CacheService.cs
using Microsoft.EntityFrameworkCore;
using WebApiProject.Data;
using WebApiProject.Data.Entities;
using WebApiProject.DTOs;

namespace WebApiProject.Services;

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
}
```

## 3. **Update the ICacheService interface**

```csharp:Services/ICacheService.cs
using WebApiProject.DTOs;

namespace WebApiProject.Services;

public interface ICacheService
{
    Task<QuoteData?> GetCachedQuoteAsync(string symbol, string region);
    Task<TrendingStock?> GetCachedTrendingStockAsync(string symbol, string region);
    Task SaveQuoteAsync(QuoteData quote, string region);
    Task SaveTrendingStockAsync(TrendingStock trendingStock, string region);
    Task<bool> IsCacheValidAsync(string symbol, string region, string dataType);
    Task SaveApiRequestAsync(string requestId, string endpoint, string method, 
        string? symbols, string region, string language, int? statusCode, int? responseTimeMs, bool cacheHit);
    Task SaveHealthMetricAsync(string serviceName, string status, bool apiKeyConfigured, 
        string? apiKeySource, string? parameterStorePath, string? supportedRegions, string? message, int? responseTimeMs);
}
```

## 4. **Integrate with your existing YahooFinanceService**

Now I'll show you how to integrate the caching with your existing service. Would you like me to:

1. **Modify your existing `YahooFinanceService`** to use the cache service?
2. **Create a new cached wrapper service** that decorates your existing service?
3. **Update your `YHController`** to use the cache service directly?

Which approach would you prefer? The second option (decorator pattern) would be the cleanest as it doesn't require modifying your existing service logic.

