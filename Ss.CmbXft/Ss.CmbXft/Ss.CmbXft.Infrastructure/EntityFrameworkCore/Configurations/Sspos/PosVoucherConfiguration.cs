using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ss.CmbXft.Domain.Entities.Sspos;

namespace Ss.CmbXft.Infrastructure.EntityFrameworkCore.Configurations.Sspos;

/// <summary>
/// 优惠券信息表实体配置
/// 对应表：[dbo].[POS_Mst_Voucher]，主键为 VoucherCode（string类型）
/// </summary>
public class PosVoucherConfiguration : IEntityTypeConfiguration<PosVoucher>
{
    public void Configure(EntityTypeBuilder<PosVoucher> builder)
    {
        builder.ToTable("POS_Mst_Voucher", "dbo", t => t.ExcludeFromMigrations());
        builder.HasKey(e => e.VoucherCode);
        builder.Property(e => e.VoucherCode).HasColumnName("VoucherCode").HasMaxLength(10).IsRequired();
        builder.Property(e => e.VoucherName).HasColumnName("VoucherName").HasMaxLength(50).IsRequired();
        builder.Property(e => e.VoucherBarcode).HasColumnName("VoucherBarcode").HasMaxLength(30);
        builder.Property(e => e.VoucherAmount).HasColumnName("VoucherAmount").HasColumnType("decimal(10,2)").IsRequired();
        builder.Property(e => e.VoucherType).HasColumnName("VoucherType").HasColumnType("char(1)");
        builder.Property(e => e.VendorName).HasColumnName("VendorName").HasMaxLength(100);
        builder.Property(e => e.ValidFrom).HasColumnName("ValidFrom").HasColumnType("smalldatetime").IsRequired();
        builder.Property(e => e.ValidTo).HasColumnName("ValidTo").HasColumnType("smalldatetime").IsRequired();
        builder.Property(e => e.MinPurchaseAmount).HasColumnName("MinPurchaseAmount").HasColumnType("decimal(10,2)");
        builder.Property(e => e.MaxQtyPerItem).HasColumnName("MaxQtyPerItem");
        builder.Property(e => e.VoucherImg).HasColumnName("VoucherImg");
        builder.Property(e => e.Position).HasColumnName("Position").HasColumnType("smallint");
        builder.Property(e => e.Active).HasColumnName("Active").IsRequired();
        builder.Property(e => e.CreateUser).HasColumnName("CreateUser").HasMaxLength(50).IsRequired();
        builder.Property(e => e.CreateDate).HasColumnName("CreateDate").HasColumnType("datetime").IsRequired();
        builder.Property(e => e.ModifyUser).HasColumnName("ModifyUser").HasMaxLength(50);
        builder.Property(e => e.ModifyDate).HasColumnName("ModifyDate").HasColumnType("datetime");
        builder.Property(e => e.MaxQtyPerReceipt).HasColumnName("MaxQtyPerReceipt");
        builder.Property(e => e.RedeemType).HasColumnName("RedeemType");
        builder.Property(e => e.RequiredQty).HasColumnName("RequiredQty").HasColumnType("decimal(18,2)");
        builder.Property(e => e.TotalQty).HasColumnName("TotalQty").HasColumnType("decimal(18,2)");
        builder.Property(e => e.IsVoidable).HasColumnName("IsVoidable");
    }
}
