using Integrations.Diadoc.Domain.Models.Settings;

namespace Integrations.Diadoc.Infrastructure.SubServices;

public interface IAuthToken
{
    Task<String> GetAccessToken(EmployeeSettings employee);
}
