using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ss.CmbXft.Domain.Entities.Sserp.Product;

namespace Ss.CmbXft.Infrastructure.EntityFrameworkCore.Configurations.Sserp.Product;

/// <summary>
/// 细分市场表实体配置
/// 对应表：[Product].[T_Segment]
/// </summary>
public class SserpSegmentConfiguration : IEntityTypeConfiguration<SserpSegment>
{
    public void Configure(EntityTypeBuilder<SserpSegment> builder)
    {
        builder.ToTable("T_Segment", "Product", t => t.ExcludeFromMigrations());
        builder.HasKey(e => e.SegmentID);
        builder.Property(e => e.SegmentID).HasColumnName("SegmentID").ValueGeneratedOnAdd();
        builder.Property(e => e.SegmentName).HasColumnName("SegmentName").HasMaxLength(40);
        builder.Property(e => e.SegmentDescription).HasColumnName("SegmentDescription").HasMaxLength(60);
        builder.Property(e => e.Status).HasColumnName("Status").HasColumnType("smallint");
        builder.Property(e => e.CreatedOn).HasColumnName("CreatedOn").HasColumnType("datetime2(7)").IsRequired();
        builder.Property(e => e.CreatedBy).HasColumnName("CreatedBy").IsRequired();
        builder.Property(e => e.ModifiedOn).HasColumnName("ModifiedOn").HasColumnType("datetime2(7)");
        builder.Property(e => e.ModifiedBy).HasColumnName("ModifiedBy");
    }
}
