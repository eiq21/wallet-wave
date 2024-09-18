using Microsoft.Extensions.DependencyInjection;

namespace Security.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddSecurityApplicacion(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });
        return services;
    }
}
