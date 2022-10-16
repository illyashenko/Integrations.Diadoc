﻿using Integrations.Diadoc.Domain.Models.Enums;

namespace Integrations.Diadoc.Domain.Models.Settings;

public class EmployeeSettings
{
    public string? Login { get; set; }
    public string? Password { get; set; }
    public string? FirstName { get; set; }
    public string? Surname { get; set; }
    public string? Patronymic { get; set; }
    public string? JobTitle { get; set; }
    public EmployeePosition Position { get; set; }
}
