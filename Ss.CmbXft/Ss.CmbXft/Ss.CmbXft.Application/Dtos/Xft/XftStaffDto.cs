using System;

namespace Ss.CmbXft.Application.Dtos;

public class XftStaffDto
{
    public long Id { get; set; }
    public string? EnterpriseId { get; set; }
    public string? StaffSeq { get; set; }
    public string? StfType { get; set; }
    public string? StfStatus { get; set; }
    public string? StfName { get; set; }
    public string? MobileNumber { get; set; }
    public string StaffJson { get; set; } = string.Empty;
    public DateTime CreateTime { get; set; }
    public DateTime? UpdateTime { get; set; }
}

public class CreateXftStaffDto
{
    public string? EnterpriseId { get; set; }
    public string? StaffSeq { get; set; }
    public string? StfType { get; set; }
    public string? StfStatus { get; set; }
    public string? StfName { get; set; }
    public string? MobileNumber { get; set; }
    public string StaffJson { get; set; } = string.Empty;
}

public class UpdateXftStaffDto : CreateXftStaffDto
{
    public long Id { get; set; }
}
