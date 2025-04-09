using FluentValidation.Results;

namespace CleanArchTemplate.Application.Common.Interfaces;

public interface IRequestValidator
{
    Task<ValidationResult> ValidateAsync<TRequest>(TRequest request, CancellationToken token = default);
}