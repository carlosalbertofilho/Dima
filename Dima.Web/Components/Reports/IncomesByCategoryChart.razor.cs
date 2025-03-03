using Dima.Core.Handlers;
using Dima.Core.Requests.Reports;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Components.Reports;

public class IncomesByCategoryChartComponent : ComponentBase
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
        var request = new GetIncomesByCategoryRequest();
        var response = await Handler.GetIncomesByCategoryReportAsync(request);
        
        if (!response.IsSuccess || response.Data is null)
        {
            Snackbar.Add(response.Message!, Severity.Error);
            return;
        }

        foreach (var item in response.Data!)
        {
            Labels.Add($"{item.Category} ({item.Incomes:C})");
            Data.Add((double) item.Incomes);
        }
    }
    
    #endregion
}