# Yahoo Finance Dashboard Frontend

A modern, responsive React TypeScript application that provides a comprehensive interface for the Yahoo Finance API backend.

## 🚀 Features

### Core Functionality
- **Real-time Stock Quotes**: Search and display stock information with live updates
- **Trending Stocks**: View trending stocks by region with performance metrics
- **Market Summary**: Comprehensive market overview with indices and sector performance
- **Multi-region Support**: Support for US, Canada, UK, Germany, France, Japan, and Australia
- **Multi-language Support**: Interface available in English, Spanish, French, German, Italian, and Portuguese

### Technical Features
- **Modern React**: Built with React 18 and TypeScript
- **Material-UI**: Professional, accessible UI components
- **React Query**: Efficient data fetching with caching and real-time updates
- **Responsive Design**: Mobile-first approach with responsive breakpoints
- **Type Safety**: Full TypeScript implementation with strict type checking
- **Error Handling**: Comprehensive error handling with user-friendly messages
- **Loading States**: Skeleton loaders and progress indicators for better UX

## 🛠️ Technology Stack

- **Frontend Framework**: React 18 with TypeScript
- **UI Library**: Material-UI (MUI) v5
- **State Management**: React Query (TanStack Query)
- **HTTP Client**: Axios
- **Build Tool**: Vite
- **Routing**: React Router v6
- **Styling**: Emotion (CSS-in-JS)
- **Icons**: Material Icons
- **Testing**: Vitest + React Testing Library

## 📁 Project Structure

```
src/
├── components/           # React components
│   ├── common/          # Shared components (Header, Sidebar)
│   ├── dashboard/       # Main dashboard components
│   └── financial/       # Financial data components
├── hooks/               # Custom React hooks
├── services/            # API service layer
├── types/               # TypeScript type definitions
├── utils/               # Utility functions
├── App.tsx              # Main application component
├── main.tsx             # Application entry point
└── index.css            # Global styles
```

## 🚀 Getting Started

### Prerequisites
- Node.js 18+ 
- npm or yarn
- Backend API running (see backend documentation)

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd frontend
   ```

2. **Install dependencies**
   ```bash
   npm install
   ```

3. **Configure environment**
   - The frontend is configured to proxy API requests to `http://localhost:8080`
   - Update the proxy configuration in `vite.config.ts` if your backend runs on a different port

4. **Start development server**
   ```bash
   npm run dev
   ```

5. **Open your browser**
   - Navigate to `http://localhost:3000`
   - The application will automatically reload when you make changes

### Available Scripts

- `npm run dev` - Start development server
- `npm run build` - Build for production
- `npm run preview` - Preview production build
- `npm run test` - Run tests
- `npm run lint` - Run ESLint

## 🔧 Configuration

### API Configuration
The frontend is configured to communicate with the backend API through a proxy:

```typescript
// vite.config.ts
server: {
  proxy: {
    '/api': {
      target: 'http://localhost:8080',
      changeOrigin: true,
      secure: false,
    },
  },
}
```

### Environment Variables
Create a `.env` file in the frontend root directory:

```env
VITE_API_BASE_URL=http://localhost:8080
VITE_APP_TITLE=Yahoo Finance Dashboard
```

## 📱 Responsive Design

The application is built with a mobile-first approach:

- **Mobile**: 320px - 767px
- **Tablet**: 768px - 1023px  
- **Desktop**: 1024px+

### Breakpoint System
- `xs`: 0px and up
- `sm`: 600px and up
- `md`: 900px and up
- `lg`: 1200px and up
- `xl`: 1536px and up

## 🎨 UI Components

### Material-UI Theme
Custom theme configuration with:
- Primary color: Blue (#1976d2)
- Secondary color: Pink (#dc004e)
- Custom card shadows and border radius
- Responsive typography

### Component Library
- **Cards**: Information display with consistent styling
- **Tables**: Data presentation with sorting and filtering
- **Forms**: Input controls with validation
- **Navigation**: Sidebar navigation with active states
- **Charts**: Data visualization components (planned)

## 🔄 Data Flow

1. **User Interaction**: User interacts with UI components
2. **Hook Calls**: Custom hooks trigger API requests
3. **API Service**: Axios-based service layer makes HTTP requests
4. **Backend**: Yahoo Finance API processes requests
5. **Response**: Data flows back through the chain
6. **UI Update**: Components re-render with new data

### State Management
- **Local State**: Component-level state with useState
- **Server State**: API data managed by React Query
- **Global State**: Application-wide state (planned with Context/Redux)

## 🧪 Testing

### Test Setup
- **Unit Tests**: Component testing with React Testing Library
- **Integration Tests**: API integration testing
- **E2E Tests**: End-to-end testing (planned with Playwright)

### Running Tests
```bash
npm run test          # Run tests in watch mode
npm run test:ui       # Run tests with UI
npm run test:coverage # Run tests with coverage report
```

## 🚀 Deployment

### Build for Production
```bash
npm run build
```

### Deployment Options
- **Static Hosting**: Netlify, Vercel, GitHub Pages
- **Container**: Docker deployment
- **CDN**: CloudFront, Cloudflare

### Docker Deployment
```dockerfile
FROM node:18-alpine as builder
WORKDIR /app
COPY package*.json ./
RUN npm ci --only=production
COPY . .
RUN npm run build

FROM nginx:alpine
COPY --from=builder /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
```

## 🔒 Security Considerations

- **API Key Management**: Backend handles API key storage
- **CORS Configuration**: Proper CORS setup in backend
- **Input Validation**: Client-side validation with backend verification
- **HTTPS**: Production deployment with SSL certificates

## 📊 Performance Optimization

- **Code Splitting**: Route-based code splitting
- **Lazy Loading**: Component lazy loading
- **Caching**: React Query caching strategies
- **Bundle Optimization**: Vite build optimization
- **Image Optimization**: Lazy loading and compression

## 🌐 Browser Support

- **Modern Browsers**: Chrome 90+, Firefox 88+, Safari 14+, Edge 90+
- **Mobile Browsers**: iOS Safari 14+, Chrome Mobile 90+
- **Progressive Enhancement**: Core functionality works without JavaScript

## 🤝 Contributing

### Development Workflow
1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests for new functionality
5. Ensure all tests pass
6. Submit a pull request

### Code Standards
- **TypeScript**: Strict mode enabled
- **ESLint**: Code quality enforcement
- **Prettier**: Code formatting
- **Conventional Commits**: Commit message format

## 📝 API Documentation

### Endpoints Used
- `GET /api/yh/quote` - Stock quotes
- `GET /api/yh/trending/{region}` - Trending stocks
- `GET /api/yh/market-summary/{region}` - Market summary
- `GET /api/yh/health` - API health check

### Data Models
See `src/types/api.ts` for complete TypeScript interfaces matching backend DTOs.

## 🐛 Troubleshooting

### Common Issues

1. **API Connection Errors**
   - Verify backend is running on port 8080
   - Check network connectivity
   - Verify CORS configuration

2. **Build Errors**
   - Clear node_modules and reinstall
   - Check TypeScript version compatibility
   - Verify all dependencies are installed

3. **Runtime Errors**
   - Check browser console for errors
   - Verify API responses match expected format
   - Check React Query dev tools

### Debug Mode
Enable debug logging in the browser console:
```typescript
// In api.ts
console.log('API Request:', config);
console.log('API Response:', response);
```

## 📈 Future Enhancements

### Planned Features
- **Real-time Charts**: Interactive price charts with Chart.js
- **Portfolio Management**: User portfolio tracking
- **Alerts & Notifications**: Price alerts and news
- **Advanced Filtering**: Enhanced search and filtering
- **Dark Mode**: Theme switching capability
- **PWA Support**: Progressive web app features

### Technical Improvements
- **WebSocket Integration**: Real-time price updates
- **Service Workers**: Offline functionality
- **Performance Monitoring**: Analytics and metrics
- **Accessibility**: Enhanced screen reader support

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 🙏 Acknowledgments

- **Yahoo Finance**: Financial data API
- **Material-UI**: UI component library
- **React Query**: Data fetching library
- **Vite**: Build tool and development server

---

For backend documentation, see the main project README.
