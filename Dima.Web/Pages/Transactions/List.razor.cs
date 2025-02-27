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

    protected bool IsBusy { get; set; } = false;
    protected List<Transaction> Transactions { get; set; }= [];
    protected string SearchTerm { get; set; } = string.Empty;
    protected int CurrentYear { get; set; } = DateTime.Now.Year;
    protected int CurrentMonth { get; set; } = DateTime.Now.Month;
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

    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
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
        }
    }

    #endregion
    
    #region Methods

    protected Func<Transaction, bool> Filter => transactions =>
        transactions.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)
        || transactions.Title.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)
        || string.IsNullOrEmpty(SearchTerm) == true;

    #endregion
}