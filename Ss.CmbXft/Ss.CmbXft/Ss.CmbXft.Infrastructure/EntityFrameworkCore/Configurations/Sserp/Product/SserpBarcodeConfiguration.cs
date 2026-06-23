using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ss.CmbXft.Domain.Entities.Sserp.Product;

namespace Ss.CmbXft.Infrastructure.EntityFrameworkCore.Configurations.Sserp.Product;

/// <summary>
/// 商品条码表实体配置
/// 对应表：[Product].[T_Barcode]，主键为 BarcodeId（int自增）
/// </summary>
public class SserpBarcodeConfiguration : IEntityTypeConfiguration<SserpBarcode>
{
    public void Configure(EntityTypeBuilder<SserpBarcode> builder)
    {
        builder.ToTable("T_Barcode", "Product", t => t.ExcludeFromMigrations());
        builder.HasKey(e => e.BarcodeId);
        builder.Property(e => e.BarcodeId).HasColumnName("BarcodeId").ValueGeneratedOnAdd();
        builder.Property(e => e.Barcode).HasColumnName("Barcode").HasMaxLength(30).IsRequired();
        builder.Property(e => e.ProductCode).HasColumnName("ProductCode").HasMaxLength(20).IsRequired();
        builder.Property(e => e.UOMID).HasColumnName("UOMID").IsRequired();
        builder.Property(e => e.IsOrder).HasColumnName("IsOrder").IsRequired();
        builder.Property(e => e.IsSelling).HasColumnName("IsSelling").IsRequired();
        builder.Property(e => e.Status).HasColumnName("Status").HasColumnType("smallint").IsRequired();
        builder.Property(e => e.CreatedOn).HasColumnName("CreatedOn").HasColumnType("datetime2(7)").IsRequired();
        builder.Property(e => e.CreatedBy).HasColumnName("CreatedBy").IsRequired();
        builder.Property(e => e.ModifiedOn).HasColumnName("ModifiedOn").HasColumnType("datetime2(7)");
        builder.Property(e => e.ModifiedBy).HasColumnName("ModifiedBy");
        // Barcode 有唯一索引
        builder.HasIndex(e => e.Barcode).IsUnique();
    }
}
