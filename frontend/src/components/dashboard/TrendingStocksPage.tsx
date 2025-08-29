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
} from '@mui/material';
import { Refresh as RefreshIcon } from '@mui/icons-material';
import { useTrendingStocks } from '@/hooks/useTrendingStocks';
import { formatCurrency, formatPercentage, formatDate } from '@/utils/formatters';

const TrendingStocksPage: React.FC = () => {
  const [selectedRegion, setSelectedRegion] = useState<string>('US');

  const { data: trendingData, isLoading, error, refetch } = useTrendingStocks(selectedRegion);

  const regions = [
    { code: 'US', name: 'United States' },
    { code: 'CA', name: 'Canada' },
    { code: 'GB', name: 'United Kingdom' },
    { code: 'DE', name: 'Germany' },
    { code: 'FR', name: 'France' },
    { code: 'JP', name: 'Japan' },
    { code: 'AU', name: 'Australia' },
  ];

  const handleRegionChange = (event: any) => {
    setSelectedRegion(event.target.value);
  };

  return (
    <Box>
      <Typography variant="h4" gutterBottom>
        Trending Stocks
      </Typography>

      {/* Region Selector */}
      <Card sx={{ mb: 3 }}>
        <CardContent>
          <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
            <Typography variant="h6">
              Select Market Region
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
          <FormControl sx={{ mt: 2, minWidth: 200 }}>
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
        </CardContent>
      </Card>

      {/* Loading State */}
      {isLoading && (
        <Card>
          <CardContent>
            <Typography variant="h6" gutterBottom>
              Loading Trending Stocks...
            </Typography>
            <Grid container spacing={2}>
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
          Error loading trending stocks: {error.message}
        </Alert>
      )}

      {/* Results */}
      {trendingData && trendingData.stocks.length > 0 && (
        <Card>
          <CardContent>
            <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 2 }}>
              <Typography variant="h6">
                Trending Stocks in {regions.find(r => r.code === selectedRegion)?.name} ({trendingData.count})
              </Typography>
              <Typography variant="caption" color="text.secondary">
                Last updated: {formatDate(trendingData.timestamp)}
              </Typography>
            </Box>

            {/* Summary Cards */}
            <Grid container spacing={2} sx={{ mb: 3 }}>
              {trendingData.stocks.slice(0, 6).map((stock) => (
                <Grid item xs={12} sm={6} md={4} key={stock.symbol}>
                  <Card variant="outlined">
                    <CardContent>
                      <Typography variant="h6" gutterBottom>
                        {stock.symbol}
                      </Typography>
                      <Typography variant="body2" color="text.secondary" gutterBottom>
                        {stock.shortName || stock.longName}
                      </Typography>
                      <Typography variant="h5" gutterBottom>
                        {formatCurrency(stock.regularMarketPrice, stock.currency)}
                      </Typography>
                      <Box sx={{ display: 'flex', alignItems: 'center', gap: 1, mb: 1 }}>
                        <Chip
                          label={formatPercentage(stock.regularMarketChangePercent)}
                          color={stock.regularMarketChangePercent && stock.regularMarketChangePercent >= 0 ? 'success' : 'error'}
                          size="small"
                        />
                        <Typography variant="body2">
                          {formatCurrency(stock.regularMarketChange, stock.currency)}
                        </Typography>
                      </Box>
                      <Typography variant="caption" color="text.secondary">
                        {stock.exchange} • {stock.quoteType}
                      </Typography>
                    </CardContent>
                  </Card>
                </Grid>
              ))}
            </Grid>

            {/* Detailed Table */}
            <Typography variant="h6" gutterBottom sx={{ mt: 3 }}>
              Complete List
            </Typography>
            <TableContainer component={Paper} variant="outlined">
              <Table>
                <TableHead>
                  <TableRow>
                    <TableCell>Rank</TableCell>
                    <TableCell>Symbol</TableCell>
                    <TableCell>Company Name</TableCell>
                    <TableCell>Price</TableCell>
                    <TableCell>Change</TableCell>
                    <TableCell>Exchange</TableCell>
                    <TableCell>Type</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {trendingData.stocks.map((stock, index) => (
                    <TableRow key={stock.symbol}>
                      <TableCell>
                        <Chip
                          label={`#${index + 1}`}
                          size="small"
                          color="primary"
                          variant="outlined"
                        />
                      </TableCell>
                      <TableCell>
                        <Typography variant="body2" fontWeight="bold">
                          {stock.symbol}
                        </Typography>
                      </TableCell>
                      <TableCell>
                        <Typography variant="body2">
                          {stock.shortName || stock.longName || 'N/A'}
                        </Typography>
                      </TableCell>
                      <TableCell>
                        <Typography variant="body2">
                          {formatCurrency(stock.regularMarketPrice, stock.currency)}
                        </Typography>
                      </TableCell>
                      <TableCell>
                        <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
                          <Chip
                            label={formatPercentage(stock.regularMarketChangePercent)}
                            color={stock.regularMarketChangePercent && stock.regularMarketChangePercent >= 0 ? 'success' : 'error'}
                            size="small"
                          />
                          <Typography variant="body2">
                            {formatCurrency(stock.regularMarketChange, stock.currency)}
                          </Typography>
                        </Box>
                      </TableCell>
                      <TableCell>
                        <Typography variant="body2">
                          {stock.exchange || 'N/A'}
                        </Typography>
                      </TableCell>
                      <TableCell>
                        <Typography variant="body2">
                          {stock.quoteType || 'N/A'}
                        </Typography>
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
      {trendingData && trendingData.stocks.length === 0 && (
        <Card>
          <CardContent>
            <Typography variant="h6" gutterBottom>
              No Trending Stocks
            </Typography>
            <Typography color="text.secondary">
              No trending stocks found for the selected region. Try selecting a different region.
            </Typography>
          </CardContent>
        </Card>
      )}
    </Box>
  );
};

export default TrendingStocksPage;
