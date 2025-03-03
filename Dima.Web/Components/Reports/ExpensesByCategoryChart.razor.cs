using Dima.Core.Handlers;
using Dima.Core.Requests.Reports;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Components.Reports;

public partial class ExpensesByCategoryChartComponet : ComponentBase
{
    #region Properties

    protected List<double> Data { get; set; } = [];
    protected List<string> Labels { get; set; } = [];

    #endregion
    
    #region Services
    
    [Inject] protected IReportHandler Handler { get; set; } = null!;
    [Inject] protected ISnackbar Snackbar { get; set; } = null!;
    
    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var request = new GetExpensesByCategoryRequest();
            var response = await Handler.GetExpensesByCategoryReportAsync(request);

            if (!response.IsSuccess || response.Data is null)
            {
                Snackbar.Add(response.Message!, Severity.Error);
                return;
            }

            foreach (var expensesByCategory in response.Data!)
            {
                Labels.Add($"{expensesByCategory.Category} ({expensesByCategory.Expenses:C})");
                Data.Add(-(double) expensesByCategory.Expenses);
            }

        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message, Severity.Error);
        }
    }

    #endregion
}