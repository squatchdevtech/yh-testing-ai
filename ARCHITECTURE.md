# Architecture Documentation

## Overview

This document describes the architecture of the YH Finance API project, which follows clean architecture principles with proper separation of concerns.

## Architecture Layers

### 1. **Presentation Layer** (`Controllers/`)
- **Responsibility**: HTTP request/response handling, input validation, error handling
- **Components**:
  - `YHController`: Handles Yahoo Finance API endpoints
  - `HealthCheckController`: Basic health monitoring

### 2. **Service Layer** (`Services/`)
- **Responsibility**: Business logic, external API integration, data processing
- **Components**:
  - `IYahooFinanceService` / `YahooFinanceService`: Yahoo Finance API integration
  - `IAwsParameterStoreService` / `AwsParameterStoreService`: AWS Parameter Store integration

### 3. **Infrastructure Layer**
- **Responsibility**: External dependencies, configuration, cross-cutting concerns
- **Components**:
  - HttpClient for API calls
  - AWS SDK for Parameter Store
  - Logging framework
  - Configuration system

## Dependency Injection

The application uses ASP.NET Core's built-in dependency injection container:

```csharp
// AWS Services
builder.Services.AddAWSService<IAmazonSimpleSystemsManagement>();
builder.Services.AddScoped<IAwsParameterStoreService, AwsParameterStoreService>();

// Business Services
builder.Services.AddScoped<IYahooFinanceService, YahooFinanceService>();

// Infrastructure
builder.Services.AddHttpClient();
```

## Service Responsibilities

### YahooFinanceService
- **Purpose**: Encapsulates all Yahoo Finance API interactions
- **Responsibilities**:
  - API key management and header configuration
  - HTTP request/response handling
  - Data validation and parsing
  - Error handling and logging
  - Region validation

### AwsParameterStoreService
- **Purpose**: Secure configuration management via AWS Parameter Store
- **Responsibilities**:
  - Parameter retrieval with decryption
  - Error handling for AWS operations
  - Logging and monitoring

### YHController
- **Purpose**: HTTP API contract and orchestration
- **Responsibilities**:
  - Request validation
  - Service orchestration
  - Response formatting
  - Exception handling and HTTP status codes

## Data Flow

```
[Client Request] 
    ↓
[YHController] 
    ↓
[YahooFinanceService] 
    ↓
[AwsParameterStoreService] → [AWS Parameter Store]
    ↓
[HttpClient] → [Yahoo Finance API]
    ↓
[Response Processing]
    ↓
[JSON Response to Client]
```

## Key Design Patterns

### 1. **Repository Pattern**
- Services act as repositories for external data sources
- Abstracted behind interfaces for testability

### 2. **Dependency Injection**
- Loose coupling between components
- Easy testing and mocking
- Configurable service lifetimes

### 3. **Single Responsibility Principle**
- Each service has one clear purpose
- Controllers only handle HTTP concerns
- Services handle business logic

### 4. **Interface Segregation**
- Clean interfaces with focused responsibilities
- Easy to mock for testing
- Clear contracts between layers

## Error Handling Strategy

### Service Layer
- Throws specific exceptions for different error types:
  - `ArgumentException` for validation errors
  - `HttpRequestException` for network issues
  - `InvalidOperationException` for data processing errors

### Controller Layer
- Catches service exceptions and maps to appropriate HTTP responses:
  - `400 Bad Request` for validation errors
  - `500 Internal Server Error` for system errors
- Logs all errors with appropriate levels

## Configuration Management

### Hierarchical Configuration
1. **AWS Parameter Store** (Production)
   - Secure, encrypted storage
   - Centralized configuration management
   - Supports key rotation

2. **appsettings.json** (Fallback)
   - Local development
   - Environment-specific overrides
   - Default values

### Environment-Specific Settings
- `appsettings.json`: Base configuration
- `appsettings.Development.json`: Development overrides
- `appsettings.Production.json`: Production overrides
- Environment variables: Runtime overrides

## Security Considerations

### API Key Management
- **Production**: Stored in AWS Parameter Store as SecureString
- **Development**: Local configuration files (excluded from source control)
- **Runtime**: Dynamically retrieved for each request

### Access Control
- IAM roles for AWS services (no hardcoded credentials)
- Least privilege principle for Parameter Store access
- Non-root container execution

### Logging
- No sensitive data in logs
- Structured logging with correlation IDs
- Appropriate log levels for different environments

## Testing Strategy

### Unit Testing
- Service layer fully testable via interfaces
- Mock external dependencies (HttpClient, AWS services)
- Test business logic in isolation

### Integration Testing
- Test controller endpoints end-to-end
- Test AWS Parameter Store integration
- Test Yahoo Finance API integration

### Health Monitoring
- `/api/healthcheck`: Basic application health
- `/api/yh/health`: Yahoo Finance service health
- Container health checks for orchestration

## Performance Considerations

### HTTP Client Management
- Single HttpClient instance via DI
- Connection pooling and reuse
- Proper disposal via DI container

### Caching Strategy
- API key cached in memory per request
- Consider implementing response caching for frequently requested data
- AWS Parameter Store calls minimized

### Async/Await
- All I/O operations are asynchronous
- Proper async patterns throughout the stack
- Non-blocking request processing

## Deployment Architecture

### Containerization
- Multi-stage Docker builds for optimization
- Non-root container execution
- Health check endpoints for orchestration

### Cloud Native
- Stateless design for horizontal scaling
- External configuration via Parameter Store
- Logging to stdout for container orchestration

### Monitoring
- Structured logging with correlation
- Health check endpoints
- AWS CloudWatch integration ready

## Future Enhancements

### Potential Improvements
1. **Caching Layer**: Redis for response caching
2. **Rate Limiting**: Protect against API abuse
3. **Circuit Breaker**: Resilience for external API calls
4. **Metrics**: Prometheus/StatsD metrics collection
5. **Authentication**: OAuth/JWT for API security
6. **API Versioning**: Support multiple API versions

### Scalability Considerations
- Database abstraction layer for persistent data
- Message queues for async processing
- Load balancing and auto-scaling ready
- Multi-region deployment support
