using System.Reflection;
using CleanArchTemplate.Presentation.Exceptions.Common;

namespace CleanArchTemplate.Presentation.Exceptions.ExceptionHandlers;

internal static class ExceptionHandlingRegistration
{
    internal static IServiceCollection AddExceptionStrategies(this IServiceCollection services)
    {
        var strategyInterfaceType = typeof(IExceptionStrategy);

        var assembly = Assembly.GetExecutingAssembly();
        var exceptionStrategyTypes = assembly
            .GetTypes()
            .Where(t => !t.IsAbstract && !t.IsInterface)
            .Where(strategyInterfaceType.IsAssignableFrom)
            .ToList();

        foreach(var exceptionStrategyType in exceptionStrategyTypes)
        {
            services.AddScoped(strategyInterfaceType, exceptionStrategyType);
        }

        return services;
    }

    internal static IApplicationBuilder UseGlobalExceptionHandling(this IApplicationBuilder app)
        => app.UseExceptionHandler(handler => handler.Run(GlobalExceptionHandler.HandleAsync));
}