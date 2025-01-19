using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings.Identity;

public class IdentityUserClaimMapping : IEntityTypeConfiguration<IdentityUserClaim<long>>
{
    public void Configure(EntityTypeBuilder<IdentityUserClaim<long>> b)
    {
        b.ToTable("IdentityClaim").HasKey(uc => uc.Id);
        b.Property(uc => uc.ClaimType).HasMaxLength(255);
        b.Property(uc => uc.ClaimValue).HasMaxLength(255);
    }
}