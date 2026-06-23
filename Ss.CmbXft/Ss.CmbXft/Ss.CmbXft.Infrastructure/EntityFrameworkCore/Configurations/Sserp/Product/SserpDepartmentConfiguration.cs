using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ss.CmbXft.Domain.Entities.Sserp.Product;

namespace Ss.CmbXft.Infrastructure.EntityFrameworkCore.Configurations.Sserp.Product;

/// <summary>
/// 部门表实体配置
/// 对应表：[Product].[T_Department]
/// </summary>
public class SserpDepartmentConfiguration : IEntityTypeConfiguration<SserpDepartment>
{
    public void Configure(EntityTypeBuilder<SserpDepartment> builder)
    {
        builder.ToTable("T_Department", "Product", t => t.ExcludeFromMigrations());
        builder.HasKey(e => e.DepartmentID);
        builder.Property(e => e.DepartmentID).HasColumnName("DepartmentID").ValueGeneratedOnAdd();
        builder.Property(e => e.DepartmentName).HasColumnName("DepartmentName").HasMaxLength(40);
        builder.Property(e => e.DepartmentDescription).HasColumnName("DepartmentDescription").HasMaxLength(60);
        builder.Property(e => e.Status).HasColumnName("Status").HasColumnType("smallint");
        builder.Property(e => e.DepartmentCode).HasColumnName("DepartmentCode");
        builder.Property(e => e.NCDepartmentCode).HasColumnName("NCDepartmentCode").HasMaxLength(5);
        builder.Property(e => e.NCDepartmentName).HasColumnName("NCDepartmentName").HasMaxLength(10);
        builder.Property(e => e.CreatedOn).HasColumnName("CreatedOn").HasColumnType("datetime2(7)").IsRequired();
        builder.Property(e => e.CreatedBy).HasColumnName("CreatedBy").IsRequired();
        builder.Property(e => e.ModifiedOn).HasColumnName("ModifiedOn").HasColumnType("datetime2(7)");
        builder.Property(e => e.ModifiedBy).HasColumnName("ModifiedBy");
    }
}
