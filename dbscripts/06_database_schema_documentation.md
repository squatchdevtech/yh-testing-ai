# Yahoo Finance API Database Schema Documentation

## Overview
This database is designed to store Yahoo Finance API data and reduce network traffic by implementing intelligent caching strategies. The schema supports all major endpoints from the YHController including quotes, trending stocks, market data, currency exchange, and cryptocurrency information.

## Database Structure

### Core Tables

#### 1. `api_requests`
Stores metadata about all API requests for auditing, monitoring, and caching analysis.
- **Purpose**: Request tracking, performance monitoring, cache hit analysis
- **Key Fields**: `request_id`, `endpoint`, `symbols`, `region`, `cache_hit`
- **Indexes**: Optimized for endpoint queries and cache analysis

#### 2. `stock_quotes`
Stores individual stock quote data with comprehensive financial metrics.
- **Purpose**: Primary storage for stock quote data with caching support
- **Key Fields**: `symbol`, `region`, `quote_timestamp`, `regular_market_price`
- **Caching**: `cache_valid_until` field controls cache expiration
- **Performance**: Partitioned by year for large datasets

#### 3. `trending_stocks`
Stores trending stock data organized by region.
- **Purpose**: Cache trending stock information to reduce API calls
- **Key Fields**: `region`, `symbol`, `trending_rank`, `job_timestamp`
- **Caching**: Regional trending data with configurable expiration

#### 4. `market_indices`
Stores market index data (S&P 500, FTSE 100, etc.).
- **Purpose**: Cache major market indices for dashboard displays
- **Key Fields**: `region`, `symbol`, `name`, `price`, `change_percent`
- **Caching**: Hourly updates for market hours

#### 5. `market_sectors`
Stores sector performance data with top stocks.
- **Purpose**: Cache sector analysis and performance metrics
- **Key Fields**: `region`, `name`, `performance`, `top_stocks` (JSON)
- **Caching**: Daily updates for sector performance

### Extended Tables

#### 6. `currency_exchange_rates`
Stores currency exchange rate data.
- **Purpose**: Cache exchange rates to reduce API calls
- **Key Fields**: `from_currency`, `to_currency`, `exchange_rate`
- **Caching**: 15-minute intervals for active trading

#### 7. `cryptocurrency_data`
Stores cryptocurrency quote data.
- **Purpose**: Cache crypto prices and market data
- **Key Fields**: `symbol`, `price`, `market_cap`, `volume_24h`
- **Caching**: 5-minute intervals for volatile crypto markets

#### 8. `bulk_quote_operations`
Stores metadata about bulk quote operations.
- **Purpose**: Track and monitor bulk quote processing
- **Key Fields**: `operation_id`, `total_symbols`, `successful_quotes`
- **Relationships**: Links to `bulk_quote_groups`

#### 9. `bulk_quote_groups`
Stores individual groups within bulk operations.
- **Purpose**: Organize bulk quote requests by logical groups
- **Key Fields**: `operation_id`, `group_name`, `symbols` (JSON)
- **Data**: Stores quote results as JSON for flexibility

#### 10. `api_health_metrics`
Stores API health checks and usage metrics.
- **Purpose**: Monitor API health and performance
- **Key Fields**: `service_name`, `status`, `api_key_configured`
- **Retention**: 30-day retention for health monitoring

## Caching Strategy

### Cache Expiration Times
- **Stock Quotes**: 15 minutes (high frequency updates)
- **Trending Stocks**: 30 minutes (moderate frequency)
- **Market Indices**: 1 hour (market hours updates)
- **Market Sectors**: 24 hours (daily performance)
- **Currency Exchange**: 15 minutes (active trading)
- **Cryptocurrency**: 5 minutes (high volatility)

### Cache Invalidation
- Automatic cleanup via scheduled events
- Manual invalidation for data updates
- Region-specific caching for localized data

## Performance Optimizations

### Indexing Strategy
- Primary keys on all tables
- Composite indexes for common query patterns
- Symbol and region-based indexes for fast lookups
- Timestamp indexes for time-based queries

### Partitioning
- Optional partitioning by year for large tables
- Improves query performance on historical data
- Automatic partition management

### Views
- `v_current_stock_quotes`: Latest quotes per symbol
- `v_current_trending_stocks`: Current trending data
- `v_current_market_indices`: Latest market indices

## Data Retention

### Automatic Cleanup
- **API Requests**: 90 days (audit trail)
- **Health Metrics**: 30 days (monitoring)
- **Expired Cache**: Daily cleanup
- **Historical Data**: Configurable retention

### Manual Archiving
- Long-term storage for compliance
- Data export capabilities
- Backup and restore procedures

## Security

### User Permissions
- **Application User**: Full CRUD access
- **Read-Only User**: SELECT access for reporting
- **Restricted Access**: No direct table modifications

### Data Protection
- Encrypted connections (TLS)
- Parameterized queries
- Input validation and sanitization

## Monitoring and Maintenance

### Automated Tasks
- Daily cache cleanup
- Performance monitoring
- Health check logging
- Error tracking

### Manual Maintenance
- Index optimization
- Table statistics updates
- Performance tuning
- Capacity planning

## Usage Examples

### Check Cache Before API Call
```sql
SELECT * FROM stock_quotes 
WHERE symbol = 'AAPL' 
AND region = 'US' 
AND cache_valid_until > NOW();
```

### Get Trending Stocks by Region
```sql
SELECT * FROM trending_stocks 
WHERE region = 'US' 
AND cache_valid_until > NOW()
ORDER BY trending_rank;
```

### Monitor API Performance
```sql
SELECT 
    endpoint,
    AVG(response_time_ms) as avg_response_time,
    COUNT(*) as total_requests,
    SUM(cache_hit) as cache_hits
FROM api_requests 
WHERE request_timestamp > DATE_SUB(NOW(), INTERVAL 24 HOUR)
GROUP BY endpoint;
```

## Future Enhancements

### Planned Features
- Real-time data streaming
- Advanced analytics and reporting
- Machine learning for cache optimization
- Multi-region data synchronization
- API rate limiting and throttling

### Scalability Considerations
- Horizontal scaling with read replicas
- Sharding by region or symbol
- Cloud-native database options
- Microservices architecture support
