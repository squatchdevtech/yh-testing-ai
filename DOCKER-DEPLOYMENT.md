# Docker Deployment Guide

This guide explains how to run your YH Finance API project using Docker.

## Prerequisites

- Docker installed and running
- Docker Compose (usually included with Docker Desktop)
- Your YH Finance API key stored in AWS Parameter Store (see AWS-SETUP.md)

## Quick Start

### Option 1: Using Docker Compose (Recommended)

1. **Build and run the application:**
```bash
docker-compose up --build
```

2. **Run in detached mode:**
```bash
docker-compose up -d --build
```

3. **Stop the application:**
```bash
docker-compose down
```

### Option 2: Using Docker Commands

1. **Build the image:**
```bash
docker build -t webapi-project .
```

2. **Run the container:**
```bash
docker run -d \
  --name webapi-project \
  -p 8080:8080 \
  -e ASPNETCORE_ENVIRONMENT=Production \
  -e AWS_REGION=us-east-1 \
  webapi-project
```

## Configuration Options

### Environment Variables

The following environment variables can be configured:

| Variable | Description | Default |
|----------|-------------|---------|
| `ASPNETCORE_ENVIRONMENT` | Application environment | `Production` |
| `ASPNETCORE_URLS` | URLs the app listens on | `http://+:8080` |
| `AWS_REGION` | AWS region for Parameter Store | `us-east-1` |
| `AWS_ACCESS_KEY_ID` | AWS access key (if not using IAM roles) | - |
| `AWS_SECRET_ACCESS_KEY` | AWS secret key (if not using IAM roles) | - |

### AWS Credentials Configuration

#### Option A: IAM Roles (Recommended for production)
When running on AWS services (EC2, ECS, Fargate), use IAM roles. No additional configuration needed.

#### Option B: AWS Credentials File (Development)
Mount your AWS credentials directory:

```yaml
volumes:
  - ~/.aws:/home/appuser/.aws:ro
```

#### Option C: Environment Variables
Set AWS credentials directly in docker-compose.yml:

```yaml
environment:
  - AWS_ACCESS_KEY_ID=your-access-key-id
  - AWS_SECRET_ACCESS_KEY=your-secret-access-key
```

⚠️ **Warning**: Never commit credentials to source control!

## Production Deployment

### Docker Compose for Production

Create a `docker-compose.prod.yml`:

```yaml
version: '3.8'

services:
  webapi:
    build:
      context: .
      dockerfile: Dockerfile
    container_name: webapi-project-prod
    ports:
      - "80:8080"
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
      - AWS_REGION=us-east-1
    restart: always
    healthcheck:
      test: ["CMD", "curl", "-f", "http://localhost:8080/api/healthcheck"]
      interval: 30s
      timeout: 10s
      retries: 3
      start_period: 40s
    logging:
      driver: "json-file"
      options:
        max-size: "10m"
        max-file: "3"
```

Deploy with:
```bash
docker-compose -f docker-compose.prod.yml up -d
```

### Container Orchestration

#### AWS ECS
1. Push image to Amazon ECR
2. Create ECS task definition
3. Configure IAM roles for Parameter Store access
4. Deploy to ECS service

#### Kubernetes
Create deployment manifests with:
- Service account with AWS IAM roles
- ConfigMap for non-sensitive configuration
- Deployment with health checks

## Testing the Deployment

1. **Check container status:**
```bash
docker-compose ps
```

2. **View logs:**
```bash
docker-compose logs -f webapi
```

3. **Test health endpoint:**
```bash
curl http://localhost:8080/api/healthcheck
curl http://localhost:8080/api/yh/health
```

4. **Test YH Finance API:**
```bash
# Get Apple stock quote
curl "http://localhost:8080/api/yh/quote?symbols=AAPL"

# Get trending US stocks
curl http://localhost:8080/api/yh/trending/US
```

## Monitoring and Logs

### View Real-time Logs
```bash
docker-compose logs -f webapi
```

### Container Stats
```bash
docker stats webapi-project
```

### Health Check Status
```bash
docker inspect --format='{{json .State.Health}}' webapi-project
```

## Troubleshooting

### Common Issues

1. **Container fails to start**
   - Check logs: `docker-compose logs webapi`
   - Verify AWS credentials and permissions
   - Ensure Parameter Store contains the API key

2. **AWS Parameter Store access denied**
   - Verify IAM permissions for Parameter Store
   - Check AWS region configuration
   - Validate AWS credentials

3. **Health check failing**
   - Verify application is listening on port 8080
   - Check if health endpoint is accessible
   - Review application logs for errors

4. **API key not found**
   - Verify Parameter Store parameter exists: `/WebApiProject/YfApi/ApiKey`
   - Check AWS region matches parameter location
   - Ensure parameter is SecureString type

### Debug Commands

```bash
# Execute shell in running container
docker exec -it webapi-project bash

# Check AWS configuration in container
docker exec -it webapi-project aws sts get-caller-identity

# Test Parameter Store access
docker exec -it webapi-project aws ssm get-parameter --name "/WebApiProject/YfApi/ApiKey" --with-decryption
```

## Performance Optimization

### Multi-stage Build
The Dockerfile uses multi-stage builds to minimize image size:
- Build stage: Uses SDK image for compilation
- Runtime stage: Uses smaller runtime image

### Security
- Runs as non-root user (`appuser`)
- Minimal base image (aspnet runtime)
- No development tools in production image

### Resource Limits
Add resource limits in docker-compose.yml:

```yaml
services:
  webapi:
    deploy:
      resources:
        limits:
          memory: 512M
          cpus: '0.5'
        reservations:
          memory: 256M
          cpus: '0.25'
```

## Backup and Recovery

### Container Data
Important paths to backup:
- Application logs: `/app/logs`
- Configuration: Environment variables and Parameter Store

### Disaster Recovery
1. Store Docker images in a registry (ECR, Docker Hub)
2. Use Infrastructure as Code (Terraform, CloudFormation)
3. Automate Parameter Store backup
4. Document deployment procedures

## Scaling

### Horizontal Scaling
```bash
docker-compose up --scale webapi=3
```

### Load Balancing
Use a reverse proxy like Nginx or AWS Application Load Balancer to distribute traffic across multiple containers.

## Security Best Practices

1. **Use IAM roles** instead of access keys when possible
2. **Keep base images updated** regularly
3. **Scan images** for vulnerabilities
4. **Use secrets management** for sensitive data
5. **Run as non-root user** (already implemented)
6. **Limit container capabilities** and use read-only filesystems when possible
