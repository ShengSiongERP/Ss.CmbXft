using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ss.CmbXft.Domain.Entities.Sspos;

namespace Ss.CmbXft.Infrastructure.EntityFrameworkCore.Configurations.Sspos;

/// <summary>
/// 控制门店表实体配置
/// 对应表：[dbo].[POS_Mst_ControlLocation]，主键为 LinkedCode（string类型）
/// </summary>
public class PosControlLocationConfiguration : IEntityTypeConfiguration<PosControlLocation>
{
    public void Configure(EntityTypeBuilder<PosControlLocation> builder)
    {
        builder.ToTable("POS_Mst_ControlLocation", "dbo", t => t.ExcludeFromMigrations());
        builder.HasKey(e => e.LinkedCode);

        builder.Property(e => e.LinkedCode).HasColumnName("LinkedCode").HasMaxLength(20).IsRequired();
        builder.Property(e => e.LocationCode).HasColumnName("LocationCode").HasMaxLength(40);
        builder.Property(e => e.Active).HasColumnName("Active");
        builder.Property(e => e.CreateDate).HasColumnName("CreateDate").HasColumnType("datetime").IsRequired();
        builder.Property(e => e.CreateUser).HasColumnName("CreateUser").HasMaxLength(50).IsRequired();
        builder.Property(e => e.ModifyDate).HasColumnName("ModifyDate").HasColumnType("datetime");
        builder.Property(e => e.ModifyUser).HasColumnName("ModifyUser").HasMaxLength(50);
    }
}
