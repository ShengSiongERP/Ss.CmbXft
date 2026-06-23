using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ss.CmbXft.Domain.Entities.Sserp.Product;

namespace Ss.CmbXft.Infrastructure.EntityFrameworkCore.Configurations.Sserp.Product;

/// <summary>
/// 品牌表实体配置
/// 对应表：[Product].[T_Brand]
/// </summary>
public class SserpBrandConfiguration : IEntityTypeConfiguration<SserpBrand>
{
    public void Configure(EntityTypeBuilder<SserpBrand> builder)
    {
        builder.ToTable("T_Brand", "Product", t => t.ExcludeFromMigrations());
        builder.HasKey(e => e.BrandID);
        builder.Property(e => e.BrandID).HasColumnName("BrandID").ValueGeneratedOnAdd();
        builder.Property(e => e.BrandName).HasColumnName("BrandName").HasMaxLength(40);
        builder.Property(e => e.BrandDescription).HasColumnName("BrandDescription").HasMaxLength(60);
        builder.Property(e => e.Status).HasColumnName("Status").HasColumnType("smallint").IsRequired();
        builder.Property(e => e.CreatedOn).HasColumnName("CreatedOn").HasColumnType("datetime2(7)").IsRequired();
        builder.Property(e => e.CreatedBy).HasColumnName("CreatedBy").IsRequired();
        builder.Property(e => e.ModifiedOn).HasColumnName("ModifiedOn").HasColumnType("datetime2(7)");
        builder.Property(e => e.ModifiedBy).HasColumnName("ModifiedBy");
    }
}
