
namespace Wallet.API;
public static class DependencyInjection
{
    public static IServiceCollection AddWalletPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}
