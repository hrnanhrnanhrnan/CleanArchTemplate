using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using CleanArchTemplate.Application.Users;
using CleanArchTemplate.Application.Users.Handlers;
using CleanArchTemplate.Application.Users.Requests;
using CleanArchTemplate.Application.Users.Responses;
using CleanArchTemplate.Application.Common.Interfaces;

namespace CleanArchTemplate.Application;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        => services.AddValidatorsFromAssembly()
                    .AddScoped<IUserService, UserService>()
                    .AddScoped<IHandler<CreateUserRequest, UserResponse>, CreateUserHandler>();


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
                services.AddScoped(validatorType);
            }

            return services;
    }
}