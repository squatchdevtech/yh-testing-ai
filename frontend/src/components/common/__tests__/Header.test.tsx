import { describe, it, expect, vi, beforeEach } from 'vitest'
import { render, screen, fireEvent } from '../../../test/test-utils'
import Header from '../Header'

// Mock the useApiHealth hook
const mockUseApiHealth = vi.fn()
vi.mock('../../../hooks/useApiHealth', () => ({
  useApiHealth: mockUseApiHealth,
}))

describe('Header', () => {
  beforeEach(() => {
    vi.clearAllMocks()
  })

  it('should render the header with title', () => {
    mockUseApiHealth.mockReturnValue({
      data: null,
      isLoading: false,
      isError: false,
      error: null,
      refetch: vi.fn(),
    })

    render(<Header />)
    
    expect(screen.getByText('Yahoo Finance Dashboard')).toBeInTheDocument()
  })

  it('should display healthy status when API is healthy', () => {
    mockUseApiHealth.mockReturnValue({
      data: { status: 'Healthy', apiKeyConfigured: true },
      isLoading: false,
      isError: false,
      error: null,
      refetch: vi.fn(),
    })

    render(<Header />)
    
    expect(screen.getByText('Healthy')).toBeInTheDocument()
    expect(screen.getByText('Healthy')).toHaveClass('MuiChip-colorSuccess')
  })

  it('should display unhealthy status when API is unhealthy', () => {
    mockUseApiHealth.mockReturnValue({
      data: { status: 'Unhealthy', apiKeyConfigured: true },
      isLoading: false,
      isError: false,
      error: null,
      refetch: vi.fn(),
    })

    render(<Header />)
    
    expect(screen.getByText('Unhealthy')).toBeInTheDocument()
    expect(screen.getByText('Unhealthy')).toHaveClass('MuiChip-colorWarning')
  })

  it('should display loading status when checking API health', () => {
    mockUseApiHealth.mockReturnValue({
      data: null,
      isLoading: true,
      isError: false,
      error: null,
      refetch: vi.fn(),
    })

    render(<Header />)
    
    expect(screen.getByText('Checking...')).toBeInTheDocument()
    expect(screen.getByText('Checking...')).toHaveClass('MuiChip-colorDefault')
  })

  it('should display error status when API health check fails', () => {
    mockUseApiHealth.mockReturnValue({
      data: null,
      isLoading: false,
      isError: false,
      error: new Error('Network error'),
      refetch: vi.fn(),
    })

    render(<Header />)
    
    expect(screen.getByText('Error')).toBeInTheDocument()
    expect(screen.getByText('Error')).toHaveClass('MuiChip-colorError')
  })

  it('should call refetch when refresh button is clicked', () => {
    const mockRefetch = vi.fn()
    mockUseApiHealth.mockReturnValue({
      data: { status: 'Healthy', apiKeyConfigured: true },
      isLoading: false,
      isError: false,
      error: null,
      refetch: mockRefetch,
    })

    render(<Header />)
    
    const refreshButton = screen.getByLabelText('Refresh API Status')
    fireEvent.click(refreshButton)
    
    expect(mockRefetch).toHaveBeenCalledTimes(1)
  })

  it('should handle unknown status gracefully', () => {
    mockUseApiHealth.mockReturnValue({
      data: { status: 'Unknown', apiKeyConfigured: true },
      isLoading: false,
      isError: false,
      error: null,
      refetch: vi.fn(),
    })

    render(<Header />)
    
    expect(screen.getByText('Unknown')).toBeInTheDocument()
    expect(screen.getByText('Unknown')).toHaveClass('MuiChip-colorWarning')
  })

  it('should handle null data gracefully', () => {
    mockUseApiHealth.mockReturnValue({
      data: null,
      isLoading: false,
      isError: false,
      error: null,
      refetch: vi.fn(),
    })

    render(<Header />)
    
    expect(screen.getByText('Unknown')).toBeInTheDocument()
    expect(screen.getByText('Unknown')).toHaveClass('MuiChip-colorWarning')
  })

  it('should have proper accessibility attributes', () => {
    mockUseApiHealth.mockReturnValue({
      data: { status: 'Healthy', apiKeyConfigured: true },
      isLoading: false,
      isError: false,
      error: null,
      refetch: vi.fn(),
    })

    render(<Header />)
    
    const refreshButton = screen.getByLabelText('Refresh API Status')
    expect(refreshButton).toBeInTheDocument()
    expect(refreshButton).toHaveAttribute('aria-label', 'Refresh API Status')
  })

  it('should maintain consistent layout with different statuses', () => {
    const statuses = ['Healthy', 'Unhealthy', 'Checking...', 'Error', 'Unknown']
    
    statuses.forEach(status => {
      mockUseApiHealth.mockReturnValue({
        data: status === 'Checking...' ? null : { status, apiKeyConfigured: true },
        isLoading: status === 'Checking...',
        isError: false,
        error: status === 'Error' ? new Error('Test error') : null,
        refetch: vi.fn(),
      })

      const { unmount } = render(<Header />)
      
      expect(screen.getByText('Yahoo Finance Dashboard')).toBeInTheDocument()
      expect(screen.getByText(status)).toBeInTheDocument()
      
      unmount()
    })
  })
})
