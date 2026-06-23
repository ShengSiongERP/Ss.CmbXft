using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ss.CmbXft.Domain.Entities.Sserp.Product;

namespace Ss.CmbXft.Infrastructure.EntityFrameworkCore.Configurations.Sserp.Product;

/// <summary>
/// 商品类别表实体配置
/// 对应表：[Product].[T_Category]
/// </summary>
public class SserpCategoryConfiguration : IEntityTypeConfiguration<SserpCategory>
{
    public void Configure(EntityTypeBuilder<SserpCategory> builder)
    {
        builder.ToTable("T_Category", "Product", t => t.ExcludeFromMigrations());
        builder.HasKey(e => e.CategoryID);
        builder.Property(e => e.CategoryID).HasColumnName("CategoryID").ValueGeneratedOnAdd();
        builder.Property(e => e.CategoryName).HasColumnName("CategoryName").HasMaxLength(40);
        builder.Property(e => e.CategoryDescription).HasColumnName("CategoryDescription").HasMaxLength(60);
        builder.Property(e => e.Status).HasColumnName("Status").HasColumnType("smallint");
        builder.Property(e => e.CreatedOn).HasColumnName("CreatedOn").HasColumnType("datetime2(7)").IsRequired();
        builder.Property(e => e.CreatedBy).HasColumnName("CreatedBy").IsRequired();
        builder.Property(e => e.ModifiedOn).HasColumnName("ModifiedOn").HasColumnType("datetime2(7)");
        builder.Property(e => e.ModifiedBy).HasColumnName("ModifiedBy");
    }
}
