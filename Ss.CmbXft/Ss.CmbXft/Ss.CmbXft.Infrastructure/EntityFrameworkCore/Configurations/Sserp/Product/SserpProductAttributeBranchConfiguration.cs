using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ss.CmbXft.Domain.Entities.Sserp.Product;

namespace Ss.CmbXft.Infrastructure.EntityFrameworkCore.Configurations.Sserp.Product;

/// <summary>
/// 商品属性门店关联表实体配置
/// 对应表：[Product].[T_ProductAttributeBranch]
/// </summary>
public class SserpProductAttributeBranchConfiguration : IEntityTypeConfiguration<SserpProductAttributeBranch>
{
    public void Configure(EntityTypeBuilder<SserpProductAttributeBranch> builder)
    {
        builder.ToTable("T_ProductAttributeBranch", "Product", t => t.ExcludeFromMigrations());
        builder.HasKey(e => e.ProductAttributeBranchID);
        builder.Property(e => e.ProductAttributeBranchID).HasColumnName("ProductAttributeBranchID").ValueGeneratedOnAdd();
        builder.Property(e => e.ProductAttributeId).HasColumnName("ProductAttributeId");
        builder.Property(e => e.BranchId).HasColumnName("BranchId");
        builder.Property(e => e.Status).HasColumnName("Status").HasColumnType("smallint");
        builder.Property(e => e.CreatedOn).HasColumnName("CreatedOn").HasColumnType("datetime2(7)").IsRequired();
        builder.Property(e => e.CreatedBy).HasColumnName("CreatedBy").IsRequired();
        builder.Property(e => e.ModifiedOn).HasColumnName("ModifiedOn").HasColumnType("datetime2(7)");
        builder.Property(e => e.ModifiedBy).HasColumnName("ModifiedBy");
    }
}
