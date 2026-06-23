using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ss.CmbXft.Domain.Entities.Sserp;

namespace Ss.CmbXft.Infrastructure.EntityFrameworkCore.Configurations.Sserp;

/// <summary>
/// AbpUser 实体配置
/// </summary>
public class AbpUserConfiguration : IEntityTypeConfiguration<AbpUser>
{
    public void Configure(EntityTypeBuilder<AbpUser> builder)
    {
        builder.ToTable("AbpUsers", t => t.ExcludeFromMigrations());
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).HasColumnName("Id").IsRequired();
        builder.Property(e => e.TenantId).HasColumnName("TenantId");
        builder.Property(e => e.UserName).HasColumnName("UserName").HasMaxLength(256).IsRequired();
        builder.Property(e => e.NormalizedUserName).HasColumnName("NormalizedUserName").HasMaxLength(256).IsRequired();
        builder.Property(e => e.Name).HasColumnName("Name").HasMaxLength(64);
        builder.Property(e => e.Surname).HasColumnName("Surname").HasMaxLength(64);
        builder.Property(e => e.Email).HasColumnName("Email").HasMaxLength(256).IsRequired();
        builder.Property(e => e.NormalizedEmail).HasColumnName("NormalizedEmail").HasMaxLength(256).IsRequired();
        builder.Property(e => e.EmailConfirmed).HasColumnName("EmailConfirmed").IsRequired();
        builder.Property(e => e.PasswordHash).HasColumnName("PasswordHash").HasMaxLength(256);
        builder.Property(e => e.SecurityStamp).HasColumnName("SecurityStamp").HasMaxLength(256).IsRequired();
        builder.Property(e => e.IsExternal).HasColumnName("IsExternal").IsRequired();
        builder.Property(e => e.PhoneNumber).HasColumnName("PhoneNumber").HasMaxLength(16);
        builder.Property(e => e.PhoneNumberConfirmed).HasColumnName("PhoneNumberConfirmed").IsRequired();
        builder.Property(e => e.TwoFactorEnabled).HasColumnName("TwoFactorEnabled").IsRequired();
        builder.Property(e => e.LockoutEnd).HasColumnName("LockoutEnd");
        builder.Property(e => e.LockoutEnabled).HasColumnName("LockoutEnabled").IsRequired();
        builder.Property(e => e.AccessFailedCount).HasColumnName("AccessFailedCount").IsRequired();
        builder.Property(e => e.ExtraProperties).HasColumnName("ExtraProperties");
        builder.Property(e => e.ConcurrencyStamp).HasColumnName("ConcurrencyStamp").HasMaxLength(40);
        builder.Property(e => e.CreationTime).HasColumnName("CreationTime").IsRequired();
        builder.Property(e => e.CreatorId).HasColumnName("CreatorId");
        builder.Property(e => e.LastModificationTime).HasColumnName("LastModificationTime");
        builder.Property(e => e.LastModifierId).HasColumnName("LastModifierId");
        builder.Property(e => e.IsDeleted).HasColumnName("IsDeleted").IsRequired();
        builder.Property(e => e.DeleterId).HasColumnName("DeleterId");
        builder.Property(e => e.DeletionTime).HasColumnName("DeletionTime");
        builder.Property(e => e.BranchId).HasColumnName("BranchId");
        builder.Property(e => e.IsFirstTimeLogin).HasColumnName("IsFirstTimeLogin").IsRequired();
        builder.Property(e => e.IsActive).HasColumnName("IsActive").IsRequired();
    }
}