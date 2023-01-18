using Diadoc.Api;
using Integrations.Diadoc.Infrastructure.Settings;
using Microsoft.Extensions.Caching.Memory;

namespace Integrations.Diadoc.Infrastructure.SubServices.TokenService;

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
        var token = await Cache.GetOrCreateAsync($"{employee.Login}-{employee.Password}", async entry =>
        {
            var responseToken = await this.Api.AuthenticateAsync(employee.Login, employee.Password);
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6);
            return responseToken;
        });

        return token;
    }
}
