using Dima.Core.Handlers;
using Dima.Web.Security;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Identity;

public partial class LogoutPage : ComponentBase
{
    #region Services
        
    [Inject] protected ISnackbar Snackbar { get; set; } = null!;
    [Inject] protected IAccountHandler Handler { get; set; } = null!;
    [Inject] protected NavigationManager NavigationManager { get; set; } = null!;
    [Inject] protected ICookieAuthenticationStateProvider StateProvider { get; set; } = null!;
    
    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        if (await StateProvider.CheckAuthenticationAsync())
        {
            await Handler.LogoutAsync();
            await StateProvider.GetAuthenticationStateAsync();
            StateProvider.NotifyAuthenticationStateChanged();
            
            // Adiciona um atraso de 3 segundos antes de redirecionar
            // tbfgvawait Task.Delay(3000);
            NavigationManager.NavigateTo("/entra");
        }
        await base.OnInitializedAsync();
    }

    #endregion
}