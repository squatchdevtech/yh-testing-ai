// Request DTOs
export interface QuoteRequest {
  symbols: string;
  region?: string;
  language?: string;
}

export interface TrendingRequest {
  region: string;
}

export interface MarketSummaryRequest {
  region: string;
  language?: string;
}

export interface CurrencyExchangeRequest {
  fromCurrency: string;
  toCurrency: string;
}

export interface CryptoRequest {
  symbols: string;
  baseCurrency?: string;
}

export interface BulkQuoteRequest {
  symbolGroups: SymbolGroup[];
  region?: string;
  language?: string;
}

export interface SymbolGroup {
  groupName: string;
  symbols: string[];
}

// Response DTOs
export interface QuoteResponse {
  symbols: string[];
  region: string;
  language: string;
  quotes: QuoteData[];
  timestamp: string;
  errorMessage?: string;
}

export interface QuoteData {
  symbol: string;
  regularMarketPrice?: number;
  regularMarketChange?: number;
  regularMarketChangePercent?: number;
  regularMarketTime?: number;
  regularMarketDayHigh?: number;
  regularMarketDayLow?: number;
  regularMarketVolume?: number;
  regularMarketPreviousClose?: number;
  currency?: string;
  marketState?: string;
  shortName?: string;
  longName?: string;
  exchange?: string;
  exchangeTimezoneName?: string;
  exchangeTimezoneShortName?: string;
  quoteType?: string;
  marketCap?: number;
  sharesOutstanding?: number;
  bookValue?: number;
  priceToBook?: number;
  fiftyTwoWeekLow?: number;
  fiftyTwoWeekHigh?: number;
  fiftyDayAverage?: number;
  twoHundredDayAverage?: number;
  trailingPE?: number;
  forwardPE?: number;
  dividendYield?: number;
  trailingAnnualDividendYield?: number;
  beta?: number;
}

export interface TrendingResponse {
  region: string;
  stocks: TrendingStock[];
  timestamp: string;
  count: number;
  jobTimestamp?: number;
  startInterval?: number;
}

export interface TrendingStock {
  symbol: string;
  shortName?: string;
  longName?: string;
  regularMarketPrice?: number;
  regularMarketChange?: number;
  regularMarketChangePercent?: number;
  currency?: string;
  marketState?: string;
  exchange?: string;
  quoteType?: string;
}

export interface MarketSummaryResponse {
  region: string;
  language: string;
  marketIndices: MarketIndex[];
  marketSectors: MarketSector[];
  timestamp: string;
  errorMessage?: string;
}

export interface MarketIndex {
  symbol: string;
  name: string;
  price: number;
  change: number;
  changePercent: number;
  marketState: string;
  currency: string;
}

export interface MarketSector {
  name: string;
  performance: number;
  topStocks: string[];
}

export interface CurrencyExchangeResponse {
  fromCurrency: string;
  toCurrency: string;
  exchangeRate: number;
  lastUpdated: string;
  errorMessage?: string;
}

export interface CryptoResponse {
  symbols: string[];
  baseCurrency: string;
  quotes: CryptoQuote[];
  timestamp: string;
  errorMessage?: string;
}

export interface CryptoQuote {
  symbol: string;
  price: number;
  change24h: number;
  changePercent24h: number;
  marketCap: number;
  volume24h: number;
  currency: string;
}

export interface BulkQuoteResponse {
  groups: BulkQuoteGroup[];
  totalSuccess: number;
  totalFailure: number;
  timestamp: string;
  errors: string[];
}

export interface BulkQuoteGroup {
  groupName: string;
  quotes: QuoteData[];
  successCount: number;
  failureCount: number;
}

export interface HealthResponse {
  service: string;
  status: string;
  apiKeyConfigured: boolean;
  apiKeySource: string;
  parameterStorePath: string;
  supportedRegions: string[];
  timestamp: string;
  message: string;
  availableEndpoints: string[];
}

export interface CapabilitiesResponse {
  endpoints: ApiEndpoint[];
  supportedRegions: string[];
  supportedLanguages: string[];
  supportedAssetTypes: string[];
  rateLimits: RateLimit[];
  timestamp: string;
}

export interface ApiEndpoint {
  path: string;
  method: string;
  description: string;
  parameters: string[];
}

export interface RateLimit {
  endpoint: string;
  requestsPerMinute: number;
  requestsPerHour: number;
}

export interface ErrorResponse {
  error: ErrorDetails;
  timestamp: string;
}

export interface ErrorDetails {
  code: string;
  message: string;
  details?: string;
}
