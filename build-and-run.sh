#!/bin/bash

# Build and run the WebApi project with Docker Compose

echo "🐳 Building and running WebApi Project with Docker..."

# Stop any existing containers
echo "🛑 Stopping existing containers..."
docker-compose down

# Build and start the application
echo "🔨 Building and starting the application..."
docker-compose up --build -d

# Wait a moment for the container to start
echo "⏳ Waiting for container to start..."
sleep 10

# Check container status
echo "📊 Container status:"
docker-compose ps

# Test the health endpoint
echo "🏥 Testing health endpoint..."
curl -s http://localhost:8080/api/healthcheck | jq '.' || echo "❌ Health check failed or jq not installed"

echo ""
echo "✅ Application should be running at http://localhost:8080"
echo "📖 API Documentation available at http://localhost:8080/swagger"
echo ""
echo "🔍 Useful commands:"
echo "  View logs: docker-compose logs -f webapi"
echo "  Stop app:  docker-compose down"
echo "  Restart:   docker-compose restart"
