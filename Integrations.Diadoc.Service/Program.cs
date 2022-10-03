using System.Configuration;
using Diadoc.Api;
using Diadoc.Api.Cryptography;
using Integrations.Diadoc.Data.Apt;
using Integrations.Diadoc.Data.Monitoring;
using Integrations.Diadoc.Domain.Models.Settings;
using Integrations.Diadoc.Infrastructure.Stores;
using Integrations.Diadoc.Infrastructure.SubServices;
using Integrations.Diadoc.Infrastructure.SubServices.DocumentBuilders;
using Integrations.Diadoc.Infrastructure.SubServices.Pusher;
using Integrations.Diadoc.Infrastructure.SubServices.TokenService;
using Integrations.Diadoc.Service.Configurations.Consul;
using Integrations.Diadoc.Service.Helpers;
using MassTransitRMQExtensions;
using MassTransitRMQExtensions.Models;
using Microsoft.EntityFrameworkCore;
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

builder.Services.AddDbContext<MonitoringContext>(context => context.UseSqlServer(builder.Configuration.GetConnectionString("Monitoring")));
builder.Services.AddDbContext<AptContext>(context => context.UseSqlServer(builder.Configuration.GetConnectionString("Apt")));

builder.Services.Configure<CommonSettings>(builder.Configuration.GetSection("CommonSettings"));

builder.Services.AddSingleton<IMemoryCache, MemoryCache>();
builder.Services.AddSingleton<IAuthToken, AuthToken>();

builder.Services.AddTransient<AptStore>();
builder.Services.AddTransient<MonitoringStore>();
builder.Services.AddTransient<DiadocExecutors>();
builder.Services.AddTransient<IDiadocPusher, DiadocPusher>();

var app = builder.Build();

app.MapGet("/About", context =>
    context.Response.WriteAsJsonAsync(new
    {
        Description = "Integration.Diadoc - Интеграция с Контур.Диадок",
        Environment = app.Environment.EnvironmentName,
        Version = typeof(Program).Assembly.GetName().Version?.ToString()
    }));

app.Run();
