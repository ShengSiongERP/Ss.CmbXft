using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ss.CmbXft.Domain.Base;
using Ss.CmbXft.Domain.Entities.Sserp;

namespace Ss.CmbXft.Infrastructure.EntityFrameworkCore.Configurations.Sserp;

/// <summary>
/// SserpERPTxnEmployee 实体配置
/// </summary>
public class SserpERPTxnEmployeeConfiguration : IEntityTypeConfiguration<SserpERPTxnEmployee>
{
    public void Configure(EntityTypeBuilder<SserpERPTxnEmployee> builder)
    {
        builder.ToTable("ERP_Txn_Employee", t => t.ExcludeFromMigrations());
        builder.HasKey(e => e.EmployeeCode);
        builder.Property(e => e.EmployeeCode).HasColumnName("EmployeeCode").HasMaxLength(10).IsRequired();
        builder.Property(e => e.EmployeeNo).HasColumnName("EmployeeNo").HasMaxLength(10).IsRequired();
        builder.Property(e => e.Name).HasColumnName("Name").HasMaxLength(50).IsRequired();
        builder.Property(e => e.EnglishName).HasColumnName("EnglishName").HasMaxLength(50).IsRequired();
        builder.Property(e => e.Sex).HasColumnName("Sex").HasColumnType("smallint").IsRequired();
        builder.Property(e => e.AGE).HasColumnName("AGE").HasColumnType("int");
        builder.Property(e => e.BIRTHDATE).HasColumnName("BIRTHDATE").HasColumnType("smalldatetime");
        builder.Property(e => e.ID).HasColumnName("ID").HasMaxLength(50).IsRequired();
        builder.Property(e => e.Department).HasColumnName("Department").HasMaxLength(30);
        builder.Property(e => e.Position).HasColumnName("Position").HasMaxLength(30);
        builder.Property(e => e.Status).HasColumnName("Status").HasColumnType("smallint");
        builder.Property(e => e.CreateUser).HasColumnName("CreateUser").HasMaxLength(50);
        builder.Property(e => e.CreateDate).HasColumnName("CreateDate").HasColumnType("datetime");
        builder.Property(e => e.ModifyUser).HasColumnName("ModifyUser").HasMaxLength(50);
        builder.Property(e => e.ModifyDate).HasColumnName("ModifyDate").HasColumnType("datetime");
        builder.Property(e => e.IsAccessAllOutlet).HasColumnName("IsAccessAllOutlet").HasColumnType("bit");
        builder.Property(e => e.WorkingLocationCode).HasColumnName("WorkingLocationCode").HasMaxLength(4);
        builder.Property(e => e.AccessGroupCode).HasColumnName("AccessGroupCode").HasMaxLength(10);
    }
}
