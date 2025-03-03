using Dima.Api.Data;
using Dima.Core.Enums;
using Dima.Core.Handlers;
using Dima.Core.Models.Reports;
using Dima.Core.Requests.Reports;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers;

public class ReportHandler(AppDbContext context) : IReportHandler
{
    public async Task<Response<List<IncomesAndExpenses>?>> GetIncomesAndExpensesReportAsync(GetIncomesAndExpensesRequest request)
    {
        try
        {
            var list = await context
                .IncomesAndExpenses
                .AsNoTracking()
                .Where(incomesAndExpenses => incomesAndExpenses.UserId == request.UserId)
                .OrderByDescending(incomesAndExpenses => incomesAndExpenses.Year)
                .ThenBy(incomesAndExpenses => incomesAndExpenses.Month)
                .ToListAsync();
            return new Response<List<IncomesAndExpenses>?>(list, 200, "Get incomes and expenses success!");

        }
        catch (Exception e)
        {
            return new Response<List<IncomesAndExpenses>?>(null, 500, e.Message);
        }
    }

    public async Task<Response<List<IncomesByCategory>?>> GetIncomesByCategoryReportAsync(GetIncomesByCategoryRequest request)
    {
        try
        {
            var list = await context.IncomesByCategories
                .AsNoTracking()
                .Where(incomesByCategory => incomesByCategory.UserId == request.UserId)
                .OrderByDescending(incomesByCategory => incomesByCategory.Year)
                .ThenBy(incomesByCategory => incomesByCategory.Category)
                .ToListAsync();
            return  new Response<List<IncomesByCategory>?>(list, 200, "Get incomes by category success!");
        }
        catch (Exception e)
        {
            return new Response<List<IncomesByCategory>?>(null, 500, e.Message);
        }
    }

    public async Task<Response<List<ExpensesByCategory>?>> GetExpensesByCategoryReportAsync(GetExpensesByCategoryRequest request)
    {
        try
        {
            var list = await context.ExpensesByCategories
                .AsNoTracking()
                .Where(expensesByCategory => expensesByCategory.UserId == request.UserId)
                .OrderByDescending(expensesByCategory => expensesByCategory.Year)
                .ThenBy(expensesByCategory => expensesByCategory.Category)
                .ToListAsync();
            return new Response<List<ExpensesByCategory>?>(list, 200, "Get expenses by category success!");
        }
        catch (Exception e)
        {
            return new Response<List<ExpensesByCategory>?>(null, 500, e.Message);
        }
    }

    public async Task<Response<FinancialSummary?>> GetFinancialSummaryReportAsync(GetFinancialSummaryRequest request)
    {
        try
        {
            var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var summary = await context.FinancialSummaries
                .AsNoTracking()
                .Where(f => f.UserId == request.UserId)
                .Select(f => new FinancialSummary(f.UserId, f.Incomes, f.Expenses))
                .FirstOrDefaultAsync();
            
            return new Response<FinancialSummary?>(summary, 200, "Get financial summary success!");
        }
        catch (Exception e)
        {
            return new Response<FinancialSummary?>(null, 500, e.Message);
        }
    }
}