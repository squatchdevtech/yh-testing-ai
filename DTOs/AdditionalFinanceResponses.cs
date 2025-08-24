namespace WebApiProject.DTOs;

/// <summary>
/// Response DTO for market summary
/// </summary>
public sealed record MarketSummaryResponse
{
    public required string Region { get; init; }
    public required string Language { get; init; }
    public required MarketIndex[] Indices { get; init; }
    public required MarketSector[] Sectors { get; init; }
    public required DateTime Timestamp { get; init; }
    public string? ErrorMessage { get; init; }
}

/// <summary>
/// Market index data
/// </summary>
public sealed record MarketIndex
{
    public required string Symbol { get; init; }
    public required string Name { get; init; }
    public decimal? Price { get; init; }
    public decimal? Change { get; init; }
    public decimal? ChangePercent { get; init; }
    public string? Currency { get; init; }
    public string? MarketState { get; init; }
    public long? MarketTime { get; init; }
}

/// <summary>
/// Market sector data
/// </summary>
public sealed record MarketSector
{
    public required string Name { get; init; }
    public decimal? Performance { get; init; }
    public decimal? ChangePercent { get; init; }
    public string[] TopStocks { get; init; } = Array.Empty<string>();
}

/// <summary>
/// Response DTO for currency exchange
/// </summary>
public sealed record CurrencyExchangeResponse
{
    public required string FromCurrency { get; init; }
    public required string ToCurrency { get; init; }
    public required string Symbol { get; init; }
    public decimal? ExchangeRate { get; init; }
    public decimal? Change { get; init; }
    public decimal? ChangePercent { get; init; }
    public long? LastUpdate { get; init; }
    public required DateTime Timestamp { get; init; }
    public string? ErrorMessage { get; init; }
}

/// <summary>
/// Response DTO for cryptocurrency data
/// </summary>
public sealed record CryptoResponse
{
    public required string[] Symbols { get; init; }
    public required string Currency { get; init; }
    public required CryptoData[] CryptoQuotes { get; init; }
    public required DateTime Timestamp { get; init; }
    public string? ErrorMessage { get; init; }
}

/// <summary>
/// Individual cryptocurrency data
/// </summary>
public sealed record CryptoData
{
    public required string Symbol { get; init; }
    public string? Name { get; init; }
    public decimal? Price { get; init; }
    public decimal? Change { get; init; }
    public decimal? ChangePercent { get; init; }
    public string? Currency { get; init; }
    public long? MarketCap { get; init; }
    public long? Volume24h { get; init; }
    public decimal? High24h { get; init; }
    public decimal? Low24h { get; init; }
    public string? MarketState { get; init; }
    public long? LastUpdate { get; init; }
}

/// <summary>
/// Response DTO for bulk quote operations
/// </summary>
public sealed record BulkQuoteResponse
{
    public required string Region { get; init; }
    public required string Language { get; init; }
    public required QuoteGroup[] QuoteGroups { get; init; }
    public required DateTime Timestamp { get; init; }
    public int TotalSymbols { get; init; }
    public int SuccessfulQuotes { get; init; }
    public int FailedQuotes { get; init; }
    public string[] Errors { get; init; } = Array.Empty<string>();
}

/// <summary>
/// Quote group for bulk operations
/// </summary>
public sealed record QuoteGroup
{
    public required string GroupName { get; init; }
    public required QuoteData[] Quotes { get; init; }
    public int SuccessCount { get; init; }
    public int ErrorCount { get; init; }
    public string[] Errors { get; init; } = Array.Empty<string>();
}

/// <summary>
/// Response DTO for API capabilities and supported features
/// </summary>
public sealed record CapabilitiesResponse
{
    public required ApiEndpoint[] AvailableEndpoints { get; init; }
    public required string[] SupportedRegions { get; init; }
    public required string[] SupportedLanguages { get; init; }
    public required string[] SupportedAssetTypes { get; init; }
    public required RateLimit RateLimits { get; init; }
    public required DateTime Timestamp { get; init; }
    public required string ApiVersion { get; init; }
}

/// <summary>
/// API endpoint information
/// </summary>
public sealed record ApiEndpoint
{
    public required string Path { get; init; }
    public required string Method { get; init; }
    public required string Description { get; init; }
    public required string[] Parameters { get; init; }
    public required string[] Examples { get; init; }
}

/// <summary>
/// Rate limiting information
/// </summary>
public sealed record RateLimit
{
    public int RequestsPerMinute { get; init; }
    public int RequestsPerHour { get; init; }
    public int RequestsPerDay { get; init; }
    public int BurstLimit { get; init; }
}
