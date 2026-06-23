using Microsoft.EntityFrameworkCore;
using Ss.CmbXft.Domain.Base;
using Ss.CmbXft.Domain.Entities;
using Ss.CmbXft.Domain.Entities.Sserp;
using Ss.CmbXft.Domain.Entities.Sserp.Product;
using Ss.CmbXft.Infrastructure.EntityFrameworkCore.Configurations.Sserp;
using Ss.CmbXft.Infrastructure.EntityFrameworkCore.Configurations.Sserp.Product;

namespace Ss.CmbXft.Infrastructure.EntityFrameworkCore;

/// <summary>
/// SSERP DbContext
/// </summary>
public class SserpDbContext : DbContext
{
    public SserpDbContext(DbContextOptions<SserpDbContext> options) : base(options)
    {
    }

    // 原有实体
    public DbSet<SserpERPTxnEmployee> Employees { get; set; }
    public DbSet<AbpUser> AbpUsers { get; set; }
    public DbSet<AbpUserRole> AbpUserRoles { get; set; }
    public DbSet<AbpRole> AbpRoles { get; set; }

    // 商品相关实体
    public DbSet<SserpProduct> Products { get; set; }
    public DbSet<SserpBarcode> Barcodes { get; set; }
    public DbSet<SserpUom> Uoms { get; set; }
    public DbSet<SserpUomConversion> UomConversions { get; set; }
    public DbSet<SserpBrand> Brands { get; set; }
    public DbSet<SserpCategory> Categories { get; set; }
    public DbSet<SserpSubCategory> SubCategories { get; set; }
    public DbSet<SserpDepartment> Departments { get; set; }
    public DbSet<SserpSegment> Segments { get; set; }
    public DbSet<SserpItemClass> ItemClasses { get; set; }
    public DbSet<SserpProductGroup> ProductGroups { get; set; }
    public DbSet<SserpTax> Taxes { get; set; }
    public DbSet<SserpProductAttribute> ProductAttributes { get; set; }
    public DbSet<SserpProductAttributeBranch> ProductAttributeBranches { get; set; }
    public DbSet<SserpPackageHeader> PackageHeaders { get; set; }
    public DbSet<SserpPackageDetail> PackageDetails { get; set; }
    public DbSet<SserpTempProduct> TempProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 全局禁用迁移逻辑
        //modelBuilder.HasDefaultSchema(string.Empty);

        base.OnModelCreating(modelBuilder);

        //// 应用下划线命名约定（驼峰式转下划线）
        //modelBuilder.ApplySnakeCaseNamingConvention();

        //// 配置软删除过滤器
        //foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        //{
        //    if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
        //    {
        //        var parameter = Expression.Parameter(entityType.ClrType, "e");
        //        var body = Expression.Equal(
        //            Expression.Call(typeof(EF), nameof(EF.Property), new[] { typeof(bool) }, parameter, Expression.Constant("IsDeleted")),
        //            Expression.Constant(false));
        //        modelBuilder.Entity(entityType.ClrType).HasQueryFilter(Expression.Lambda(body, parameter));
        //    }
        //}

        #region 原有实体配置
        modelBuilder.ApplyConfiguration(new SserpERPTxnEmployeeConfiguration());
        modelBuilder.ApplyConfiguration(new AbpUserConfiguration());
        modelBuilder.ApplyConfiguration(new AbpUserRoleConfiguration());
        modelBuilder.ApplyConfiguration(new AbpRoleConfiguration());
        #endregion

        #region 商品相关实体配置
        modelBuilder.ApplyConfiguration(new SserpProductConfiguration());
        modelBuilder.ApplyConfiguration(new SserpBarcodeConfiguration());
        modelBuilder.ApplyConfiguration(new SserpUomConfiguration());
        modelBuilder.ApplyConfiguration(new SserpUomConversionConfiguration());
        modelBuilder.ApplyConfiguration(new SserpBrandConfiguration());
        modelBuilder.ApplyConfiguration(new SserpCategoryConfiguration());
        modelBuilder.ApplyConfiguration(new SserpSubCategoryConfiguration());
        modelBuilder.ApplyConfiguration(new SserpDepartmentConfiguration());
        modelBuilder.ApplyConfiguration(new SserpSegmentConfiguration());
        modelBuilder.ApplyConfiguration(new SserpItemClassConfiguration());
        modelBuilder.ApplyConfiguration(new SserpProductGroupConfiguration());
        modelBuilder.ApplyConfiguration(new SserpTaxConfiguration());
        modelBuilder.ApplyConfiguration(new SserpProductAttributeConfiguration());
        modelBuilder.ApplyConfiguration(new SserpProductAttributeBranchConfiguration());
        modelBuilder.ApplyConfiguration(new SserpPackageHeaderConfiguration());
        modelBuilder.ApplyConfiguration(new SserpPackageDetailConfiguration());
        modelBuilder.ApplyConfiguration(new SserpTempProductConfiguration());
        #endregion
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is IHasCreate || e.Entity is IHasUpdate || e.Entity is ISoftDelete);

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                if (entry.Entity is IHasCreate creationEntity)
                {
                    creationEntity.CreateTime = DateTime.UtcNow;
                }
            }
            else if (entry.State == EntityState.Modified)
            {
                if (entry.Entity is IHasUpdate modificationEntity)
                {
                    modificationEntity.UpdateTime = DateTime.UtcNow;
                }
            }
            else if (entry.State == EntityState.Deleted)
            {
                if (entry.Entity is ISoftDelete softDeleteEntity)
                {
                    entry.State = EntityState.Modified;
                    softDeleteEntity.IsDeleted = true;
                }
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
