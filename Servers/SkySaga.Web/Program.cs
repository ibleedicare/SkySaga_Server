using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.DependencyInjection;

using SkySaga.Web.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
});

var app = builder.Build();

app.UseHttpLogging();

app.MapGet("/ping", () => Results.Ok());

app.MapAccountEndpoints();
app.MapMatchMakingEndpoints();
app.MapBinaryStorageEndpoint();
app.MapGameConductorEndpoints();
app.MapAuthenticationEndpoints();
app.MapPersistentRecordEndpoints();

app.Run();