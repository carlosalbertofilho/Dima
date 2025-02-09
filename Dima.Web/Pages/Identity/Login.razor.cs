using Dima.Core.Handlers;
using Dima.Core.Requests.Account;
using Dima.Web.Security;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Identity;

public partial class LoginPage : ComponentBase
{
    #region Services
        
    [Inject] protected ISnackbar Snackbar { get; set; } = null!;
    [Inject] protected IAccountHandler Handler { get; set; } = null!;
    [Inject] protected NavigationManager NavigationManager { get; set; } = null!;
    [Inject] protected ICookieAuthenticationStateProvider StateProvider { get; set; } = null!;
    
    #endregion
    
    #region Properties
    
    protected bool IsBusy { get; private set; }
    protected LoginRequest InputModel { get; set; } = new();
    
    #endregion

    #region Overrides

    protected override Task OnInitializedAsync()
    {
        try
        {
            var authState = StateProvider.GetAuthenticationStateAsync();
            var user = authState.Result.User;
            if (user.Identity is { IsAuthenticated: true })
                NavigationManager.NavigateTo("/");

        }
        catch
        {
            Console.WriteLine("Unauthorized users");
            //throw;
        }
        return Task.CompletedTask;
    }

    #endregion

    #region Methods

    protected async Task OnValidSubmitAsync()
    {
        IsBusy = true;
        try
        {
            var result = await Handler.LoginAsync(InputModel);
            if (result.IsSuccess)
            {
                await StateProvider.GetAuthenticationStateAsync();
                StateProvider.NotifyAuthenticationStateChanged();
                NavigationManager.NavigateTo("/");
            }
            else
                Snackbar.Add(result.Message!, Severity.Error);
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    #endregion
}