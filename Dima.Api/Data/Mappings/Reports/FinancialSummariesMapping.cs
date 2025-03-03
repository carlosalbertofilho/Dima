using Dima.Core.Models.Reports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings.Reports;

public class FinancialSummariesMapping : IEntityTypeConfiguration<FinancialSummary>
{
    public void Configure(EntityTypeBuilder<FinancialSummary> builder)
    {
        builder
            .HasNoKey()
            .ToView("vwGetFinancialSummary");
    }
}