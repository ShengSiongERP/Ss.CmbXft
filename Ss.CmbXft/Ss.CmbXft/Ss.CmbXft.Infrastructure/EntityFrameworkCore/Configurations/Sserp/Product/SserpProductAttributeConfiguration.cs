using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ss.CmbXft.Domain.Entities.Sserp.Product;

namespace Ss.CmbXft.Infrastructure.EntityFrameworkCore.Configurations.Sserp.Product;

/// <summary>
/// 商品属性表实体配置
/// 对应表：[Product].[T_ProductAttribute]
/// </summary>
public class SserpProductAttributeConfiguration : IEntityTypeConfiguration<SserpProductAttribute>
{
    public void Configure(EntityTypeBuilder<SserpProductAttribute> builder)
    {
        builder.ToTable("T_ProductAttribute", "Product", t => t.ExcludeFromMigrations());
        builder.HasKey(e => e.ProductAttributeID);
        builder.Property(e => e.ProductAttributeID).HasColumnName("ProductAttributeID").ValueGeneratedOnAdd();
        builder.Property(e => e.AttributeId).HasColumnName("AttributeId");
        builder.Property(e => e.ProductCode).HasColumnName("ProductCode").HasMaxLength(20);
        builder.Property(e => e.Value).HasColumnName("Value").HasMaxLength(255);
        builder.Property(e => e.Status).HasColumnName("Status").HasColumnType("smallint");
        builder.Property(e => e.CreatedOn).HasColumnName("CreatedOn").HasColumnType("datetime2(7)").IsRequired();
        builder.Property(e => e.CreatedBy).HasColumnName("CreatedBy").IsRequired();
        builder.Property(e => e.ModifiedOn).HasColumnName("ModifiedOn").HasColumnType("datetime2(7)");
        builder.Property(e => e.ModifiedBy).HasColumnName("ModifiedBy");
        // 唯一约束：ProductCode + AttributeId
        builder.HasIndex(e => new { e.ProductCode, e.AttributeId }).IsUnique();
    }
}
