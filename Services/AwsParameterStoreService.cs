using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;

namespace WebApiProject.Services;

public interface IAwsParameterStoreService
{
    Task<Result<string>> GetParameterAsync(string parameterName);
}

public class AwsParameterStoreService : IAwsParameterStoreService
{
    private readonly IAmazonSimpleSystemsManagement _ssmClient;
    private readonly ILogger<AwsParameterStoreService> _logger;

    public AwsParameterStoreService(IAmazonSimpleSystemsManagement ssmClient, ILogger<AwsParameterStoreService> logger)
    {
        _ssmClient = ssmClient;
        _logger = logger;
    }

    public async Task<Result<string>> GetParameterAsync(string parameterName)
    {
        if (string.IsNullOrWhiteSpace(parameterName))
        {
            return DomainErrors.Validation.Required(nameof(parameterName));
        }

        try
        {
            var request = new GetParameterRequest
            {
                Name = parameterName,
                WithDecryption = true // Important for SecureString parameters
            };

            var response = await _ssmClient.GetParameterAsync(request);
            
            if (string.IsNullOrEmpty(response.Parameter?.Value))
            {
                _logger.LogWarning("Parameter {ParameterName} exists but has no value", parameterName);
                return DomainErrors.Data.NotFound("Parameter", parameterName);
            }

            return Result.Success(response.Parameter.Value);
        }
        catch (ParameterNotFoundException)
        {
            _logger.LogWarning("Parameter {ParameterName} not found in AWS Parameter Store", parameterName);
            return DomainErrors.Data.NotFound("Parameter", parameterName);
        }
        catch (AmazonSimpleSystemsManagementException ex) when (ex.ErrorCode == "InvalidParameter")
        {
            _logger.LogError(ex, "Invalid parameter {ParameterName} in AWS Parameter Store", parameterName);
            return DomainErrors.Validation.InvalidValue(nameof(parameterName), parameterName);
        }
        catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
        {
            _logger.LogError(ex, "Timeout retrieving parameter {ParameterName} from AWS Parameter Store", parameterName);
            return DomainErrors.Network.Timeout();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving parameter {ParameterName} from AWS Parameter Store", parameterName);
            return Error.From(ex);
        }
    }
}
