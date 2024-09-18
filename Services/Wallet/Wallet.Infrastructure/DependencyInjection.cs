using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wallet.Application.Abstractions.Clock;
using Wallet.Domain.Abstractions;
using Wallet.Domain.Wallets;
using Wallet.Infrastructure.Clock;
using Wallet.Infrastructure.Persistence;
using Wallet.Infrastructure.Persistence.Repositories;

namespace Wallet.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddWalletInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        services
        .AddWalletServicePrivider()
        .AddWalletPersistence(configuration);

        return services;
    }

    private static IServiceCollection AddWalletServicePrivider(this IServiceCollection services)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        return services;
    }

    private static IServiceCollection AddWalletPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WalletDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<WalletDbContext>());

        services.AddScoped<IWalletRepository, WalletRepository>();
        return services;
    }

}
