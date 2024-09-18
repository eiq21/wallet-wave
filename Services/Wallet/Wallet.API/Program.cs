using Wallet.API;
using Wallet.API.Extensions;
using Wallet.Application;
using Wallet.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
    .AddWalletPresentation()
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
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}


