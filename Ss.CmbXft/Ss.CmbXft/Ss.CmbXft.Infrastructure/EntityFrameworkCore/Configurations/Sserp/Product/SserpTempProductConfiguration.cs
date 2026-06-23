using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ss.CmbXft.Domain.Entities.Sserp.Product;

namespace Ss.CmbXft.Infrastructure.EntityFrameworkCore.Configurations.Sserp.Product;

/// <summary>
/// 临时商品表实体配置
/// 对应表：[Product].[T_TempProduct]
/// </summary>
public class SserpTempProductConfiguration : IEntityTypeConfiguration<SserpTempProduct>
{
    public void Configure(EntityTypeBuilder<SserpTempProduct> builder)
    {
        builder.ToTable("T_TempProduct", "Product", t => t.ExcludeFromMigrations());
        builder.HasKey(e => e.TempProductID);
        builder.Property(e => e.TempProductID).HasColumnName("TempProductID").ValueGeneratedOnAdd();
        builder.Property(e => e.CartonBarcode).HasColumnName("CartonBarcode").HasMaxLength(20);
        builder.Property(e => e.Barcode).HasColumnName("Barcode").HasMaxLength(20).IsRequired();
        builder.Property(e => e.ProductDescription).HasColumnName("ProductDescription").HasMaxLength(60).IsRequired();
        builder.Property(e => e.Brand).HasColumnName("Brand").HasMaxLength(40).IsRequired();
        builder.Property(e => e.CountryOfOrigin).HasColumnName("CountryOfOrigin");
        builder.Property(e => e.ProductType).HasColumnName("ProductType").HasMaxLength(60);
        builder.Property(e => e.UOM).HasColumnName("UOM").HasMaxLength(2).IsRequired();
        builder.Property(e => e.UOMConversionFactor).HasColumnName("UOMConversionFactor").HasColumnType("decimal(8,3)");
        builder.Property(e => e.ProductCartonGrossWeight).HasColumnName("ProductCartonGrossWeight").HasColumnType("decimal(8,3)");
        builder.Property(e => e.ProductCartonWidth).HasColumnName("ProductCartonWidth").HasColumnType("decimal(8,3)");
        builder.Property(e => e.ProductCartonLength).HasColumnName("ProductCartonLength").HasColumnType("decimal(8,3)");
        builder.Property(e => e.ProductCartonHeight).HasColumnName("ProductCartonHeight").HasColumnType("decimal(8,3)");
        builder.Property(e => e.ProductNetWeight).HasColumnName("ProductNetWeight").HasColumnType("decimal(8,3)");
        builder.Property(e => e.ProductGrossWeight).HasColumnName("ProductGrossWeight").HasColumnType("decimal(8,3)");
        builder.Property(e => e.ProductWidth).HasColumnName("ProductWidth").HasColumnType("decimal(8,3)");
        builder.Property(e => e.ProductLength).HasColumnName("ProductLength").HasColumnType("decimal(8,3)");
        builder.Property(e => e.ProductHeight).HasColumnName("ProductHeight").HasColumnType("decimal(8,3)");
        builder.Property(e => e.CartonCostWG).HasColumnName("CartonCostWG").HasColumnType("decimal(18,6)").IsRequired();
        builder.Property(e => e.LooseCostWG).HasColumnName("LooseCostWG").HasColumnType("decimal(18,6)").IsRequired();
        builder.Property(e => e.RSP).HasColumnName("RSP").HasColumnType("decimal(18,6)").IsRequired();
        builder.Property(e => e.Margin).HasColumnName("Margin").HasColumnType("decimal(10,3)").IsRequired();
        builder.Property(e => e.TaxRate).HasColumnName("TaxRate").HasColumnType("decimal(8,3)").IsRequired();
        builder.Property(e => e.Remarks).HasColumnName("Remarks").HasMaxLength(100);
        builder.Property(e => e.Department).HasColumnName("Department").HasMaxLength(40).IsRequired();
        builder.Property(e => e.Category).HasColumnName("Category").HasMaxLength(40).IsRequired();
        builder.Property(e => e.SubCategory).HasColumnName("SubCategory").HasMaxLength(40).IsRequired();
        builder.Property(e => e.Segment).HasColumnName("Segment").HasMaxLength(40).IsRequired();
        builder.Property(e => e.ItemClass).HasColumnName("ItemClass").HasMaxLength(40).IsRequired();
        builder.Property(e => e.IsAutoZero).HasColumnName("IsAutoZero").IsRequired();
        builder.Property(e => e.IsSentToScale).HasColumnName("IsSentToScale").IsRequired();
        builder.Property(e => e.Temperature).HasColumnName("Temperature").HasColumnType("decimal(8,2)");
        builder.Property(e => e.Vendor).HasColumnName("Vendor").HasMaxLength(40);
        builder.Property(e => e.TaxCode).HasColumnName("TaxCode").HasMaxLength(50);
        builder.Property(e => e.Buyer).HasColumnName("Buyer").HasMaxLength(10);
        builder.Property(e => e.ShelfLife).HasColumnName("ShelfLife").HasMaxLength(10);
        builder.Property(e => e.OverrideTax).HasColumnName("OverrideTax").HasMaxLength(10);
        builder.Property(e => e.TareWeight).HasColumnName("TareWeight").HasColumnType("decimal(8,3)");
        builder.Property(e => e.ProductImage).HasColumnName("ProductImage");
        builder.Property(e => e.Status).HasColumnName("Status").HasColumnType("smallint");
        builder.Property(e => e.CreatedOn).HasColumnName("CreatedOn").HasColumnType("datetime2(7)").IsRequired();
        builder.Property(e => e.CreatedBy).HasColumnName("CreatedBy").IsRequired();
        builder.Property(e => e.ModifiedOn).HasColumnName("ModifiedOn").HasColumnType("datetime2(7)");
        builder.Property(e => e.ModifiedBy).HasColumnName("ModifiedBy");
    }
}
