import React, { useState } from 'react';
import {
  Box,
  Card,
  CardContent,
  Typography,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  Grid,
  Chip,
  Skeleton,
  Alert,
  Button,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  LinearProgress,
} from '@mui/material';
import { Refresh as RefreshIcon, TrendingUp as TrendingUpIcon } from '@mui/icons-material';
import { useMarketSummary } from '@/hooks/useMarketSummary';
import { formatCurrency, formatPercentage, formatDate } from '@/utils/formatters';

const MarketSummaryPage: React.FC = () => {
  const [selectedRegion, setSelectedRegion] = useState<string>('US');
  const [selectedLanguage, setSelectedLanguage] = useState<string>('en');

  const { data: marketData, isLoading, error, refetch } = useMarketSummary(selectedRegion, selectedLanguage);

  const regions = [
    { code: 'US', name: 'United States' },
    { code: 'CA', name: 'Canada' },
    { code: 'GB', name: 'United Kingdom' },
    { code: 'DE', name: 'Germany' },
    { code: 'FR', name: 'France' },
    { code: 'JP', name: 'Japan' },
    { code: 'AU', name: 'Australia' },
  ];

  const languages = [
    { code: 'en', name: 'English' },
    { code: 'es', name: 'Spanish' },
    { code: 'fr', name: 'French' },
    { code: 'de', name: 'German' },
    { code: 'it', name: 'Italian' },
    { code: 'pt', name: 'Portuguese' },
  ];

  const handleRegionChange = (event: any) => {
    setSelectedRegion(event.target.value);
  };

  const handleLanguageChange = (event: any) => {
    setSelectedLanguage(event.target.value);
  };

  const getPerformanceColor = (performance: number) => {
    if (performance >= 0) return 'success';
    if (performance >= -5) return 'warning';
    return 'error';
  };

  return (
    <Box>
      <Typography variant="h4" gutterBottom>
        Market Summary
      </Typography>

      {/* Controls */}
      <Card sx={{ mb: 3 }}>
        <CardContent>
          <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
            <Typography variant="h6">
              Market Overview
            </Typography>
            <Button
              startIcon={<RefreshIcon />}
              onClick={() => refetch()}
              variant="outlined"
              size="small"
            >
              Refresh
            </Button>
          </Box>
          <Grid container spacing={2} sx={{ mt: 2 }}>
            <Grid item xs={12} sm={6} md={3}>
              <FormControl fullWidth>
                <InputLabel>Region</InputLabel>
                <Select
                  value={selectedRegion}
                  label="Region"
                  onChange={handleRegionChange}
                >
                  {regions.map((region) => (
                    <MenuItem key={region.code} value={region.code}>
                      {region.name}
                    </MenuItem>
                  ))}
                </Select>
              </FormControl>
            </Grid>
            <Grid item xs={12} sm={6} md={3}>
              <FormControl fullWidth>
                <InputLabel>Language</InputLabel>
                <Select
                  value={selectedLanguage}
                  label="Language"
                  onChange={handleLanguageChange}
                >
                  {languages.map((lang) => (
                    <MenuItem key={lang.code} value={lang.code}>
                      {lang.name}
                    </MenuItem>
                  ))}
                </Select>
              </FormControl>
            </Grid>
          </Grid>
        </CardContent>
      </Card>

      {/* Loading State */}
      {isLoading && (
        <Card>
          <CardContent>
            <Typography variant="h6" gutterBottom>
              Loading Market Data...
            </Typography>
            <Box sx={{ width: '100%' }}>
              <LinearProgress />
            </Box>
            <Grid container spacing={2} sx={{ mt: 2 }}>
              {[...Array(6)].map((_, index) => (
                <Grid item xs={12} sm={6} md={4} key={index}>
                  <Skeleton variant="rectangular" height={120} />
                </Grid>
              ))}
            </Grid>
          </CardContent>
        </Card>
      )}

      {/* Error State */}
      {error && (
        <Alert severity="error" sx={{ mb: 2 }}>
          Error loading market summary: {error.message}
        </Alert>
      )}

      {/* Market Indices */}
      {marketData && marketData.marketIndices.length > 0 && (
        <Card sx={{ mb: 3 }}>
          <CardContent>
            <Typography variant="h6" gutterBottom>
              Market Indices - {regions.find(r => r.code === selectedRegion)?.name}
            </Typography>
            
            <Grid container spacing={2}>
              {marketData.marketIndices.map((index) => (
                <Grid item xs={12} sm={6} md={4} key={index.symbol}>
                  <Card variant="outlined">
                    <CardContent>
                      <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 1 }}>
                        <Typography variant="h6">
                          {index.symbol}
                        </Typography>
                        <Chip
                          label={index.marketState}
                          size="small"
                          variant="outlined"
                          color={index.marketState === 'REGULAR' ? 'success' : 'default'}
                        />
                      </Box>
                      <Typography variant="body2" color="text.secondary" gutterBottom>
                        {index.name}
                      </Typography>
                      <Typography variant="h5" gutterBottom>
                        {formatCurrency(index.price, index.currency)}
                      </Typography>
                      <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
                        <Chip
                          label={formatPercentage(index.changePercent)}
                          color={index.changePercent >= 0 ? 'success' : 'error'}
                          size="small"
                        />
                        <Typography variant="body2">
                          {formatCurrency(index.change, index.currency)}
                        </Typography>
                      </Box>
                    </CardContent>
                  </Card>
                </Grid>
              ))}
            </Grid>
          </CardContent>
        </Card>
      )}

      {/* Market Sectors */}
      {marketData && marketData.marketSectors.length > 0 && (
        <Card sx={{ mb: 3 }}>
          <CardContent>
            <Typography variant="h6" gutterBottom>
              Market Sectors Performance
            </Typography>
            
            <Grid container spacing={2}>
              {marketData.marketSectors.map((sector, index) => (
                <Grid item xs={12} sm={6} md={4} key={index}>
                  <Card variant="outlined">
                    <CardContent>
                      <Typography variant="h6" gutterBottom>
                        {sector.name}
                      </Typography>
                      <Box sx={{ display: 'flex', alignItems: 'center', gap: 1, mb: 2 }}>
                        <Chip
                          label={formatPercentage(sector.performance)}
                          color={getPerformanceColor(sector.performance)}
                          size="small"
                        />
                        <TrendingUpIcon 
                          color={sector.performance >= 0 ? 'success' : 'error'}
                        />
                      </Box>
                      <Typography variant="body2" color="text.secondary" gutterBottom>
                        Top Stocks:
                      </Typography>
                      <Box sx={{ display: 'flex', flexWrap: 'wrap', gap: 0.5 }}>
                        {sector.topStocks.slice(0, 5).map((stock, stockIndex) => (
                          <Chip
                            key={stockIndex}
                            label={stock}
                            size="small"
                            variant="outlined"
                          />
                        ))}
                      </Box>
                    </CardContent>
                  </Card>
                </Grid>
              ))}
            </Grid>
          </CardContent>
        </Card>
      )}

      {/* Detailed Table View */}
      {marketData && marketData.marketIndices.length > 0 && (
        <Card>
          <CardContent>
            <Typography variant="h6" gutterBottom>
              Detailed Market Data
            </Typography>
            
            <TableContainer component={Paper} variant="outlined">
              <Table>
                <TableHead>
                  <TableRow>
                    <TableCell>Index</TableCell>
                    <TableCell>Name</TableCell>
                    <TableCell>Price</TableCell>
                    <TableCell>Change</TableCell>
                    <TableCell>Change %</TableCell>
                    <TableCell>Currency</TableCell>
                    <TableCell>Market State</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {marketData.marketIndices.map((index) => (
                    <TableRow key={index.symbol}>
                      <TableCell>
                        <Typography variant="body2" fontWeight="bold">
                          {index.symbol}
                        </Typography>
                      </TableCell>
                      <TableCell>
                        <Typography variant="body2">
                          {index.name}
                        </Typography>
                      </TableCell>
                      <TableCell>
                        <Typography variant="body2">
                          {formatCurrency(index.price, index.currency)}
                        </Typography>
                      </TableCell>
                      <TableCell>
                        <Typography variant="body2">
                          {formatCurrency(index.change, index.currency)}
                        </Typography>
                      </TableCell>
                      <TableCell>
                        <Chip
                          label={formatPercentage(index.changePercent)}
                          color={index.changePercent >= 0 ? 'success' : 'error'}
                          size="small"
                        />
                      </TableCell>
                      <TableCell>
                        <Typography variant="body2">
                          {index.currency}
                        </Typography>
                      </TableCell>
                      <TableCell>
                        <Chip
                          label={index.marketState}
                          size="small"
                          variant="outlined"
                          color={index.marketState === 'REGULAR' ? 'success' : 'default'}
                        />
                      </TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            </TableContainer>
          </CardContent>
        </Card>
      )}

      {/* No Results */}
      {marketData && marketData.marketIndices.length === 0 && (
        <Card>
          <CardContent>
            <Typography variant="h6" gutterBottom>
              No Market Data
            </Typography>
            <Typography color="text.secondary">
              No market data found for the selected region. Try selecting a different region.
            </Typography>
          </CardContent>
        </Card>
      )}

      {/* Last Updated Info */}
      {marketData && (
        <Box sx={{ mt: 2, textAlign: 'center' }}>
          <Typography variant="caption" color="text.secondary">
            Last updated: {formatDate(marketData.timestamp)} • 
            Region: {marketData.region} • 
            Language: {marketData.language}
          </Typography>
        </Box>
      )}
    </Box>
  );
};

export default MarketSummaryPage;
