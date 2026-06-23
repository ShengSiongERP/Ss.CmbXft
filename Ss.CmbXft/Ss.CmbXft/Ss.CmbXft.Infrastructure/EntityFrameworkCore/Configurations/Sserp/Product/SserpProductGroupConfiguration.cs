using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ss.CmbXft.Domain.Entities.Sserp.Product;

namespace Ss.CmbXft.Infrastructure.EntityFrameworkCore.Configurations.Sserp.Product;

/// <summary>
/// 商品分组表实体配置
/// 对应表：[Product].[T_ProductGroup]
/// </summary>
public class SserpProductGroupConfiguration : IEntityTypeConfiguration<SserpProductGroup>
{
    public void Configure(EntityTypeBuilder<SserpProductGroup> builder)
    {
        builder.ToTable("T_ProductGroup", "Product", t => t.ExcludeFromMigrations());
        builder.HasKey(e => e.ProductGroupID);
        builder.Property(e => e.ProductGroupID).HasColumnName("ProductGroupID").ValueGeneratedOnAdd();
        builder.Property(e => e.DepartmentID).HasColumnName("DepartmentID");
        builder.Property(e => e.CategoryID).HasColumnName("CategoryID");
        builder.Property(e => e.SubCategoryID).HasColumnName("SubCategoryID");
        builder.Property(e => e.SegmentID).HasColumnName("SegmentID");
        builder.Property(e => e.Temperature).HasColumnName("Temperature").HasColumnType("decimal(8,3)");
        builder.Property(e => e.Status).HasColumnName("Status").HasColumnType("smallint");
        builder.Property(e => e.ItemClassID).HasColumnName("ItemClassID");
        builder.Property(e => e.CreatedOn).HasColumnName("CreatedOn").HasColumnType("datetime2(7)").IsRequired();
        builder.Property(e => e.CreatedBy).HasColumnName("CreatedBy").IsRequired();
        builder.Property(e => e.ModifiedOn).HasColumnName("ModifiedOn").HasColumnType("datetime2(7)");
        builder.Property(e => e.ModifiedBy).HasColumnName("ModifiedBy");
    }
}
