using System.Net.Http.Json;
using System.Security.Claims;
using Dima.Core.Models.Account;
using Microsoft.AspNetCore.Components.Authorization;

namespace Dima.Web.Security;

public class CookieAuthenticationStateProvider(IHttpClientFactory clientFactory)
    : AuthenticationStateProvider, ICookieAuthenticationStateProvider
{
    private readonly HttpClient _client = clientFactory.CreateClient(Configuration.HttpClientName);
    private bool _isAuthenticated;

    public async Task<bool> CheckAuthenticationAsync()
    {
        await GetAuthenticationStateAsync();
        return _isAuthenticated;
    }

    public void NotifyAuthenticationStateChanged()
        => base.NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        _isAuthenticated = false;
        var user = new ClaimsPrincipal(new ClaimsIdentity());
        
        var userinfo = await GetUser();
        if (userinfo is null) return new AuthenticationState(user);
        var claims = await GetClaims(userinfo);
        
        var id = new ClaimsIdentity(claims, nameof(CookieAuthenticationStateProvider));
        user = new ClaimsPrincipal(id);
        
        _isAuthenticated = true;
        return new AuthenticationState(user);
    }

    private async Task<User?> GetUser()
    {
        try
        {
            return await _client.GetFromJsonAsync<User?>("/v1/users/manage/info");
        }
        catch
        {
            return null;
        }
    }

    private async Task<List<Claim>> GetClaims(User user)
    {
        var claims = new List<Claim>()
        {
            new(ClaimTypes.Name, user.Email),
            new(ClaimTypes.Email, user.Email),
        };

        claims.AddRange(user.Claims
            .Where(x => x.Key != ClaimTypes.Name && x.Key != ClaimTypes.Email)
            .Select(x => new Claim(x.Key, x.Value)));

        // obtém claims de roles
        claims.AddRange(await GetRoleClaimsAsync());

        return claims;
    }

    private async Task<List<Claim>> GetRoleClaimsAsync()
    {
        var roleClaims = new List<Claim>();
        try
        {
            var roles = await _client.GetFromJsonAsync<RoleClaim[]>("v1/users/roles");
            roleClaims.AddRange(
                from role in roles ?? []
                where !string.IsNullOrWhiteSpace(role.Value) && !string.IsNullOrWhiteSpace(role.Type)
                select new Claim(role.Type!, role.Value!, role.ValueType, role.Issuer, role.OriginalIssuer));
        }
        catch
        {
            return roleClaims;
        }

        return roleClaims;
    }
}