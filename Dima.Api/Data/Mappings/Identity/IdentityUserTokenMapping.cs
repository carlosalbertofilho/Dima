using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings.Identity;

public class IdentityUserTokenMapping : IEntityTypeConfiguration<IdentityUserToken<long>>
{
    public void Configure(EntityTypeBuilder<IdentityUserToken<long>> b)
    {
        b.ToTable("IdentityUserToken");
        b.HasKey(ut => new {ut.UserId, ut.LoginProvider, ut.Name});
        b.Property(ut => ut.LoginProvider).HasMaxLength(128);
        b.Property(ut => ut.Name).HasMaxLength(180);
    }
}