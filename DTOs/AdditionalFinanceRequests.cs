using System.ComponentModel.DataAnnotations;

namespace WebApiProject.DTOs;

/// <summary>
/// Request DTO for getting market summary
/// </summary>
public sealed record MarketSummaryRequest
{
    /// <summary>
    /// Market region (US, AU, CA, FR, DE, HK, IT, ES, GB, IN)
    /// </summary>
    [Required(ErrorMessage = "Region is required")]
    [MaxLength(2, ErrorMessage = "Region must be 2 characters")]
    public required string Region { get; init; }

    /// <summary>
    /// Response language (en, fr, de, it, es, zh)
    /// </summary>
    [MaxLength(2, ErrorMessage = "Language must be 2 characters")]
    public string Language { get; init; } = "en";
}

/// <summary>
/// Request DTO for getting currency exchange rates
/// </summary>
public sealed record CurrencyExchangeRequest
{
    /// <summary>
    /// From currency code (e.g., USD)
    /// </summary>
    [Required(ErrorMessage = "From currency is required")]
    [MaxLength(3, ErrorMessage = "Currency code must be 3 characters")]
    public required string FromCurrency { get; init; }

    /// <summary>
    /// To currency code (e.g., EUR)
    /// </summary>
    [Required(ErrorMessage = "To currency is required")]
    [MaxLength(3, ErrorMessage = "Currency code must be 3 characters")]
    public required string ToCurrency { get; init; }
}

/// <summary>
/// Request DTO for getting cryptocurrency data
/// </summary>
public sealed record CryptoRequest
{
    /// <summary>
    /// Comma-separated list of crypto symbols (max 10)
    /// </summary>
    [Required(ErrorMessage = "Symbols are required")]
    [MaxLength(200, ErrorMessage = "Symbols parameter too long")]
    public required string Symbols { get; init; }

    /// <summary>
    /// Base currency for comparison (USD, EUR, BTC)
    /// </summary>
    [MaxLength(3, ErrorMessage = "Currency must be 3 characters")]
    public string Currency { get; init; } = "USD";

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
/// Request DTO for bulk quote operations
/// </summary>
public sealed record BulkQuoteRequest
{
    /// <summary>
    /// Array of symbol groups to process
    /// </summary>
    [Required(ErrorMessage = "Symbol groups are required")]
    public required SymbolGroup[] SymbolGroups { get; init; }

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
}

/// <summary>
/// Symbol group for bulk operations
/// </summary>
public sealed record SymbolGroup
{
    /// <summary>
    /// Group identifier
    /// </summary>
    [Required(ErrorMessage = "Group name is required")]
    public required string GroupName { get; init; }

    /// <summary>
    /// Symbols in this group
    /// </summary>
    [Required(ErrorMessage = "Symbols are required")]
    public required string[] Symbols { get; init; }
}
