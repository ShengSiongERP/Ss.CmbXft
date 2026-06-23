using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ss.CmbXft.Domain.Entities.Sserp.Product;

namespace Ss.CmbXft.Infrastructure.EntityFrameworkCore.Configurations.Sserp.Product;

/// <summary>
/// 商品信息表实体配置
/// 对应表：[Product].[T_Product]，主键为 ProductCode（string类型）
/// </summary>
public class SserpProductConfiguration : IEntityTypeConfiguration<SserpProduct>
{
    public void Configure(EntityTypeBuilder<SserpProduct> builder)
    {
        builder.ToTable("T_Product", "Product", t => t.ExcludeFromMigrations());
        builder.HasKey(e => e.ProductCode);
        builder.Property(e => e.ProductCode).HasColumnName("ProductCode").HasMaxLength(20).IsRequired();
        builder.Property(e => e.ProductDescription1).HasColumnName("ProductDescription1").HasMaxLength(60);
        builder.Property(e => e.ProductDescription2).HasColumnName("ProductDescription2").HasMaxLength(60);
        builder.Property(e => e.ProductDescription3).HasColumnName("ProductDescription3").HasMaxLength(60);
        builder.Property(e => e.ProductGroupID).HasColumnName("ProductGroupID");
        builder.Property(e => e.TaxID).HasColumnName("TaxID");
        builder.Property(e => e.BrandID).HasColumnName("BrandID");
        builder.Property(e => e.DefaultUOMID).HasColumnName("DefaultUOMID");
        builder.Property(e => e.ProductTypeName).HasColumnName("ProductTypeName").HasMaxLength(10);
        builder.Property(e => e.ProductNetWeight).HasColumnName("ProductNetWeight").HasColumnType("decimal(8,3)");
        builder.Property(e => e.ProductGrossWeight).HasColumnName("ProductGrossWeight").HasColumnType("decimal(8,3)");
        builder.Property(e => e.ProductLength).HasColumnName("ProductLength").HasColumnType("decimal(8,3)");
        builder.Property(e => e.ProductHeight).HasColumnName("ProductHeight").HasColumnType("decimal(8,3)");
        builder.Property(e => e.ProductWidth).HasColumnName("ProductWidth").HasColumnType("decimal(8,3)");
        builder.Property(e => e.Status).HasColumnName("Status").HasColumnType("smallint");
        builder.Property(e => e.CountryOfOrigin).HasColumnName("CountryOfOrigin").HasMaxLength(20);
        builder.Property(e => e.ProductImage).HasColumnName("ProductImage");
        builder.Property(e => e.CreatedOn).HasColumnName("CreatedOn").HasColumnType("datetime2(7)").IsRequired();
        builder.Property(e => e.CreatedBy).HasColumnName("CreatedBy").IsRequired();
        builder.Property(e => e.ModifiedOn).HasColumnName("ModifiedOn").HasColumnType("datetime2(7)");
        builder.Property(e => e.ModifiedBy).HasColumnName("ModifiedBy");
    }
}
