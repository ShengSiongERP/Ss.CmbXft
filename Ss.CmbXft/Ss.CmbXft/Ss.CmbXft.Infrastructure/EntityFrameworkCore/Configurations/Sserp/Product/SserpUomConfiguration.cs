using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ss.CmbXft.Domain.Entities.Sserp.Product;

namespace Ss.CmbXft.Infrastructure.EntityFrameworkCore.Configurations.Sserp.Product;

/// <summary>
/// 计量单位表实体配置
/// 对应表：[Product].[T_UOM]
/// </summary>
public class SserpUomConfiguration : IEntityTypeConfiguration<SserpUom>
{
    public void Configure(EntityTypeBuilder<SserpUom> builder)
    {
        builder.ToTable("T_UOM", "Product", t => t.ExcludeFromMigrations());
        builder.HasKey(e => e.UOMID);
        builder.Property(e => e.UOMID).HasColumnName("UOMID").ValueGeneratedOnAdd();
        builder.Property(e => e.UOMName).HasColumnName("UOMName").HasMaxLength(40);
        builder.Property(e => e.UOMDescription).HasColumnName("UOMDescription").HasMaxLength(60);
        builder.Property(e => e.Status).HasColumnName("Status").HasColumnType("smallint");
        builder.Property(e => e.CreatedOn).HasColumnName("CreatedOn").HasColumnType("datetime2(7)").IsRequired();
        builder.Property(e => e.CreatedBy).HasColumnName("CreatedBy").IsRequired();
        builder.Property(e => e.ModifiedOn).HasColumnName("ModifiedOn").HasColumnType("datetime2(7)");
        builder.Property(e => e.ModifiedBy).HasColumnName("ModifiedBy");
    }
}
