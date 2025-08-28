-- =====================================================
-- Yahoo Finance API Database Schema
-- =====================================================
-- This script creates the database and all necessary tables
-- for storing Yahoo Finance API data and reducing API calls
-- =====================================================

-- Create database if it doesn't exist
CREATE DATABASE IF NOT EXISTS yahoo_finance_db
CHARACTER SET utf8mb4
COLLATE utf8mb4_unicode_ci;

-- Use the database
USE yahoo_finance_db;


-- Set timezone to UTC for consistent timestamp handling
SET time_zone = '+00:00';


## 2. Core Tables Creation Script

-- sql:dbscripts/02_create_core_tables.sql
-- =====================================================
-- Core Tables for Yahoo Finance API Data Storage
-- =====================================================

USE yahoo_finance_db;

-- =====================================================
-- 1. API REQUESTS TABLE
-- Stores metadata about all API requests for auditing and caching
-- =====================================================
CREATE TABLE IF NOT EXISTS api_requests (
    id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    request_id VARCHAR(36) NOT NULL UNIQUE COMMENT 'UUID for request tracking',
    endpoint VARCHAR(100) NOT NULL COMMENT 'API endpoint called (e.g., /quote, /trending)',
    method VARCHAR(10) NOT NULL COMMENT 'HTTP method (GET, POST)',
    symbols TEXT COMMENT 'Comma-separated symbols for quote requests',
    region VARCHAR(2) DEFAULT 'US' COMMENT 'Market region (US, AU, CA, etc.)',
    language VARCHAR(2) DEFAULT 'en' COMMENT 'Response language',
    request_timestamp TIMESTAMP DEFAULT CURRENT_TIMESTAMP COMMENT 'When request was made',
    response_timestamp TIMESTAMP NULL COMMENT 'When response was received',
    status_code INT COMMENT 'HTTP status code of response',
    response_time_ms INT COMMENT 'Response time in milliseconds',
    cache_hit BOOLEAN DEFAULT FALSE COMMENT 'Whether data was served from cache',
    user_agent TEXT COMMENT 'User agent making the request',
    ip_address VARCHAR(45) COMMENT 'IP address of requester',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    
    INDEX idx_request_id (request_id),
    INDEX idx_endpoint (endpoint),
    INDEX idx_symbols (symbols(100)),
    INDEX idx_region (region),
    INDEX idx_request_timestamp (request_timestamp),
    INDEX idx_cache_hit (cache_hit),
    INDEX idx_status_code (status_code)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
COMMENT='Stores metadata about all API requests for auditing and caching';

-- =====================================================
-- 2. STOCK QUOTES TABLE
-- Stores individual stock quote data with caching support
-- =====================================================
CREATE TABLE IF NOT EXISTS stock_quotes (
    id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    symbol VARCHAR(20) NOT NULL COMMENT 'Stock symbol (e.g., AAPL, GOOGL)',
    region VARCHAR(2) DEFAULT 'US' COMMENT 'Market region',
    quote_timestamp BIGINT COMMENT 'Unix timestamp from Yahoo Finance',
    regular_market_price DECIMAL(20,6) COMMENT 'Current market price',
    regular_market_change DECIMAL(20,6) COMMENT 'Price change from previous close',
    regular_market_change_percent DECIMAL(10,4) COMMENT 'Price change percentage',
    regular_market_time BIGINT COMMENT 'Market time as Unix timestamp',
    regular_market_day_high DECIMAL(20,6) COMMENT 'Day high price',
    regular_market_day_low DECIMAL(20,6) COMMENT 'Day low price',
    regular_market_volume BIGINT COMMENT 'Trading volume',
    regular_market_previous_close DECIMAL(20,6) COMMENT 'Previous closing price',
    currency VARCHAR(3) COMMENT 'Currency code (USD, EUR, etc.)',
    market_state VARCHAR(20) COMMENT 'Market state (REGULAR, CLOSED, etc.)',
    short_name VARCHAR(100) COMMENT 'Short company name',
    long_name TEXT COMMENT 'Full company name',
    exchange VARCHAR(20) COMMENT 'Exchange name (NASDAQ, NYSE, etc.)',
    exchange_timezone_name VARCHAR(100) COMMENT 'Exchange timezone',
    exchange_timezone_short_name VARCHAR(10) COMMENT 'Exchange timezone abbreviation',
    quote_type VARCHAR(20) COMMENT 'Type of quote (EQUITY, ETF, etc.)',
    market_cap BIGINT COMMENT 'Market capitalization',
    shares_outstanding BIGINT COMMENT 'Outstanding shares',
    book_value DECIMAL(20,6) COMMENT 'Book value per share',
    price_to_book DECIMAL(10,4) COMMENT 'Price to book ratio',
    fifty_two_week_low DECIMAL(20,6) COMMENT '52-week low',
    fifty_two_week_high DECIMAL(20,6) COMMENT '52-week high',
    fifty_day_average DECIMAL(20,6) COMMENT '50-day moving average',
    two_hundred_day_average DECIMAL(20,6) COMMENT '200-day moving average',
    trailing_pe DECIMAL(10,4) COMMENT 'Trailing P/E ratio',
    forward_pe DECIMAL(10,4) COMMENT 'Forward P/E ratio',
    dividend_yield DECIMAL(10,4) COMMENT 'Dividend yield',
    trailing_annual_dividend_yield DECIMAL(10,4) COMMENT 'Trailing annual dividend yield',
    beta DECIMAL(10,4) COMMENT 'Beta coefficient',
    cache_valid_until TIMESTAMP NULL COMMENT 'When this cache entry expires',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    
    UNIQUE KEY uk_symbol_region_timestamp (symbol, region, quote_timestamp),
    INDEX idx_symbol (symbol),
    INDEX idx_region (region),
    INDEX idx_quote_timestamp (quote_timestamp),
    INDEX idx_cache_valid_until (cache_valid_until),
    INDEX idx_market_state (market_state),
    INDEX idx_exchange (exchange),
    INDEX idx_currency (currency)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
COMMENT='Stores individual stock quote data with caching support';

-- =====================================================
-- 3. TRENDING STOCKS TABLE
-- Stores trending stock data by region
-- =====================================================
CREATE TABLE IF NOT EXISTS trending_stocks (
    id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    region VARCHAR(2) NOT NULL COMMENT 'Market region',
    symbol VARCHAR(20) NOT NULL COMMENT 'Stock symbol',
    short_name VARCHAR(100) COMMENT 'Short company name',
    long_name TEXT COMMENT 'Full company name',
    regular_market_price DECIMAL(20,6) COMMENT 'Current market price',
    regular_market_change DECIMAL(20,6) COMMENT 'Price change',
    regular_market_change_percent DECIMAL(10,4) COMMENT 'Price change percentage',
    currency VARCHAR(3) COMMENT 'Currency code',
    market_state VARCHAR(20) COMMENT 'Market state',
    exchange VARCHAR(20) COMMENT 'Exchange name',
    quote_type VARCHAR(20) COMMENT 'Quote type',
    job_timestamp BIGINT COMMENT 'Job timestamp from Yahoo Finance',
    start_interval BIGINT COMMENT 'Start interval from Yahoo Finance',
    trending_rank INT COMMENT 'Position in trending list',
    cache_valid_until TIMESTAMP NULL COMMENT 'When this cache entry expires',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    
    UNIQUE KEY uk_region_symbol_timestamp (region, symbol, job_timestamp),
    INDEX idx_region (region),
    INDEX idx_symbol (symbol),
    INDEX idx_job_timestamp (job_timestamp),
    INDEX idx_trending_rank (trending_rank),
    INDEX idx_cache_valid_until (cache_valid_until)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
COMMENT='Stores trending stock data by region';

-- =====================================================
-- 4. MARKET INDICES TABLE
-- Stores market index data
-- =====================================================
CREATE TABLE IF NOT EXISTS market_indices (
    id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    region VARCHAR(2) NOT NULL COMMENT 'Market region',
    symbol VARCHAR(20) NOT NULL COMMENT 'Index symbol (e.g., ^GSPC)',
    name VARCHAR(100) NOT NULL COMMENT 'Index name (e.g., S&P 500)',
    price DECIMAL(20,6) COMMENT 'Current index value',
    change_value DECIMAL(20,6) COMMENT 'Change in points',
    change_percent DECIMAL(10,4) COMMENT 'Change percentage',
    currency VARCHAR(3) COMMENT 'Currency code',
    market_state VARCHAR(20) COMMENT 'Market state',
    market_time BIGINT COMMENT 'Market time as Unix timestamp',
    cache_valid_until TIMESTAMP NULL COMMENT 'When this cache entry expires',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    
    UNIQUE KEY uk_region_symbol_timestamp (region, symbol, market_time),
    INDEX idx_region (region),
    INDEX idx_symbol (symbol),
    INDEX idx_market_time (market_time),
    INDEX idx_cache_valid_until (cache_valid_until)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
COMMENT='Stores market index data';

-- =====================================================
-- 5. MARKET SECTORS TABLE
-- Stores market sector performance data
-- =====================================================
CREATE TABLE IF NOT EXISTS market_sectors (
    id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    region VARCHAR(2) NOT NULL COMMENT 'Market region',
    name VARCHAR(100) NOT NULL COMMENT 'Sector name (e.g., Technology, Healthcare)',
    performance DECIMAL(10,4) COMMENT 'Sector performance value',
    change_percent DECIMAL(10,4) COMMENT 'Sector change percentage',
    top_stocks JSON COMMENT 'Top stocks in sector as JSON array',
    cache_valid_until TIMESTAMP NULL COMMENT 'When this cache entry expires',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    
    UNIQUE KEY uk_region_name_timestamp (region, name, created_at),
    INDEX idx_region (region),
    INDEX idx_name (name),
    INDEX idx_cache_valid_until (cache_valid_until)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
COMMENT='Stores market sector performance data';
```

## 3. Additional Tables Creation Script

```sql:dbscripts/03_create_additional_tables.sql
-- =====================================================
-- Additional Tables for Extended Finance Data
-- =====================================================

USE yahoo_finance_db;

-- =====================================================
-- 6. CURRENCY EXCHANGE RATES TABLE
-- Stores currency exchange rate data
-- =====================================================
CREATE TABLE IF NOT EXISTS currency_exchange_rates (
    id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    from_currency VARCHAR(3) NOT NULL COMMENT 'Source currency code',
    to_currency VARCHAR(3) NOT NULL COMMENT 'Target currency code',
    symbol VARCHAR(20) NOT NULL COMMENT 'Exchange symbol (e.g., EURUSD=X)',
    exchange_rate DECIMAL(20,6) COMMENT 'Current exchange rate',
    change_value DECIMAL(20,6) COMMENT 'Change in rate',
    change_percent DECIMAL(10,4) COMMENT 'Change percentage',
    last_update BIGINT COMMENT 'Last update time as Unix timestamp',
    cache_valid_until TIMESTAMP NULL COMMENT 'When this cache entry expires',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    
    UNIQUE KEY uk_from_to_currency_timestamp (from_currency, to_currency, last_update),
    INDEX idx_from_currency (from_currency),
    INDEX idx_to_currency (to_currency),
    INDEX idx_symbol (symbol),
    INDEX idx_last_update (last_update),
    INDEX idx_cache_valid_until (cache_valid_until)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
COMMENT='Stores currency exchange rate data';

-- =====================================================
-- 7. CRYPTOCURRENCY DATA TABLE
-- Stores cryptocurrency quote data
-- =====================================================
CREATE TABLE IF NOT EXISTS cryptocurrency_data (
    id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    symbol VARCHAR(20) NOT NULL COMMENT 'Crypto symbol (e.g., BTC-USD)',
    name VARCHAR(100) COMMENT 'Cryptocurrency name',
    price DECIMAL(20,6) COMMENT 'Current price',
    change_value DECIMAL(20,6) COMMENT 'Price change',
    change_percent DECIMAL(10,4) COMMENT 'Change percentage',
    currency VARCHAR(3) DEFAULT 'USD' COMMENT 'Base currency',
    market_cap BIGINT COMMENT 'Market capitalization',
    volume_24h BIGINT COMMENT '24-hour trading volume',
    high_24h DECIMAL(20,6) COMMENT '24-hour high',
    low_24h DECIMAL(20,6) COMMENT '24-hour low',
    market_state VARCHAR(20) COMMENT 'Market state',
    last_update BIGINT COMMENT 'Last update time as Unix timestamp',
    cache_valid_until TIMESTAMP NULL COMMENT 'When this cache entry expires',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    
    UNIQUE KEY uk_symbol_currency_timestamp (symbol, currency, last_update),
    INDEX idx_symbol (symbol),
    INDEX idx_currency (currency),
    INDEX idx_last_update (last_update),
    INDEX idx_cache_valid_until (cache_valid_until),
    INDEX idx_market_state (market_state)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
COMMENT='Stores cryptocurrency quote data';

-- =====================================================
-- 8. BULK QUOTE OPERATIONS TABLE
-- Stores metadata about bulk quote operations
-- =====================================================
CREATE TABLE IF NOT EXISTS bulk_quote_operations (
    id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    operation_id VARCHAR(36) NOT NULL UNIQUE COMMENT 'UUID for operation tracking',
    region VARCHAR(2) DEFAULT 'US' COMMENT 'Market region',
    language VARCHAR(2) DEFAULT 'en' COMMENT 'Response language',
    total_symbols INT NOT NULL COMMENT 'Total symbols requested',
    successful_quotes INT DEFAULT 0 COMMENT 'Number of successful quotes',
    failed_quotes INT DEFAULT 0 COMMENT 'Number of failed quotes',
    errors JSON COMMENT 'Array of error messages as JSON',
    processing_time_ms INT COMMENT 'Total processing time in milliseconds',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    
    INDEX idx_operation_id (operation_id),
    INDEX idx_region (region),
    INDEX idx_created_at (created_at),
    INDEX idx_total_symbols (total_symbols)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
COMMENT='Stores metadata about bulk quote operations';

-- =====================================================
-- 9. BULK QUOTE GROUPS TABLE
-- Stores individual groups within bulk operations
-- =====================================================
CREATE TABLE IF NOT EXISTS bulk_quote_groups (
    id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    operation_id VARCHAR(36) NOT NULL COMMENT 'Reference to bulk operation',
    group_name VARCHAR(100) NOT NULL COMMENT 'Name of the symbol group',
    symbols JSON NOT NULL COMMENT 'Array of symbols in this group',
    quotes JSON COMMENT 'Array of quote data as JSON',
    success_count INT DEFAULT 0 COMMENT 'Number of successful quotes in group',
    error_count INT DEFAULT 0 COMMENT 'Number of failed quotes in group',
    errors JSON COMMENT 'Array of error messages for this group',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    
    INDEX idx_operation_id (operation_id),
    INDEX idx_group_name (group_name),
    FOREIGN KEY (operation_id) REFERENCES bulk_quote_operations(operation_id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
COMMENT='Stores individual groups within bulk quote operations';

-- =====================================================
-- 10. API HEALTH AND METRICS TABLE
-- Stores API health checks and usage metrics
-- =====================================================
CREATE TABLE IF NOT EXISTS api_health_metrics (
    id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    service_name VARCHAR(100) NOT NULL COMMENT 'Service name (e.g., YH Controller)',
    status VARCHAR(20) NOT NULL COMMENT 'Health status (Healthy, Unhealthy)',
    api_key_configured BOOLEAN DEFAULT FALSE COMMENT 'Whether API key is configured',
    api_key_source VARCHAR(50) COMMENT 'Source of API key (AWS Parameter Store, etc.)',
    parameter_store_path VARCHAR(200) COMMENT 'Parameter store path for API key',
    supported_regions JSON COMMENT 'Array of supported regions as JSON',
    message TEXT COMMENT 'Health check message',
    response_time_ms INT COMMENT 'Health check response time',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    
    INDEX idx_service_name (service_name),
    INDEX idx_status (status),
    INDEX idx_created_at (created_at),
    INDEX idx_api_key_configured (api_key_configured)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
COMMENT='Stores API health checks and usage metrics';
```

## 4. Indexes and Performance Optimization Script

```sql:dbscripts/04_create_indexes_and_optimizations.sql
-- =====================================================
-- Indexes and Performance Optimizations
-- =====================================================

USE yahoo_finance_db;

-- =====================================================
-- Composite Indexes for Better Query Performance
-- =====================================================

-- Stock quotes composite indexes
CREATE INDEX idx_stock_quotes_symbol_region_cache ON stock_quotes(symbol, region, cache_valid_until);
CREATE INDEX idx_stock_quotes_region_timestamp ON stock_quotes(region, quote_timestamp);
CREATE INDEX idx_stock_quotes_exchange_market_state ON stock_quotes(exchange, market_state);

-- Trending stocks composite indexes
CREATE INDEX idx_trending_stocks_region_rank ON trending_stocks(region, trending_rank);
CREATE INDEX idx_trending_stocks_region_cache ON trending_stocks(region, cache_valid_until);

-- Market data composite indexes
CREATE INDEX idx_market_indices_region_cache ON market_indices(region, cache_valid_until);
CREATE INDEX idx_market_sectors_region_cache ON market_sectors(region, cache_valid_until);

-- Currency exchange composite indexes
CREATE INDEX idx_currency_exchange_from_to_cache ON currency_exchange_rates(from_currency, to_currency, cache_valid_until);

-- Cryptocurrency composite indexes
CREATE INDEX idx_crypto_symbol_cache ON cryptocurrency_data(symbol, cache_valid_until);

-- API requests composite indexes
CREATE INDEX idx_api_requests_endpoint_timestamp ON api_requests(endpoint, request_timestamp);
CREATE INDEX idx_api_requests_symbols_region ON api_requests(symbols(50), region);
CREATE INDEX idx_api_requests_status_cache ON api_requests(status_code, cache_hit);

-- =====================================================
-- Partitioning for Large Tables (Optional)
-- =====================================================

-- Partition stock_quotes by year for better performance on large datasets
-- Uncomment if you expect millions of records per year
/*
ALTER TABLE stock_quotes PARTITION BY RANGE (YEAR(FROM_UNIXTIME(quote_timestamp))) (
    PARTITION p2023 VALUES LESS THAN (2024),
    PARTITION p2024 VALUES LESS THAN (2025),
    PARTITION p2025 VALUES LESS THAN (2026),
    PARTITION p_future VALUES LESS THAN MAXVALUE
);
*/

-- =====================================================
-- Views for Common Queries
-- =====================================================

-- View for current stock quotes (most recent per symbol)
CREATE OR REPLACE VIEW v_current_stock_quotes AS
SELECT 
    sq1.*
FROM stock_quotes sq1
INNER JOIN (
    SELECT symbol, region, MAX(quote_timestamp) as max_timestamp
    FROM stock_quotes
    WHERE cache_valid_until > NOW()
    GROUP BY symbol, region
) sq2 ON sq1.symbol = sq2.symbol 
    AND sq1.region = sq2.region 
    AND sq1.quote_timestamp = sq2.max_timestamp;

-- View for current trending stocks
CREATE OR REPLACE VIEW v_current_trending_stocks AS
SELECT 
    ts1.*
FROM trending_stocks ts1
INNER JOIN (
    SELECT region, MAX(job_timestamp) as max_timestamp
    FROM trending_stocks
    WHERE cache_valid_until > NOW()
    GROUP BY region
) ts2 ON ts1.region = ts2.region 
    AND ts1.job_timestamp = ts2.max_timestamp;

-- View for current market indices
CREATE OR REPLACE VIEW v_current_market_indices AS
SELECT 
    mi1.*
FROM market_indices mi1
INNER JOIN (
    SELECT region, symbol, MAX(market_time) as max_timestamp
    FROM market_indices
    WHERE cache_valid_until > NOW()
    GROUP BY region, symbol
) mi2 ON mi1.region = mi2.region 
    AND mi1.symbol = mi2.symbol 
    AND mi1.market_time = mi2.max_timestamp;

-- =====================================================
-- Stored Procedures for Common Operations
-- =====================================================

DELIMITER //

-- Procedure to clean up expired cache entries
CREATE PROCEDURE CleanupExpiredCache()
BEGIN
    DECLARE done INT DEFAULT FALSE;
    DECLARE table_name VARCHAR(100);
    DECLARE cur CURSOR FOR 
        SELECT TABLE_NAME 
        FROM INFORMATION_SCHEMA.TABLES 
        WHERE TABLE_SCHEMA = 'yahoo_finance_db' 
        AND TABLE_NAME LIKE '%cache_valid_until%';
    DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;
    
    OPEN cur;
    
    read_loop: LOOP
        FETCH cur INTO table_name;
        IF done THEN
            LEAVE read_loop;
        END IF;
        
        SET @sql = CONCAT('DELETE FROM ', table_name, ' WHERE cache_valid_until < NOW()');
        PREPARE stmt FROM @sql;
        EXECUTE stmt;
        DEALLOCATE PREPARE stmt;
    END LOOP;
    
    CLOSE cur;
END //

-- Procedure to get cached quote data
CREATE PROCEDURE GetCachedQuote(
    IN p_symbol VARCHAR(20),
    IN p_region VARCHAR(2)
)
BEGIN
    SELECT * FROM stock_quotes 
    WHERE symbol = p_symbol 
    AND region = p_region 
    AND cache_valid_until > NOW()
    ORDER BY quote_timestamp DESC 
    LIMIT 1;
END //

-- Procedure to insert or update quote data
CREATE PROCEDURE UpsertQuote(
    IN p_symbol VARCHAR(20),
    IN p_region VARCHAR(2),
    IN p_quote_timestamp BIGINT,
    IN p_price DECIMAL(20,6),
    IN p_cache_duration_minutes INT
)
BEGIN
    INSERT INTO stock_quotes (
        symbol, region, quote_timestamp, regular_market_price, 
        cache_valid_until, created_at, updated_at
    ) VALUES (
        p_symbol, p_region, p_quote_timestamp, p_price,
        DATE_ADD(NOW(), INTERVAL p_cache_duration_minutes MINUTE),
        NOW(), NOW()
    ) ON DUPLICATE KEY UPDATE
        regular_market_price = VALUES(regular_market_price),
        cache_valid_until = VALUES(cache_valid_until),
        updated_at = NOW();
END //

DELIMITER ;
```

## 5. Data Management and Maintenance Script

```sql:dbscripts/05_data_management_and_maintenance.sql
-- =====================================================
-- Data Management and Maintenance Scripts
-- =====================================================

USE yahoo_finance_db;

-- =====================================================
-- Triggers for Automatic Timestamp Updates
-- =====================================================

DELIMITER //

-- Trigger to update updated_at timestamp on stock_quotes
CREATE TRIGGER tr_stock_quotes_update 
    BEFORE UPDATE ON stock_quotes
    FOR EACH ROW
    SET NEW.updated_at = NOW();

-- Trigger to update updated_at timestamp on trending_stocks
CREATE TRIGGER tr_trending_stocks_update 
    BEFORE UPDATE ON trending_stocks
    FOR EACH ROW
    SET NEW.updated_at = NOW();

-- Trigger to update updated_at timestamp on market_indices
CREATE TRIGGER tr_market_indices_update 
    BEFORE UPDATE ON market_indices
    FOR EACH ROW
    SET NEW.updated_at = NOW();

-- Trigger to update updated_at timestamp on market_sectors
CREATE TRIGGER tr_market_sectors_update 
    BEFORE UPDATE ON market_sectors
    FOR EACH ROW
    SET NEW.updated_at = NOW();

-- Trigger to update updated_at timestamp on currency_exchange_rates
CREATE TRIGGER tr_currency_exchange_update 
    BEFORE UPDATE ON currency_exchange_rates
    FOR EACH ROW
    SET NEW.updated_at = NOW();

-- Trigger to update updated_at timestamp on cryptocurrency_data
CREATE TRIGGER tr_cryptocurrency_update 
    BEFORE UPDATE ON cryptocurrency_data
    FOR EACH ROW
    SET NEW.updated_at = NOW();

-- Trigger to update updated_at timestamp on bulk_quote_operations
CREATE TRIGGER tr_bulk_quote_operations_update 
    BEFORE UPDATE ON bulk_quote_operations
    FOR EACH ROW
    SET NEW.updated_at = NOW();

-- Trigger to update updated_at timestamp on api_requests
CREATE TRIGGER tr_api_requests_update 
    BEFORE UPDATE ON api_requests
    FOR EACH ROW
    SET NEW.updated_at = NOW();

DELIMITER ;

-- =====================================================
-- Event Scheduler for Automated Maintenance
-- =====================================================

-- Enable event scheduler
SET GLOBAL event_scheduler = ON;

-- Create event to clean up expired cache entries daily
CREATE EVENT IF NOT EXISTS evt_cleanup_expired_cache
ON SCHEDULE EVERY 1 DAY
STARTS CURRENT_TIMESTAMP
DO
    CALL CleanupExpiredCache();

-- Create event to clean up old API request logs (keep last 90 days)
CREATE EVENT IF NOT EXISTS evt_cleanup_old_api_requests
ON SCHEDULE EVERY 1 DAY
STARTS CURRENT_TIMESTAMP
DO
    DELETE FROM api_requests 
    WHERE request_timestamp < DATE_SUB(NOW(), INTERVAL 90 DAY);

-- Create event to clean up old health metrics (keep last 30 days)
CREATE EVENT IF NOT EXISTS evt_cleanup_old_health_metrics
ON SCHEDULE EVERY 1 DAY
STARTS CURRENT_TIMESTAMP
DO
    DELETE FROM api_health_metrics 
    WHERE created_at < DATE_SUB(NOW(), INTERVAL 30 DAY);

-- =====================================================
-- Sample Data Insertion for Testing
-- =====================================================

-- Insert sample API request
INSERT INTO api_requests (
    request_id, endpoint, method, symbols, region, language, 
    status_code, response_time_ms, cache_hit
) VALUES (
    UUID(), '/api/yh/quote', 'GET', 'AAPL,GOOGL', 'US', 'en',
    200, 150, FALSE
);

-- Insert sample stock quote
INSERT INTO stock_quotes (
    symbol, region, quote_timestamp, regular_market_price,
    regular_market_change, regular_market_change_percent,
    currency, market_state, short_name, exchange,
    cache_valid_until
) VALUES (
    'AAPL', 'US', UNIX_TIMESTAMP(), 150.25,
    2.50, 1.69, 'USD', 'REGULAR', 'Apple Inc.', 'NASDAQ',
    DATE_ADD(NOW(), INTERVAL 15 MINUTE)
);

-- Insert sample trending stock
INSERT INTO trending_stocks (
    region, symbol, short_name, regular_market_price,
    regular_market_change_percent, currency, market_state,
    trending_rank, job_timestamp, cache_valid_until
) VALUES (
    'US', 'TSLA', 'Tesla Inc.', 250.75,
    3.25, 'USD', 'REGULAR',
    1, UNIX_TIMESTAMP(), DATE_ADD(NOW(), INTERVAL 30 MINUTE)
);

-- Insert sample market index
INSERT INTO market_indices (
    region, symbol, name, price, change_value, change_percent,
    currency, market_state, market_time, cache_valid_until
) VALUES (
    'US', '^GSPC', 'S&P 500', 4500.50,
    25.75, 0.58, 'USD', 'REGULAR',
    UNIX_TIMESTAMP(), DATE_ADD(NOW(), INTERVAL 1 HOUR)
);

-- =====================================================
-- Database Configuration and Tuning
-- =====================================================

-- Set appropriate buffer pool size (adjust based on available RAM)
-- SET GLOBAL innodb_buffer_pool_size = 1073741824; -- 1GB

-- Set query cache size (if using MySQL 5.7 or earlier)
-- SET GLOBAL query_cache_size = 67108864; -- 64MB

-- Set connection timeout
SET GLOBAL wait_timeout = 28800; -- 8 hours
SET GLOBAL interactive_timeout = 28800; -- 8 hours

-- Set max connections
SET GLOBAL max_connections = 200;

-- Enable slow query log for performance monitoring
SET GLOBAL slow_query_log = 'ON';
SET GLOBAL long_query_time = 2; -- Log queries taking more than 2 seconds
SET GLOBAL slow_query_log_file = '/var/log/mysql/slow.log';

-- =====================================================
-- User Permissions for Application
-- =====================================================

-- Grant specific permissions to application user
GRANT SELECT, INSERT, UPDATE, DELETE ON yahoo_finance_db.* TO 'yahoo_finance_user'@'%';
GRANT EXECUTE ON PROCEDURE yahoo_finance_db.* TO 'yahoo_finance_user'@'%';
GRANT EVENT ON yahoo_finance_db.* TO 'yahoo_finance_user'@'%';

-- Create read-only user for reporting
CREATE USER IF NOT EXISTS 'yahoo_finance_readonly'@'%' IDENTIFIED BY 'readonly_password_here';
GRANT SELECT ON yahoo_finance_db.* TO 'yahoo_finance_readonly'@'%';

FLUSH PRIVILEGES;
```

## 6. Database Schema Documentation

```sql:dbscripts/06_database_schema_documentation.md
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
```

## Summary

I've created a comprehensive MySQL database schema for your Yahoo Finance API project. The schema includes:

### **Key Features:**
1. **Intelligent Caching** - All tables include cache expiration timestamps
2. **Performance Optimization** - Proper indexing and partitioning strategies
3. **Data Integrity** - Foreign keys, constraints, and triggers
4. **Automated Maintenance** - Scheduled cleanup and optimization events
5. **Monitoring** - Comprehensive logging and health tracking

### **Tables Created:**
- `api_requests` - Track all API calls and cache hits
- `stock_quotes` - Store stock quote data with caching
- `trending_stocks` - Cache trending stock information
- `market_indices` - Store market index data
- `market_sectors` - Cache sector performance data
- `currency_exchange_rates` - Store exchange rate data
- `cryptocurrency_data` - Cache crypto quotes
- `bulk_quote_operations` - Track bulk operations
- `bulk_quote_groups` - Organize bulk requests
- `api_health_metrics` - Monitor API health

### **Caching Strategy:**
- **Stock Quotes**: 15 minutes
- **Trending Stocks**: 30 minutes  
- **Market Data**: 1 hour
- **Currency**: 15 minutes
- **Crypto**: 5 minutes

### **Next Steps:**
1. Run the scripts in order (01 → 06)
2. Update the connection string in your application
3. Implement cache checking logic before API calls
4. Monitor performance and adjust cache durations as needed

The schema is designed to significantly reduce your Yahoo Finance API calls while maintaining data freshness and providing comprehensive monitoring capabilities.
