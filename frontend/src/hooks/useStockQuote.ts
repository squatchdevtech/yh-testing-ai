import { useQuery } from '@tanstack/react-query';
import { apiService } from '@/services/api';
import { QuoteRequest } from '@/types/api';

export const useStockQuote = (request: QuoteRequest) => {
  return useQuery({
    queryKey: ['stockQuote', request.symbols, request.region, request.language],
    queryFn: () => apiService.getQuote(request),
    enabled: !!request.symbols && request.symbols.trim().length > 0,
    staleTime: 30000, // 30 seconds
    refetchInterval: 60000, // 1 minute
    retry: 3,
    retryDelay: (attemptIndex) => Math.min(1000 * 2 ** attemptIndex, 30000),
  });
};

export const useStockQuotePost = (request: QuoteRequest) => {
  return useQuery({
    queryKey: ['stockQuotePost', request.symbols, request.region, request.language],
    queryFn: () => apiService.getQuotePost(request),
    enabled: !!request.symbols && request.symbols.trim().length > 0,
    staleTime: 30000,
    refetchInterval: 60000,
    retry: 3,
    retryDelay: (attemptIndex) => Math.min(1000 * 2 ** attemptIndex, 30000),
  });
};
