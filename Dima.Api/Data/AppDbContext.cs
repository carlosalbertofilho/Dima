using System.Reflection;
using Dima.Api.Models;
using Dima.Core.Models;
using Dima.Core.Models.Reports;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext
    <  ApplicationUser,
       IdentityRole<long>,
       long,
       IdentityUserClaim<long>,
       IdentityUserRole<long>,
       IdentityUserLogin<long>,
       IdentityRoleClaim<long>,
       IdentityUserToken<long>
    >(options)
{
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;
    public DbSet<ExpensesByCategory> ExpensesByCategories { get; set; } = null!;
    public DbSet<IncomesByCategory> IncomesByCategories { get; set;  } = null!;
    public DbSet<IncomesAndExpenses> IncomesAndExpenses { get; set; } = null!;
    public DbSet<FinancialSummary> FinancialSummaries { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}