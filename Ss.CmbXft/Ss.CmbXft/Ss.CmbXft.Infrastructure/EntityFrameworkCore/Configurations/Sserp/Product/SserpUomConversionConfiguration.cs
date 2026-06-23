using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ss.CmbXft.Domain.Entities.Sserp.Product;

namespace Ss.CmbXft.Infrastructure.EntityFrameworkCore.Configurations.Sserp.Product;

/// <summary>
/// 单位换算表实体配置
/// 对应表：[Product].[T_UOMConversion]
/// </summary>
public class SserpUomConversionConfiguration : IEntityTypeConfiguration<SserpUomConversion>
{
    public void Configure(EntityTypeBuilder<SserpUomConversion> builder)
    {
        builder.ToTable("T_UOMConversion", "Product", t => t.ExcludeFromMigrations());
        builder.HasKey(e => e.UOMConversionID);
        builder.Property(e => e.UOMConversionID).HasColumnName("UOMConversionID").ValueGeneratedOnAdd();
        builder.Property(e => e.ProductCode).HasColumnName("ProductCode").HasMaxLength(20);
        builder.Property(e => e.UOMID).HasColumnName("UOMID");
        builder.Property(e => e.ConversionFactor).HasColumnName("ConversionFactor").HasColumnType("decimal(10,2)");
        builder.Property(e => e.Status).HasColumnName("Status").HasColumnType("smallint");
        builder.Property(e => e.IsStock).HasColumnName("IsStock").IsRequired();
        builder.Property(e => e.CreatedOn).HasColumnName("CreatedOn").HasColumnType("datetime2(7)").IsRequired();
        builder.Property(e => e.CreatedBy).HasColumnName("CreatedBy").IsRequired();
        builder.Property(e => e.ModifiedOn).HasColumnName("ModifiedOn").HasColumnType("datetime2(7)");
        builder.Property(e => e.ModifiedBy).HasColumnName("ModifiedBy");
    }
}
