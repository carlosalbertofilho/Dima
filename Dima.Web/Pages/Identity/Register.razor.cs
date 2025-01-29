using Dima.Core.Handlers;
using Dima.Core.Requests.Account;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace Dima.Web.Pages.Identity;

public partial class RegisterPage : ComponentBase
{
    #region Dependencies
    [Inject]
    protected ISnackbar Snackbar { get; set; } = null!;
    [Inject]
    protected IAccountHandler AccountHandler { get; set; } = null!;
    [Inject]
    protected NavigationManager NavigationManager { get; set; } = null!;
    [Inject]
    protected AuthenticationStateProvider StateProvider { get; set; } = null!;
    #endregion

    #region Properties
    
    protected bool IsBusy { get; set; } = false;
    protected RegisterRequest InputModel { get; set; } = new();
    
    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        var authState = await StateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        if (user.Identity is { IsAuthenticated: true })
            NavigationManager.NavigateTo("/");
    }

    #endregion

    #region Methods

    protected async Task OnValidSubmitAsync()
    {
        IsBusy = true;
        try
        {
            var result = await AccountHandler.RegisterAsync(InputModel);
            if (result.IsSuccess)
                NavigationManager.NavigateTo("/login");
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