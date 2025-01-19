using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings.Identity;

public class IdentityUserLoginMapping : IEntityTypeConfiguration<IdentityUserLogin<long>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<long>> b)
    {
        b.ToTable("IdentityUserLogin");
        b.HasKey(ul => new {ul.LoginProvider, ul.ProviderKey});
        b.Property(ul => ul.LoginProvider).HasMaxLength(128);
        b.Property(ul => ul.ProviderKey).HasMaxLength(128);
        b.Property(ul => ul.ProviderDisplayName).HasMaxLength(255);
    }
}