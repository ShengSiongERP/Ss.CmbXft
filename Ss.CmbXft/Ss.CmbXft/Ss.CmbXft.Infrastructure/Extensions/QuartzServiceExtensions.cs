using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Ss.CmbXft.Application.Services;
using Ss.CmbXft.Infrastructure.Jobs;

namespace Ss.CmbXft.Infrastructure.Extensions;

/// <summary>
/// Quartz 定时任务服务注册扩展方法。
/// 使用 RAMJobStore（纯内存存储），适合单实例部署场景。
/// </summary>
public static class QuartzServiceExtensions
{
    /// <summary>
    /// 注册 Quartz 调度器及所有业务 Job。
    /// 配置来源优先级：appsettings.json > 代码默认值。
    /// </summary>
    public static IServiceCollection AddQuartzJobs(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddQuartz(q =>
        {
            q.UseSimpleTypeLoader();
            q.UseInMemoryStore();

            q.InterruptJobsOnShutdown = true;
            q.InterruptJobsOnShutdownWithWait = true;

            q.SchedulerId = "SsCmbXft-Scheduler";
            q.SchedulerName = "SsCmbXft Scheduler";

            RegisterXftStaffSyncJob(q, configuration);
            RegisterProductSyncJob(q, configuration);
            RegisterStoreSyncJob(q, configuration);
            RegisterCouponSyncJob(q, configuration);
        });

        services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
            options.StartDelay = TimeSpan.FromSeconds(10);
        });

        // 注册 Job 管理服务
        services.AddSingleton<IJobAdminService, JobAdminService>();

        return services;
    }

    private static void RegisterXftStaffSyncJob(IServiceCollectionQuartzConfigurator q, IConfiguration configuration)
    {
        var enabled = configuration["Quartz:XftStaffSyncJob:Enabled"];
        if (string.Equals(enabled, "false", StringComparison.OrdinalIgnoreCase)) return;

        var cronExpr = configuration["Quartz:XftStaffSyncJob:CronExpression"] ?? "0 0 3 * * ?";

        var jobKey = new JobKey(nameof(XftStaffSyncJob), "DEFAULT");

        q.AddJob<XftStaffSyncJob>(opts => opts
            .WithIdentity(jobKey)
            .WithDescription("薪福通员工数据同步 - 从薪福通拉取并同步员工数据到本地数据库")
            .StoreDurably());

        q.AddTrigger(opts => opts
            .ForJob(jobKey)
            .WithIdentity($"{nameof(XftStaffSyncJob)}-Trigger", "DEFAULT")
            .WithCronSchedule(cronExpr, x =>
            {
                x.WithMisfireHandlingInstructionFireAndProceed();
                x.InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("China Standard Time"));
            })
            .WithDescription($"薪福通员工同步触发器 - Cron: {cronExpr}"));
    }

    private static void RegisterProductSyncJob(IServiceCollectionQuartzConfigurator q, IConfiguration configuration)
    {
        var enabled = configuration["Quartz:ProductSyncJob:Enabled"];
        if (string.Equals(enabled, "false", StringComparison.OrdinalIgnoreCase)) return;

        var cronExpr = configuration["Quartz:ProductSyncJob:CronExpression"] ?? "0 0 4 * * ?";
        var jobKey = new JobKey(nameof(ProductSyncJob), "DEFAULT");

        q.AddJob<ProductSyncJob>(opts => opts
            .WithIdentity(jobKey)
            .WithDescription("商品数据同步 - 从SSERP拉取商品数据并同步到昇菘会员系统")
            .StoreDurably());

        q.AddTrigger(opts => opts
            .ForJob(jobKey)
            .WithIdentity($"{nameof(ProductSyncJob)}-Trigger", "DEFAULT")
            .WithCronSchedule(cronExpr, x =>
            {
                x.WithMisfireHandlingInstructionFireAndProceed();
                x.InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("China Standard Time"));
            })
            .WithDescription($"商品同步触发器 - Cron: {cronExpr}"));
    }

    private static void RegisterStoreSyncJob(IServiceCollectionQuartzConfigurator q, IConfiguration configuration)
    {
        var enabled = configuration["Quartz:StoreSyncJob:Enabled"];
        if (string.Equals(enabled, "false", StringComparison.OrdinalIgnoreCase)) return;

        var cronExpr = configuration["Quartz:StoreSyncJob:CronExpression"] ?? "0 0 4 * * ?";
        var jobKey = new JobKey(nameof(StoreSyncJob), "DEFAULT");

        q.AddJob<StoreSyncJob>(opts => opts
            .WithIdentity(jobKey)
            .WithDescription("门店数据同步 - 从POS拉取门店数据并同步到昇菘会员系统")
            .StoreDurably());

        q.AddTrigger(opts => opts
            .ForJob(jobKey)
            .WithIdentity($"{nameof(StoreSyncJob)}-Trigger", "DEFAULT")
            .WithCronSchedule(cronExpr, x =>
            {
                x.WithMisfireHandlingInstructionFireAndProceed();
                x.InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("China Standard Time"));
            })
            .WithDescription($"门店同步触发器 - Cron: {cronExpr}"));
    }

    private static void RegisterCouponSyncJob(IServiceCollectionQuartzConfigurator q, IConfiguration configuration)
    {
        var enabled = configuration["Quartz:CouponSyncJob:Enabled"];
        if (string.Equals(enabled, "false", StringComparison.OrdinalIgnoreCase)) return;

        var cronExpr = configuration["Quartz:CouponSyncJob:CronExpression"] ?? "0 0 4 * * ?";
        var jobKey = new JobKey(nameof(CouponSyncJob), "DEFAULT");

        q.AddJob<CouponSyncJob>(opts => opts
            .WithIdentity(jobKey)
            .WithDescription("优惠券数据同步 - 从POS拉取优惠券数据并同步到昇菘会员系统")
            .StoreDurably());

        q.AddTrigger(opts => opts
            .ForJob(jobKey)
            .WithIdentity($"{nameof(CouponSyncJob)}-Trigger", "DEFAULT")
            .WithCronSchedule(cronExpr, x =>
            {
                x.WithMisfireHandlingInstructionFireAndProceed();
                x.InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("China Standard Time"));
            })
            .WithDescription($"优惠券同步触发器 - Cron: {cronExpr}"));
    }
}