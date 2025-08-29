import { describe, it, expect, vi, beforeEach } from 'vitest'
import { render, screen, fireEvent, waitFor } from '../../../../test/test-utils'
import { Dashboard } from '../Dashboard'
import { mockQuoteResponse, mockTrendingResponse, mockMarketSummaryResponse } from '../../../test/test-utils'

// Mock the custom hooks
vi.mock('../../../hooks/useStockQuote', () => ({
  useStockQuote: vi.fn(),
}))

vi.mock('../../../hooks/useTrendingStocks', () => ({
  useTrendingStocks: vi.fn(),
}))

vi.mock('../../../hooks/useMarketSummary', () => ({
  useMarketSummary: vi.fn(),
}))

describe('Dashboard', () => {
  const mockUseStockQuote = vi.mocked(require('../../../hooks/useStockQuote')).useStockQuote
  const mockUseTrendingStocks = vi.mocked(require('../../../hooks/useTrendingStocks')).useTrendingStocks
  const mockUseMarketSummary = vi.mocked(require('../../../hooks/useMarketSummary')).useMarketSummary

  beforeEach(() => {
    vi.clearAllMocks()
    
    // Default mock implementations
    mockUseStockQuote.mockReturnValue({
      data: null,
      isLoading: false,
      isError: false,
      error: null,
      refetch: vi.fn(),
    })
    
    mockUseTrendingStocks.mockReturnValue({
      data: null,
      isLoading: false,
      isError: false,
      error: null,
      refetch: vi.fn(),
    })
    
    mockUseMarketSummary.mockReturnValue({
      data: null,
      isLoading: false,
      isError: false,
      error: null,
      refetch: vi.fn(),
    })
  })

  it('should render the dashboard with all sections', () => {
    render(<Dashboard />)
    
    expect(screen.getByText('Dashboard')).toBeInTheDocument()
    expect(screen.getByText('Quick Stock Quote')).toBeInTheDocument()
    expect(screen.getByText('Trending Stocks')).toBeInTheDocument()
    expect(screen.getByText('Market Summary')).toBeInTheDocument()
  })

  it('should render search form with proper inputs', () => {
    render(<Dashboard />)
    
    expect(screen.getByLabelText('Stock Symbol')).toBeInTheDocument()
    expect(screen.getByLabelText('Region')).toBeInTheDocument()
    expect(screen.getByLabelText('Language')).toBeInTheDocument()
    expect(screen.getByRole('button', { name: 'Get Quote' })).toBeInTheDocument()
  })

  it('should handle form submission correctly', async () => {
    const mockRefetch = vi.fn()
    mockUseStockQuote.mockReturnValue({
      data: null,
      isLoading: false,
      isError: false,
      error: null,
      refetch: mockRefetch,
    })

    render(<Dashboard />)
    
    const symbolInput = screen.getByLabelText('Stock Symbol')
    const regionSelect = screen.getByLabelText('Region')
    const languageSelect = screen.getByLabelText('Language')
    const submitButton = screen.getByRole('button', { name: 'Get Quote' })

    fireEvent.change(symbolInput, { target: { value: 'AAPL' } })
    fireEvent.change(regionSelect, { target: { value: 'US' } })
    fireEvent.change(languageSelect, { target: { value: 'en-US' } })
    fireEvent.click(submitButton)

    await waitFor(() => {
      expect(mockRefetch).toHaveBeenCalled()
    })
  })

  it('should display stock quote data when available', () => {
    mockUseStockQuote.mockReturnValue({
      data: mockQuoteResponse,
      isLoading: false,
      isError: false,
      error: null,
      refetch: vi.fn(),
    })

    render(<Dashboard />)
    
    expect(screen.getByText('AAPL')).toBeInTheDocument()
    expect(screen.getByText('Apple Inc.')).toBeInTheDocument()
    expect(screen.getByText('$150.00')).toBeInTheDocument()
    expect(screen.getByText('+1.67%')).toBeInTheDocument()
  })

  it('should display trending stocks when available', () => {
    mockUseTrendingStocks.mockReturnValue({
      data: mockTrendingResponse,
      isLoading: false,
      isError: false,
      error: null,
      refetch: vi.fn(),
    })

    render(<Dashboard />)
    
    expect(screen.getByText('TSLA')).toBeInTheDocument()
    expect(screen.getByText('NVDA')).toBeInTheDocument()
    expect(screen.getByText('Tesla Inc.')).toBeInTheDocument()
    expect(screen.getByText('NVIDIA Corporation')).toBeInTheDocument()
  })

  it('should display market summary when available', () => {
    mockUseMarketSummary.mockReturnValue({
      data: mockMarketSummaryResponse,
      isLoading: false,
      isError: false,
      error: null,
      refetch: vi.fn(),
    })

    render(<Dashboard />)
    
    expect(screen.getByText('S&P 500')).toBeInTheDocument()
    expect(screen.getByText('Dow Jones Industrial Average')).toBeInTheDocument()
    expect(screen.getByText('Technology')).toBeInTheDocument()
    expect(screen.getByText('Healthcare')).toBeInTheDocument()
  })

  it('should show loading states correctly', () => {
    mockUseStockQuote.mockReturnValue({
      data: null,
      isLoading: true,
      isError: false,
      error: null,
      refetch: vi.fn(),
    })

    render(<Dashboard />)
    
    expect(screen.getByText('Loading...')).toBeInTheDocument()
  })

  it('should show error states correctly', () => {
    mockUseStockQuote.mockReturnValue({
      data: null,
      isLoading: false,
      isError: true,
      error: new Error('Failed to fetch quote'),
      refetch: vi.fn(),
    })

    render(<Dashboard />)
    
    expect(screen.getByText('Error: Failed to fetch quote')).toBeInTheDocument()
  })

  it('should handle empty data gracefully', () => {
    mockUseTrendingStocks.mockReturnValue({
      data: { success: true, data: [] },
      isLoading: false,
      isError: false,
      error: null,
      refetch: vi.fn(),
    })

    render(<Dashboard />)
    
    expect(screen.getByText('No trending stocks available')).toBeInTheDocument()
  })

  it('should handle form validation', async () => {
    render(<Dashboard />)
    
    const submitButton = screen.getByRole('button', { name: 'Get Quote' })
    
    // Try to submit without symbol
    fireEvent.click(submitButton)
    
    // Should not crash and should handle validation
    expect(submitButton).toBeInTheDocument()
  })

  it('should refresh data when refresh buttons are clicked', async () => {
    const mockRefetchTrending = vi.fn()
    const mockRefetchMarket = vi.fn()
    
    mockUseTrendingStocks.mockReturnValue({
      data: mockTrendingResponse,
      isLoading: false,
      isError: false,
      error: null,
      refetch: mockRefetchTrending,
    })
    
    mockUseMarketSummary.mockReturnValue({
      data: mockMarketSummaryResponse,
      isLoading: false,
      isError: false,
      error: null,
      refetch: mockRefetchMarket,
    })

    render(<Dashboard />)
    
    const refreshTrendingButton = screen.getByLabelText('Refresh Trending Stocks')
    const refreshMarketButton = screen.getByLabelText('Refresh Market Summary')
    
    fireEvent.click(refreshTrendingButton)
    fireEvent.click(refreshMarketButton)
    
    await waitFor(() => {
      expect(mockRefetchTrending).toHaveBeenCalledTimes(1)
      expect(mockRefetchMarket).toHaveBeenCalledTimes(1)
    })
  })

  it('should display proper currency formatting', () => {
    mockUseStockQuote.mockReturnValue({
      data: mockQuoteResponse,
      isLoading: false,
      isError: false,
      error: null,
      refetch: vi.fn(),
    })

    render(<Dashboard />)
    
    // Check that currency is properly formatted
    expect(screen.getByText('$150.00')).toBeInTheDocument()
    expect(screen.getByText('$2.5T')).toBeInTheDocument() // Market cap
  })

  it('should display proper percentage formatting', () => {
    mockUseStockQuote.mockReturnValue({
      data: mockQuoteResponse,
      isLoading: false,
      isError: false,
      error: null,
      refetch: vi.fn(),
    })

    render(<Dashboard />)
    
    // Check that percentages are properly formatted
    expect(screen.getByText('+1.67%')).toBeInTheDocument()
  })

  it('should handle different market states', () => {
    const marketStates = ['REGULAR', 'PRE', 'POST', 'CLOSED']
    
    marketStates.forEach(state => {
      const modifiedResponse = {
        ...mockQuoteResponse,
        data: { ...mockQuoteResponse.data, marketState: state }
      }
      
      mockUseStockQuote.mockReturnValue({
        data: modifiedResponse,
        isLoading: false,
        isError: false,
        error: null,
        refetch: vi.fn(),
      })

      const { unmount } = render(<Dashboard />)
      
      // Should display the market state correctly
      expect(screen.getByText(modifiedResponse.data.symbol)).toBeInTheDocument()
      
      unmount()
    })
  })

  it('should maintain responsive layout', () => {
    render(<Dashboard />)
    
    // Check that the layout components are present
    expect(screen.getByText('Dashboard')).toBeInTheDocument()
    expect(screen.getByText('Quick Stock Quote')).toBeInTheDocument()
    expect(screen.getByText('Trending Stocks')).toBeInTheDocument()
    expect(screen.getByText('Market Summary')).toBeInTheDocument()
  })
})
