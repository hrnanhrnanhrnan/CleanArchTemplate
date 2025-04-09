using CleanArchTemplate.Application.Common;

namespace CleanArchTemplate.Presentation.Exceptions.Common;

public interface IExceptionStrategy 
{
    bool CanHandle(Exception exception);
    int GetStatusCode();
    ErrorResponse GetResponseObject(Exception exception);
}