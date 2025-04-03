using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CleanArchTemplate.Application.Users;
using CleanArchTemplate.Infrastructure.Contexts;
using CleanArchTemplate.Infrastructure.Repositories;

namespace CleanArchTemplate.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        => services.AddDbContext<ApplicationDbContext>(o =>
        {
            o.UseSqlite(configuration.GetConnectionString("Sqlite"));
        })
        .AddScoped<IUserRepository, UserRepository>();
}