using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ss.CmbXft.Domain.Entities.Sserp;

namespace Ss.CmbXft.Infrastructure.EntityFrameworkCore.Configurations.Sserp;

/// <summary>
/// AbpRole 实体配置
/// </summary>
public class AbpRoleConfiguration : IEntityTypeConfiguration<AbpRole>
{
    public void Configure(EntityTypeBuilder<AbpRole> builder)
    {
        builder.ToTable("AbpRoles", t => t.ExcludeFromMigrations());
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).HasColumnName("Id").IsRequired();
        builder.Property(e => e.TenantId).HasColumnName("TenantId");
        builder.Property(e => e.Name).HasColumnName("Name").HasMaxLength(256).IsRequired();
        builder.Property(e => e.NormalizedName).HasColumnName("NormalizedName").HasMaxLength(256).IsRequired();
        builder.Property(e => e.IsDefault).HasColumnName("IsDefault").IsRequired();
        builder.Property(e => e.IsStatic).HasColumnName("IsStatic").IsRequired();
        builder.Property(e => e.IsPublic).HasColumnName("IsPublic").IsRequired();
        builder.Property(e => e.ExtraProperties).HasColumnName("ExtraProperties");
        builder.Property(e => e.ConcurrencyStamp).HasColumnName("ConcurrencyStamp").HasMaxLength(40);
    }
}