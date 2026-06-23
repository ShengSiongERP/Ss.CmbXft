using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ss.CmbXft.Domain.Entities;

namespace Ss.CmbXft.Infrastructure.EntityFrameworkCore.Configurations;

/// <summary>
/// XftStaff 实体配置
/// </summary>
public class XftStaffConfiguration : IEntityTypeConfiguration<XftStaff>
{
    public void Configure(EntityTypeBuilder<XftStaff> builder)
    {
        builder.ToTable("xft_staff");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedNever();

        builder.HasIndex(e => e.StaffSeq)
            .IsUnique()
            .HasDatabaseName("ix_xft_staffs_staff_seq");

        builder.HasIndex(e => e.EnterpriseId)
            .HasDatabaseName("ix_xft_staffs_enterprise_id");

        builder.HasIndex(e => e.MobileNumber)
            .HasDatabaseName("ix_xft_staffs_mobile_number");

        builder.Property(e => e.EnterpriseId).HasMaxLength(50);
        builder.Property(e => e.StaffSeq).HasMaxLength(50);
        builder.Property(e => e.StfType).HasMaxLength(20);
        builder.Property(e => e.StfStatus).HasMaxLength(20);
        builder.Property(e => e.StfName).HasMaxLength(100);
        builder.Property(e => e.MobileNumber).HasMaxLength(20);
    }
}