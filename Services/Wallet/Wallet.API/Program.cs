using Microsoft.AspNetCore.Authorization;
using Wallet.API;
using Wallet.API.Extensions;
using Wallet.Application;
using Wallet.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
    .AddWalletPresentation(builder.Configuration)
    .AddWalletApplicacion()
    .AddWalletInfrastructure(builder.Configuration);
}

var app = builder.Build();
{

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseCustomExceptionHandler();
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseMiddleware<AuthorizationMiddleware>();
    app.MapControllers();
    app.Run();
}


