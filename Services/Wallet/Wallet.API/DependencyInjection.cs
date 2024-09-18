
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Wallet.API;
public static class DependencyInjection
{
    public static IServiceCollection AddWalletPresentation(this IServiceCollection services,
    IConfiguration configuration)
    {
        services.AddControllers();
        services.AddWalletAuthentication(configuration);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    private static IServiceCollection AddWalletAuthentication(this IServiceCollection services,
    IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuer = true,
               ValidateAudience = true,
               ValidateLifetime = true,
               ValidateIssuerSigningKey = true,
               ValidIssuer = configuration["JwtSettings:Issuer"],
               ValidAudience = configuration["JwtSettings:Audience"],
               IssuerSigningKey = new SymmetricSecurityKey(
              Encoding.UTF8.GetBytes(configuration["JwtSettings:Secret"])
           )
           });

        //         services.AddSwaggerGen(c =>
        //    {
        //        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Wallet API", Version = "v1" });
        //        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        //        {
        //            Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        //            Name = "Authorization",
        //            In = ParameterLocation.Header,
        //            Type = SecuritySchemeType.ApiKey,
        //            Scheme = "Bearer"
        //        });
        //        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
        //        {
        //            {
        //                new OpenApiSecurityScheme
        //                {
        //                    Reference = new OpenApiReference
        //                    {
        //                        Type = ReferenceType.SecurityScheme,
        //                        Id = "Bearer"
        //                    },
        //                    Scheme = "oauth2",
        //                    Name = "Bearer",
        //                    In = ParameterLocation.Header
        //                },
        //                new List<string>()
        //            }
        //        });
        //    });

        return services;
    }
}
