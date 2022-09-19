﻿using FluentValidation;

namespace Integrations.Diadoc.Service.Configurations.Consul;

public class ConsulDbConfiguration
{
    public string? Apt { get; set; }
    public string? SberLogistica { get; set; }
}

public class ConsulDbConfigurationValidator : AbstractValidator<ConsulDbConfiguration>
{
    public ConsulDbConfigurationValidator()
    {
        RuleFor(r => r.Apt)
            .NotNull()
            .NotEmpty()
            .WithMessage(r => $"Поле {nameof(r.Apt)} не найдено.");

        RuleFor(r => r.SberLogistica)
            .NotNull()
            .NotEmpty()
            .WithMessage(r => $"Поле {nameof(r.SberLogistica)} не найдено.");
    }
}
