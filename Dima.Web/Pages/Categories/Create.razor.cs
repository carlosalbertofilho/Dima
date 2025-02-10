using Dima.Core.Handlers;
using Dima.Core.Requests.Categories;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Categories;

public partial class CreateCategoryPage : ComponentBase
{
    #region Properties

    protected bool IsBusy { get; set; } = false;
    protected CreateCategoryRequest InputModel { get; set; } = new();

    #endregion

    #region Services

    [Inject] protected ICategoryHandler Handler { get; set; } = null!;

    [Inject] protected NavigationManager NavigationManager { get; set; } = null!;

    [Inject] protected ISnackbar Snackbar { get; set; } = null!;

    #endregion

    #region Methods

    protected async Task OnValidSubmitAsync()
    {
        IsBusy = true;

        try
        {
            var result = await Handler.CreateAsync(InputModel);
            if (result.IsSuccess)
            {
                Snackbar.Add(result.Message!, Severity.Success);
                NavigationManager.NavigateTo("/categorias");
            }
            else
                Snackbar.Add(result.Message!, Severity.Error);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    #endregion
}