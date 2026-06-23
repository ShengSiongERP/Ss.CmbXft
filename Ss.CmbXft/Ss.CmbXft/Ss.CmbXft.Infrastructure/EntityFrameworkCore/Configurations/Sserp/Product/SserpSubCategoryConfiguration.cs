using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ss.CmbXft.Domain.Entities.Sserp.Product;

namespace Ss.CmbXft.Infrastructure.EntityFrameworkCore.Configurations.Sserp.Product;

/// <summary>
/// 商品子类别表实体配置
/// 对应表：[Product].[T_SubCategory]
/// </summary>
public class SserpSubCategoryConfiguration : IEntityTypeConfiguration<SserpSubCategory>
{
    public void Configure(EntityTypeBuilder<SserpSubCategory> builder)
    {
        builder.ToTable("T_SubCategory", "Product", t => t.ExcludeFromMigrations());
        builder.HasKey(e => e.SubCategoryID);
        builder.Property(e => e.SubCategoryID).HasColumnName("SubCategoryID").ValueGeneratedOnAdd();
        builder.Property(e => e.SubCategoryName).HasColumnName("SubCategoryName").HasMaxLength(40);
        builder.Property(e => e.SubCategoryDescription).HasColumnName("SubCategoryDescription").HasMaxLength(60);
        builder.Property(e => e.Status).HasColumnName("Status").HasColumnType("smallint");
        builder.Property(e => e.CreatedOn).HasColumnName("CreatedOn").HasColumnType("datetime2(7)").IsRequired();
        builder.Property(e => e.CreatedBy).HasColumnName("CreatedBy").IsRequired();
        builder.Property(e => e.ModifiedOn).HasColumnName("ModifiedOn").HasColumnType("datetime2(7)");
        builder.Property(e => e.ModifiedBy).HasColumnName("ModifiedBy");
    }
}
