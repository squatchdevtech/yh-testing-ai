using System.ComponentModel.DataAnnotations;

namespace WebApiProject.DTOs;

/// <summary>
/// Request DTO for getting stock quotes
/// </summary>
public sealed record QuoteRequest
{
    /// <summary>
    /// Comma-separated list of symbols (max 10)
    /// </summary>
    [Required(ErrorMessage = "Symbols are required")]
    [MaxLength(200, ErrorMessage = "Symbols parameter too long")]
    public required string Symbols { get; init; }

    /// <summary>
    /// Market region (US, AU, CA, FR, DE, HK, IT, ES, GB, IN)
    /// </summary>
    [MaxLength(2, ErrorMessage = "Region must be 2 characters")]
    public string Region { get; init; } = "US";

    /// <summary>
    /// Response language (en, fr, de, it, es, zh)
    /// </summary>
    [MaxLength(2, ErrorMessage = "Language must be 2 characters")]
    public string Language { get; init; } = "en";

    /// <summary>
    /// Validates if the symbols string contains valid format
    /// </summary>
    public bool HasValidSymbolFormat()
    {
        if (string.IsNullOrWhiteSpace(Symbols))
            return false;

        var symbols = Symbols.Split(',', StringSplitOptions.RemoveEmptyEntries);
        return symbols.Length <= 10 && symbols.All(s => !string.IsNullOrWhiteSpace(s.Trim()));
    }

    /// <summary>
    /// Gets the individual symbols as an array
    /// </summary>
    public string[] GetSymbolsArray()
    {
        return Symbols.Split(',', StringSplitOptions.RemoveEmptyEntries)
                     .Select(s => s.Trim().ToUpperInvariant())
                     .ToArray();
    }
}

/// <summary>
/// Request DTO for getting trending stocks
/// </summary>
public sealed record TrendingRequest
{
    /// <summary>
    /// Market region (US, AU, CA, FR, DE, HK, IT, ES, GB, IN)
    /// </summary>
    [Required(ErrorMessage = "Region is required")]
    [MaxLength(2, ErrorMessage = "Region must be 2 characters")]
    public required string Region { get; init; }
}
