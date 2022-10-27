using Integrations.Diadoc.Infrastructure.Settings;

namespace Integrations.Diadoc.Infrastructure.SubServices.TokenService;

public interface IAuthToken
{
    Task<String> GetAccessToken(EmployeeSettings employee);
}
