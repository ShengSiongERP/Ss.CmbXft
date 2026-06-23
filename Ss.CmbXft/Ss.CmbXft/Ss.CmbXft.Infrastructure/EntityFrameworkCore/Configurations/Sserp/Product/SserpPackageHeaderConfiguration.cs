using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ss.CmbXft.Domain.Entities.Sserp.Product;

namespace Ss.CmbXft.Infrastructure.EntityFrameworkCore.Configurations.Sserp.Product;

/// <summary>
/// 包装头表实体配置
/// 对应表：[Product].[T_PackageHeader]
/// </summary>
public class SserpPackageHeaderConfiguration : IEntityTypeConfiguration<SserpPackageHeader>
{
    public void Configure(EntityTypeBuilder<SserpPackageHeader> builder)
    {
        builder.ToTable("T_PackageHeader", "Product", t => t.ExcludeFromMigrations());
        builder.HasKey(e => e.PackageHeaderID);
        builder.Property(e => e.PackageHeaderID).HasColumnName("PackageHeaderID").ValueGeneratedOnAdd();
        builder.Property(e => e.ProductCode).HasColumnName("ProductCode").HasMaxLength(20);
        builder.Property(e => e.ValidFrom).HasColumnName("ValidFrom").HasColumnType("smalldatetime");
        builder.Property(e => e.ValidTill).HasColumnName("ValidTill").HasColumnType("smalldatetime");
        builder.Property(e => e.Status).HasColumnName("Status").HasColumnType("smallint");
        builder.Property(e => e.CreatedOn).HasColumnName("CreatedOn").HasColumnType("datetime2(7)").IsRequired();
        builder.Property(e => e.CreatedBy).HasColumnName("CreatedBy").IsRequired();
        builder.Property(e => e.ModifiedOn).HasColumnName("ModifiedOn").HasColumnType("datetime2(7)");
        builder.Property(e => e.ModifiedBy).HasColumnName("ModifiedBy");
    }
}
