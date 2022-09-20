using Diadoc.Api;
using Diadoc.Api.Cryptography;
using Integrations.Diadoc.Domain.Models.Settings;
using Integrations.Diadoc.Domain.Stores;
using Integrations.Diadoc.Infrastructure.Stores;
using Integrations.Diadoc.Infrastructure.SubServices;
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

var diadocSettings = builder.Configuration.GetSection("ApiSettings");
builder.Services.AddSingleton<IDiadocApi>(sd => new DiadocApi(
    diadocSettings["ClientId"],
    diadocSettings["ApiUrl"],
    new WinApiCrypt()));

builder.Services.Configure<CommonSettings>(builder.Configuration.GetSection("CommonSettings"));

builder.Services.AddSingleton<IMemoryCache, MemoryCache>();
builder.Services.AddSingleton<IAuthToken, AuthToken>();

builder.Services.AddTransient<IAptStore, AptStore>();
builder.Services.AddTransient<IMonitoringStore, MonitoringStore>();

var app = builder.Build();

app.MapGet("/About", context =>
    context.Response.WriteAsJsonAsync(new
    {
        Description = "Integration.Diadoc - Интеграция с Контур.Диадок",
        Environment = app.Environment.EnvironmentName,
        Version = typeof(Program).Assembly.GetName().Version?.ToString()
    }));

app.Run();
