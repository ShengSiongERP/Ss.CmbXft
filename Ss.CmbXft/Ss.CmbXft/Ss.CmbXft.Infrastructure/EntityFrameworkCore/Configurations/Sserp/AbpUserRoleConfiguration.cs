using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ss.CmbXft.Domain.Entities.Sserp;

namespace Ss.CmbXft.Infrastructure.EntityFrameworkCore.Configurations.Sserp;

/// <summary>
/// AbpUserRole 实体配置
/// </summary>
public class AbpUserRoleConfiguration : IEntityTypeConfiguration<AbpUserRole>
{
    public void Configure(EntityTypeBuilder<AbpUserRole> builder)
    {
        builder.ToTable("AbpUserRoles", t => t.ExcludeFromMigrations());
        builder.HasKey(e => new { e.UserId, e.RoleId });

        builder.Property(e => e.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(e => e.RoleId).HasColumnName("RoleId").IsRequired();
        builder.Property(e => e.TenantId).HasColumnName("TenantId");
    }
}