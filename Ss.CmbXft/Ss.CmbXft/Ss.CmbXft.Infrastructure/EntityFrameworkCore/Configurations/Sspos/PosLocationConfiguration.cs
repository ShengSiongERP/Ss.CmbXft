using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ss.CmbXft.Domain.Entities.Sspos;

namespace Ss.CmbXft.Infrastructure.EntityFrameworkCore.Configurations.Sspos;

/// <summary>
/// 门店信息表实体配置
/// 对应表：[dbo].[POS_MLocation]，主键为 LocationCode（string类型）
/// </summary>
public class PosLocationConfiguration : IEntityTypeConfiguration<PosLocation>
{
    public void Configure(EntityTypeBuilder<PosLocation> builder)
    {
        builder.ToTable("POS_MLocation", "dbo", t => t.ExcludeFromMigrations());
        builder.HasKey(e => e.LocationCode);
        builder.Property(e => e.LocationCode).HasColumnName("LocationCode").HasMaxLength(10).IsRequired();
        builder.Property(e => e.LocationName).HasColumnName("LocationName").HasMaxLength(50).IsRequired();
        builder.Property(e => e.PayrollLocationCode).HasColumnName("PayrollLocationCode").HasMaxLength(10);
        builder.Property(e => e.Address1).HasColumnName("Address1").HasMaxLength(50);
        builder.Property(e => e.Address2).HasColumnName("Address2").HasMaxLength(50);
        builder.Property(e => e.Address3).HasColumnName("Address3").HasMaxLength(50);
        builder.Property(e => e.TelePhone).HasColumnName("TelePhone").HasMaxLength(50);
        builder.Property(e => e.FaxNo).HasColumnName("FaxNo").HasMaxLength(50);
        builder.Property(e => e.GSTRegNo).HasColumnName("GSTRegNo").HasMaxLength(50);
        builder.Property(e => e.CoRegNo).HasColumnName("CoRegNo").HasMaxLength(50);
        builder.Property(e => e.WareHouse).HasColumnName("WareHouse").IsRequired();
        builder.Property(e => e.GST).HasColumnName("GST").HasColumnType("numeric(18,6)");
        builder.Property(e => e.GSTPer).HasColumnName("GSTPer").HasColumnType("numeric(18,2)");
        builder.Property(e => e.PersonIncharge).HasColumnName("PersonIncharge").HasMaxLength(50);
        builder.Property(e => e.ShortCode).HasColumnName("ShortCode").HasMaxLength(4);
        builder.Property(e => e.EODDate).HasColumnName("EODDate").HasColumnType("datetime");
        builder.Property(e => e.BackupDays).HasColumnName("BackupDays").HasColumnType("smallint");
        builder.Property(e => e.CreateUser).HasColumnName("CreateUser").HasMaxLength(30).IsRequired();
        builder.Property(e => e.CreateDate).HasColumnName("CreateDate").HasColumnType("datetime");
        builder.Property(e => e.ModifyUser).HasColumnName("ModifyUser").HasMaxLength(30);
        builder.Property(e => e.ModifyDate).HasColumnName("ModifyDate").HasColumnType("datetime");
        builder.Property(e => e.Shift1).HasColumnName("Shift1").HasMaxLength(20);
        builder.Property(e => e.Shift2).HasColumnName("Shift2").HasMaxLength(20);
        builder.Property(e => e.Shift3).HasColumnName("Shift3").HasMaxLength(20);
        builder.Property(e => e.Shift4).HasColumnName("Shift4").HasMaxLength(20);
        builder.Property(e => e.ISDigi).HasColumnName("ISDigi");
        builder.Property(e => e.Active).HasColumnName("Active").IsRequired();
        builder.Property(e => e.BusinessDate).HasColumnName("BusinessDate").HasColumnType("datetime");
        builder.Property(e => e.LastShiftClose).HasColumnName("LastShiftClose");
        builder.Property(e => e.PayInAmount).HasColumnName("PayInAmount").HasColumnType("numeric(18,2)");
    }
}
