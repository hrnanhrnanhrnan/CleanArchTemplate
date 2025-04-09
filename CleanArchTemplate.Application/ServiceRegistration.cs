using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using CleanArchTemplate.Application.Users;
using CleanArchTemplate.Application.Common.Interfaces;
using CleanArchTemplate.Application.Common;

namespace CleanArchTemplate.Application;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        => services.AddValidatorsFromAssembly()
                .AddScoped<IRequestValidator, RequestValidator>()
                .AddScoped<IUserService, UserService>();


    private static IServiceCollection AddValidatorsFromAssembly(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var validatorTypes = assembly.GetTypes()
            .Where(t => !t.IsAbstract && 
                        !t.IsInterface && 
                        t.GetInterfaces().Any(i => 
                            i.IsGenericType &&
                            i.GetGenericTypeDefinition() == typeof(IValidator<>)))
            .ToList();

        foreach(var validatorType in validatorTypes)
        {
            var interfaceType = validatorType
                .GetInterfaces()
                .First(i => i.IsGenericType 
                            && i.GetGenericTypeDefinition() == typeof(IValidator<>));

            services.AddScoped(interfaceType, validatorType);
        }

        return services;
    }
}