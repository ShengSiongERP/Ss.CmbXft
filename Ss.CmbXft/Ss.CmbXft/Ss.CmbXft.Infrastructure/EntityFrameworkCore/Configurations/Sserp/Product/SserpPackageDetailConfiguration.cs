using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ss.CmbXft.Domain.Entities.Sserp.Product;

namespace Ss.CmbXft.Infrastructure.EntityFrameworkCore.Configurations.Sserp.Product;

/// <summary>
/// 包装明细表实体配置
/// 对应表：[Product].[T_PackageDetail]
/// </summary>
public class SserpPackageDetailConfiguration : IEntityTypeConfiguration<SserpPackageDetail>
{
    public void Configure(EntityTypeBuilder<SserpPackageDetail> builder)
    {
        builder.ToTable("T_PackageDetail", "Product", t => t.ExcludeFromMigrations());
        builder.HasKey(e => e.PackageDetailID);
        builder.Property(e => e.PackageDetailID).HasColumnName("PackageDetailID").ValueGeneratedOnAdd();
        builder.Property(e => e.PackageHeaderID).HasColumnName("PackageHeaderID");
        builder.Property(e => e.ProductCode).HasColumnName("ProductCode").HasMaxLength(20);
        builder.Property(e => e.Quantity).HasColumnName("Quantity").HasColumnType("decimal(6,4)");
        builder.Property(e => e.RetailPrice).HasColumnName("RetailPrice").HasColumnType("decimal(18,2)");
        builder.Property(e => e.Ratio).HasColumnName("Ratio").HasColumnType("decimal(6,4)");
        builder.Property(e => e.MinPrice).HasColumnName("MinPrice").HasColumnType("decimal(18,2)");
        builder.Property(e => e.Status).HasColumnName("Status").HasColumnType("smallint");
        builder.Property(e => e.CreatedOn).HasColumnName("CreatedOn").HasColumnType("datetime2(7)").IsRequired();
        builder.Property(e => e.CreatedBy).HasColumnName("CreatedBy").IsRequired();
        builder.Property(e => e.ModifiedOn).HasColumnName("ModifiedOn").HasColumnType("datetime2(7)");
        builder.Property(e => e.ModifiedBy).HasColumnName("ModifiedBy");
    }
}
