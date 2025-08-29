import { useQuery } from '@tanstack/react-query';
import { apiService } from '@/services/api';

export const useApiHealth = () => {
  return useQuery({
    queryKey: ['apiHealth'],
    queryFn: () => apiService.getHealth(),
    staleTime: 60000, // 1 minute
    refetchInterval: 300000, // 5 minutes
    retry: 3,
    retryDelay: (attemptIndex) => Math.min(1000 * 2 ** attemptIndex, 30000),
  });
};

export const useMainApiHealth = () => {
  return useQuery({
    queryKey: ['mainApiHealth'],
    queryFn: () => apiService.getApiHealth(),
    staleTime: 60000, // 1 minute
    refetchInterval: 300000, // 5 minutes
    retry: 3,
    retryDelay: (attemptIndex) => Math.min(1000 * 2 ** attemptIndex, 30000),
  });
};
