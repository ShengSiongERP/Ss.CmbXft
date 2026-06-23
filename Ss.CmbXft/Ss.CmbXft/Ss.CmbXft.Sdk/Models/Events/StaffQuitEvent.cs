using Newtonsoft.Json;

namespace Ss.CmbXft.Sdk.Models.Events;

/// <summary>
/// 离职管理事件模型
/// </summary>
public class StaffQuitEvent
{
    /// <summary>
    /// 操作类型
    /// LAUNCH 办理
    /// APPROVE_END 审批结束
    /// COF 确认离职
    /// EFFECTIVE 生效
    /// CANCEL 取消
    /// </summary>
    [JsonProperty("actionType")]
    public string? ActionType { get; set; }

    /// <summary>
    /// 审批状态
    /// WAT 未发起
    /// ING 审核中
    /// PAS 审核通过
    /// RVK 审核撤销
    /// REJ 审核否决
    /// NPM 没有审批
    /// </summary>
    [JsonProperty("approveStatus")]
    public string? ApproveStatus { get; set; }

    /// <summary>
    /// 员工序号
    /// </summary>
    [JsonProperty("staffSeq")]
    public string? StaffSeq { get; set; }

    /// <summary>
    /// 单据号
    /// </summary>
    [JsonProperty("trsSeq")]
    public string? TrsSeq { get; set; }
}

/// <summary>
/// 离职管理事件操作类型枚举
/// </summary>
public enum StaffQuitActionType
{
    /// <summary>
    /// 办理
    /// </summary>
    [JsonProperty("LAUNCH")]
    Launch,

    /// <summary>
    /// 审批结束
    /// </summary>
    [JsonProperty("APPROVE_END")]
    ApproveEnd,

    /// <summary>
    /// 确认离职
    /// </summary>
    [JsonProperty("COF")]
    Confirm,

    /// <summary>
    /// 生效
    /// </summary>
    [JsonProperty("EFFECTIVE")]
    Effective,

    /// <summary>
    /// 取消
    /// </summary>
    [JsonProperty("CANCEL")]
    Cancel
}

/// <summary>
/// 离职管理事件审批状态枚举
/// </summary>
public enum StaffQuitApproveStatus
{
    /// <summary>
    /// 未发起
    /// </summary>
    [JsonProperty("WAT")]
    NotInitiated,

    /// <summary>
    /// 审核中
    /// </summary>
    [JsonProperty("ING")]
    InProgress,

    /// <summary>
    /// 审核通过
    /// </summary>
    [JsonProperty("PAS")]
    Approved,

    /// <summary>
    /// 审核撤销
    /// </summary>
    [JsonProperty("RVK")]
    Revoked,

    /// <summary>
    /// 审核否决
    /// </summary>
    [JsonProperty("REJ")]
    Rejected,

    /// <summary>
    /// 没有审批
    /// </summary>
    [JsonProperty("NPM")]
    NoApproval
}
