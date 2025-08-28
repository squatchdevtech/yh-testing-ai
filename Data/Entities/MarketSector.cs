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
