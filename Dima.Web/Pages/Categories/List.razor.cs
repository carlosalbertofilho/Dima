using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Categories;


public partial class ListCategoryPage : ComponentBase
{
    #region Properties

    protected bool IsBusy { get; set; } = false;
    protected List<Category> Categories { get; set; } = [];
    protected string SearchText { get; set; } = string.Empty;

    #endregion

    #region Services

    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [Inject] private ICategoryHandler Handler { get; set; } = null!;
    [Inject] private IDialogService DialogService { get; set; } = null!;

    #endregion

    #region Override

    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;
        try
        {
            var request = new GetAllCategoriesRequest();
            var result = await Handler.GetAllAsync(request);
            if (result.IsSuccess) Categories = result.Data! ?? [];
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

    #region Methods

    protected Func<Category, bool> SearchFunc => category 
        => string.IsNullOrWhiteSpace(SearchText) ||
           category.Id.ToString().Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
           category.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
           category.Description?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) == true;

    
    protected async Task OnDeleteButtonClickedAsync(long id, string title)
    {
        var result = await DialogService.ShowMessageBox
        ( "ATENÇÃO"
            , $"Deseja excluir a categoria {title}?\n" +
              $"Esta é uma ação irreversível!\n" +
              $"Deseja continuar?"
            , yesText: "Sim"
            , cancelText: "Cancelar");
        
        if (result is true ) await OnDeleteAsync(id, title);
        
        StateHasChanged();
    }

    private async Task OnDeleteAsync(long id, string title)
    {
        try
        {
            var request = new DeleteCategoryRequest
            {
                Id = id
            };
            await Handler.DeleteAsync(request);
            Categories.RemoveAll(x => x.Id == id);
            Snackbar.Add($"Categoria {title} excluída com sucesso!", Severity.Success);
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message, Severity.Error);
        }
    }
    #endregion
}