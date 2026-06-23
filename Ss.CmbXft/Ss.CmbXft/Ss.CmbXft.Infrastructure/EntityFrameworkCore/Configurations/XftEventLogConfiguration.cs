using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ss.CmbXft.Domain.Entities;

namespace Ss.CmbXft.Infrastructure.EntityFrameworkCore.Configurations;

/// <summary>
/// XftEventLog 实体配置
/// </summary>
public class XftEventLogConfiguration : IEntityTypeConfiguration<XftEventLog>
{
    public void Configure(EntityTypeBuilder<XftEventLog> builder)
    {
        builder.ToTable("xft_event_log");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedNever();

        builder.HasIndex(e => e.EventId)
            .HasDatabaseName("ix_xft_event_logs_event_id");

        builder.HasIndex(e => e.BusinessKey)
            .HasDatabaseName("ix_xft_event_logs_business_key");

        builder.HasIndex(e => e.ProcessStatus)
            .HasDatabaseName("ix_xft_event_logs_process_status");

        builder.Property(e => e.EventId).HasMaxLength(100).IsRequired();
        builder.Property(e => e.BusinessKey).HasMaxLength(100).IsRequired();
        builder.Property(e => e.PrjCod).HasMaxLength(50);
        builder.Property(e => e.EventTime).HasMaxLength(50).IsRequired();
        builder.Property(e => e.AppId).HasMaxLength(50).IsRequired();
        builder.Property(e => e.EventRcdInf).IsRequired();
        builder.Property(e => e.Signature).HasMaxLength(200);
        builder.Property(e => e.ProcessMessage).HasMaxLength(500);
    }
}