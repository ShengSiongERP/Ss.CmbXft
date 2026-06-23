using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ss.CmbXft.Sdk.Models.Staff;

/// <summary>
/// 创建员工结果项
/// </summary>
public class CreateStaffResultItem
{
    [JsonProperty("stfSeq")]
    public string? StfSeq { get; set; }

    [JsonProperty("certificateType")]
    public string? CertificateType { get; set; }

    [JsonProperty("certificateNumber")]
    public string? CertificateNumber { get; set; }

    [JsonProperty("errorMessage")]
    public string? ErrorMessage { get; set; }
}

/// <summary>
/// 创建员工响应体
/// </summary>
public class CreateStaffResponseBody : List<CreateStaffResultItem>
{
}