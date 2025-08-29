import { useQuery } from '@tanstack/react-query';
import { apiService } from '@/services/api';

export const useMarketSummary = (region: string, language: string = 'en') => {
  return useQuery({
    queryKey: ['marketSummary', region, language],
    queryFn: () => apiService.getMarketSummary(region, language),
    enabled: !!region && region.trim().length > 0,
    staleTime: 600000, // 10 minutes
    refetchInterval: 600000, // 10 minutes
    retry: 3,
    retryDelay: (attemptIndex) => Math.min(1000 * 2 ** attemptIndex, 30000),
  });
};
