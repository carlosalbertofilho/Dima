using System.Net.Http.Json;
using System.Text;
using Dima.Core.Handlers;
using Dima.Core.Requests.Account;
using Dima.Core.Responses;

namespace Dima.Web.Handler;

public class AccountHandler(IHttpClientFactory httpClientFactory) : IAccountHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<Response<string?>> LoginAsync(LoginRequest request)
    {
        var result =await _client.PostAsJsonAsync("v1/users/login?useCookies=true", request);
        return result.IsSuccessStatusCode
            ? new Response<string?>("Login Ok", 200, "Login Realizado com sucesso")
            : new Response<string?>(null, 400, "Não foi possível realizar o login");
    }

    public async Task<Response<string?>> RegisterAsync(RegisterRequest request)
    {
        var result = await _client.PostAsJsonAsync("v1/users/register", request);
        return result.IsSuccessStatusCode
            ? new Response<string?>("Usuário Cadastrado", 200, "Cadastro realizado com sucesso")
            : new Response<string?>(null, 400, "Não foi possível realizar o cadastro");
    }

    public async Task LogoutAsync()
    {
        var emptyContent = new StringContent("{}", Encoding.UTF8, "application/json");
        await _client.PostAsJsonAsync("v1/users/logout", emptyContent);
    }
}