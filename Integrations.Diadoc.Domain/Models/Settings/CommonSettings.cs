using Integrations.Diadoc.Domain.Models.Enums;

namespace Integrations.Diadoc.Domain.Models.Settings;

public class CommonSettings
{
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
        return this.EmployeeSettings?.Single(s => s.Position == position)!;
    }
}
