using Dima.Core.Models.Reports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings.Reports;

public class ExpensesByCategoriesMapping : IEntityTypeConfiguration<ExpensesByCategory>
{
    public void Configure(EntityTypeBuilder<ExpensesByCategory> builder)
    {
        builder
            .HasNoKey()
            .ToView("vwGetExpensesByCategory");
    }
}