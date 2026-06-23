using Microsoft.Extensions.Logging;
using Ss.CmbXft.Application.Dtos.Sspos;
using Quartz;
using Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Interfaces;

namespace Ss.CmbXft.Infrastructure.Jobs;

/// <summary>
/// 优惠券数据同步定时任务。
/// 按计划从POS数据库拉取优惠券数据并同步到昇菘会员系统。
/// </summary>
/// <remarks>
/// 调度配置见 <see cref="QuartzServiceExtensions.AddQuartzJobs"/> 中的 CouponSyncJob 注册。
/// 也可通过 appsettings.json 的 Quartz 节调整 cron 表达式。
/// </remarks>
[DisallowConcurrentExecution]
public class CouponSyncJob : JobBase
{
    private readonly ICouponSyncAppService _couponSyncService;
    private readonly ILogger<CouponSyncJob> _logger;

    public CouponSyncJob(
        ICouponSyncAppService couponSyncService,
        ILogger<CouponSyncJob> logger)
    {
        _couponSyncService = couponSyncService;
        _logger = logger;
    }

    protected override ILogger Logger => _logger;

    protected override async Task ExecuteJobAsync(IJobExecutionContext context, CancellationToken cancellationToken)
    {
        _logger.LogInformation("开始执行优惠券数据同步...");

        var query = new CouponSyncQueryDto();
        var result = await _couponSyncService.SyncCouponsAsync(query, cancellationToken);

        _logger.LogInformation("优惠券数据同步完成：{Msg}", result.Msg);
    }
}
