using System.Globalization;
using Dima.Core.Handlers;
using Dima.Core.Requests.Reports;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Components.Reports;

public partial class IncomesAndExpensesChartComponent : ComponentBase
{
    #region Properties

    protected ChartOptions Options { get; } = new();
    protected List<ChartSeries>? Series { get; set; }
    protected List<string>? Labels { get; } = [];

    #endregion

    #region Services

    [Inject] protected ISnackbar Snackbar { get; set; } = null!;
    [Inject] protected IReportHandler Handler { get; set; } = null!;

    #endregion

    #region Override

    protected override async Task OnInitializedAsync()
    {
        var request = new GetIncomesAndExpensesRequest();
        var response = await Handler.GetIncomesAndExpensesReportAsync(request);

        if (!response.IsSuccess || response.Data is null)
        {
            Snackbar.Add(response.Message!, Severity.Error);
            return;
        }
        
        var incomes = new List<double>();
        var expenses = new List<double>();

        foreach (var item in response.Data!)
        {
            incomes.Add((double) item.Incomes);
            expenses.Add(-(double) item.Expenses);
            Labels!.Add(GetMonthName(item.Month));
        }

        Options.YAxisTicks = 1000;
        Options.LineStrokeWidth = 5;
        Options.ChartPalette = ["#76FF01", Colors.Red.Default];
        Series =
        [
            new ChartSeries { Name = "Receitas", Data = incomes.ToArray() },
            new ChartSeries { Name = "Despesas", Data = expenses.ToArray() }
        ];
        StateHasChanged();
    }

    #endregion

    #region Methods

    private static string GetMonthName(int month)
        => new DateTime(DateTime.Now.Year, month, 1)
            .ToString("MMMM", CultureInfo.CurrentCulture);

    #endregion
}