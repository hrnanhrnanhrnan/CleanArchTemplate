using CleanArchTemplate.Application.Common.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchTemplate.Application.Common;

public class RequestValidator(IServiceProvider serviceProvider) : IRequestValidator
{
    public Task<ValidationResult> ValidateAsync<TReuqest>(TReuqest request, CancellationToken token = default)
    {
        var validator = serviceProvider.GetService<IValidator<TReuqest>>()
            ?? throw new InvalidOperationException($"No validator found for {typeof(TReuqest).Name}");

        return validator.ValidateAsync(request, token);
    }
}
