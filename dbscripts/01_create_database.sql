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

-- Create database user with appropriate permissions
-- Note: Replace 'your_password_here' with a secure password
CREATE USER IF NOT EXISTS 'yahoo_finance_user'@'localhost' IDENTIFIED BY 'mysql4rfv%TGB4rfv%TGB';
GRANT ALL PRIVILEGES ON yahoo_finance_db.* TO 'yahoo_finance_user'@'localhost';
GRANT ALL PRIVILEGES ON yahoo_finance_db.* TO 'yahoo_finance_user'@'%';
FLUSH PRIVILEGES;

-- Set timezone to UTC for consistent timestamp handling
SET time_zone = '+00:00';
