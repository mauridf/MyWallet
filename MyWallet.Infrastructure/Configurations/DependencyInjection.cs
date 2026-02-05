using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyWallet.Infrastructure.Persistence;
using MyWallet.Application.Interfaces;
using MyWallet.Application.UseCases;

namespace MyWallet.Infrastructure.Configurations;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = Environment.GetEnvironmentVariable("MYWALLET_CONNECTION_STRING");

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("Connection string not found in environment variables.");

        services.AddDbContext<MyWalletDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IUserService, UserService>();

        return services;
    }
}
