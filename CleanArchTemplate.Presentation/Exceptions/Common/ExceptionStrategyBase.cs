using CleanArchTemplate.Application.Common;

namespace CleanArchTemplate.Presentation.Exceptions.Common;

public abstract class ExceptionStrategyBase<TException> : IExceptionStrategy
    where TException : Exception
{
    public bool CanHandle(Exception exception) => exception is TException;
    public abstract ErrorResponse GetHttpResponseObject(TException exception);
    public abstract int GetHttpStatusCode();
    public ErrorResponse GetResponseObject(Exception exception)
        => GetHttpResponseObject((TException)exception);
    public int GetStatusCode()
        => GetHttpStatusCode();
}