using System.Globalization;
using Dima.Core.Handlers;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Dima.Web;
using Dima.Web.Handler;
using Dima.Web.Security;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;

// Configurando cultura pt-BR
var cultureInfo = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

// header
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Services
builder.Services.AddScoped<CookieHandler>();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CookieAuthenticationStateProvider>();
builder.Services.AddScoped(x => 
    (ICookieAuthenticationStateProvider)x.GetRequiredService<AuthenticationStateProvider>());

builder.Services.AddMudServices();

builder.Services
    .AddHttpClient(Configuration.HttpClientName,
        options => { options.BaseAddress = new Uri(Configuration.BackendUrl); })
    .AddHttpMessageHandler<CookieHandler>();

builder.Services.AddTransient<IAccountHandler, AccountHandler>();
builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();
builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();

// ativa internacionalização
builder.Services.AddLocalization();

await builder.Build().RunAsync();
