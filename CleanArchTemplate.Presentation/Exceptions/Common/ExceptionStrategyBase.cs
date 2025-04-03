namespace CleanArchTemplate.Presentation.Exceptions.Common;

public abstract class ExceptionStrategyBase<TException> : IExceptionStrategy
    where TException : Exception
{
    public bool CanHandle(Exception exception) => exception is TException;
    public abstract object GetHttpResponseObject(TException exception);
    public abstract int GetHttpStatusCode();
    public object GetResponseObject(Exception exception)
        => GetHttpResponseObject((TException)exception);
    public int GetStatusCode()
        => GetHttpStatusCode();
}