using Microsoft.OpenApi.Models;
using OrdersService.HealthChecks;
using OrdersService.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.WithProperty("Service", "OrdersService")
        .WriteTo.Console();
});

builder.Services.AddControllers();

var catalogServiceBaseUrl = builder.Configuration["Services:CatalogServiceBaseUrl"]
    ?? throw new InvalidOperationException("Services:CatalogServiceBaseUrl is not configured.");

builder.Services.AddHttpClient<ICatalogServiceClient, CatalogServiceClient>(client =>
{
    client.BaseAddress = new Uri(catalogServiceBaseUrl);
    client.Timeout = TimeSpan.FromSeconds(10);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Orders Service",
        Version = "v1"
    });
});

builder.Services.AddHealthChecks()
    .AddCheck<CatalogServiceHealthCheck>("catalog-service");

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Orders Service v1");
});

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
