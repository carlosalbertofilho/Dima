using Dima.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings.Identity;

public class IdentityUserMapping : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> b)
    {
        b.ToTable("IdentityUser").HasKey(u => u.Id);
        
        b.HasIndex(u => u.NormalizedUserName).IsUnique();
        b.HasIndex(u => u.NormalizedEmail).IsUnique();
        
        b.Property(u => u.Email).HasMaxLength(180);
        b.Property(u => u.NormalizedEmail).HasMaxLength(180);
        b.Property(u => u.UserName).HasMaxLength(180);
        b.Property(u => u.NormalizedUserName).HasMaxLength(180);
        b.Property(u => u.PhoneNumber).HasMaxLength(20);
        b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();
        
        b.HasMany<IdentityUserClaim<long>>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();
        b.HasMany<IdentityUserLogin<long>>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();
        b.HasMany<IdentityUserToken<long>>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();
        b.HasMany<IdentityUserRole<long>>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
    }
}