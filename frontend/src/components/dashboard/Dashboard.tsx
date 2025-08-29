import React, { useState } from 'react';
import {
  Grid,
  Card,
  CardContent,
  Typography,
  Box,
  TextField,
  Button,
  Alert,
  Skeleton,
  Chip,
} from '@mui/material';
import { Search as SearchIcon } from '@mui/icons-material';
import { useStockQuote } from '@/hooks/useStockQuote';
import { useTrendingStocks } from '@/hooks/useTrendingStocks';
import { useMarketSummary } from '@/hooks/useMarketSummary';
import { QuoteRequest } from '@/types/api';
import { formatCurrency, formatPercentage, formatDate } from '@/utils/formatters';

const Dashboard: React.FC = () => {
  const [quoteRequest, setQuoteRequest] = useState<QuoteRequest>({
    symbols: '',
    region: 'US',
    language: 'en',
  });

  const [searchSymbols, setSearchSymbols] = useState<string>('');

  // API hooks
  const { data: quoteData, isLoading: quoteLoading, error: quoteError } = useStockQuote(quoteRequest);
  const { data: trendingData, isLoading: trendingLoading, error: trendingError } = useTrendingStocks('US');
  const { data: marketData, isLoading: marketLoading, error: marketError } = useMarketSummary('US');

  const handleSearch = () => {
    if (searchSymbols.trim()) {
      setQuoteRequest({
        ...quoteRequest,
        symbols: searchSymbols.trim(),
      });
    }
  };

  const handleKeyPress = (event: React.KeyboardEvent) => {
    if (event.key === 'Enter') {
      handleSearch();
    }
  };

  return (
    <Box>
      <Typography variant="h4" gutterBottom>
        Financial Dashboard
      </Typography>
      
      {/* Search Section */}
      <Card sx={{ mb: 3 }}>
        <CardContent>
          <Typography variant="h6" gutterBottom>
            Quick Stock Lookup
          </Typography>
          <Box sx={{ display: 'flex', gap: 2, alignItems: 'center' }}>
            <TextField
              label="Stock Symbols (comma-separated)"
              placeholder="AAPL, MSFT, GOOGL"
              value={searchSymbols}
              onChange={(e) => setSearchSymbols(e.target.value)}
              onKeyPress={handleKeyPress}
              sx={{ flexGrow: 1 }}
              helperText="Enter up to 10 stock symbols separated by commas"
            />
            <Button
              variant="contained"
              startIcon={<SearchIcon />}
              onClick={handleSearch}
              disabled={!searchSymbols.trim()}
            >
              Search
            </Button>
          </Box>
        </CardContent>
      </Card>

      {/* Stock Quotes Widget */}
      <Grid container spacing={3} sx={{ mb: 3 }}>
        <Grid item xs={12} lg={8}>
          <Card>
            <CardContent>
              <Typography variant="h6" gutterBottom>
                Stock Quotes
              </Typography>
              
              {quoteLoading && (
                <Box>
                  <Skeleton variant="rectangular" height={60} sx={{ mb: 1 }} />
                  <Skeleton variant="rectangular" height={60} sx={{ mb: 1 }} />
                  <Skeleton variant="rectangular" height={60} />
                </Box>
              )}
              
              {quoteError && (
                <Alert severity="error" sx={{ mb: 2 }}>
                  Error loading stock quotes. Please try again.
                </Alert>
              )}
              
              {quoteData && quoteData.quotes.length > 0 && (
                <Box>
                  {quoteData.quotes.map((quote) => (
                    <Box
                      key={quote.symbol}
                      sx={{
                        display: 'flex',
                        justifyContent: 'space-between',
                        alignItems: 'center',
                        p: 2,
                        mb: 1,
                        border: '1px solid',
                        borderColor: 'divider',
                        borderRadius: 1,
                        backgroundColor: 'background.paper',
                      }}
                    >
                      <Box>
                        <Typography variant="h6">{quote.symbol}</Typography>
                        <Typography variant="body2" color="text.secondary">
                          {quote.shortName || quote.longName}
                        </Typography>
                      </Box>
                      <Box sx={{ textAlign: 'right' }}>
                        <Typography variant="h6">
                          {formatCurrency(quote.regularMarketPrice, quote.currency)}
                        </Typography>
                        <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
                          <Chip
                            label={formatPercentage(quote.regularMarketChangePercent)}
                            color={quote.regularMarketChangePercent && quote.regularMarketChangePercent >= 0 ? 'success' : 'error'}
                            size="small"
                          />
                          <Typography variant="body2" color="text.secondary">
                            {formatCurrency(quote.regularMarketChange, quote.currency)}
                          </Typography>
                        </Box>
                      </Box>
                    </Box>
                  ))}
                  <Typography variant="caption" color="text.secondary">
                    Last updated: {formatDate(quoteData.timestamp)}
                  </Typography>
                </Box>
              )}
              
              {quoteData && quoteData.quotes.length === 0 && (
                <Typography color="text.secondary">
                  Enter stock symbols above to view quotes
                </Typography>
              )}
            </CardContent>
          </Card>
        </Grid>

        {/* Trending Stocks Widget */}
        <Grid item xs={12} lg={4}>
          <Card>
            <CardContent>
              <Typography variant="h6" gutterBottom>
                Trending Stocks (US)
              </Typography>
              
              {trendingLoading && (
                <Box>
                  <Skeleton variant="rectangular" height={40} sx={{ mb: 1 }} />
                  <Skeleton variant="rectangular" height={40} sx={{ mb: 1 }} />
                  <Skeleton variant="rectangular" height={40} sx={{ mb: 1 }} />
                  <Skeleton variant="rectangular" height={40} />
                </Box>
              )}
              
              {trendingError && (
                <Alert severity="error" sx={{ mb: 2 }}>
                  Error loading trending stocks
                </Alert>
              )}
              
              {trendingData && trendingData.stocks.length > 0 && (
                <Box>
                  {trendingData.stocks.slice(0, 5).map((stock) => (
                    <Box
                      key={stock.symbol}
                      sx={{
                        display: 'flex',
                        justifyContent: 'space-between',
                        alignItems: 'center',
                        p: 1,
                        mb: 1,
                        border: '1px solid',
                        borderColor: 'divider',
                        borderRadius: 1,
                      }}
                    >
                      <Box>
                        <Typography variant="body2" fontWeight="bold">
                          {stock.symbol}
                        </Typography>
                        <Typography variant="caption" color="text.secondary">
                          {stock.shortName}
                        </Typography>
                      </Box>
                      <Box sx={{ textAlign: 'right' }}>
                        <Typography variant="body2">
                          {formatCurrency(stock.regularMarketPrice, stock.currency)}
                        </Typography>
                        <Chip
                          label={formatPercentage(stock.regularMarketChangePercent)}
                          color={stock.regularMarketChangePercent && stock.regularMarketChangePercent >= 0 ? 'success' : 'error'}
                          size="small"
                        />
                      </Box>
                    </Box>
                  ))}
                  <Typography variant="caption" color="text.secondary">
                    {trendingData.count} trending stocks
                  </Typography>
                </Box>
              )}
            </CardContent>
          </Card>
        </Grid>
      </Grid>

      {/* Market Summary Widget */}
      <Grid container spacing={3}>
        <Grid item xs={12}>
          <Card>
            <CardContent>
              <Typography variant="h6" gutterBottom>
                Market Summary (US)
              </Typography>
              
              {marketLoading && (
                <Box>
                  <Skeleton variant="rectangular" height={40} sx={{ mb: 1 }} />
                  <Skeleton variant="rectangular" height={40} sx={{ mb: 1 }} />
                  <Skeleton variant="rectangular" height={40} />
                </Box>
              )}
              
              {marketError && (
                <Alert severity="error" sx={{ mb: 2 }}>
                  Error loading market summary
                </Alert>
              )}
              
              {marketData && marketData.marketIndices.length > 0 && (
                <Grid container spacing={2}>
                  {marketData.marketIndices.slice(0, 6).map((index) => (
                    <Grid item xs={12} sm={6} md={4} key={index.symbol}>
                      <Box
                        sx={{
                          p: 2,
                          border: '1px solid',
                          borderColor: 'divider',
                          borderRadius: 1,
                          backgroundColor: 'background.paper',
                        }}
                      >
                        <Typography variant="body2" fontWeight="bold">
                          {index.name}
                        </Typography>
                        <Typography variant="h6">
                          {formatCurrency(index.price, index.currency)}
                        </Typography>
                        <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
                          <Chip
                            label={formatPercentage(index.changePercent)}
                            color={index.changePercent >= 0 ? 'success' : 'error'}
                            size="small"
                          />
                          <Typography variant="caption" color="text.secondary">
                            {formatCurrency(index.change, index.currency)}
                          </Typography>
                        </Box>
                      </Box>
                    </Grid>
                  ))}
                </Grid>
              )}
            </CardContent>
          </Card>
        </Grid>
      </Grid>
    </Box>
  );
};

export default Dashboard;
