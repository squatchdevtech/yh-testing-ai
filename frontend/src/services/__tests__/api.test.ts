import { describe, it, expect, vi, beforeEach, afterEach } from 'vitest'
import { ApiService } from '../api'
import { mockQuoteResponse, mockTrendingResponse, mockMarketSummaryResponse, mockHealthResponse } from '../../test/test-utils'

// Mock axios
vi.mock('axios', () => ({
  default: {
    create: vi.fn(() => ({
      get: vi.fn(),
      post: vi.fn(),
      interceptors: {
        request: { use: vi.fn() },
        response: { use: vi.fn() },
      },
    })),
  },
}))

describe('ApiService', () => {
  let apiService: ApiService
  let mockAxios: any

  beforeEach(() => {
    vi.clearAllMocks()
    
    // Create a fresh instance for each test
    const axios = require('axios')
    mockAxios = axios.default.create()
    apiService = new ApiService()
    
    // Replace the axios instance with our mock
    ;(apiService as any).axios = mockAxios
  })

  afterEach(() => {
    vi.restoreAllMocks()
  })

  describe('constructor', () => {
    it('should create an instance with default configuration', () => {
      expect(apiService).toBeInstanceOf(ApiService)
    })
  })

  describe('getQuote', () => {
    it('should fetch quote data successfully', async () => {
      const mockResponse = { data: mockQuoteResponse }
      mockAxios.get.mockResolvedValueOnce(mockResponse)

      const result = await apiService.getQuote('AAPL', 'US', 'en-US')

      expect(mockAxios.get).toHaveBeenCalledWith('/yh/quote', {
        params: {
          symbol: 'AAPL',
          region: 'US',
          lang: 'en-US',
        },
      })
      expect(result).toEqual(mockQuoteResponse)
    })

    it('should handle errors gracefully', async () => {
      const errorMessage = 'Network error'
      mockAxios.get.mockRejectedValueOnce(new Error(errorMessage))

      await expect(apiService.getQuote('INVALID', 'US', 'en-US')).rejects.toThrow(errorMessage)
    })
  })

  describe('getTrending', () => {
    it('should fetch trending stocks successfully', async () => {
      const mockResponse = { data: mockTrendingResponse }
      mockAxios.get.mockResolvedValueOnce(mockResponse)

      const result = await apiService.getTrending('US')

      expect(mockAxios.get).toHaveBeenCalledWith('/yh/trending', {
        params: { region: 'US' },
      })
      expect(result).toEqual(mockTrendingResponse)
    })

    it('should handle errors gracefully', async () => {
      const errorMessage = 'API error'
      mockAxios.get.mockRejectedValueOnce(new Error(errorMessage))

      await expect(apiService.getTrending('INVALID')).rejects.toThrow(errorMessage)
    })
  })

  describe('getMarketSummary', () => {
    it('should fetch market summary successfully', async () => {
      const mockResponse = { data: mockMarketSummaryResponse }
      mockAxios.get.mockResolvedValueOnce(mockResponse)

      const result = await apiService.getMarketSummary('US', 'en-US')

      expect(mockAxios.get).toHaveBeenCalledWith('/yh/market-summary', {
        params: {
          region: 'US',
          lang: 'en-US',
        },
      })
      expect(result).toEqual(mockMarketSummaryResponse)
    })

    it('should handle errors gracefully', async () => {
      const errorMessage = 'Service unavailable'
      mockAxios.get.mockRejectedValueOnce(new Error(errorMessage))

      await expect(apiService.getMarketSummary('US', 'en-US')).rejects.toThrow(errorMessage)
    })
  })

  describe('getCurrencyExchange', () => {
    it('should fetch currency exchange data successfully', async () => {
      const mockResponse = { data: { success: true, data: {} } }
      mockAxios.get.mockResolvedValueOnce(mockResponse)

      const result = await apiService.getCurrencyExchange('USD', 'EUR')

      expect(mockAxios.get).toHaveBeenCalledWith('/yh/currency-exchange', {
        params: {
          fromCurrency: 'USD',
          toCurrency: 'EUR',
        },
      })
      expect(result).toEqual(mockResponse.data)
    })

    it('should handle errors gracefully', async () => {
      const errorMessage = 'Invalid currency pair'
      mockAxios.get.mockRejectedValueOnce(new Error(errorMessage))

      await expect(apiService.getCurrencyExchange('INVALID', 'EUR')).rejects.toThrow(errorMessage)
    })
  })

  describe('getCrypto', () => {
    it('should fetch cryptocurrency data successfully', async () => {
      const mockResponse = { data: { success: true, data: {} } }
      mockAxios.get.mockResolvedValueOnce(mockResponse)

      const result = await apiService.getCrypto('BTC-USD')

      expect(mockAxios.get).toHaveBeenCalledWith('/yh/crypto', {
        params: { symbol: 'BTC-USD' },
      })
      expect(result).toEqual(mockResponse.data)
    })

    it('should handle errors gracefully', async () => {
      const errorMessage = 'Invalid crypto symbol'
      mockAxios.get.mockRejectedValueOnce(new Error(errorMessage))

      await expect(apiService.getCrypto('INVALID')).rejects.toThrow(errorMessage)
    })
  })

  describe('getBulkQuotes', () => {
    it('should fetch bulk quotes successfully', async () => {
      const mockResponse = { data: { success: true, data: [] } }
      mockAxios.post.mockResolvedValueOnce(mockResponse)

      const symbols = ['AAPL', 'TSLA', 'GOOGL']
      const result = await apiService.getBulkQuotes(symbols, 'US', 'en-US')

      expect(mockAxios.post).toHaveBeenCalledWith('/yh/bulk-quotes', {
        symbols,
        region: 'US',
        lang: 'en-US',
      })
      expect(result).toEqual(mockResponse.data)
    })

    it('should handle errors gracefully', async () => {
      const errorMessage = 'Too many symbols'
      mockAxios.post.mockRejectedValueOnce(new Error(errorMessage))

      const symbols = ['AAPL', 'TSLA']
      await expect(apiService.getBulkQuotes(symbols, 'US', 'en-US')).rejects.toThrow(errorMessage)
    })
  })

  describe('getHealth', () => {
    it('should fetch health status successfully', async () => {
      const mockResponse = { data: mockHealthResponse }
      mockAxios.get.mockResolvedValueOnce(mockResponse)

      const result = await apiService.getHealth()

      expect(mockAxios.get).toHaveBeenCalledWith('/yh/health')
      expect(result).toEqual(mockHealthResponse)
    })

    it('should handle errors gracefully', async () => {
      const errorMessage = 'Health check failed'
      mockAxios.get.mockRejectedValueOnce(new Error(errorMessage))

      await expect(apiService.getHealth()).rejects.toThrow(errorMessage)
    })
  })

  describe('getCapabilities', () => {
    it('should fetch API capabilities successfully', async () => {
      const mockResponse = { data: { success: true, data: {} } }
      mockAxios.get.mockResolvedValueOnce(mockResponse)

      const result = await apiService.getCapabilities()

      expect(mockAxios.get).toHaveBeenCalledWith('/yh/capabilities')
      expect(result).toEqual(mockResponse.data)
    })

    it('should handle errors gracefully', async () => {
      const errorMessage = 'Capabilities unavailable'
      mockAxios.get.mockRejectedValueOnce(new Error(errorMessage))

      await expect(apiService.getCapabilities()).rejects.toThrow(errorMessage)
    })
  })

  describe('getApiHealth', () => {
    it('should fetch main API health successfully', async () => {
      const mockResponse = { data: { status: 'Healthy' } }
      mockAxios.get.mockResolvedValueOnce(mockResponse)

      const result = await apiService.getApiHealth()

      expect(mockAxios.get).toHaveBeenCalledWith('/HealthCheck')
      expect(result).toEqual(mockResponse.data)
    })

    it('should handle errors gracefully', async () => {
      const errorMessage = 'Main API down'
      mockAxios.get.mockRejectedValueOnce(new Error(errorMessage))

      await expect(apiService.getApiHealth()).rejects.toThrow(errorMessage)
    })
  })

  describe('error handling', () => {
    it('should handle network errors', async () => {
      const networkError = new Error('Network Error')
      mockAxios.get.mockRejectedValueOnce(networkError)

      await expect(apiService.getQuote('AAPL', 'US', 'en-US')).rejects.toThrow('Network Error')
    })

    it('should handle timeout errors', async () => {
      const timeoutError = new Error('Request timeout')
      mockAxios.get.mockRejectedValueOnce(timeoutError)

      await expect(apiService.getQuote('AAPL', 'US', 'en-US')).rejects.toThrow('Request timeout')
    })

    it('should handle malformed responses', async () => {
      const malformedResponse = { data: null }
      mockAxios.get.mockResolvedValueOnce(malformedResponse)

      const result = await apiService.getQuote('AAPL', 'US', 'en-US')
      expect(result).toBeNull()
    })
  })

  describe('request/response interceptors', () => {
    it('should log requests', () => {
      // The interceptors are set up in the constructor
      expect(mockAxios.interceptors.request.use).toHaveBeenCalled()
      expect(mockAxios.interceptors.response.use).toHaveBeenCalled()
    })
  })
})
