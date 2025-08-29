import { format, formatDistanceToNow } from 'date-fns';

// Number formatting
export const formatCurrency = (value: number | undefined, currency: string = 'USD'): string => {
  if (value === undefined || value === null) return 'N/A';
  
  return new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: currency,
    minimumFractionDigits: 2,
    maximumFractionDigits: 2,
  }).format(value);
};

export const formatNumber = (value: number | undefined, decimals: number = 2): string => {
  if (value === undefined || value === null) return 'N/A';
  
  return new Intl.NumberFormat('en-US', {
    minimumFractionDigits: decimals,
    maximumFractionDigits: decimals,
  }).format(value);
};

export const formatLargeNumber = (value: number | undefined): string => {
  if (value === undefined || value === null) return 'N/A';
  
  if (value >= 1e12) {
    return `${(value / 1e12).toFixed(2)}T`;
  } else if (value >= 1e9) {
    return `${(value / 1e9).toFixed(2)}B`;
  } else if (value >= 1e6) {
    return `${(value / 1e6).toFixed(2)}M`;
  } else if (value >= 1e3) {
    return `${(value / 1e3).toFixed(2)}K`;
  }
  
  return value.toFixed(2);
};

export const formatPercentage = (value: number | undefined, decimals: number = 2): string => {
  if (value === undefined || value === null) return 'N/A';
  
  return `${value >= 0 ? '+' : ''}${value.toFixed(decimals)}%`;
};

// Date formatting
export const formatDate = (date: string | Date | undefined): string => {
  if (!date) return 'N/A';
  
  const dateObj = typeof date === 'string' ? new Date(date) : date;
  return format(dateObj, 'MMM dd, yyyy HH:mm:ss');
};

export const formatRelativeTime = (date: string | Date | undefined): string => {
  if (!date) return 'N/A';
  
  const dateObj = typeof date === 'string' ? new Date(date) : date;
  return formatDistanceToNow(dateObj, { addSuffix: true });
};

// Market state formatting
export const formatMarketState = (state: string | undefined): string => {
  if (!state) return 'Unknown';
  
  const stateMap: Record<string, string> = {
    'REGULAR': 'Open',
    'PRE': 'Pre-Market',
    'POST': 'After Hours',
    'CLOSED': 'Closed',
    'HOLIDAY': 'Holiday',
  };
  
  return stateMap[state] || state;
};

// Symbol formatting
export const formatSymbol = (symbol: string): string => {
  return symbol.toUpperCase().trim();
};

// Validation helpers
export const isValidSymbol = (symbol: string): boolean => {
  return symbol.trim().length > 0 && symbol.trim().length <= 10;
};

export const isValidRegion = (region: string): boolean => {
  return region.trim().length === 2;
};

export const isValidCurrency = (currency: string): boolean => {
  return currency.trim().length === 3;
};
