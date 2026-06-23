using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ss.CmbXft.Domain.Entities.Sserp.Product;

namespace Ss.CmbXft.Infrastructure.EntityFrameworkCore.Configurations.Sserp.Product;

/// <summary>
/// 税率表实体配置
/// 对应表：[dbo].[T_Tax]
/// </summary>
public class SserpTaxConfiguration : IEntityTypeConfiguration<SserpTax>
{
    public void Configure(EntityTypeBuilder<SserpTax> builder)
    {
        builder.ToTable("T_Tax", "dbo", t => t.ExcludeFromMigrations());
        builder.HasKey(e => e.TaxID);
        builder.Property(e => e.TaxID).HasColumnName("TaxID");
        builder.Property(e => e.TaxName).HasColumnName("TaxName").HasMaxLength(40);
        builder.Property(e => e.TaxDescription).HasColumnName("TaxDescription").HasMaxLength(60);
        builder.Property(e => e.TaxRate).HasColumnName("TaxRate").HasColumnType("decimal(8,3)");
        builder.Property(e => e.ValidFrom).HasColumnName("ValidFrom");
        builder.Property(e => e.ValidTill).HasColumnName("ValidTill");
        builder.Property(e => e.Status).HasColumnName("Status").HasColumnType("smallint");
        builder.Property(e => e.CreatedOn).HasColumnName("CreatedOn").HasColumnType("datetime2(7)").IsRequired();
        builder.Property(e => e.CreatedBy).HasColumnName("CreatedBy").IsRequired();
        builder.Property(e => e.ModifiedOn).HasColumnName("ModifiedOn").HasColumnType("datetime2(7)");
        builder.Property(e => e.ModifiedBy).HasColumnName("ModifiedBy");
    }
}
