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
