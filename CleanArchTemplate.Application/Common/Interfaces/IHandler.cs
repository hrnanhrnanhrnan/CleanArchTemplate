namespace CleanArchTemplate.Application.Common.Interfaces;

public interface IHandler<TRequest, TResponse>
{
    Task<Result<TResponse>> InvokeAsync(TRequest request);
}