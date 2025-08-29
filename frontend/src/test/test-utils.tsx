import React, { ReactElement } from 'react'
import { render, RenderOptions } from '@testing-library/react'
import { QueryClient, QueryClientProvider } from '@tanstack/react-query'
import { ThemeProvider } from '@mui/material/styles'
import { BrowserRouter } from 'react-router-dom'
import { createTheme } from '@mui/material/styles'
import { CssBaseline } from '@mui/material'

// Create a custom theme for testing
const testTheme = createTheme({
  palette: {
    mode: 'light',
    primary: {
      main: '#1976d2',
    },
    secondary: {
      main: '#dc004e',
    },
  },
})

// Create a custom render function that includes providers
const AllTheProviders = ({ children }: { children: React.ReactNode }) => {
  const queryClient = new QueryClient({
    defaultOptions: {
      queries: {
        retry: false,
        cacheTime: 0,
      },
      mutations: {
        retry: false,
      },
    },
  })

  return (
    <QueryClientProvider client={queryClient}>
      <ThemeProvider theme={testTheme}>
        <CssBaseline />
        <BrowserRouter>
          {children}
        </BrowserRouter>
      </ThemeProvider>
    </QueryClientProvider>
  )
}

const customRender = (
  ui: ReactElement,
  options?: Omit<RenderOptions, 'wrapper'>
) => render(ui, { wrapper: AllTheProviders, ...options })

// Re-export everything
export * from '@testing-library/react'

// Override render method
export { customRender as render }

// Mock data for testing
export const mockQuoteResponse = {
  success: true,
  data: {
    symbol: 'AAPL',
    regularMarketPrice: 150.0,
    regularMarketChange: 2.5,
    regularMarketChangePercent: 1.67,
    regularMarketTime: 1640995200,
    marketState: 'REGULAR',
    currency: 'USD',
    exchangeName: 'NMS',
    shortName: 'Apple Inc.',
    longName: 'Apple Inc.',
    marketCap: 2500000000000,
    volume: 50000000,
    averageVolume: 60000000,
    dayHigh: 152.0,
    dayLow: 148.0,
    yearHigh: 180.0,
    yearLow: 120.0,
    priceToBook: 15.5,
    priceToSales: 6.2,
    dividendRate: 0.88,
    dividendYield: 0.59,
    peRatio: 25.0,
    forwardPE: 23.0,
    beta: 1.2,
    fiftyTwoWeekChange: 25.0,
    fiftyTwoWeekChangePercent: 20.0,
  },
  message: 'Quote retrieved successfully',
  timestamp: '2024-01-01T00:00:00Z',
  requestId: 'test-request-id',
}

export const mockTrendingResponse = {
  success: true,
  data: [
    {
      symbol: 'TSLA',
      regularMarketPrice: 250.0,
      regularMarketChange: 15.0,
      regularMarketChangePercent: 6.38,
      regularMarketTime: 1640995200,
      marketState: 'REGULAR',
      currency: 'USD',
      exchangeName: 'NMS',
      shortName: 'Tesla Inc.',
      longName: 'Tesla Inc.',
      marketCap: 800000000000,
      volume: 30000000,
      averageVolume: 35000000,
      dayHigh: 255.0,
      dayLow: 240.0,
      yearHigh: 300.0,
      yearLow: 150.0,
      priceToBook: 20.0,
      priceToSales: 8.0,
      dividendRate: 0.0,
      dividendYield: 0.0,
      peRatio: 80.0,
      forwardPE: 60.0,
      beta: 2.0,
      fiftyTwoWeekChange: 100.0,
      fiftyTwoWeekChangePercent: 66.67,
    },
    {
      symbol: 'NVDA',
      regularMarketPrice: 500.0,
      regularMarketChange: 25.0,
      regularMarketChangePercent: 5.26,
      regularMarketTime: 1640995200,
      marketState: 'REGULAR',
      currency: 'USD',
      exchangeName: 'NMS',
      shortName: 'NVIDIA Corporation',
      longName: 'NVIDIA Corporation',
      marketCap: 1200000000000,
      volume: 20000000,
      averageVolume: 25000000,
      dayHigh: 510.0,
      dayLow: 490.0,
      yearHigh: 600.0,
      yearLow: 300.0,
      priceToBook: 25.0,
      priceToSales: 15.0,
      dividendRate: 0.16,
      dividendYield: 0.03,
      peRatio: 60.0,
      forwardPE: 45.0,
      beta: 1.8,
      fiftyTwoWeekChange: 200.0,
      fiftyTwoWeekChangePercent: 66.67,
    },
  ],
  message: 'Trending stocks retrieved successfully',
  timestamp: '2024-01-01T00:00:00Z',
  requestId: 'test-request-id',
}

export const mockMarketSummaryResponse = {
  success: true,
  data: {
    marketIndices: [
      {
        symbol: '^GSPC',
        name: 'S&P 500',
        price: 4500.0,
        change: 45.0,
        changePercent: 1.01,
        marketState: 'REGULAR',
        currency: 'USD',
        exchangeName: 'INDEX',
        volume: 0,
        averageVolume: 0,
        dayHigh: 4510.0,
        dayLow: 4480.0,
        yearHigh: 4800.0,
        yearLow: 4000.0,
      },
      {
        symbol: '^DJI',
        name: 'Dow Jones Industrial Average',
        price: 35000.0,
        change: 300.0,
        changePercent: 0.86,
        marketState: 'REGULAR',
        currency: 'USD',
        exchangeName: 'INDEX',
        volume: 0,
        averageVolume: 0,
        dayHigh: 35100.0,
        dayLow: 34900.0,
        yearHigh: 37000.0,
        yearLow: 32000.0,
      },
    ],
    marketSectors: [
      {
        name: 'Technology',
        performance: 2.5,
        change: 1.2,
        volume: 1000000000,
        marketCap: 5000000000000,
      },
      {
        name: 'Healthcare',
        performance: -0.8,
        change: -0.3,
        volume: 500000000,
        marketCap: 3000000000000,
      },
    ],
  },
  message: 'Market summary retrieved successfully',
  timestamp: '2024-01-01T00:00:00Z',
  requestId: 'test-request-id',
}

export const mockHealthResponse = {
  success: true,
  data: {
    status: 'Healthy',
    timestamp: '2024-01-01T00:00:00Z',
    version: '1.0.0',
    uptime: 3600,
    memoryUsage: {
      used: 100000000,
      total: 2000000000,
      percentage: 5.0,
    },
    databaseStatus: 'Connected',
    externalApiStatus: 'Operational',
  },
  message: 'Health check completed successfully',
  timestamp: '2024-01-01T00:00:00Z',
  requestId: 'test-request-id',
}

// Mock API service
export const mockApiService = {
  getQuote: vi.fn(),
  getTrending: vi.fn(),
  getMarketSummary: vi.fn(),
  getCurrencyExchange: vi.fn(),
  getCrypto: vi.fn(),
  getBulkQuotes: vi.fn(),
  getHealth: vi.fn(),
  getCapabilities: vi.fn(),
  getApiHealth: vi.fn(),
}

// Mock React Query hooks
export const mockUseQuery = vi.fn()
export const mockUseMutation = vi.fn()

// Mock router
export const mockNavigate = vi.fn()
export const mockUseLocation = vi.fn()
export const mockUseParams = vi.fn()
export const mockUseSearchParams = vi.fn()

// Mock window methods
export const mockScrollTo = vi.fn()
export const mockResizeObserver = vi.fn()
