import { describe, it, expect } from 'vitest'
import {
  formatCurrency,
  formatNumber,
  formatLargeNumber,
  formatPercentage,
  formatDate,
  formatRelativeTime,
  formatMarketState,
  formatSymbol,
  isValidSymbol,
  isValidRegion,
  isValidCurrency,
} from '../formatters'

describe('formatters', () => {
  describe('formatCurrency', () => {
    it('should format positive numbers correctly', () => {
      expect(formatCurrency(1234.56, 'USD')).toBe('$1,234.56')
      expect(formatCurrency(1000000, 'USD')).toBe('$1,000,000.00')
    })

    it('should format negative numbers correctly', () => {
      expect(formatCurrency(-1234.56, 'USD')).toBe('-$1,234.56')
      expect(formatCurrency(-1000000, 'USD')).toBe('-$1,000,000.00')
    })

    it('should handle zero correctly', () => {
      expect(formatCurrency(0, 'USD')).toBe('$0.00')
    })

    it('should handle different currencies', () => {
      expect(formatCurrency(1234.56, 'EUR')).toBe('€1,234.56')
      expect(formatCurrency(1234.56, 'GBP')).toBe('£1,234.56')
    })

    it('should handle undefined currency', () => {
      expect(formatCurrency(1234.56)).toBe('$1,234.56')
    })
  })

  describe('formatNumber', () => {
    it('should format numbers with default options', () => {
      expect(formatNumber(1234.5678)).toBe('1,234.57')
      expect(formatNumber(1000000)).toBe('1,000,000.00')
    })

    it('should format numbers with custom decimal places', () => {
      expect(formatNumber(1234.5678, 3)).toBe('1,234.568')
      expect(formatNumber(1234.5678, 0)).toBe('1,235')
    })

    it('should handle negative numbers', () => {
      expect(formatNumber(-1234.5678)).toBe('-1,234.57')
    })

    it('should handle zero', () => {
      expect(formatNumber(0)).toBe('0.00')
    })
  })

  describe('formatLargeNumber', () => {
    it('should format thousands', () => {
      expect(formatLargeNumber(1500)).toBe('1.50K')
      expect(formatLargeNumber(25000)).toBe('25.00K')
    })

    it('should format millions', () => {
      expect(formatLargeNumber(1500000)).toBe('1.50M')
      expect(formatLargeNumber(25000000)).toBe('25.00M')
    })

    it('should format billions', () => {
      expect(formatLargeNumber(1500000000)).toBe('1.50B')
      expect(formatLargeNumber(25000000000)).toBe('25.00B')
    })

    it('should format trillions', () => {
      expect(formatLargeNumber(1500000000000)).toBe('1.50T')
      expect(formatLargeNumber(25000000000000)).toBe('25.00T')
    })

    it('should handle numbers less than 1000', () => {
      expect(formatLargeNumber(500)).toBe('500.00')
      expect(formatLargeNumber(999)).toBe('999.00')
    })

    it('should handle zero', () => {
      expect(formatLargeNumber(0)).toBe('0.00')
    })
  })

  describe('formatPercentage', () => {
    it('should format positive percentages', () => {
      expect(formatPercentage(5.25)).toBe('+5.25%')
      expect(formatPercentage(100)).toBe('+100.00%')
    })

    it('should format negative percentages', () => {
      expect(formatPercentage(-5.25)).toBe('-5.25%')
      expect(formatPercentage(-100)).toBe('-100.00%')
    })

    it('should handle zero', () => {
      expect(formatPercentage(0)).toBe('+0.00%')
    })

    it('should handle custom decimal places', () => {
      expect(formatPercentage(5.256, 1)).toBe('+5.3%')
      expect(formatPercentage(-5.256, 1)).toBe('-5.3%')
    })
  })

  describe('formatDate', () => {
    it('should format dates correctly', () => {
      const date = new Date('2024-01-15T10:30:00Z')
      expect(formatDate(date)).toBe('Jan 15, 2024 04:30:00')
    })

    it('should handle different date formats', () => {
      const date = new Date('2024-12-25T15:45:00Z')
      expect(formatDate(date)).toBe('Dec 25, 2024 09:45:00')
    })

    it('should handle invalid dates', () => {
      expect(() => formatDate(new Date('invalid'))).toThrow()
    })
  })

  describe('formatRelativeTime', () => {
    it('should format recent times', () => {
      const now = new Date()
      const oneMinuteAgo = new Date(now.getTime() - 60000)
      expect(formatRelativeTime(oneMinuteAgo)).toBe('1 minute ago')
    })

    it('should format older times', () => {
      const now = new Date()
      const oneHourAgo = new Date(now.getTime() - 3600000)
      expect(formatRelativeTime(oneHourAgo)).toContain('hour ago')
    })

    it('should handle very old dates', () => {
      const oldDate = new Date('2020-01-01T00:00:00Z')
      expect(formatRelativeTime(oldDate)).toContain('ago')
    })
  })

  describe('formatMarketState', () => {
    it('should format regular market state', () => {
      expect(formatMarketState('REGULAR')).toBe('Open')
      expect(formatMarketState('regular')).toBe('regular')
    })

    it('should format pre market state', () => {
      expect(formatMarketState('PRE')).toBe('Pre-Market')
      expect(formatMarketState('pre')).toBe('pre')
    })

    it('should format post market state', () => {
      expect(formatMarketState('POST')).toBe('After Hours')
      expect(formatMarketState('post')).toBe('post')
    })

    it('should format closed market state', () => {
      expect(formatMarketState('CLOSED')).toBe('Closed')
      expect(formatMarketState('closed')).toBe('closed')
    })

    it('should handle unknown states', () => {
      expect(formatMarketState('UNKNOWN')).toBe('UNKNOWN')
      expect(formatMarketState('')).toBe('Unknown')
    })
  })

  describe('formatSymbol', () => {
    it('should format symbols correctly', () => {
      expect(formatSymbol('AAPL')).toBe('AAPL')
      expect(formatSymbol('^GSPC')).toBe('^GSPC')
    })

    it('should handle empty symbols', () => {
      expect(formatSymbol('')).toBe('')
      expect(formatSymbol('   ')).toBe('')
    })

    it('should trim whitespace', () => {
      expect(formatSymbol('  AAPL  ')).toBe('AAPL')
    })
  })

  describe('isValidSymbol', () => {
    it('should validate valid symbols', () => {
      expect(isValidSymbol('AAPL')).toBe(true)
      expect(isValidSymbol('TSLA')).toBe(true)
      expect(isValidSymbol('^GSPC')).toBe(true)
      expect(isValidSymbol('GOOGL')).toBe(true)
    })

    it('should reject invalid symbols', () => {
      expect(isValidSymbol('')).toBe(false)
      expect(isValidSymbol('   ')).toBe(false)
      expect(isValidSymbol('123')).toBe(true) // Numbers are valid
      expect(isValidSymbol('A')).toBe(true) // Single character is valid
      expect(isValidSymbol('A'.repeat(11))).toBe(false)
    })

    it('should handle edge cases', () => {
      expect(isValidSymbol('A'.repeat(10))).toBe(true)
      expect(isValidSymbol('A'.repeat(11))).toBe(false)
    })
  })

  describe('isValidRegion', () => {
    it('should validate valid regions', () => {
      expect(isValidRegion('US')).toBe(true)
      expect(isValidRegion('GB')).toBe(true)
      expect(isValidRegion('CA')).toBe(true)
      expect(isValidRegion('AU')).toBe(true)
    })

    it('should reject invalid regions', () => {
      expect(isValidRegion('')).toBe(false)
      expect(isValidRegion('   ')).toBe(false)
      expect(isValidRegion('XX')).toBe(true) // Any 2-character string is valid
      expect(isValidRegion('123')).toBe(false)
    })

    it('should handle case sensitivity', () => {
      expect(isValidRegion('us')).toBe(true) // 2 characters
      expect(isValidRegion('Us')).toBe(true) // 2 characters
    })
  })

  describe('isValidCurrency', () => {
    it('should validate valid currencies', () => {
      expect(isValidCurrency('USD')).toBe(true)
      expect(isValidCurrency('EUR')).toBe(true)
      expect(isValidCurrency('GBP')).toBe(true)
      expect(isValidCurrency('JPY')).toBe(true)
    })

    it('should reject invalid currencies', () => {
      expect(isValidCurrency('')).toBe(false)
      expect(isValidCurrency('   ')).toBe(false)
      expect(isValidCurrency('XXX')).toBe(true) // Any 3-character string is valid
      expect(isValidCurrency('123')).toBe(true) // Any 3-character string is valid
    })

    it('should handle case sensitivity', () => {
      expect(isValidCurrency('usd')).toBe(true) // 3 characters
      expect(isValidCurrency('Usd')).toBe(true) // 3 characters
    })
  })
})
