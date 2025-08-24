using WebApiProject.DTOs;

namespace WebApiProject.Services;

public interface IYahooFinanceService
{
    /// <summary>
    /// Gets real-time quote data for the specified request
    /// </summary>
    /// <param name="request">Quote request containing symbols, region, and language</param>
    /// <returns>Result containing quote response DTO or error</returns>
    Task<Result<QuoteResponse>> GetQuoteAsync(QuoteRequest request);

    /// <summary>
    /// Gets trending stocks for the specified request
    /// </summary>
    /// <param name="request">Trending request containing region</param>
    /// <returns>Result containing trending response DTO or error</returns>
    Task<Result<TrendingResponse>> GetTrendingAsync(TrendingRequest request);

    /// <summary>
    /// Validates if the provided region is supported
    /// </summary>
    /// <param name="region">Region to validate</param>
    /// <returns>True if region is valid</returns>
    bool IsValidRegion(string region);

    /// <summary>
    /// Gets the list of supported regions
    /// </summary>
    /// <returns>Array of supported regions</returns>
    string[] GetSupportedRegions();
}
