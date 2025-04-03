namespace CleanArchTemplate.Presentation.Exceptions.Common;

public interface IExceptionStrategy 
{
    bool CanHandle(Exception exception);
    int GetStatusCode();
    object GetResponseObject(Exception exception);
}