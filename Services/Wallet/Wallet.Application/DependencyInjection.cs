using Microsoft.Extensions.DependencyInjection;

namespace Wallet.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddWalletApplicacion(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });
        return services;
    }
}
