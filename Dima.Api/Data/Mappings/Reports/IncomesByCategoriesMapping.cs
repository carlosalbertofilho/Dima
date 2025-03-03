using Dima.Core.Models.Reports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings.Reports;

public class IncomesByCategoriesMapping : IEntityTypeConfiguration<IncomesByCategory>
{
    public void Configure(EntityTypeBuilder<IncomesByCategory> builder)
    {
        builder
            .HasNoKey()
            .ToView("vwGetIncomesByCategory");
    }
}