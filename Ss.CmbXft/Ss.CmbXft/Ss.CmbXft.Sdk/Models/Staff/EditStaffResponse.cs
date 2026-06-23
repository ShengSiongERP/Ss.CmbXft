using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ss.CmbXft.Sdk.Models.Staff;

/// <summary>
/// 编辑员工结果项
/// </summary>
public class EditStaffResultItem
{
    /// <summary>
    /// 员工序号
    /// </summary>
    [JsonProperty("stfSeq")]
    public string? StfSeq { get; set; }

    /// <summary>
    /// 证件类型
    /// </summary>
    [JsonProperty("certificateType")]
    public string? CertificateType { get; set; }

    /// <summary>
    /// 证件号
    /// </summary>
    [JsonProperty("certificateNumber")]
    public string? CertificateNumber { get; set; }

    /// <summary>
    /// 校验信息
    /// </summary>
    [JsonProperty("errorMessage")]
    public string? ErrorMessage { get; set; }
}

/// <summary>
/// 编辑员工响应体
/// </summary>
public class EditStaffResponseBody : List<EditStaffResultItem>
{
}