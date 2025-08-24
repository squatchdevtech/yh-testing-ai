using System.Text.Json;
using WebApiProject.DTOs;

namespace WebApiProject.Services.Mappers;

/// <summary>
/// Maps Yahoo Finance API responses to DTOs
/// </summary>
public static class YahooFinanceMapper
{
    /// <summary>
    /// Maps JSON response to QuoteResponse DTO
    /// </summary>
    public static Result<QuoteResponse> MapToQuoteResponse(
        JsonDocument jsonDocument, 
        string[] symbols, 
        string region, 
        string language)
    {
        try
        {
            var root = jsonDocument.RootElement;
            
            if (!root.TryGetProperty("quoteResponse", out var quoteResponseElement))
            {
                return DomainErrors.Data.ParseError("Missing quoteResponse in API response");
            }

            var quotes = new List<QuoteData>();

            if (quoteResponseElement.TryGetProperty("result", out var resultElement) && 
                resultElement.ValueKind == JsonValueKind.Array)
            {
                foreach (var quoteElement in resultElement.EnumerateArray())
                {
                    var quoteData = MapToQuoteData(quoteElement);
                    if (quoteData != null)
                    {
                        quotes.Add(quoteData);
                    }
                }
            }

            // Check for API errors
            string? errorMessage = null;
            if (quoteResponseElement.TryGetProperty("error", out var errorElement) && 
                errorElement.ValueKind != JsonValueKind.Null)
            {
                errorMessage = errorElement.GetString();
            }

            var response = new QuoteResponse
            {
                Symbols = symbols,
                Region = region,
                Language = language,
                Quotes = quotes.ToArray(),
                Timestamp = DateTime.UtcNow,
                ErrorMessage = errorMessage
            };

            return Result.Success(response);
        }
        catch (Exception ex)
        {
            return DomainErrors.Data.ParseError($"Failed to map quote response: {ex.Message}");
        }
    }

    /// <summary>
    /// Maps JSON response to TrendingResponse DTO
    /// </summary>
    public static Result<TrendingResponse> MapToTrendingResponse(
        JsonDocument jsonDocument, 
        string region)
    {
        try
        {
            var root = jsonDocument.RootElement;
            
            if (!root.TryGetProperty("finance", out var financeElement))
            {
                return DomainErrors.Data.ParseError("Missing finance element in API response");
            }

            var stocks = new List<TrendingStock>();
            int count = 0;
            long? jobTimestamp = null;
            long? startInterval = null;

            if (financeElement.TryGetProperty("result", out var resultElement) && 
                resultElement.ValueKind == JsonValueKind.Array)
            {
                foreach (var trendingElement in resultElement.EnumerateArray())
                {
                    if (trendingElement.TryGetProperty("count", out var countElement))
                    {
                        count = countElement.GetInt32();
                    }

                    if (trendingElement.TryGetProperty("jobTimestamp", out var jobTimestampElement))
                    {
                        jobTimestamp = jobTimestampElement.GetInt64();
                    }

                    if (trendingElement.TryGetProperty("startInterval", out var startIntervalElement))
                    {
                        startInterval = startIntervalElement.GetInt64();
                    }

                    if (trendingElement.TryGetProperty("quotes", out var quotesElement) && 
                        quotesElement.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var stockElement in quotesElement.EnumerateArray())
                        {
                            var stock = MapToTrendingStock(stockElement);
                            if (stock != null)
                            {
                                stocks.Add(stock);
                            }
                        }
                    }
                }
            }

            var response = new TrendingResponse
            {
                Region = region.ToUpper(),
                Stocks = stocks.ToArray(),
                Timestamp = DateTime.UtcNow,
                Count = count,
                JobTimestamp = jobTimestamp,
                StartInterval = startInterval
            };

            return Result.Success(response);
        }
        catch (Exception ex)
        {
            return DomainErrors.Data.ParseError($"Failed to map trending response: {ex.Message}");
        }
    }

    private static QuoteData? MapToQuoteData(JsonElement quoteElement)
    {
        try
        {
            if (!quoteElement.TryGetProperty("symbol", out var symbolElement))
            {
                return null; // Skip quotes without symbol
            }

            var symbol = symbolElement.GetString();
            if (string.IsNullOrEmpty(symbol))
            {
                return null;
            }

            return new QuoteData
            {
                Symbol = symbol,
                RegularMarketPrice = GetOptionalDecimal(quoteElement, "regularMarketPrice"),
                RegularMarketChange = GetOptionalDecimal(quoteElement, "regularMarketChange"),
                RegularMarketChangePercent = GetOptionalDecimal(quoteElement, "regularMarketChangePercent"),
                RegularMarketTime = GetOptionalLong(quoteElement, "regularMarketTime"),
                RegularMarketDayHigh = GetOptionalDecimal(quoteElement, "regularMarketDayHigh"),
                RegularMarketDayLow = GetOptionalDecimal(quoteElement, "regularMarketDayLow"),
                RegularMarketVolume = GetOptionalLong(quoteElement, "regularMarketVolume"),
                RegularMarketPreviousClose = GetOptionalDecimal(quoteElement, "regularMarketPreviousClose"),
                Currency = GetOptionalString(quoteElement, "currency"),
                MarketState = GetOptionalString(quoteElement, "marketState"),
                ShortName = GetOptionalString(quoteElement, "shortName"),
                LongName = GetOptionalString(quoteElement, "longName"),
                Exchange = GetOptionalString(quoteElement, "exchange"),
                ExchangeTimezoneName = GetOptionalString(quoteElement, "exchangeTimezoneName"),
                ExchangeTimezoneShortName = GetOptionalString(quoteElement, "exchangeTimezoneShortName"),
                QuoteType = GetOptionalString(quoteElement, "quoteType"),
                MarketCap = GetOptionalLong(quoteElement, "marketCap"),
                SharesOutstanding = GetOptionalLong(quoteElement, "sharesOutstanding"),
                BookValue = GetOptionalDecimal(quoteElement, "bookValue"),
                PriceToBook = GetOptionalDecimal(quoteElement, "priceToBook"),
                FiftyTwoWeekLow = GetOptionalDecimal(quoteElement, "fiftyTwoWeekLow"),
                FiftyTwoWeekHigh = GetOptionalDecimal(quoteElement, "fiftyTwoWeekHigh"),
                FiftyDayAverage = GetOptionalDecimal(quoteElement, "fiftyDayAverage"),
                TwoHundredDayAverage = GetOptionalDecimal(quoteElement, "twoHundredDayAverage"),
                TrailingPE = GetOptionalDecimal(quoteElement, "trailingPE"),
                ForwardPE = GetOptionalDecimal(quoteElement, "forwardPE"),
                DividendYield = GetOptionalDecimal(quoteElement, "dividendYield"),
                TrailingAnnualDividendYield = GetOptionalDecimal(quoteElement, "trailingAnnualDividendYield"),
                Beta = GetOptionalDecimal(quoteElement, "beta")
            };
        }
        catch
        {
            return null; // Skip invalid quotes
        }
    }

    private static TrendingStock? MapToTrendingStock(JsonElement stockElement)
    {
        try
        {
            if (!stockElement.TryGetProperty("symbol", out var symbolElement))
            {
                return null; // Skip stocks without symbol
            }

            var symbol = symbolElement.GetString();
            if (string.IsNullOrEmpty(symbol))
            {
                return null;
            }

            return new TrendingStock
            {
                Symbol = symbol,
                ShortName = GetOptionalString(stockElement, "shortName"),
                LongName = GetOptionalString(stockElement, "longName"),
                RegularMarketPrice = GetOptionalDecimal(stockElement, "regularMarketPrice"),
                RegularMarketChange = GetOptionalDecimal(stockElement, "regularMarketChange"),
                RegularMarketChangePercent = GetOptionalDecimal(stockElement, "regularMarketChangePercent"),
                Currency = GetOptionalString(stockElement, "currency"),
                MarketState = GetOptionalString(stockElement, "marketState"),
                Exchange = GetOptionalString(stockElement, "exchange"),
                QuoteType = GetOptionalString(stockElement, "quoteType")
            };
        }
        catch
        {
            return null; // Skip invalid stocks
        }
    }

    private static decimal? GetOptionalDecimal(JsonElement element, string propertyName)
    {
        if (element.TryGetProperty(propertyName, out var property))
        {
            if (property.ValueKind == JsonValueKind.Number && property.TryGetDecimal(out var value))
            {
                return value;
            }
        }
        return null;
    }

    private static long? GetOptionalLong(JsonElement element, string propertyName)
    {
        if (element.TryGetProperty(propertyName, out var property))
        {
            if (property.ValueKind == JsonValueKind.Number && property.TryGetInt64(out var value))
            {
                return value;
            }
        }
        return null;
    }

    private static string? GetOptionalString(JsonElement element, string propertyName)
    {
        if (element.TryGetProperty(propertyName, out var property) && 
            property.ValueKind == JsonValueKind.String)
        {
            return property.GetString();
        }
        return null;
    }
}
