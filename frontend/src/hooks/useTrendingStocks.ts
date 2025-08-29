import { useQuery } from '@tanstack/react-query';
import { apiService } from '@/services/api';

export const useTrendingStocks = (region: string) => {
  return useQuery({
    queryKey: ['trendingStocks', region],
    queryFn: () => apiService.getTrending(region),
    enabled: !!region && region.trim().length > 0,
    staleTime: 300000, // 5 minutes
    refetchInterval: 300000, // 5 minutes
    retry: 3,
    retryDelay: (attemptIndex) => Math.min(1000 * 2 ** attemptIndex, 30000),
  });
};
