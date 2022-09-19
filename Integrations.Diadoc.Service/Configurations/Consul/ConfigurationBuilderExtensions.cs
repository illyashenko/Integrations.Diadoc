using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Winton.Extensions.Configuration.Consul;

namespace Integrations.SberLogistica.Configurations.Consul;

public static class ConfigurationBuilderExtensions
{
    internal static IConfigurationBuilder AddConsul(this IConfigurationBuilder builder)
    {
        var configuration = builder.Build();
        var token = configuration.GetValue<string>(ConsulSettings.Token);
        var consulSettings = new ConsulSettings()
        {
            Host = configuration.GetValue<string>(ConsulSettings.ConsulHost),
            Key = configuration.GetValue<string>(ConsulSettings.ConsulKey)
        };

        return builder.AddConsul(consulSettings.Key,
            o =>
            {
                o.ConsulConfigurationOptions = cco =>
                {
                    cco.Address = new Uri(consulSettings.Host);
                    cco.Token = token;
                };
                o.ReloadOnChange = true;
            });
    }

    internal static  WebApplicationBuilder ValidateConsulConfig(this WebApplicationBuilder builder)
    {
        var serviceConfiguration = builder.Configuration.Get<ConsulServiceConfiguration>(); 
        var validator = new ConsulServiceConfigValidator();
        
        if (serviceConfiguration is not null)
        {
            var result = validator.Validate(serviceConfiguration);
            if (!result.IsValid)
            {
                var errors = result.Errors.Select(f => f.ErrorMessage).ToList();
                throw new Exception(string.Join(' ', errors ));
            }
        }
        return builder; 
    }
    public static WebApplicationBuilder AddConsul(this WebApplicationBuilder builder) 
    {
        var configBuilder = (IConfigurationBuilder)builder.Configuration; 
        configBuilder.AddConsul();
        builder.ValidateConsulConfig();
        
        return builder;
    }
}
