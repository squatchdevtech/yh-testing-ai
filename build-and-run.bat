@echo off
REM Build and run the WebApi project with Docker Compose (Windows batch file)

echo 🐳 Building and running WebApi Project with Docker...

REM Stop any existing containers
echo 🛑 Stopping existing containers...
docker-compose down

REM Build and start the application
echo 🔨 Building and starting the application...
docker-compose up --build -d

REM Wait a moment for the container to start
echo ⏳ Waiting for container to start...
timeout /t 10 /nobreak > nul

REM Check container status
echo 📊 Container status:
docker-compose ps

REM Test the health endpoint
echo 🏥 Testing health endpoint...
curl -s http://localhost:8080/api/healthcheck

echo.
echo ✅ Application should be running at http://localhost:8080
echo 📖 API Documentation available at http://localhost:8080/swagger
echo.
echo 🔍 Useful commands:
echo   View logs: docker-compose logs -f webapi
echo   Stop app:  docker-compose down
echo   Restart:   docker-compose restart
echo.
pause
