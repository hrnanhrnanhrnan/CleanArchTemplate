using CleanArchTemplate.Presentation.Exceptions.Common;
using Microsoft.AspNetCore.Diagnostics;

namespace CleanArchTemplate.Presentation.Exceptions.ExceptionHandlers;

internal static class GlobalExceptionHandler
{
    internal static async Task HandleAsync(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error 
            ?? new Exception("Unknown error");

        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        logger.LogError(
            exception,
            "Error occurred: {ErrorMessage}", 
            exception.Message
        );

        var exceptionStrategies = context.RequestServices.GetServices<IExceptionStrategy>();
        var strategy = exceptionStrategies.First(ex => ex.CanHandle(exception));

        if (strategy is not null)
        {
            context.Response.StatusCode = strategy.GetStatusCode();
            await context.Response.WriteAsJsonAsync(strategy.GetResponseObject(exception));
            return;
        }

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsJsonAsync(new { Error = exception.Message });
    }
}