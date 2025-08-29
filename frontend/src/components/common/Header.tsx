import React from 'react';
import {
  AppBar,
  Toolbar,
  Typography,
  Box,
  Chip,
  IconButton,
  Tooltip,
} from '@mui/material';
import {
  TrendingUp as TrendingUpIcon,
  Refresh as RefreshIcon,
} from '@mui/icons-material';
import { useApiHealth } from '@/hooks/useApiHealth';

const Header: React.FC = () => {
  const { data: healthData, isLoading, error, refetch } = useApiHealth();

  const getStatusColor = () => {
    if (isLoading) return 'default';
    if (error || !healthData?.apiKeyConfigured) return 'error';
    if (healthData?.status === 'Healthy') return 'success';
    return 'warning';
  };

  const getStatusText = () => {
    if (isLoading) return 'Checking...';
    if (error) return 'Error';
    if (!healthData?.apiKeyConfigured) return 'No API Key';
    return healthData?.status || 'Unknown';
  };

  return (
    <AppBar position="fixed" sx={{ zIndex: (theme) => theme.zIndex.drawer + 1 }}>
      <Toolbar>
        <TrendingUpIcon sx={{ mr: 2 }} />
        <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
          Yahoo Finance Dashboard
        </Typography>
        
        <Box sx={{ display: 'flex', alignItems: 'center', gap: 2 }}>
          <Chip
            label={getStatusText()}
            color={getStatusColor()}
            size="small"
            variant="outlined"
          />
          
          <Tooltip title="Refresh API Status">
            <IconButton
              color="inherit"
              onClick={() => refetch()}
              disabled={isLoading}
              size="small"
            >
              <RefreshIcon />
            </IconButton>
          </Tooltip>
        </Box>
      </Toolbar>
    </AppBar>
  );
};

export default Header;
