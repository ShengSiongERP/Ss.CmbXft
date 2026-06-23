using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ss.CmbXft.Domain.Entities.Sserp.Product;

namespace Ss.CmbXft.Infrastructure.EntityFrameworkCore.Configurations.Sserp.Product;

/// <summary>
/// 商品分类表实体配置
/// 对应表：[Product].[T_ItemClass]
/// </summary>
public class SserpItemClassConfiguration : IEntityTypeConfiguration<SserpItemClass>
{
    public void Configure(EntityTypeBuilder<SserpItemClass> builder)
    {
        builder.ToTable("T_ItemClass", "Product", t => t.ExcludeFromMigrations());
        builder.HasKey(e => e.ItemClassID);
        builder.Property(e => e.ItemClassID).HasColumnName("ItemClassID").ValueGeneratedOnAdd();
        builder.Property(e => e.ItemClassName).HasColumnName("ItemClassName").HasMaxLength(60);
        builder.Property(e => e.ItemClassCode).HasColumnName("ItemClassCode").HasMaxLength(40);
        builder.Property(e => e.TaxID).HasColumnName("TaxID").IsRequired();
        builder.Property(e => e.Status).HasColumnName("Status").HasColumnType("smallint");
        builder.Property(e => e.NCDepartmentCode).HasColumnName("NCDepartmentCode").HasMaxLength(5);
        builder.Property(e => e.CreatedOn).HasColumnName("CreatedOn").HasColumnType("datetime2(7)").IsRequired();
        builder.Property(e => e.CreatedBy).HasColumnName("CreatedBy").IsRequired();
        builder.Property(e => e.ModifiedOn).HasColumnName("ModifiedOn").HasColumnType("datetime2(7)");
        builder.Property(e => e.ModifiedBy).HasColumnName("ModifiedBy");
    }
}
