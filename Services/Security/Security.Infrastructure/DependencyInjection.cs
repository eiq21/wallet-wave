using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Security.Application.Abstractions.Authentication;
using Security.Application.Abstractions.Clock;
using Security.Domain.Abstractions;
using Security.Domain.Services;
using Security.Domain.Users;
using Security.Infrastructure.Authentication.PasswordHasher;
using Security.Infrastructure.Authentication.TokenGenerator;
using Security.Infrastructure.Clock;
using Security.Infrastructure.Persistence;
using Security.Infrastructure.Persistence.Repositories;

namespace Security.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddSecurityInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        services
        .AddSecurityServicePrivider()
        .AddSecurityAuthentication(configuration)
        .AddSecurityPersistence(configuration);

        return services;
    }

    private static IServiceCollection AddSecurityPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SecurityDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<SecurityDbContext>());
        return services;
    }

    private static IServiceCollection AddSecurityServicePrivider(this IServiceCollection services)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        return services;
    }

    private static IServiceCollection AddSecurityAuthentication(this IServiceCollection services,
    IConfiguration configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret)),
            });


        return services;
    }
}
