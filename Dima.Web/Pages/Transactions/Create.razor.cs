using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Requests.Transactions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Transactions;

public partial class CreateTransactionsPage : ComponentBase
{
    #region Properties

    protected bool IsBusy { get; set; } = false;
    protected CreateTransactionRequest InputModel { get; set; } = new();
    protected List<Category> Categories { get; set; } = [];

    #endregion

    #region Services

    [Inject] protected ICategoryHandler CategoryHandler { get; set; } = null!;
    [Inject] protected ITransactionHandler TransactionHandler { get; set; } = null!;
    [Inject] protected NavigationManager NavigationManager { get; set; } = null!;
    [Inject] protected ISnackbar Snackbar { get; set; } = null!;

    #endregion

    #region Override

    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;

        try
        {
            var request = new GetAllCategoriesRequest();
            var result = await CategoryHandler.GetAllAsync(request);
            if (result.IsSuccess)
            {
                Categories = result.Data ?? [];
                InputModel.CategoryId = Categories.FirstOrDefault()?.Id ?? 0;
                StateHasChanged();
            }
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

    #region Methods

    protected async Task OnValidSubmitAsync()
    {
        IsBusy = true;
        try
        {
            var result = await TransactionHandler.CreateAsync(InputModel);
            if (result.IsSuccess)
            {
                Snackbar.Add(result.Message!, Severity.Success);
                NavigationManager.NavigateTo("/lancamentos/historico");
            }
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