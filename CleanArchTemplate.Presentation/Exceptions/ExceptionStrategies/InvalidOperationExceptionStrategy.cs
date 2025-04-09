using CleanArchTemplate.Application.Common;
using CleanArchTemplate.Presentation.Exceptions.Common;

namespace CleanArchTemplate.Presentation.Exceptions.ExceptionStrategies;

internal sealed class InvalidOperationExceptionStrategy : ExceptionStrategyBase<InvalidOperationException>
{
    public override ErrorResponse GetHttpResponseObject(InvalidOperationException exception)
        => new(exception.Message);

    public override int GetHttpStatusCode()
        => StatusCodes.Status501NotImplemented;
}