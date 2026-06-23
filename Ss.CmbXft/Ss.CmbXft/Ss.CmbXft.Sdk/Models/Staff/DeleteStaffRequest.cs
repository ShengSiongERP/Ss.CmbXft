using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ss.CmbXft.Sdk.Models.Staff;

/// <summary>
/// 删除员工请求
/// </summary>
public class DeleteStaffRequest
{
    /// <summary>
    /// 员工序号集合
    /// </summary>
    [JsonProperty("stfSeqList")]
    public List<string> StfSeqList { get; set; } = new List<string>();
}

/// <summary>
/// 删除员工错误信息
/// </summary>
public class DeleteStaffError
{
    /// <summary>
    /// 校验类型
    /// </summary>
    [JsonProperty("verifyType")]
    public string VerifyType { get; set; } = string.Empty;

    /// <summary>
    /// 错误信息
    /// </summary>
    [JsonProperty("errorMessage")]
    public string ErrorMessage { get; set; } = string.Empty;
}

/// <summary>
/// 删除员工结果项
/// </summary>
public class DeleteStaffResultItem
{
    /// <summary>
    /// 员工序号
    /// </summary>
    [JsonProperty("stfSeq")]
    public string StfSeq { get; set; } = string.Empty;

    /// <summary>
    /// 错误列表
    /// </summary>
    [JsonProperty("errorList")]
    public List<DeleteStaffError>? ErrorList { get; set; }
}

/// <summary>
/// 删除员工响应体
/// </summary>
public class DeleteStaffResponseBody : List<DeleteStaffResultItem>
{
}
