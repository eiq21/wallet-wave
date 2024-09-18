using Security.API;
using Security.Infrastructure;
using Security.Application;
using Security.API.Extensions;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
    .AddSecurityPresentation()
    .AddSecurityApplicacion()
    .AddSecurityInfrastructure(builder.Configuration);
}


var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCustomExceptionHandler();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}


