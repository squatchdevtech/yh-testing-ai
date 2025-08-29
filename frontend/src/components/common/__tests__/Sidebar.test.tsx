import { describe, it, expect, vi, beforeEach } from 'vitest'
import { render, screen, fireEvent } from '../../../../test/test-utils'
import { Sidebar } from '../Sidebar'

// Mock react-router-dom
vi.mock('react-router-dom', async () => {
  const actual = await vi.importActual('react-router-dom')
  return {
    ...actual,
    useLocation: vi.fn(),
    useNavigate: vi.fn(),
  }
})

describe('Sidebar', () => {
  const mockUseLocation = vi.mocked(require('react-router-dom')).useLocation
  const mockUseNavigate = vi.mocked(require('react-router-dom')).useNavigate

  beforeEach(() => {
    vi.clearAllMocks()
    
    // Default mock implementations
    mockUseLocation.mockReturnValue({
      pathname: '/',
      search: '',
      hash: '',
      state: null,
      key: 'default',
    })
    
    mockUseNavigate.mockReturnValue(vi.fn())
  })

  it('should render the sidebar with branding', () => {
    render(<Sidebar />)
    
    expect(screen.getByText('Finance Hub')).toBeInTheDocument()
    expect(screen.getByText('Yahoo Finance Hub')).toBeInTheDocument()
  })

  it('should render all navigation items', () => {
    render(<Sidebar />)
    
    expect(screen.getByText('Dashboard')).toBeInTheDocument()
    expect(screen.getByText('Stock Quotes')).toBeInTheDocument()
    expect(screen.getByText('Trending Stocks')).toBeInTheDocument()
    expect(screen.getByText('Market Summary')).toBeInTheDocument()
  })

  it('should highlight active route correctly', () => {
    mockUseLocation.mockReturnValue({
      pathname: '/stock-quotes',
      search: '',
      hash: '',
      state: null,
      key: 'default',
    })

    render(<Sidebar />)
    
    const stockQuotesButton = screen.getByText('Stock Quotes').closest('button')
    expect(stockQuotesButton).toHaveClass('Mui-selected')
  })

  it('should highlight dashboard as active when on root path', () => {
    mockUseLocation.mockReturnValue({
      pathname: '/',
      search: '',
      hash: '',
      state: null,
      key: 'default',
    })

    render(<Sidebar />)
    
    const dashboardButton = screen.getByText('Dashboard').closest('button')
    expect(dashboardButton).toHaveClass('Mui-selected')
  })

  it('should handle nested routes correctly', () => {
    mockUseLocation.mockReturnValue({
      pathname: '/trending-stocks',
      search: '',
      hash: '',
      state: null,
      key: 'default',
    })

    render(<Sidebar />)
    
    const trendingButton = screen.getByText('Trending Stocks').closest('button')
    expect(trendingButton).toHaveClass('Mui-selected')
  })

  it('should have proper navigation structure', () => {
    render(<Sidebar />)
    
    const navItems = screen.getAllByRole('button')
    expect(navItems).toHaveLength(4) // Dashboard, Stock Quotes, Trending Stocks, Market Summary
  })

  it('should have proper accessibility attributes', () => {
    render(<Sidebar />)
    
    const navItems = screen.getAllByRole('button')
    navItems.forEach(item => {
      expect(item).toHaveAttribute('aria-label')
    })
  })

  it('should handle different route patterns', () => {
    const routes = [
      { path: '/', expected: 'Dashboard' },
      { path: '/stock-quotes', expected: 'Stock Quotes' },
      { path: '/trending-stocks', expected: 'Trending Stocks' },
      { path: '/market-summary', expected: 'Market Summary' },
    ]

    routes.forEach(({ path, expected }) => {
      mockUseLocation.mockReturnValue({
        pathname: path,
        search: '',
        hash: '',
        state: null,
        key: 'default',
      })

      const { unmount } = render(<Sidebar />)
      
      const activeButton = screen.getByText(expected).closest('button')
      expect(activeButton).toHaveClass('Mui-selected')
      
      unmount()
    })
  })

  it('should maintain consistent styling across different states', () => {
    const routes = ['/', '/stock-quotes', '/trending-stocks', '/market-summary']
    
    routes.forEach(route => {
      mockUseLocation.mockReturnValue({
        pathname: route,
        search: '',
        hash: '',
        state: null,
        key: 'default',
      })

      const { unmount } = render(<Sidebar />)
      
      // All navigation items should be present
      expect(screen.getByText('Dashboard')).toBeInTheDocument()
      expect(screen.getByText('Stock Quotes')).toBeInTheDocument()
      expect(screen.getByText('Trending Stocks')).toBeInTheDocument()
      expect(screen.getByText('Market Summary')).toBeInTheDocument()
      
      unmount()
    })
  })

  it('should handle unknown routes gracefully', () => {
    mockUseLocation.mockReturnValue({
      pathname: '/unknown-route',
      search: '',
      hash: '',
      state: null,
      key: 'default',
    })

    render(<Sidebar />)
    
    // Should not crash and should show no active selection
    expect(screen.getByText('Dashboard')).toBeInTheDocument()
    expect(screen.getByText('Stock Quotes')).toBeInTheDocument()
    expect(screen.getByText('Trending Stocks')).toBeInTheDocument()
    expect(screen.getByText('Market Summary')).toBeInTheDocument()
  })

  it('should have proper Material-UI classes', () => {
    render(<Sidebar />)
    
    const sidebar = screen.getByRole('navigation')
    expect(sidebar).toHaveClass('MuiDrawer-root')
  })

  it('should render with proper icons', () => {
    render(<Sidebar />)
    
    // Check that icons are present (Material-UI icons)
    const dashboardIcon = screen.getByTestId('DashboardIcon')
    const stockIcon = screen.getByTestId('ShowChartIcon')
    const trendingIcon = screen.getByTestId('TrendingUpIcon')
    const marketIcon = screen.getByTestId('AssessmentIcon')
    
    expect(dashboardIcon).toBeInTheDocument()
    expect(stockIcon).toBeInTheDocument()
    expect(trendingIcon).toBeInTheDocument()
    expect(marketIcon).toBeInTheDocument()
  })
})
