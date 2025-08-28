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
