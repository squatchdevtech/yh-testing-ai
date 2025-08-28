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
