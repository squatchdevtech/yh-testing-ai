import axios, { AxiosInstance, AxiosResponse } from 'axios';
import {
  QuoteRequest,
  QuoteResponse,
  TrendingResponse,
  MarketSummaryResponse,
  CurrencyExchangeResponse,
  CryptoResponse,
  BulkQuoteRequest,
  BulkQuoteResponse,
  HealthResponse,
  CapabilitiesResponse,
} from '@/types/api';

class ApiService {
  private api: AxiosInstance;

  constructor() {
    this.api = axios.create({
      baseURL: '/api',
      timeout: 30000,
      headers: {
        'Content-Type': 'application/json',
      },
    });

    // Add request interceptor for logging
    this.api.interceptors.request.use(
      (config) => {
        console.log(`API Request: ${config.method?.toUpperCase()} ${config.url}`);
        return config;
      },
      (error) => {
        console.error('API Request Error:', error);
        return Promise.reject(error);
      }
    );

    // Add response interceptor for error handling
    this.api.interceptors.response.use(
      (response: AxiosResponse) => {
        return response;
      },
      (error) => {
        console.error('API Response Error:', error);
        if (error.response?.status === 401) {
          // Handle unauthorized access
          console.error('Unauthorized access to API');
        }
        return Promise.reject(error);
      }
    );
  }

  // Stock Quote Methods
  async getQuote(request: QuoteRequest): Promise<QuoteResponse> {
    const response = await this.api.get<QuoteResponse>('/yh/quote', {
      params: request,
    });
    return response.data;
  }

  async getQuotePost(request: QuoteRequest): Promise<QuoteResponse> {
    const response = await this.api.post<QuoteResponse>('/yh/quote', request);
    return response.data;
  }

  // Trending Stocks Methods
  async getTrending(region: string): Promise<TrendingResponse> {
    const response = await this.api.get<TrendingResponse>(`/yh/trending/${region}`);
    return response.data;
  }

  // Market Summary Methods
  async getMarketSummary(region: string, language: string = 'en'): Promise<MarketSummaryResponse> {
    const response = await this.api.get<MarketSummaryResponse>(`/yh/market-summary/${region}`, {
      params: { language },
    });
    return response.data;
  }

  // Currency Exchange Methods
  async getCurrencyExchange(fromCurrency: string, toCurrency: string): Promise<CurrencyExchangeResponse> {
    const response = await this.api.get<CurrencyExchangeResponse>(
      `/yh/currency-exchange/${fromCurrency}/${toCurrency}`
    );
    return response.data;
  }

  // Cryptocurrency Methods
  async getCrypto(symbols: string[], baseCurrency: string = 'USD'): Promise<CryptoResponse> {
    const response = await this.api.get<CryptoResponse>('/yh/crypto', {
      params: {
        symbols: symbols.join(','),
        baseCurrency,
      },
    });
    return response.data;
  }

  // Bulk Quote Methods
  async getBulkQuotes(request: BulkQuoteRequest): Promise<BulkQuoteResponse> {
    const response = await this.api.post<BulkQuoteResponse>('/yh/bulk-quotes', request);
    return response.data;
  }

  // API Health and Capabilities
  async getHealth(): Promise<HealthResponse> {
    const response = await this.api.get<HealthResponse>('/yh/health');
    return response.data;
  }

  async getCapabilities(): Promise<CapabilitiesResponse> {
    const response = await this.api.get<CapabilitiesResponse>('/yh/capabilities');
    return response.data;
  }

  // Health Check for the main API
  async getApiHealth(): Promise<any> {
    const response = await this.api.get('/HealthCheck');
    return response.data;
  }
}

// Export singleton instance
export const apiService = new ApiService();
export default apiService;
