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
