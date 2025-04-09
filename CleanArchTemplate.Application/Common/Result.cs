namespace CleanArchTemplate.Application.Common;

public sealed class Result<T>
{
    public T? Value { get; }
    public ErrorResponse? Error { get; }
    public bool IsSuccess { get; }
    
    private Result(T? value, ErrorResponse? errorMessage, bool isSuccess)
    {
        Value = value;
        Error = errorMessage;
        IsSuccess = isSuccess;
    }

    public static Result<T> Success(T value) => new(value, null, true);
    public static Result<T> Failure(string errorMessage) => new(default, new(errorMessage), false);

    public void Deconstruct(out T? value, out ErrorResponse? error, out bool isSuccess)
    {
        value = Value;
        error = Error;
        isSuccess = IsSuccess;
    }
}

public sealed class Result
{
    public ErrorResponse? Error { get; }
    public bool IsSuccess { get; }
    
    private Result(ErrorResponse? errorMessage, bool isSuccess)
    {
        Error = errorMessage;
        IsSuccess = isSuccess;
    }

    public static Result Success() => new(null, true);
    public static Result Failure(string errorMessage) => new(new(errorMessage), false);

    public void Deconstruct(out ErrorResponse? error, out bool isSuccess)
    {
        error = Error;
        isSuccess = IsSuccess;
    }
}