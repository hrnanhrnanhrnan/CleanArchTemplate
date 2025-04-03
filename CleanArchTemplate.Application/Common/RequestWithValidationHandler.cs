using CleanArchTemplate.Application.Common.Interfaces;
using FluentValidation;

namespace CleanArchTemplate.Application.Common;

internal abstract class RequestWithValidationHandler<TRequest, TValidator, TResponse>(TValidator validator) 
    : IHandler<TRequest, TResponse>
    where TValidator : AbstractValidator<TRequest>
{
    public async Task<Result<TResponse>> InvokeAsync(TRequest request)
    {
        await validator.ValidateAndThrowAsync(request);
        return await OnSuccessfullValidation(request);
    }

    protected abstract Task<Result<TResponse>> OnSuccessfullValidation(TRequest request);
}