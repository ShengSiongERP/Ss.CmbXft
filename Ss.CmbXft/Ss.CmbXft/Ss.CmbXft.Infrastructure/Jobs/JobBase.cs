using Microsoft.Extensions.Logging;
using Quartz;

namespace Ss.CmbXft.Infrastructure.Jobs;

/// <summary>
/// 定时任务基类，提供统一的日志记录、异常处理和执行监控能力。
/// 所有业务 Job 应继承此基类并实现 <see cref="ExecuteJobAsync"/> 方法。
/// </summary>
/// <remarks>
/// Quartz Jobs 通过 DI 容器实例化（默认 Scope），因此子类可通过构造函数注入任意服务。
/// 子类只需重写 <see cref="Logger"/> 属性以提供对应的日志实例。
/// 执行完成后会自动将执行快照写入 JobDataMap，供 JobAdminService 查询展示。
/// </remarks>
public abstract class JobBase : IJob
{
    #region JobDataMap 快照键常量

    /// <summary>最后执行耗时（毫秒）</summary>
    public const string LastElapsedMsKey = "LastElapsedMs";

    /// <summary>最后执行时间（UTC）</summary>
    public const string LastFireTimeKey = "LastFireTime";

    /// <summary>最后执行结果消息</summary>
    public const string LastResultKey = "LastResultMessage";

    /// <summary>最后执行是否成功</summary>
    public const string IsSuccessKey = "LastIsSuccess";

    #endregion

    /// <summary>
    /// 子类通过 DI 注入具体的 <see cref="ILogger{T}"/> 并返回。
    /// </summary>
    protected abstract ILogger Logger { get; }

    /// <summary>
    /// 任务名称，用于日志标识和监控。默认取类名。
    /// </summary>
    protected virtual string JobName => GetType().Name;

    /// <summary>
    /// 子类实现具体的业务逻辑。框架负责异常捕获、计时和日志记录。
    /// </summary>
    protected abstract Task ExecuteJobAsync(IJobExecutionContext context, CancellationToken cancellationToken);

    /// <summary>
    /// 是否在执行前记录 JobDataMap 等详细上下文。默认关闭以减少日志噪音。
    /// </summary>
    protected virtual bool EnableDetailedLogging => false;

    /// <summary>
    /// Quartz 调度入口。封装统一的日志、计时、异常处理和取消逻辑。
    /// </summary>
    public async Task Execute(IJobExecutionContext context)
    {
        var startTime = DateTimeOffset.UtcNow;
        var ct = context.CancellationToken;

        Logger.LogInformation(
            "定时任务 [{JobName}] 开始执行 | 触发时间: {FireTime:yyyy-MM-dd HH:mm:ss} | 下次执行: {NextFireTime}",
            JobName,
            context.FireTimeUtc.LocalDateTime,
            context.NextFireTimeUtc?.LocalDateTime);

        if (EnableDetailedLogging)
        {
            Logger.LogDebug("定时任务 [{JobName}] 上下文详情: {Detail}", JobName,
                string.Join(", ", context.JobDetail.JobDataMap.Select(kv => $"{kv.Key}={kv.Value}")));
        }

        try
        {
            await ExecuteJobAsync(context, ct);

            var elapsed = DateTimeOffset.UtcNow - startTime;
            Logger.LogInformation("定时任务 [{JobName}] 执行成功 | 耗时: {ElapsedMs}ms",
                JobName, (long)elapsed.TotalMilliseconds);

            // 写入成功快照到 JobDataMap
            WriteExecutionSnapshot(context, (long)elapsed.TotalMilliseconds, success: true, "OK");
        }
        catch (OperationCanceledException) when (ct.IsCancellationRequested)
        {
            Logger.LogWarning("定时任务 [{JobName}] 被取消（应用关闭中）", JobName);
            var elapsed = DateTimeOffset.UtcNow - startTime;
            WriteExecutionSnapshot(context, (long)elapsed.TotalMilliseconds, success: false, "Cancelled");
        }
        catch (Exception ex)
        {
            var elapsed = DateTimeOffset.UtcNow - startTime;
            Logger.LogError(ex, "定时任务 [{JobName}] 执行失败 | 耗时: {ElapsedMs}ms | 异常: {Message}",
                JobName, (long)elapsed.TotalMilliseconds, ex.Message);

            // 写入失败快照到 JobDataMap
            WriteExecutionSnapshot(context, (long)elapsed.TotalMilliseconds, success: false, ex.Message);

            // 告知 Quartz 不自动重新执行此 Job
            throw new JobExecutionException(ex, refireImmediately: false);
        }
    }

    /// <summary>
    /// 将最近一次执行结果写入 JobDataMap，供管理接口查询。
    /// 注意：RAMJobStore 下 JobDetail.JobDataMap 是可变的引用。
    /// </summary>
    private void WriteExecutionSnapshot(IJobExecutionContext context, long elapsedMs, bool success, string message)
    {
        try
        {
            var dataMap = context.JobDetail.JobDataMap;
            dataMap[LastElapsedMsKey] = elapsedMs;
            dataMap[LastFireTimeKey] = DateTime.UtcNow;
            dataMap[LastResultKey] = message;
            dataMap[IsSuccessKey] = success;
        }
        catch (Exception ex)
        {
            // 快照写入失败不应影响主流程
            Logger.LogWarning(ex, "定时任务 [{JobName}] 写入执行快照失败", JobName);
        }
    }
}