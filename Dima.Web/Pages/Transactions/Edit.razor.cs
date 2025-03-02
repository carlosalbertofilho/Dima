using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Requests.Transactions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Transactions;

public partial class EditTransactionPage : ComponentBase
{
    #region Properties
    [Parameter] public string Id { get; set; } = string.Empty;

    protected bool IsBusy { get; private set; }
    protected UpdateTransactionRequest InputModel { get; set; } = new();
    protected List<Category> Categories { get; private set; } = [];

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
        await GetTransactionById();
        await GetCategories();
    }

    #endregion

    #region MyRegion

    protected async Task OnValidSubmitAsync()
    {
        IsBusy = true;
        try
        {
            var result = await TransactionHandler.UpdateAsync(InputModel!);
            if (result.IsSuccess)
            {
                Snackbar.Add(result.Message!, Severity.Success);
                NavigationManager.NavigateTo("/lancamentos/historico");
            }
        }
        catch
        {
            Snackbar.Add("Erro ao atualizar lançamento {#Id: " + Id + "}", Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }
    private async Task GetTransactionById()
    {
        IsBusy = true;
        try
        {
            var request = new GetTransactionByIdRequest { Id = long.Parse(Id) };
            var response = await TransactionHandler.GetByIdAsync(request);
            if (response is {IsSuccess: true, Data: not null}) InputModel = response.Data;
        }
        catch
        {
            Snackbar.Add($"Erro ao obter lançamento Id: {Id}", Severity.Error);
        }
        finally
        {
            IsBusy = false;
            StateHasChanged();
        }
    }

    private async Task GetCategories()
    {
        IsBusy = true;
        try
        {
            var request = new GetAllCategoriesRequest();
            var response = await CategoryHandler.GetAllAsync(request);
            if (response is {IsSuccess: true, Data: not null}) Categories = response.Data!;
        }
        catch
        {
            Snackbar.Add($"Erro ao obter categorias", Severity.Error);
        }
        finally
        {
            IsBusy = false;
            StateHasChanged();
        }
    }
    #endregion
}