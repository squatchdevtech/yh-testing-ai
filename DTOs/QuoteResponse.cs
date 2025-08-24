namespace WebApiProject.DTOs;

/// <summary>
/// Response DTO for stock quotes
/// </summary>
public sealed record QuoteResponse
{
    public required string[] Symbols { get; init; }
    public required string Region { get; init; }
    public required string Language { get; init; }
    public required QuoteData[] Quotes { get; init; }
    public required DateTime Timestamp { get; init; }
    public string? ErrorMessage { get; init; }
}

/// <summary>
/// Individual quote data
/// </summary>
public sealed record QuoteData
{
    public required string Symbol { get; init; }
    public decimal? RegularMarketPrice { get; init; }
    public decimal? RegularMarketChange { get; init; }
    public decimal? RegularMarketChangePercent { get; init; }
    public long? RegularMarketTime { get; init; }
    public decimal? RegularMarketDayHigh { get; init; }
    public decimal? RegularMarketDayLow { get; init; }
    public long? RegularMarketVolume { get; init; }
    public decimal? RegularMarketPreviousClose { get; init; }
    public string? Currency { get; init; }
    public string? MarketState { get; init; }
    public string? ShortName { get; init; }
    public string? LongName { get; init; }
    public string? Exchange { get; init; }
    public string? ExchangeTimezoneName { get; init; }
    public string? ExchangeTimezoneShortName { get; init; }
    public string? QuoteType { get; init; }
    public long? MarketCap { get; init; }
    public long? SharesOutstanding { get; init; }
    public decimal? BookValue { get; init; }
    public decimal? PriceToBook { get; init; }
    public decimal? FiftyTwoWeekLow { get; init; }
    public decimal? FiftyTwoWeekHigh { get; init; }
    public decimal? FiftyDayAverage { get; init; }
    public decimal? TwoHundredDayAverage { get; init; }
    public decimal? TrailingPE { get; init; }
    public decimal? ForwardPE { get; init; }
    public decimal? DividendYield { get; init; }
    public decimal? TrailingAnnualDividendYield { get; init; }
    public decimal? Beta { get; init; }
}

/// <summary>
/// Response DTO for trending stocks
/// </summary>
public sealed record TrendingResponse
{
    public required string Region { get; init; }
    public required TrendingStock[] Stocks { get; init; }
    public required DateTime Timestamp { get; init; }
    public int Count { get; init; }
    public long? JobTimestamp { get; init; }
    public long? StartInterval { get; init; }
}

/// <summary>
/// Individual trending stock data
/// </summary>
public sealed record TrendingStock
{
    public required string Symbol { get; init; }
    public string? ShortName { get; init; }
    public string? LongName { get; init; }
    public decimal? RegularMarketPrice { get; init; }
    public decimal? RegularMarketChange { get; init; }
    public decimal? RegularMarketChangePercent { get; init; }
    public string? Currency { get; init; }
    public string? MarketState { get; init; }
    public string? Exchange { get; init; }
    public string? QuoteType { get; init; }
}

/// <summary>
/// Health check response DTO
/// </summary>
public sealed record HealthResponse
{
    public required string Service { get; init; }
    public required string Status { get; init; }
    public required bool ApiKeyConfigured { get; init; }
    public required string ApiKeySource { get; init; }
    public required string ParameterStorePath { get; init; }
    public required string[] SupportedRegions { get; init; }
    public required DateTime Timestamp { get; init; }
    public required string Message { get; init; }
    public required string[] AvailableEndpoints { get; init; }
}

/// <summary>
/// Standard error response DTO
/// </summary>
public sealed record ErrorResponse
{
    public required ErrorDetails Error { get; init; }
    public required DateTime Timestamp { get; init; }
}

/// <summary>
/// Error details
/// </summary>
public sealed record ErrorDetails
{
    public required string Code { get; init; }
    public required string Message { get; init; }
    public string? Details { get; init; }
}
