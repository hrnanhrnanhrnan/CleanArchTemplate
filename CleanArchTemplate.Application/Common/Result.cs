namespace CleanArchTemplate.Application.Common;

public sealed class Result<T>
{
    public T? Value { get; }
    public string? Error { get; }
    public bool IsSuccess { get; }
    
    private Result(T? value, string? errorMessage, bool isSuccess)
    {
        Value = value;
        Error = errorMessage;
        IsSuccess = isSuccess;
    }

    public static Result<T> Success(T value) => new(value, null, true);
    public static Result<T> Failure(string errorMessage) => new(default, errorMessage, false);

    public void Deconstruct(out T? value, out string? error, out bool isSuccess)
    {
        value = Value;
        error = Error;
        isSuccess = IsSuccess;
    }
}

public sealed class Result
{
    public string? Error { get; }
    public bool IsSuccess { get; }
    
    private Result(string? errorMessage, bool isSuccess)
    {
        Error = errorMessage;
        IsSuccess = isSuccess;
    }

    public static Result Success() => new(null, true);
    public static Result Failure(string errorMessage) => new(errorMessage, false);

    public void Deconstruct(out string? error, out bool isSuccess)
    {
        error = Error;
        isSuccess = IsSuccess;
    }
}