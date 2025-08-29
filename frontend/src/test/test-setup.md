# Testing Infrastructure Setup

## Overview
This project uses Vitest as the testing framework with React Testing Library for component testing. The testing setup is designed to provide a comprehensive testing environment for the Yahoo Finance Frontend application.

## Testing Stack

### Core Testing Framework
- **Vitest**: Fast unit testing framework with Vite integration
- **React Testing Library**: Component testing utilities
- **jsdom**: DOM environment for testing
- **@testing-library/jest-dom**: Custom matchers for DOM testing

### Additional Tools
- **@vitest/ui**: Visual test runner interface
- **@vitest/coverage-v8**: Code coverage reporting
- **@testing-library/user-event**: User interaction simulation

## Configuration Files

### vitest.config.ts
- Configures Vitest with React plugin
- Sets up jsdom environment
- Configures path aliases
- Sets up coverage reporting
- Configures test setup files

### src/test/setup.ts
- Global test setup and teardown
- Mock implementations for browser APIs
- Console error/warning filtering
- Global test utilities

### src/test/test-utils.tsx
- Custom render function with providers
- Mock data for testing
- Common test utilities
- Provider wrapper components

## Test File Structure

```
src/
├── components/
│   ├── common/
│   │   └── __tests__/
│   │       ├── Header.test.tsx
│   │       └── Sidebar.test.tsx
│   └── dashboard/
│       └── __tests__/
│           └── Dashboard.test.tsx
├── hooks/
│   └── __tests__/
│       └── useStockQuote.test.tsx
├── services/
│   └── __tests__/
│       └── api.test.ts
├── utils/
│   └── __tests__/
│       └── formatters.test.ts
└── test/
    ├── setup.ts
    ├── test-utils.tsx
    └── test-setup.md
```

## Running Tests

### Available Scripts
```bash
# Run tests in watch mode (development)
npm test

# Run tests once
npm run test:run

# Run tests with coverage
npm run test:coverage

# Run tests with UI interface
npm run test:ui

# Run tests in watch mode
npm run test:watch
```

### Test Commands
```bash
# Run all tests
npm run test:run

# Run tests for specific file
npm run test:run -- src/components/common/__tests__/Header.test.tsx

# Run tests with specific pattern
npm run test:run -- --grep "Header"

# Run tests with coverage
npm run test:coverage

# Run tests in UI mode
npm run test:ui
```

## Test Writing Guidelines

### Component Testing
- Use `render` from test-utils for consistent provider setup
- Test user interactions with `fireEvent` or `userEvent`
- Focus on behavior, not implementation details
- Use semantic queries (getByRole, getByLabelText, etc.)

### Hook Testing
- Use `renderHook` for testing custom hooks
- Mock dependencies appropriately
- Test different states (loading, success, error)
- Verify side effects and cleanup

### Service Testing
- Mock external dependencies (axios, API calls)
- Test success and error scenarios
- Verify correct API calls and parameters
- Test error handling and edge cases

### Utility Testing
- Test edge cases and boundary conditions
- Verify correct formatting and validation
- Test with various input types
- Ensure consistent output format

## Mock Data

### Available Mock Data
- `mockQuoteResponse`: Stock quote data
- `mockTrendingResponse`: Trending stocks data
- `mockMarketSummaryResponse`: Market summary data
- `mockHealthResponse`: API health data

### Using Mock Data
```typescript
import { mockQuoteResponse } from '../../test/test-utils'

// Use in tests
mockUseStockQuote.mockReturnValue({
  data: mockQuoteResponse,
  isLoading: false,
  isError: false,
  error: null,
  refetch: vi.fn(),
})
```

## Testing Best Practices

### Test Organization
- Group related tests with `describe` blocks
- Use descriptive test names
- Follow AAA pattern (Arrange, Act, Assert)
- Keep tests focused and isolated

### Mocking Strategy
- Mock at the right level (API calls, not internal logic)
- Use `vi.mock()` for module mocking
- Create reusable mock implementations
- Reset mocks between tests

### Assertions
- Use specific assertions (toBe, toContain, etc.)
- Test user-visible behavior
- Avoid testing implementation details
- Use custom matchers when appropriate

### Error Handling
- Test error states and edge cases
- Verify error messages and UI feedback
- Test loading states and transitions
- Ensure graceful degradation

## Coverage Goals

### Target Coverage
- **Statements**: 80%+
- **Branches**: 75%+
- **Functions**: 80%+
- **Lines**: 80%+

### Coverage Exclusions
- Test files and setup
- Configuration files
- Type definitions
- Build artifacts

## Troubleshooting

### Common Issues
1. **Provider Setup**: Ensure components are wrapped with necessary providers
2. **Mock Dependencies**: Check that all external dependencies are properly mocked
3. **Async Operations**: Use `waitFor` for asynchronous operations
4. **DOM Environment**: Ensure jsdom is properly configured

### Debug Tips
- Use `screen.debug()` to inspect rendered output
- Check console for unmocked dependencies
- Verify test setup and teardown
- Use `--reporter=verbose` for detailed output

## Continuous Integration

### CI/CD Integration
- Tests run on every commit
- Coverage reports generated
- Test results reported to CI system
- Failures block deployment

### Pre-commit Hooks
- Run tests before commit
- Ensure coverage thresholds met
- Lint and format code
- Type checking validation
