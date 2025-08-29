
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { ThemeProvider, createTheme } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';
import { Box } from '@mui/material';

// Components
import Header from '@/components/common/Header';
import Sidebar from '@/components/common/Sidebar';
import Dashboard from '@/components/dashboard/Dashboard';
import StockQuotePage from '@/components/dashboard/StockQuotePage';
import TrendingStocksPage from '@/components/dashboard/TrendingStocksPage';
import MarketSummaryPage from '@/components/dashboard/MarketSummaryPage';

// Create a client
const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      staleTime: 5 * 60 * 1000, // 5 minutes
      retry: 3,
      retryDelay: (attemptIndex) => Math.min(1000 * 2 ** attemptIndex, 30000),
    },
  },
});

// Create theme
const theme = createTheme({
  palette: {
    mode: 'light',
    primary: {
      main: '#1976d2',
    },
    secondary: {
      main: '#dc004e',
    },
    background: {
      default: '#f5f5f5',
      paper: '#ffffff',
    },
  },
  typography: {
    fontFamily: '"Roboto", "Helvetica", "Arial", sans-serif',
    h4: {
      fontWeight: 600,
    },
    h5: {
      fontWeight: 600,
    },
    h6: {
      fontWeight: 600,
    },
  },
  components: {
    MuiCard: {
      styleOverrides: {
        root: {
          boxShadow: '0 2px 8px rgba(0,0,0,0.1)',
          borderRadius: 8,
        },
      },
    },
    MuiButton: {
      styleOverrides: {
        root: {
          textTransform: 'none',
          borderRadius: 6,
        },
      },
    },
  },
});

function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <ThemeProvider theme={theme}>
        <CssBaseline />
        <Router>
          <Box sx={{ display: 'flex', minHeight: '100vh' }}>
            <Header />
            <Sidebar />
            <Box
              component="main"
              sx={{
                flexGrow: 1,
                p: 3,
                mt: 8, // Account for header height
                ml: { xs: 0, md: 30 }, // Account for sidebar width on desktop
                backgroundColor: 'background.default',
                minHeight: 'calc(100vh - 64px)', // Account for header height
              }}
            >
              <Routes>
                <Route path="/" element={<Dashboard />} />
                <Route path="/quotes" element={<StockQuotePage />} />
                <Route path="/trending" element={<TrendingStocksPage />} />
                <Route path="/market-summary" element={<MarketSummaryPage />} />
              </Routes>
            </Box>
          </Box>
        </Router>
      </ThemeProvider>
    </QueryClientProvider>
  );
}

export default App;
