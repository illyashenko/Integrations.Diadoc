﻿namespace Integrations.Diadoc.Infrastructure.Settings;

public class CommonSettings
{
    public Organizations Organization { get; set; }
    public string? OrgId { get; set; }
    public string? FromBoxId { get; set; }
    public string? Inn { get; set; }
    public string? Kpp { get; set; }
    public string? OrganizationName { get; set; }
    public OrganizationAddress? Address { get; set; }
    public string? CertificateThumbprint { get; set; }
    public int MaxDocumentsCount { get; set; }
    public string[]? DocumentFilter { get; set; }
    public EmployeeSettings[]? EmployeeSettings { get; set; }

    public EmployeeSettings GetDataEmployee(EmployeePosition position)
    {
        return this.EmployeeSettings!.Single(s => s.Position == position);
    }
}
