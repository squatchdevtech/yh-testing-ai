import React, { useState } from 'react';
import {
  Box,
  Card,
  CardContent,
  Typography,
  TextField,
  Button,
  Grid,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  Alert,
  Skeleton,
  Chip,

  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  Tabs,
  Tab,
} from '@mui/material';
import { Search as SearchIcon, Refresh as RefreshIcon } from '@mui/icons-material';
import { useStockQuote } from '@/hooks/useStockQuote';
import { QuoteRequest } from '@/types/api';
import {
  formatCurrency,
  formatPercentage,
  formatDate,
  formatLargeNumber,
  formatNumber,
  formatMarketState,
} from '@/utils/formatters';

interface TabPanelProps {
  children?: React.ReactNode;
  index: number;
  value: number;
}

function TabPanel(props: TabPanelProps) {
  const { children, value, index, ...other } = props;

  return (
    <div
      role="tabpanel"
      hidden={value !== index}
      id={`quote-tabpanel-${index}`}
      aria-labelledby={`quote-tab-${index}`}
      {...other}
    >
      {value === index && <Box sx={{ p: 3 }}>{children}</Box>}
    </div>
  );
}

const StockQuotePage: React.FC = () => {
  const [quoteRequest, setQuoteRequest] = useState<QuoteRequest>({
    symbols: '',
    region: 'US',
    language: 'en',
  });

  const [searchSymbols, setSearchSymbols] = useState<string>('');
  const [tabValue, setTabValue] = useState(0);

  const { data: quoteData, isLoading, error, refetch } = useStockQuote(quoteRequest);

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

  const handleTabChange = (_event: React.SyntheticEvent, newValue: number) => {
    setTabValue(newValue);
  };

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

  return (
    <Box>
      <Typography variant="h4" gutterBottom>
        Stock Quotes
      </Typography>

      {/* Search Form */}
      <Card sx={{ mb: 3 }}>
        <CardContent>
          <Typography variant="h6" gutterBottom>
            Search Stocks
          </Typography>
          <Grid container spacing={2} alignItems="center">
            <Grid item xs={12} md={6}>
              <TextField
                fullWidth
                label="Stock Symbols"
                placeholder="AAPL, MSFT, GOOGL, TSLA"
                value={searchSymbols}
                onChange={(e) => setSearchSymbols(e.target.value)}
                onKeyPress={handleKeyPress}
                helperText="Enter up to 10 stock symbols separated by commas"
              />
            </Grid>
            <Grid item xs={12} sm={6} md={2}>
              <FormControl fullWidth>
                <InputLabel>Region</InputLabel>
                <Select
                  value={quoteRequest.region}
                  label="Region"
                  onChange={(e) => setQuoteRequest({ ...quoteRequest, region: e.target.value })}
                >
                  {regions.map((region) => (
                    <MenuItem key={region.code} value={region.code}>
                      {region.name}
                    </MenuItem>
                  ))}
                </Select>
              </FormControl>
            </Grid>
            <Grid item xs={12} sm={6} md={2}>
              <FormControl fullWidth>
                <InputLabel>Language</InputLabel>
                <Select
                  value={quoteRequest.language}
                  label="Language"
                  onChange={(e) => setQuoteRequest({ ...quoteRequest, language: e.target.value })}
                >
                  {languages.map((lang) => (
                    <MenuItem key={lang.code} value={lang.code}>
                      {lang.name}
                    </MenuItem>
                  ))}
                </Select>
              </FormControl>
            </Grid>
            <Grid item xs={12} sm={6} md={2}>
              <Button
                fullWidth
                variant="contained"
                startIcon={<SearchIcon />}
                onClick={handleSearch}
                disabled={!searchSymbols.trim()}
              >
                Search
              </Button>
            </Grid>
          </Grid>
        </CardContent>
      </Card>

      {/* Results */}
      {isLoading && (
        <Card>
          <CardContent>
            <Typography variant="h6" gutterBottom>
              Loading Stock Data...
            </Typography>
            <Box>
              <Skeleton variant="rectangular" height={60} sx={{ mb: 1 }} />
              <Skeleton variant="rectangular" height={60} sx={{ mb: 1 }} />
              <Skeleton variant="rectangular" height={60} />
            </Box>
          </CardContent>
        </Card>
      )}

      {error && (
        <Alert severity="error" sx={{ mb: 2 }}>
          Error loading stock quotes: {error.message}
        </Alert>
      )}

      {quoteData && quoteData.quotes.length > 0 && (
        <Card>
          <CardContent>
            <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 2 }}>
              <Typography variant="h6">
                Results ({quoteData.quotes.length} stocks)
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

            <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
              <Tabs value={tabValue} onChange={handleTabChange} aria-label="quote tabs">
                <Tab label="Summary" />
                <Tab label="Detailed" />
                <Tab label="Metrics" />
              </Tabs>
            </Box>

            {/* Summary Tab */}
            <TabPanel value={tabValue} index={0}>
              <Grid container spacing={2}>
                {quoteData.quotes.map((quote) => (
                  <Grid item xs={12} sm={6} md={4} key={quote.symbol}>
                    <Card variant="outlined">
                      <CardContent>
                        <Typography variant="h6" gutterBottom>
                          {quote.symbol}
                        </Typography>
                        <Typography variant="body2" color="text.secondary" gutterBottom>
                          {quote.shortName || quote.longName}
                        </Typography>
                        <Typography variant="h5" gutterBottom>
                          {formatCurrency(quote.regularMarketPrice, quote.currency)}
                        </Typography>
                        <Box sx={{ display: 'flex', alignItems: 'center', gap: 1, mb: 1 }}>
                          <Chip
                            label={formatPercentage(quote.regularMarketChangePercent)}
                            color={quote.regularMarketChangePercent && quote.regularMarketChangePercent >= 0 ? 'success' : 'error'}
                            size="small"
                          />
                          <Typography variant="body2">
                            {formatCurrency(quote.regularMarketChange, quote.currency)}
                          </Typography>
                        </Box>
                        <Typography variant="caption" color="text.secondary">
                          {quote.exchange} • {formatMarketState(quote.marketState)}
                        </Typography>
                      </CardContent>
                    </Card>
                  </Grid>
                ))}
              </Grid>
            </TabPanel>

            {/* Detailed Tab */}
            <TabPanel value={tabValue} index={1}>
              <TableContainer component={Paper} variant="outlined">
                <Table>
                  <TableHead>
                    <TableRow>
                      <TableCell>Symbol</TableCell>
                      <TableCell>Price</TableCell>
                      <TableCell>Change</TableCell>
                      <TableCell>Volume</TableCell>
                      <TableCell>Market Cap</TableCell>
                      <TableCell>Exchange</TableCell>
                      <TableCell>Market State</TableCell>
                    </TableRow>
                  </TableHead>
                  <TableBody>
                    {quoteData.quotes.map((quote) => (
                      <TableRow key={quote.symbol}>
                        <TableCell>
                          <Box>
                            <Typography variant="body2" fontWeight="bold">
                              {quote.symbol}
                            </Typography>
                            <Typography variant="caption" color="text.secondary">
                              {quote.shortName}
                            </Typography>
                          </Box>
                        </TableCell>
                        <TableCell>
                          <Typography variant="body2">
                            {formatCurrency(quote.regularMarketPrice, quote.currency)}
                          </Typography>
                        </TableCell>
                        <TableCell>
                          <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
                            <Chip
                              label={formatPercentage(quote.regularMarketChangePercent)}
                              color={quote.regularMarketChangePercent && quote.regularMarketChangePercent >= 0 ? 'success' : 'error'}
                              size="small"
                            />
                            <Typography variant="body2">
                              {formatCurrency(quote.regularMarketChange, quote.currency)}
                            </Typography>
                          </Box>
                        </TableCell>
                        <TableCell>
                          <Typography variant="body2">
                            {formatLargeNumber(quote.regularMarketVolume)}
                          </Typography>
                        </TableCell>
                        <TableCell>
                          <Typography variant="body2">
                            {formatLargeNumber(quote.marketCap)}
                          </Typography>
                        </TableCell>
                        <TableCell>
                          <Typography variant="body2">
                            {quote.exchange}
                          </Typography>
                        </TableCell>
                        <TableCell>
                          <Chip
                            label={formatMarketState(quote.marketState)}
                            size="small"
                            variant="outlined"
                          />
                        </TableCell>
                      </TableRow>
                    ))}
                  </TableBody>
                </Table>
              </TableContainer>
            </TabPanel>

            {/* Metrics Tab */}
            <TabPanel value={tabValue} index={2}>
              <Grid container spacing={2}>
                {quoteData.quotes.map((quote) => (
                  <Grid item xs={12} md={6} key={quote.symbol}>
                    <Card variant="outlined">
                      <CardContent>
                        <Typography variant="h6" gutterBottom>
                          {quote.symbol} - Key Metrics
                        </Typography>
                        <Grid container spacing={2}>
                          <Grid item xs={6}>
                            <Typography variant="caption" color="text.secondary">
                              52 Week High
                            </Typography>
                            <Typography variant="body2">
                              {formatCurrency(quote.fiftyTwoWeekHigh, quote.currency)}
                            </Typography>
                          </Grid>
                          <Grid item xs={6}>
                            <Typography variant="caption" color="text.secondary">
                              52 Week Low
                            </Typography>
                            <Typography variant="body2">
                              {formatCurrency(quote.fiftyTwoWeekLow, quote.currency)}
                            </Typography>
                          </Grid>
                          <Grid item xs={6}>
                            <Typography variant="caption" color="text.secondary">
                              50 Day Avg
                            </Typography>
                            <Typography variant="body2">
                              {formatCurrency(quote.fiftyDayAverage, quote.currency)}
                            </Typography>
                          </Grid>
                          <Grid item xs={6}>
                            <Typography variant="caption" color="text.secondary">
                              200 Day Avg
                            </Typography>
                            <Typography variant="body2">
                              {formatCurrency(quote.twoHundredDayAverage, quote.currency)}
                            </Typography>
                          </Grid>
                          <Grid item xs={6}>
                            <Typography variant="caption" color="text.secondary">
                              P/E Ratio
                            </Typography>
                            <Typography variant="body2">
                              {formatNumber(quote.trailingPE)}
                            </Typography>
                          </Grid>
                          <Grid item xs={6}>
                            <Typography variant="caption" color="text.secondary">
                              Beta
                            </Typography>
                            <Typography variant="body2">
                              {formatNumber(quote.beta)}
                            </Typography>
                          </Grid>
                        </Grid>
                      </CardContent>
                    </Card>
                  </Grid>
                ))}
              </Grid>
            </TabPanel>
          </CardContent>
        </Card>
      )}

      {quoteData && quoteData.quotes.length === 0 && (
        <Card>
          <CardContent>
            <Typography variant="h6" gutterBottom>
              No Results
            </Typography>
            <Typography color="text.secondary">
              Enter stock symbols above to view quotes
            </Typography>
          </CardContent>
        </Card>
      )}

      {/* Last Updated Info */}
      {quoteData && (
        <Box sx={{ mt: 2, textAlign: 'center' }}>
          <Typography variant="caption" color="text.secondary">
            Last updated: {formatDate(quoteData.timestamp)} • 
            Region: {quoteData.region} • 
            Language: {quoteData.language}
          </Typography>
        </Box>
      )}
    </Box>
  );
};

export default StockQuotePage;
