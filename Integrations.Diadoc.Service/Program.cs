using Integrations.Diadoc.Service.Configurations.Consul;
using MassTransitRMQExtensions;
using MassTransitRMQExtensions.Models;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

builder.AddConsul();

var rabbitConfig = builder.Configuration.GetSection("Rabbit");
var mqConfig = new RabbitMqConfig(rabbitConfig["UserName"], rabbitConfig["Password"],
    new Uri($"amqp://{rabbitConfig["HostName"]}:{rabbitConfig["Port"]}"));

builder.Services.ConfigureMassTransitControllers(mqConfig);

builder.Services.AddSingleton<IMemoryCache, MemoryCache>();

var app = builder.Build();

app.MapGet("/About", context =>
    context.Response.WriteAsJsonAsync(new
    {
        Description = "Integration.Diadoc - Интеграция с Контур.Диадок",
        Environment = app.Environment.EnvironmentName,
        Version = typeof(Program).Assembly.GetName().Version?.ToString()
    }));

app.Run();
