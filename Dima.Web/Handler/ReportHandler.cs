using System.Net.Http.Json;
using Dima.Core.Handlers;
using Dima.Core.Models.Reports;
using Dima.Core.Requests.Reports;
using Dima.Core.Responses;

namespace Dima.Web.Handler;

public class ReportHandler(IHttpClientFactory httpClientFactory) : IReportHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
    private const string Url = "v1/reports";

    public async Task<Response<List<IncomesAndExpenses>?>> GetIncomesAndExpensesReportAsync
        (GetIncomesAndExpensesRequest request) =>
        await _client.GetFromJsonAsync<Response<List<IncomesAndExpenses>?>>($"{Url}/incomes-expenses")
        ?? new Response<List<IncomesAndExpenses>?>(null, 400, "Não foi possível obter os dados");

    public async Task<Response<List<IncomesByCategory>?>> GetIncomesByCategoryReportAsync
        (GetIncomesByCategoryRequest request) =>
        await _client.GetFromJsonAsync<Response<List<IncomesByCategory>?>>($"{Url}/incomes")
        ?? new Response<List<IncomesByCategory>?>(null, 400, "Não foi possível obter os dados");
        

    public async Task<Response<List<ExpensesByCategory>?>> GetExpensesByCategoryReportAsync
        (GetExpensesByCategoryRequest request) =>
        await _client.GetFromJsonAsync<Response<List<ExpensesByCategory>?>>($"{Url}/expenses")
        ?? new Response<List<ExpensesByCategory>?>(null, 400, "Não foi possível obter os dados");

    public async Task<Response<FinancialSummary?>> GetFinancialSummaryReportAsync
        (GetFinancialSummaryRequest request) => 
        await _client.GetFromJsonAsync<Response<FinancialSummary?>>($"{Url}/summary")
        ?? new Response<FinancialSummary?>(null, 400, "Não foi possível obter os dados");
}