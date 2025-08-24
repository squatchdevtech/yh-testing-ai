namespace WebApiProject.Services;

/// <summary>
/// Represents the result of an operation that can succeed or fail.
/// </summary>
/// <typeparam name="T">The type of the success value.</typeparam>
public sealed record Result<T>
{
    public T? Value { get; }
    public Error? Error { get; }
    public bool IsSuccess => Error is null;
    public bool IsFailure => Error is not null;

    private Result(T value)
    {
        Value = value;
        Error = null;
    }

    private Result(Error error)
    {
        Value = default;
        Error = error;
    }

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failure(Error error) => new(error);

    /// <summary>
    /// Transforms the result if successful, otherwise returns the error.
    /// </summary>
    public Result<TResult> Map<TResult>(Func<T, TResult> transform)
    {
        return IsSuccess 
            ? Result<TResult>.Success(transform(Value!))
            : Result<TResult>.Failure(Error!);
    }

    /// <summary>
    /// Transforms the result if successful, otherwise returns the error.
    /// </summary>
    public async Task<Result<TResult>> MapAsync<TResult>(Func<T, Task<TResult>> transform)
    {
        return IsSuccess 
            ? Result<TResult>.Success(await transform(Value!))
            : Result<TResult>.Failure(Error!);
    }

    /// <summary>
    /// Chains another result-returning operation if this result is successful.
    /// </summary>
    public Result<TResult> Bind<TResult>(Func<T, Result<TResult>> transform)
    {
        return IsSuccess 
            ? transform(Value!)
            : Result<TResult>.Failure(Error!);
    }

    /// <summary>
    /// Chains another async result-returning operation if this result is successful.
    /// </summary>
    public async Task<Result<TResult>> BindAsync<TResult>(Func<T, Task<Result<TResult>>> transform)
    {
        return IsSuccess 
            ? await transform(Value!)
            : Result<TResult>.Failure(Error!);
    }

    /// <summary>
    /// Returns the value if successful, otherwise throws an exception.
    /// </summary>
    public T Unwrap()
    {
        return IsSuccess 
            ? Value! 
            : throw new InvalidOperationException($"Cannot unwrap failed result: {Error}");
    }

    /// <summary>
    /// Returns the value if successful, otherwise returns the default value.
    /// </summary>
    public T UnwrapOr(T defaultValue)
    {
        return IsSuccess ? Value! : defaultValue;
    }

    /// <summary>
    /// Returns the value if successful, otherwise computes a default value.
    /// </summary>
    public T UnwrapOrElse(Func<Error, T> defaultValueFactory)
    {
        return IsSuccess ? Value! : defaultValueFactory(Error!);
    }

    /// <summary>
    /// Executes an action if the result is successful.
    /// </summary>
    public Result<T> OnSuccess(Action<T> action)
    {
        if (IsSuccess)
        {
            action(Value!);
        }
        return this;
    }

    /// <summary>
    /// Executes an action if the result is a failure.
    /// </summary>
    public Result<T> OnFailure(Action<Error> action)
    {
        if (IsFailure)
        {
            action(Error!);
        }
        return this;
    }

    public static implicit operator Result<T>(T value) => Success(value);
    public static implicit operator Result<T>(Error error) => Failure(error);
}

/// <summary>
/// Represents an error with a code and message.
/// </summary>
public sealed record Error(string Code, string Message)
{
    public static Error From(string code, string message) => new(code, message);
    public static Error From(Exception exception) => new(exception.GetType().Name, exception.Message);

    public override string ToString() => $"{Code}: {Message}";
}

/// <summary>
/// Static factory methods for creating Result instances.
/// </summary>
public static class Result
{
    public static Result<T> Success<T>(T value) => Result<T>.Success(value);
    public static Result<T> Failure<T>(Error error) => Result<T>.Failure(error);
    public static Result<T> Failure<T>(string code, string message) => Result<T>.Failure(Error.From(code, message));
}

/// <summary>
/// Represents a unit of work with no return value.
/// </summary>
public readonly struct Unit : IEquatable<Unit>
{
    public static readonly Unit Value = new();
    
    public bool Equals(Unit other) => true;
    public override bool Equals(object? obj) => obj is Unit;
    public override int GetHashCode() => 0;
    public override string ToString() => "()";
    
    public static bool operator ==(Unit left, Unit right) => true;
    public static bool operator !=(Unit left, Unit right) => false;
}

/// <summary>
/// Common domain errors.
/// </summary>
public static class DomainErrors
{
    public static class Validation
    {
        public static Error Required(string fieldName) => 
            Error.From("VALIDATION_REQUIRED", $"{fieldName} is required");
            
        public static Error InvalidValue(string fieldName, string value) => 
            Error.From("VALIDATION_INVALID", $"Invalid value '{value}' for {fieldName}");
            
        public static Error InvalidFormat(string fieldName) => 
            Error.From("VALIDATION_FORMAT", $"{fieldName} has invalid format");
    }

    public static class Network
    {
        public static Error RequestFailed(int statusCode) => 
            Error.From("NETWORK_REQUEST_FAILED", $"Request failed with status code {statusCode}");
            
        public static Error Timeout() => 
            Error.From("NETWORK_TIMEOUT", "Request timed out");
            
        public static Error ConnectionFailed() => 
            Error.From("NETWORK_CONNECTION_FAILED", "Failed to establish connection");
    }

    public static class Data
    {
        public static Error ParseError(string dataType) => 
            Error.From("DATA_PARSE_ERROR", $"Failed to parse {dataType}");
            
        public static Error NotFound(string resourceType, string identifier) => 
            Error.From("DATA_NOT_FOUND", $"{resourceType} with identifier '{identifier}' not found");
    }

    public static class Configuration
    {
        public static Error MissingApiKey() => 
            Error.From("CONFIG_MISSING_API_KEY", "API key is not configured");
            
        public static Error InvalidConfiguration(string setting) => 
            Error.From("CONFIG_INVALID", $"Invalid configuration for {setting}");
    }
}
