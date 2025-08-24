# AWS Parameter Store Setup Guide

This guide will help you set up AWS Systems Manager Parameter Store to securely store your YH Finance API key.

## Prerequisites

1. AWS Account with appropriate permissions
2. AWS CLI installed and configured
3. YH Finance API key from the service provider

## Step 1: Configure AWS Credentials

### Option A: AWS CLI Profile (Recommended for local development)
```bash
aws configure
```
Enter your AWS Access Key ID, Secret Access Key, region (e.g., us-east-1), and output format.

### Option B: IAM Role (Recommended for production)
If running on EC2, ECS, or Lambda, use IAM roles instead of access keys.

## Step 2: Create Parameter in AWS Parameter Store

### Via AWS CLI:
```bash
aws ssm put-parameter \
    --name "/WebApiProject/YfApi/ApiKey" \
    --value "YOUR_YH_FINANCE_API_KEY_HERE" \
    --type "SecureString" \
    --description "YH Finance API Key for WebApiProject"
```

### Via AWS Console:
1. Go to AWS Systems Manager → Parameter Store
2. Click "Create parameter"
3. Name: `/WebApiProject/YfApi/ApiKey`
4. Type: `SecureString`
5. Value: Your YH Finance API key
6. Description: `YH Finance API Key for WebApiProject`
7. Click "Create parameter"

## Step 3: Set Up IAM Permissions

Your application needs permissions to read from Parameter Store. Create an IAM policy:

```json
{
    "Version": "2012-10-17",
    "Statement": [
        {
            "Effect": "Allow",
            "Action": [
                "ssm:GetParameter",
                "ssm:GetParameters"
            ],
            "Resource": "arn:aws:ssm:*:*:parameter/WebApiProject/*"
        }
    ]
}
```

Attach this policy to:
- Your IAM user (for local development)
- Your EC2 instance role (for EC2 deployment)
- Your ECS task role (for ECS deployment)
- Your Lambda execution role (for Lambda deployment)

## Step 4: Configure AWS Region

Update `appsettings.json` to specify your AWS region:

```json
{
  "AWS": {
    "Region": "us-east-1"
  }
}
```

## Step 5: Test the Setup

1. Run your application
2. Navigate to `GET /api/yh/health`
3. Check the response - it should show:
   - `ApiKeyConfigured: true`
   - `ApiKeySource: "AWS Parameter Store"`

## Fallback for Local Development

For local development, you can still use `appsettings.Development.json`:

```json
{
  "YfApi": {
    "ApiKey": "your-dev-api-key-here"
  }
}
```

The application will try Parameter Store first, then fall back to configuration.

## Security Best Practices

1. **Never commit API keys to source control**
2. **Use SecureString type** in Parameter Store for encryption at rest
3. **Use IAM roles** instead of access keys when possible
4. **Follow least privilege principle** for IAM permissions
5. **Rotate API keys regularly**
6. **Monitor Parameter Store access** via CloudTrail

## Troubleshooting

### Common Issues:

1. **"Parameter not found"**: Verify the parameter name exactly matches `/WebApiProject/YfApi/ApiKey`
2. **"Access Denied"**: Check IAM permissions for Parameter Store access
3. **"Invalid region"**: Ensure AWS region is correctly configured
4. **"Network error"**: Verify AWS connectivity and credentials

### Debug Steps:

1. Check application logs for AWS-related errors
2. Verify AWS credentials: `aws sts get-caller-identity`
3. Test parameter access: `aws ssm get-parameter --name "/WebApiProject/YfApi/ApiKey" --with-decryption`
4. Check the health endpoint: `GET /api/yh/health`

## Cost Considerations

- Parameter Store Standard parameters: Free up to 10,000 requests per month
- SecureString parameters use AWS KMS: Small charge per API call
- Advanced parameters: Higher throughput but additional cost

For most applications, the cost is minimal (typically under $1/month).
