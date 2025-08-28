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

```csharp:Data/Entities/BulkQuoteOperation.cs
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

```csharp:Data/Entities/BulkQuoteOperation.cs
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

```csharp:Data/Entities/BulkQuoteOperation.cs
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

```csharp:Data/Entities/BulkQuoteOperation.cs
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

```csharp:Data/Entities/BulkQuoteOperation.cs
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

```csharp:Data/Entities/BulkQuoteOperation.cs
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

```csharp:Data/Entities/BulkQuoteOperation.cs
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

```csharp:Data/Entities/BulkQuoteOperation.cs
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

```csharp:Data/Entities/BulkQuoteOperation.cs
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

```csharp:Data/Entities/BulkQuoteOperation.cs
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

```csharp:Data/Entities/BulkQuoteOperation.cs
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

```csharp:Data/Entities/BulkQuoteOperation.cs
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

```csharp:Data/Entities/BulkQuoteOperation.cs
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

```csharp:Data/Entities/BulkQuoteOperation.cs
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

```csharp:Data/Entities/BulkQuoteOperation.cs
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

```csharp:Data/Entities/BulkQuoteOperation.cs
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

```csharp:Data/Entities/BulkQuoteOperation.cs
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

```csharp:Data/Entities/BulkQuoteOperation.cs
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

```csharp:Data/Entities/BulkQuoteOperation.cs
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

```csharp:Data/Entities/BulkQuoteOperation.cs
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

```csharp:Data/Entities/BulkQuoteOperation.cs
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

```csharp:Data/Entities/BulkQuoteOperation.cs
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

```csharp:Data/Entities/BulkQuoteOperation.cs
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

```csharp:Data/Entities/BulkQuoteOperation.cs
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

```csharp:Data/Entities/BulkQuoteOperation.cs
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
