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
