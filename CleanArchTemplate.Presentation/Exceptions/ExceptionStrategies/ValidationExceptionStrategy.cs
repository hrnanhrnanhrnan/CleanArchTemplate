using CleanArchTemplate.Presentation.Exceptions.Common;
using FluentValidation;

namespace CleanArchTemplate.Presentation.Exceptions.ExceptionStrategies;

public class ValidationExceptionStrategy : ExceptionStrategyBase<ValidationException>
{
    public override object GetHttpResponseObject(ValidationException exception)
        => new 
        { 
            Errors = exception.Errors.ToDictionary(x => x.PropertyName, x => x.ErrorMessage)
        };

    public override int GetHttpStatusCode() => StatusCodes.Status400BadRequest;
}