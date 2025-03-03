using Dima.Core.Models.Reports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings.Reports;

public class IncomesAndExpensesMapping : IEntityTypeConfiguration<IncomesAndExpenses>
{
    public void Configure(EntityTypeBuilder<IncomesAndExpenses> builder)
    {
        builder
            .HasNoKey()
            .ToView("vwGetIncomesAndExpenses");
    }
}