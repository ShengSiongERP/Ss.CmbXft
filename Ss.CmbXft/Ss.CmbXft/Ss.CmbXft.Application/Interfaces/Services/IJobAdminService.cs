using Ss.CmbXft.Application.Dtos.Job;

namespace Ss.CmbXft.Application.Services;

/// <summary>
/// 定时任务管理服务接口。
/// 提供查询、手动触发、暂停/恢复等运维能力。
/// </summary>
public interface IJobAdminService
{
    /// <summary>
    /// 获取调度器状态概览。
    /// </summary>
    Task<SchedulerOverviewDto> GetSchedulerStatusAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取所有已注册的定时任务列表（含触发器状态）。
    /// </summary>
    Task<List<JobDetailDto>> GetAllJobsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取指定 Job 详情。
    /// </summary>
    Task<JobDetailDto?> GetJobDetailAsync(string jobName, string group = "DEFAULT", CancellationToken cancellationToken = default);

    /// <summary>
    /// 立即手动触发指定 Job（不等下次 Cron 时间，立即执行一次）。
    /// </summary>
    Task<bool> TriggerJobAsync(string jobName, string group = "DEFAULT", CancellationToken cancellationToken = default);

    /// <summary>
    /// 暂停指定 Job（同时暂停关联的所有触发器）。
    /// </summary>
    Task<bool> PauseJobAsync(string jobName, string group = "DEFAULT", CancellationToken cancellationToken = default);

    /// <summary>
    /// 恢复指定 Job（同时恢复关联的所有触发器）。
    /// </summary>
    Task<bool> ResumeJobAsync(string jobName, string group = "DEFAULT", CancellationToken cancellationToken = default);

    /// <summary>
    /// 中断正在执行的 Job（调用 IJob.Interrupt）。
    /// </summary>
    Task<bool> InterruptJobAsync(string jobName, string group = "DEFAULT", CancellationToken cancellationToken = default);
}

/// <summary>
/// 调度器概览 DTO。
/// </summary>
public class SchedulerOverviewDto
{
    /// <summary>调度器名称</summary>
    public string SchedulerName { get; set; } = string.Empty;

    /// <summary>调度器实例 ID</summary>
    public string SchedulerInstanceId { get; set; } = string.Empty;

    /// <summary>调度器状态（Started / Standby / Shutdown）</summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>运行模式（集群 / 单机）</summary>
    public string RunningSince { get; set; } = string.Empty;

    /// <summary>已注册 Job 总数</summary>
    public int TotalJobs { get; set; }

    /// <summary>当前正在执行的 Job 数</summary>
    public int CurrentlyExecutingJobs { get; set; }

    /// <summary>线程池大小</summary>
    public int ThreadPoolSize { get; set; }

    /// <summary>检查时间（UTC）</summary>
    public DateTime CheckTimeUtc { get; set; }
}