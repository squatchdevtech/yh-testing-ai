# WebApiProject-v1

A comprehensive .NET 9 Web API for Yahoo Finance data integration with AWS Parameter Store support.

## Overview

This project provides a robust API for accessing financial data through Yahoo Finance with enhanced features including:

- **Real-time stock quotes** with comprehensive financial data
- **Trending stocks** by market region
- **Currency exchange rates** with real-time updates
- **Cryptocurrency data** with market metrics
- **Bulk quote processing** for portfolio management
- **Market summaries** with indices and sector performance

## Features

### 🎯 Core API Endpoints

- **GET** `/api/yh/quote` - Real-time stock quotes
- **POST** `/api/yh/quote` - Bulk quote requests via JSON
- **GET** `/api/yh/trending/{region}` - Trending stocks by region
- **GET** `/api/yh/currency-exchange/{from}/{to}` - Currency exchange rates
- **GET** `/api/yh/crypto` - Cryptocurrency quotes
- **POST** `/api/yh/bulk-quotes` - Organized bulk quote processing
- **GET** `/api/yh/capabilities` - API capabilities and documentation
- **GET** `/api/yh/health` - Health check with system status

### 🔧 Technical Features

- **Strongly-typed DTOs** for all requests and responses
- **Result<T> pattern** for explicit error handling
- **AWS Parameter Store** integration for secure API key management
- **Comprehensive validation** with data annotations
- **Docker support** with multi-stage builds
- **Swagger/OpenAPI** documentation

### 🌍 Supported Regions

`US`, `AU`, `CA`, `FR`, `DE`, `HK`, `IT`, `ES`, `GB`, `IN`

### 🗣️ Supported Languages

`en`, `fr`, `de`, `it`, `es`, `zh`

## Quick Start

### Prerequisites

- .NET 9.0 SDK
- AWS CLI configured (for Parameter Store)
- Docker (optional)

### Running Locally

1. **Clone the repository**
   ```bash
   git clone https://github.com/[username]/WebApiProject-v1.git
   cd WebApiProject-v1
   ```

2. **Configure API Key**
   
   **Option 1: AWS Parameter Store (Recommended)**
   ```bash
   aws ssm put-parameter \
     --name "/WebApiProject/YfApi/ApiKey" \
     --value "your-yahoo-finance-api-key" \
     --type "SecureString"
   ```

   **Option 2: Local Configuration**
   ```json
   // appsettings.Development.json
   {
     "YfApi": {
       "ApiKey": "your-local-api-key-for-testing"
     }
   }
   ```

3. **Run the application**
   ```bash
   dotnet run
   ```

4. **Access Swagger UI**
   Navigate to `https://localhost:5001/swagger`

### Docker Deployment

```bash
# Build and run with Docker Compose
docker-compose up --build

# Or build manually
docker build -t webapi-project .
docker run -p 5000:8080 webapi-project
```

## API Examples

### Stock Quotes
```http
GET /api/yh/quote?symbols=AAPL,GOOGL,MSFT&region=US&lang=en
```

### Currency Exchange
```http
GET /api/yh/currency-exchange/USD/EUR
```

### Cryptocurrency
```http
GET /api/yh/crypto?symbols=BTC,ETH&currency=USD
```

### Bulk Quotes
```http
POST /api/yh/bulk-quotes
Content-Type: application/json

{
  "symbolGroups": [
    {
      "groupName": "Tech Stocks",
      "symbols": ["AAPL", "GOOGL", "MSFT"]
    },
    {
      "groupName": "Energy",
      "symbols": ["XOM", "CVX"]
    }
  ],
  "region": "US"
}
```

## Architecture

### Service Layer
- **YahooFinanceService** - Core financial data operations
- **AwsParameterStoreService** - Secure configuration management
- **YahooFinanceMapper** - JSON to DTO transformation

### Controllers
- **YHController** - Main financial data endpoints
- **HealthCheckController** - Basic health monitoring

### DTOs
- **Request DTOs** - Input validation and transformation
- **Response DTOs** - Structured output with comprehensive data
- **Error DTOs** - Consistent error handling

## Configuration

### AWS Settings
```json
{
  "AWS": {
    "Region": "us-east-1"
  }
}
```

### Yahoo Finance API
```json
{
  "YfApi": {
    "ApiKey": "your-api-key"
  }
}
```

## Error Handling

The API uses a comprehensive `Result<T>` pattern for error handling:

```json
{
  "error": {
    "code": "VALIDATION_REQUIRED",
    "message": "Symbols parameter is required"
  },
  "timestamp": "2024-01-15T10:30:00Z"
}
```

## Rate Limits

- **100** requests per minute
- **2,000** requests per hour
- **10,000** requests per day
- **Burst limit**: 10 requests

## Contributing

1. Fork the repository
2. Create a feature branch
3. Follow the functional C# coding guidelines
4. Add unit tests for new features
5. Submit a pull request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Support

For support and questions:
- Create an issue in the GitHub repository
- Check the `/api/yh/capabilities` endpoint for API documentation
- Review the Swagger UI for interactive documentation
