namespace Ss.CmbXft.Application.Dtos.Job;

/// <summary>
/// 定时任务详情 DTO。
/// </summary>
public class JobDetailDto
{
    /// <summary>Job 名称（唯一标识）</summary>
    public string JobName { get; set; } = string.Empty;

    /// <summary>Job 分组</summary>
    public string Group { get; set; } = string.Empty;

    /// <summary>Job 描述</summary>
    public string? Description { get; set; }

    /// <summary>Job 类型全名</summary>
    public string JobType { get; set; } = string.Empty;

    /// <summary>是否持久化（StoreDurably）</summary>
    public bool IsDurable { get; set; }

    /// <summary>是否正在执行中</summary>
    public bool IsRunning { get; set; }

    /// <summary>上次执行时间（UTC）</summary>
    public DateTime? PreviousFireTimeUtc { get; set; }

    /// <summary>下次执行时间（UTC）</summary>
    public DateTime? NextFireTimeUtc { get; set; }

    /// <summary>关联触发器列表</summary>
    public List<JobTriggerDto> Triggers { get; set; } = [];

    /// <summary>最近一次执行结果快照（来自 JobDataMap）</summary>
    public JobExecutionSnapshot? LastExecution { get; set; }
}

/// <summary>
/// 触发器信息 DTO。
/// </summary>
public class JobTriggerDto
{
    /// <summary>触发器名称</summary>
    public string TriggerName { get; set; } = string.Empty;

    /// <summary>触发器分组</summary>
    public string Group { get; set; } = string.Empty;

    /// <summary>触发器描述</summary>
    public string? Description { get; set; }

    /// <summary>触发器类型（Cron / Simple / CalendarInterval 等）</summary>
    public string TriggerType { get; set; } = string.Empty;

    /// <summary>Cron 表达式（仅 Cron 触发器）</summary>
    public string? CronExpression { get; set; }

    /// <summary>触发器状态</summary>
    public string State { get; set; } = string.Empty;

    /// <summary>开始时间（UTC）</summary>
    public DateTime? StartTimeUtc { get; set; }

    /// <summary>结束时间（UTC），null 表示永不结束</summary>
    public DateTime? EndTimeUtc { get; set; }

    /// <summary>上次触发时间（UTC）</summary>
    public DateTime? PreviousFireTimeUtc { get; set; }

    /// <summary>下次触发时间（UTC）</summary>
    public DateTime? NextFireTimeUtc { get; set; }

    /// <summary>时区</summary>
    public string? TimeZone { get; set; }
}

/// <summary>
/// 最近一次执行结果快照。
/// </summary>
public class JobExecutionSnapshot
{
    /// <summary>最后执行时间（UTC）</summary>
    public DateTime? LastFireTimeUtc { get; set; }

    /// <summary>执行耗时（毫秒），-1 表示无记录</summary>
    public long ElapsedMilliseconds { get; set; } = -1;

    /// <summary>执行结果消息</summary>
    public string? ResultMessage { get; set; }

    /// <summary>是否成功</summary>
    public bool? IsSuccess { get; set; }
}