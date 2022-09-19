using Diadoc.Api;
using Integrations.Diadoc.Domain.Models.Settings;
using Microsoft.Extensions.Caching.Memory;

namespace Integrations.Diadoc.Infrastructure.SubServices;

public class AuthToken : IAuthToken
{
    private IMemoryCache Cache { get; set; }
    private IDiadocApi Api { get; set; }

    public AuthToken(IMemoryCache cache, IDiadocApi api)
    {
        this.Cache = cache;
        this.Api = api;
    }

    public async Task<string> GetAccessToken(EmployeeSettings employee)
    {
        var token = await Cache.GetOrCreateAsync(employee.Login, async entry =>
        {
            var responseToken = await Api.AuthenticateAsync(employee.Login, employee.Password);
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(12);
            return responseToken;
        });

        return token;
    }
}
