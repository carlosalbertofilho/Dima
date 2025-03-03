using Dima.Core.Handlers;
using Dima.Core.Models.Reports;
using Dima.Core.Requests.Reports;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages;

public partial class HomePage : ComponentBase
{
    #region Properties

    protected bool ShowValues { get; set; } = true;
    protected FinancialSummary? Summary { get; set; }

    #endregion
    
    #region Methods
    
    [Inject] protected IReportHandler Handler { get; set; } = null!;
    [Inject] protected ISnackbar Snackbar { get; set; } = null!;
    
    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        var request = new GetFinancialSummaryRequest();
        var response = await Handler.GetFinancialSummaryReportAsync(request);
        
        if (!response.IsSuccess || response.Data is null)
        {
            Snackbar.Add(response.Message!, Severity.Error);
            return;
        }
        
        Summary = response.Data;
    }

    #endregion

    #region Methods

    protected void ToggleValues() => ShowValues = !ShowValues;

    #endregion
}