using Dima.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings;

public class TransactionMapping : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder
            .ToTable("Transacoes")
            .HasKey(x => x.Id);
        
        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();
        
        builder
            .Property(x => x.UserId)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);
        
        builder
            .Property(x => x.Type)
            .IsRequired()
            .HasColumnType("SMALLINT");
        
        builder
            .Property(x => x.Amount)
            .IsRequired()
            .HasColumnType("MONEY");
        
        builder
            .Property(x => x.CategoryId)
            .HasColumnType("BIGINT");
        
        builder
            .HasOne(x => x.Category)
            .WithMany()
            .HasForeignKey(x => x.CategoryId)
            .HasConstraintName("FK_Transacoes_Categorias");
        
        builder
            .Property(x => x.PaidOrReceivedAt)
            .IsRequired()
            .HasColumnType("DATETIME");
        
        builder
            .Property(x => x.CreatedAt)
            .IsRequired()
            .HasColumnType("DATETIME");
        
        
        
        
        
    }
}