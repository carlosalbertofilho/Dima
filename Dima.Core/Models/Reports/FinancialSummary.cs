namespace Dima.Core.Models.Reports;

public record FinancialSummary(string UserId, decimal Incomes, decimal Expenses)
{
    // The expenses values are negative number on database
    public decimal Balance => Incomes + Expenses;
};