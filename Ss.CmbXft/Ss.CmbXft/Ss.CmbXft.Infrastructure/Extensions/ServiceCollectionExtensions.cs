using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Ss.CmbXft.Application.Interfaces.Services.Sserp.SsMember;
using Ss.CmbXft.Domain.Repositories;
using Ss.CmbXft.Domain.Services;
using Ss.CmbXft.Infrastructure.EntityFrameworkCore;
using Ss.CmbXft.Infrastructure.Services;
using Ss.CmbXft.Infrastructure.Tenant;
using Ss.CmbXft.Infrastructure.ThirdParty;
using Ss.CmbXft.Infrastructure.ThirdParty.SsMember;
using Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Interfaces;
using Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Services;
using Ss.CmbXft.Sdk.Configuration;

namespace Ss.CmbXft.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // 注册erp pos数据库
        services.AddDbContext<SserpDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Sserp")));

        // 注册pos数据库
        services.AddDbContext<SsposDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Sspos")));

        // 注册Sserp数据库仓储和工作单元
        services.AddScoped(typeof(IRepository<,>), typeof(SserpRepository<,>));
        services.AddScoped(typeof(ISserpRepository<,>), typeof(SserpRepository<,>));
        services.AddScoped<ISserpUnitOfWork, SserpUnitOfWork>();

        // 注册Sspos数据库仓储和工作单元
        services.AddScoped(typeof(IRepository<,>), typeof(SsposRepository<,>));
        services.AddScoped(typeof(ISsposRepository<,>), typeof(SsposRepository<,>));
        services.AddScoped<ISsposUnitOfWork, SsposUnitOfWork>();

        // 注册 XFT 配置
        services.Configure<XftOptions>(configuration.GetSection("XftOptions"));

        // 注册 昇菘会员系统 配置
        services.Configure<SsMemberOptions>(configuration.GetSection("SsMemberOptions"));

        // 注册 Http 客户端
        services.AddHttpClient();
        services.AddHttpClient<IXftOpenClient, XftOpenClient>();
        services.AddHttpClient<ISsMemberClient, SsMemberClient>();

        // 注册 昇菘会员系统服务实现（依赖Infrastructure中的HttpClient）
        services.AddScoped<ISsMemberGoodsService, SsMemberGoodsService>();
        services.AddScoped<ISsMemberUserService, SsMemberUserService>();
        services.AddScoped<ISsMemberStoreService, SsMemberStoreService>();
        services.AddScoped<ISsMemberCouponService, SsMemberCouponService>();

        // 注册 商品同步服务（读取SSERP数据同步到会员系统）
        services.AddScoped<IProductSyncAppService, ProductSyncAppService>();

        // 注册门店和优惠券同步服务（读取POS数据同步到会员系统）
        services.AddScoped<IStoreSyncAppService, StoreSyncAppService>();
        services.AddScoped<ICouponSyncAppService, CouponSyncAppService>();

        // 注册 HttpContextAccessor
        services.AddHttpContextAccessor();

        // 注册当前租户服务
        services.AddScoped<ICurrentTenant, CurrentTenant>();

        return services;
    }

    public static IServiceCollection AddSerilogLogging(this IServiceCollection services, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .CreateLogger();

        return services;
    }
}
