using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiProject.Data.Entities;

[Table("stock_quotes")]
public class StockQuote
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [Column("symbol")]
    [StringLength(20)]
    public string Symbol { get; set; } = string.Empty;

    [Column("region")]
    [StringLength(2)]
    public string Region { get; set; } = "US";

    [Column("quote_timestamp")]
    public long? QuoteTimestamp { get; set; }

    [Column("regular_market_price")]
    [Column(TypeName = "decimal(20,6)")]
    public decimal? RegularMarketPrice { get; set; }

    [Column("regular_market_change")]
    [Column(TypeName = "decimal(20,6)")]
    public decimal? RegularMarketChange { get; set; }

    [Column("regular_market_change_percent")]
    [Column(TypeName = "decimal(10,4)")]
    public decimal? RegularMarketChangePercent { get; set; }

    [Column("regular_market_time")]
    public long? RegularMarketTime { get; set; }

    [Column("regular_market_day_high")]
    [Column(TypeName = "decimal(20,6)")]
    public decimal? RegularMarketDayHigh { get; set; }

    [Column("regular_market_day_low")]
    [Column(TypeName = "decimal(20,6)")]
    public decimal? RegularMarketDayLow { get; set; }

    [Column("regular_market_volume")]
    public long? RegularMarketVolume { get; set; }

    [Column("regular_market_previous_close")]
    [Column(TypeName = "decimal(20,6)")]
    public decimal? RegularMarketPreviousClose { get; set; }

    [Column("currency")]
    [StringLength(3)]
    public string? Currency { get; set; }

    [Column("market_state")]
    [StringLength(20)]
    public string? MarketState { get; set; }

    [Column("short_name")]
    [StringLength(100)]
    public string? ShortName { get; set; }

    [Column("long_name")]
    public string? LongName { get; set; }

    [Column("exchange")]
    [StringLength(20)]
    public string? Exchange { get; set; }

    [Column("exchange_timezone_name")]
    [StringLength(100)]
    public string? ExchangeTimezoneName { get; set; }

    [Column("exchange_timezone_short_name")]
    [StringLength(10)]
    public string? ExchangeTimezoneShortName { get; set; }

    [Column("quote_type")]
    [StringLength(20)]
    public string? QuoteType { get; set; }

    [Column("market_cap")]
    public long? MarketCap { get; set; }

    [Column("shares_outstanding")]
    public long? SharesOutstanding { get; set; }

    [Column("book_value")]
    [Column(TypeName = "decimal(20,6)")]
    public decimal? BookValue { get; set; }

    [Column("price_to_book")]
    [Column(TypeName = "decimal(10,4)")]
    public decimal? PriceToBook { get; set; }

    [Column("fifty_two_week_low")]
    [Column(TypeName = "decimal(20,6)")]
    public decimal? FiftyTwoWeekLow { get; set; }

    [Column("fifty_two_week_high")]
    [Column(TypeName = "decimal(20,6)")]
    public decimal? FiftyTwoWeekHigh { get; set; }

    [Column("fifty_day_average")]
    [Column(TypeName = "decimal(20,6)")]
    public decimal? FiftyDayAverage { get; set; }

    [Column("two_hundred_day_average")]
    [Column(TypeName = "decimal(20,6)")]
    public decimal? TwoHundredDayAverage { get; set; }

    [Column("trailing_pe")]
    [Column(TypeName = "decimal(10,4)")]
    public decimal? TrailingPE { get; set; }

    [Column("forward_pe")]
    [Column(TypeName = "decimal(10,4)")]
    public decimal? ForwardPE { get; set; }

    [Column("dividend_yield")]
    [Column(TypeName = "decimal(10,4)")]
    public decimal? DividendYield { get; set; }

    [Column("trailing_annual_dividend_yield")]
    [Column(TypeName = "decimal(10,4)")]
    public decimal? TrailingAnnualDividendYield { get; set; }

    [Column("beta")]
    [Column(TypeName = "decimal(10,4)")]
    public decimal? Beta { get; set; }

    [Column("cache_valid_until")]
    public DateTime? CacheValidUntil { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
}
