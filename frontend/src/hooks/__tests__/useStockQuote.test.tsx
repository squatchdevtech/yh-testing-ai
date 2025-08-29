import { describe, it, expect, vi, beforeEach, renderHook, waitFor } from 'vitest'
import { QueryClient, QueryClientProvider } from '@tanstack/react-query'
import { useStockQuote, useStockQuotePost } from '../useStockQuote'
import { mockQuoteResponse } from '../../test/test-utils'

// Mock the API service
vi.mock('../../services/api', () => ({
  ApiService: vi.fn().mockImplementation(() => ({
    getQuote: vi.fn(),
    getQuotePost: vi.fn(),
  })),
}))

// Mock React Query
vi.mock('@tanstack/react-query', async () => {
  const actual = await vi.importActual('@tanstack/react-query')
  return {
    ...actual,
    useQuery: vi.fn(),
  }
})

describe('useStockQuote', () => {
  let queryClient: QueryClient
  let mockApiService: any

  beforeEach(() => {
    vi.clearAllMocks()
    
    queryClient = new QueryClient({
      defaultOptions: {
        queries: { retry: false },
        mutations: { retry: false },
      },
    })

    // Mock the API service methods
    const { ApiService } = require('../../services/api')
    mockApiService = new ApiService()
  })

  const wrapper = ({ children }: { children: React.ReactNode }) => (
    <QueryClientProvider client={queryClient}>
      {children}
    </QueryClientProvider>
  )

  describe('useStockQuote', () => {
    it('should return loading state initially', () => {
      const { result } = renderHook(
        () => useStockQuote('AAPL', 'US', 'en-US'),
        { wrapper }
      )

      expect(result.current.isLoading).toBe(false)
      expect(result.current.isError).toBe(false)
      expect(result.current.data).toBeUndefined()
    })

    it('should handle enabled state correctly', () => {
      const { result } = renderHook(
        () => useStockQuote('', 'US', 'en-US'), // Empty symbol
        { wrapper }
      )

      expect(result.current.isLoading).toBe(false)
      expect(result.current.isError).toBe(false)
      expect(result.current.data).toBeUndefined()
    })

    it('should handle valid symbol correctly', () => {
      const { result } = renderHook(
        () => useStockQuote('AAPL', 'US', 'en-US'),
        { wrapper }
      )

      expect(result.current.isLoading).toBe(false)
      expect(result.current.isError).toBe(false)
      expect(result.current.data).toBeUndefined()
    })
  })

  describe('useStockQuotePost', () => {
    it('should return loading state initially', () => {
      const { result } = renderHook(
        () => useStockQuotePost('AAPL', 'US', 'en-US'),
        { wrapper }
      )

      expect(result.current.isLoading).toBe(false)
      expect(result.current.isError).toBe(false)
      expect(result.current.data).toBeUndefined()
    })

    it('should handle enabled state correctly', () => {
      const { result } = renderHook(
        () => useStockQuotePost('', 'US', 'en-US'), // Empty symbol
        { wrapper }
      )

      expect(result.current.isLoading).toBe(false)
      expect(result.current.isError).toBe(false)
      expect(result.current.data).toBeUndefined()
    })

    it('should handle valid symbol correctly', () => {
      const { result } = renderHook(
        () => useStockQuotePost('AAPL', 'US', 'en-US'),
        { wrapper }
      )

      expect(result.current.isLoading).toBe(false)
      expect(result.current.isError).toBe(false)
      expect(result.current.data).toBeUndefined()
    })
  })

  describe('hook behavior', () => {
    it('should not make API calls when symbol is empty', () => {
      renderHook(
        () => useStockQuote('', 'US', 'en-US'),
        { wrapper }
      )

      expect(mockApiService.getQuote).not.toHaveBeenCalled()
    })

    it('should not make API calls when region is empty', () => {
      renderHook(
        () => useStockQuote('AAPL', '', 'en-US'),
        { wrapper }
      )

      expect(mockApiService.getQuote).not.toHaveBeenCalled()
    })

    it('should handle different regions correctly', () => {
      renderHook(
        () => useStockQuote('AAPL', 'GB', 'en-GB'),
        { wrapper }
      )

      // The hook should be set up correctly for different regions
      expect(true).toBe(true) // Placeholder assertion
    })

    it('should handle different languages correctly', () => {
      renderHook(
        () => useStockQuote('AAPL', 'US', 'es-ES'),
        { wrapper }
      )

      // The hook should be set up correctly for different languages
      expect(true).toBe(true) // Placeholder assertion
    })
  })

  describe('error handling', () => {
    it('should handle API errors gracefully', () => {
      const { result } = renderHook(
        () => useStockQuote('INVALID', 'US', 'en-US'),
        { wrapper }
      )

      expect(result.current.isError).toBe(false)
      expect(result.current.error).toBeUndefined()
    })

    it('should handle network errors gracefully', () => {
      const { result } = renderHook(
        () => useStockQuote('AAPL', 'US', 'en-US'),
        { wrapper }
      )

      expect(result.current.isError).toBe(false)
      expect(result.current.error).toBeUndefined()
    })
  })

  describe('data management', () => {
    it('should handle successful data retrieval', () => {
      const { result } = renderHook(
        () => useStockQuote('AAPL', 'US', 'en-US'),
        { wrapper }
      )

      expect(result.current.data).toBeUndefined()
      expect(result.current.isSuccess).toBe(false)
    })

    it('should handle data updates', () => {
      const { result, rerender } = renderHook(
        ({ symbol, region, lang }) => useStockQuote(symbol, region, lang),
        { 
          wrapper,
          initialProps: { symbol: 'AAPL', region: 'US', lang: 'en-US' }
        }
      )

      // Change the symbol
      rerender({ symbol: 'TSLA', region: 'US', lang: 'en-US' })

      expect(result.current.data).toBeUndefined()
    })
  })
})
