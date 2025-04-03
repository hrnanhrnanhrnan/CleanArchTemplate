using CleanArchTemplate.Presentation.Exceptions.Common;
using CleanArchTemplate.Presentation.Exceptions.ExceptionStrategies;

namespace CleanArchTemplate.Presentation.Exceptions.ExceptionHandlers;

internal static class ExceptionHandlingRegistration
{
    internal static IServiceCollection AddExceptionStrategies(this IServiceCollection services)
        => services.AddSingleton<IExceptionStrategy, ValidationExceptionStrategy>();

    internal static IApplicationBuilder UseGlobalExceptionHandling(this IApplicationBuilder app)
        => app.UseExceptionHandler(handler => handler.Run(GlobalExceptionHandler.HandleAsync));
}