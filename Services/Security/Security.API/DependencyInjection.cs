namespace Security.API;
public static class DependencyInjection
{
    public static IServiceCollection AddSecurityPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}
