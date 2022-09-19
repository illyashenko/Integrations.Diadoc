using FluentValidation;

namespace Integrations.Diadoc.Service.Configurations.Consul;

public class ConsulServiceConfiguration
{
    public ConsulDbConfiguration ConnectionStrings { get; set; }
    public ConsulRabbitConfiguration Rabbit { get; set; }
    public int? TargetServerId { get; set; }
}

public class ConsulServiceConfigValidator : AbstractValidator<ConsulServiceConfiguration>
{
    public ConsulServiceConfigValidator()
    {
        RuleFor(r => r.ConnectionStrings)
            .NotNull()
            .SetValidator(new ConsulDbConfigurationValidator())
            .WithMessage(r => $"Поле {nameof(r.ConnectionStrings)} не найдено.");

        RuleFor(r => r.Rabbit)
            .NotNull()
            .SetValidator(new ConsulRabbitConfigurationValidator())
            .WithMessage(r => $"Поле {nameof(r.Rabbit)} не найдено.");
        
        RuleFor(r => r.TargetServerId)
            .NotNull()
            .WithMessage(r => $"Поле {nameof(r.TargetServerId)} не найдено.");
    }
}
