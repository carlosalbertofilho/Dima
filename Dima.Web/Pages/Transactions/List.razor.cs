using Dima.Core.Common.Extensions;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Transactions;

public partial class ListTransactionsPage : ComponentBase
{
    #region Properties
    
    private int _currentYear = DateTime.Now.Year;
    private int _currentMonth = DateTime.Now.Month;
    protected bool IsBusy { get; set; } = false;
    protected List<Transaction> Transactions { get; private set; }= [];
    protected string SearchTerm { get; set; } = string.Empty;

    protected int CurrentYear
    {
        get => _currentYear;
        set
        {
            _currentYear = value;
            GetTransactionsAsync();
        }
    }

    protected int CurrentMonth
    {
        get => _currentMonth;
        set
        {
            _currentMonth = value;
            GetTransactionsAsync();
        }
    }
    protected int[] Years { get; set; } = 
    [
        DateTime.Now.Year,
        DateTime.Now.Year - 1,
        DateTime.Now.Year - 2,
        DateTime.Now.Year - 3,
    ];

    #endregion

    #region Services

    [Inject] protected ITransactionHandler Handler { get; set; } = null!;
    [Inject] protected NavigationManager NavigationManager { get; set; } = null!;
    [Inject] protected ISnackbar Snackbar { get; set; } = null!;
    [Inject] protected IDialogService DialogService { get; set; } = null!;

    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
        => await GetTransactionsAsync();

    #endregion
    
    #region Methods

    private async Task GetTransactionsAsync()
    {
        IsBusy = true;
        try
        {
            var request = new GetTransactionsByPeriodRequest
            {
                StartDate = DateTime.Now.GetFirstDayOfMonth(CurrentMonth, CurrentYear),
                EndDate = DateTime.Now.GetLastDayOfMonth(CurrentMonth, CurrentYear),
                PageNumber = 1,
                PageSize = 50
            };
            
            var result = await Handler.GetByPeriodAsync(request);
            if (result.IsSuccess)
                Transactions = result.Data ?? [];
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
            StateHasChanged();
        }
    }
    
    protected async Task OnDeleteButtonClick(long id, string title)
    {
        var result = await DialogService.ShowMessageBox
        ( "ATENÇÃO"
         , $"Deseja excluir o lançamento {title}?\n" +
           "Esta é uma ação irreversível\n" +
           "Deseja continuar?"
         , yesText: "Sim"
         , cancelText: "Cancelar");
        if (result == true) await OnDelete(id, title);
    }

    private async Task OnDelete(long id, string title)
    {
        IsBusy = true;
        try
        {
            var request = new DeleteTransactionRequest { Id = id };
            var response = await Handler.DeleteAsync(request);
            if (response.IsSuccess)
            {
                Snackbar.Add(response.Message!, Severity.Success);
                Transactions.RemoveAll(x => x.Id == id);
                StateHasChanged();
            }
            else
                Snackbar.Add(response.Message!, Severity.Error);
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

    protected Func<Transaction, bool> Filter => transactions =>
        transactions.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)
        || transactions.Title.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)
        || string.IsNullOrEmpty(SearchTerm) == true;

    #endregion
}