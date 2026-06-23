using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ss.CmbXft.Sdk.Models.Staff;

/// <summary>
/// 编辑员工基本信息
/// </summary>
public class EditStaffBasicInfo : CreateStaffBasicInfo
{
    /// <summary>
    /// 员工序号（必填）
    /// </summary>
    [JsonProperty("stfSeq")]
    public string StfSeq { get; set; } = string.Empty;
}

/// <summary>
/// 编辑员工请求项
/// </summary>
public class EditStaffRequestItem
{
    /// <summary>
    /// 员工基本信息、员工个人信息
    /// </summary>
    [JsonProperty("staffBasicInfo")]
    public EditStaffBasicInfo? StaffBasicInfo { get; set; }

    /// <summary>
    /// 员工在职信息、离职信息
    /// </summary>
    [JsonProperty("staffHrmInfo")]
    public CreateStaffHrmInfo? StaffHrmInfo { get; set; }

    /// <summary>
    /// 员工工资社保
    /// </summary>
    [JsonProperty("staffWagesAndSocialSecurityInfo")]
    public CreateStaffWagesAndSocialSecurityInfo? StaffWagesAndSocialSecurityInfo { get; set; }

    /// <summary>
    /// 员工紧急联系人信息
    /// </summary>
    [JsonProperty("staffEmergencyContact")]
    public CreateStaffEmergencyContact? StaffEmergencyContact { get; set; }

    /// <summary>
    /// 员工个税基础信息
    /// </summary>
    [JsonProperty("staffTaxBasicInfo")]
    public CreateStaffTaxBasicInfo? StaffTaxBasicInfo { get; set; }

    /// <summary>
    /// 员工奖惩记录信息
    /// </summary>
    [JsonProperty("staffAwardInfoList")]
    public List<EditStaffAwardInfo>? StaffAwardInfoList { get; set; }

    /// <summary>
    /// 员工工作经历信息
    /// </summary>
    [JsonProperty("staffWorkInfoList")]
    public List<EditStaffWorkInfo>? StaffWorkInfoList { get; set; }

    /// <summary>
    /// 员工教育经历信息
    /// </summary>
    [JsonProperty("staffEducationInfoList")]
    public List<EditStaffEducationInfo>? StaffEducationInfoList { get; set; }

    /// <summary>
    /// 员工专业证书信息
    /// </summary>
    [JsonProperty("staffCertificateInfoList")]
    public List<EditStaffCertificateInfo>? StaffCertificateInfoList { get; set; }

    /// <summary>
    /// 员工职称信息
    /// </summary>
    [JsonProperty("staffJobTitleInfoList")]
    public List<EditStaffJobTitleInfo>? StaffJobTitleInfoList { get; set; }

    /// <summary>
    /// 员工家庭成员信息
    /// </summary>
    [JsonProperty("staffFamilyMemberInfoList")]
    public List<EditStaffFamilyMemberInfo>? StaffFamilyMemberInfoList { get; set; }

    /// <summary>
    /// 员工个税申报信息
    /// </summary>
    [JsonProperty("staffTaxDeclarationInfoList")]
    public List<EditStaffTaxDeclarationInfo>? StaffTaxDeclarationInfoList { get; set; }

    /// <summary>
    /// 员工兼任信息
    /// </summary>
    [JsonProperty("staffAdjunctInfoList")]
    public List<EditStaffAdjunctInfo>? StaffAdjunctInfoList { get; set; }

    /// <summary>
    /// 员工自定义分组信息
    /// </summary>
    [JsonProperty("customerFieldInfoList")]
    public List<EditCustomFieldInfo>? CustomerFieldInfoList { get; set; }

    /// <summary>
    /// 员工合同信息
    /// </summary>
    [JsonProperty("staffContractInfoList")]
    public List<EditStaffContractInfo>? StaffContractInfoList { get; set; }

    /// <summary>
    /// 员工个人材料附件信息
    /// </summary>
    [JsonProperty("staffAttachmentInfoList")]
    public List<AttachmentInfo>? StaffAttachmentInfoList { get; set; }

    /// <summary>
    /// 培训记录
    /// </summary>
    [JsonProperty("staffTrainRecordInfoList")]
    public List<EditStaffTrainRecordInfo>? StaffTrainRecordInfoList { get; set; }

    /// <summary>
    /// 绩效考核
    /// </summary>
    [JsonProperty("staffPerformanceInfoList")]
    public List<EditStaffPerformanceInfo>? StaffPerformanceInfoList { get; set; }

    /// <summary>
    /// 多记录分组更新方式标志
    /// </summary>
    [JsonProperty("multiGroupUpdateFlag")]
    public string? MultiGroupUpdateFlag { get; set; }
}

/// <summary>
/// 编辑员工请求
/// </summary>
public class EditStaffRequest : List<EditStaffRequestItem>
{
}

/// <summary>
/// 编辑员工奖惩记录信息
/// </summary>
public class EditStaffAwardInfo : CreateStaffAwardInfo
{
    /// <summary>
    /// 奖惩记录流水号
    /// </summary>
    [JsonProperty("tradeSeq")]
    public string? TradeSeq { get; set; }
}

/// <summary>
/// 编辑员工工作经历信息
/// </summary>
public class EditStaffWorkInfo : CreateStaffWorkInfo
{
    /// <summary>
    /// 工作经历流水号
    /// </summary>
    [JsonProperty("tradeSeq")]
    public string? TradeSeq { get; set; }
}

/// <summary>
/// 编辑员工教育经历信息
/// </summary>
public class EditStaffEducationInfo : CreateStaffEducationInfo
{
    /// <summary>
    /// 教育经历流水号
    /// </summary>
    [JsonProperty("tradeSeq")]
    public string? TradeSeq { get; set; }
}

/// <summary>
/// 编辑员工专业证书信息
/// </summary>
public class EditStaffCertificateInfo : CreateStaffCertificateInfo
{
    /// <summary>
    /// 专业证书流水号
    /// </summary>
    [JsonProperty("tradeSeq")]
    public string? TradeSeq { get; set; }
}

/// <summary>
/// 编辑员工职称信息
/// </summary>
public class EditStaffJobTitleInfo : CreateStaffJobTitleInfo
{
    /// <summary>
    /// 职称流水号
    /// </summary>
    [JsonProperty("tradeSeq")]
    public string? TradeSeq { get; set; }
}

/// <summary>
/// 编辑员工家庭成员信息
/// </summary>
public class EditStaffFamilyMemberInfo : CreateStaffFamilyMemberInfo
{
    /// <summary>
    /// 家庭成员流水号
    /// </summary>
    [JsonProperty("tradeSeq")]
    public string? TradeSeq { get; set; }
}

/// <summary>
/// 编辑员工个税申报信息
/// </summary>
public class EditStaffTaxDeclarationInfo
{
    /// <summary>
    /// 税局登记序号
    /// </summary>
    [JsonProperty("registerSequence")]
    public string? RegisterSequence { get; set; } = string.Empty;

    /// <summary>
    /// 任职受雇类型
    /// </summary>
    [JsonProperty("employedType")]
    public string? EmployedType { get; set; }

    /// <summary>
    /// 受雇日期
    /// </summary>
    [JsonProperty("employedDate")]
    public string? EmployedDate { get; set; }

    /// <summary>
    /// 入职年度就业情形
    /// </summary>
    [JsonProperty("employmentSituation")]
    public string? EmploymentSituation { get; set; }

    /// <summary>
    /// 人员状态
    /// </summary>
    [JsonProperty("personnelState")]
    public string? PersonnelState { get; set; }

    /// <summary>
    /// 职务
    /// </summary>
    [JsonProperty("duty")]
    public string? Duty { get; set; }

    /// <summary>
    /// 税局员工备注
    /// </summary>
    [JsonProperty("taxRemark")]
    public string? TaxRemark { get; set; }

    /// <summary>
    /// 税局离职日期
    /// </summary>
    [JsonProperty("taxOfficeQuitDate")]
    public string? TaxOfficeQuitDate { get; set; }

    /// <summary>
    /// 涉税事由
    /// </summary>
    [JsonProperty("taxRelatedReason")]
    public string? TaxRelatedReason { get; set; }

    /// <summary>
    /// 个人投资总额
    /// </summary>
    [JsonProperty("totalPersonalInvestment")]
    public decimal? TotalPersonalInvestment { get; set; }

    /// <summary>
    /// 个人投资比例
    /// </summary>
    [JsonProperty("individualInvestmentPercentage")]
    public decimal? IndividualInvestmentPercentage { get; set; }

    /// <summary>
    /// 是否扣除减除费用
    /// </summary>
    [JsonProperty("isDeductionsDeducted")]
    public string? IsDeductionsDeducted { get; set; }

    /// <summary>
    /// 个税通知手机号
    /// </summary>
    [JsonProperty("taxMobileNumber")]
    public string? TaxMobileNumber { get; set; }

    /// <summary>
    /// 其他情况说明
    /// </summary>
    [JsonProperty("otherEmployeeTypeNote")]
    public string? OtherEmployeeTypeNote { get; set; }
}

/// <summary>
/// 编辑员工兼任信息
/// </summary>
public class EditStaffAdjunctInfo : CreateStaffAdjunctInfo
{
    /// <summary>
    /// 兼任信息流水号
    /// </summary>
    [JsonProperty("tradeSeq")]
    public string? TradeSeq { get; set; }
}

/// <summary>
/// 编辑员工自定义分组信息
/// </summary>
public class EditCustomFieldInfo : CustomFieldInfo
{
    /// <summary>
    /// 自定义分组自定义附件序号
    /// </summary>
    [JsonProperty("attachmentFieldInnerSeq")]
    public int? AttachmentFieldInnerSeq { get; set; }

    /// <summary>
    /// 自定义分组自定义附件文件名
    /// </summary>
    [JsonProperty("attachmentFieldName")]
    public string? AttachmentFieldName { get; set; }

    /// <summary>
    /// 自定义分组业务流水号
    /// </summary>
    [JsonProperty("businessSeq")]
    public string? BusinessSeq { get; set; }
}

/// <summary>
/// 编辑员工合同信息
/// </summary>
public class EditStaffContractInfo : CreateStaffContractInfo
{
    /// <summary>
    /// 合同流水号
    /// </summary>
    [JsonProperty("tradeSeq")]
    public string? TradeSeq { get; set; }
}

/// <summary>
/// 编辑员工培训记录
/// </summary>
public class EditStaffTrainRecordInfo
{
    /// <summary>
    /// 课程名称
    /// </summary>
    [JsonProperty("courseName")]
    public string? CourseName { get; set; }

    /// <summary>
    /// 课程编号
    /// </summary>
    [JsonProperty("courseNumber")]
    public string? CourseNumber { get; set; }

    /// <summary>
    /// 培训类型
    /// </summary>
    [JsonProperty("trainType")]
    public string? TrainType { get; set; }

    /// <summary>
    /// 培训方式
    /// </summary>
    [JsonProperty("trainWay")]
    public string? TrainWay { get; set; }

    /// <summary>
    /// 培训机构
    /// </summary>
    [JsonProperty("trainInstitution")]
    public string? TrainInstitution { get; set; }

    /// <summary>
    /// 讲师
    /// </summary>
    [JsonProperty("lecturer")]
    public string? Lecturer { get; set; }

    /// <summary>
    /// 培训开始日期
    /// </summary>
    [JsonProperty("trainBegin")]
    public string? TrainBegin { get; set; }

    /// <summary>
    /// 培训结束日期
    /// </summary>
    [JsonProperty("trainEnd")]
    public string? TrainEnd { get; set; }

    /// <summary>
    /// 培训地点
    /// </summary>
    [JsonProperty("trainLocation")]
    public string? TrainLocation { get; set; }

    /// <summary>
    /// 考试时间
    /// </summary>
    [JsonProperty("examDate")]
    public string? ExamDate { get; set; }

    /// <summary>
    /// 考试结果
    /// </summary>
    [JsonProperty("examResult")]
    public string? ExamResult { get; set; }

    /// <summary>
    /// 培训记录流水号
    /// </summary>
    [JsonProperty("tradeSeq")]
    public string? TradeSeq { get; set; }

    /// <summary>
    /// 自定义字段信息
    /// </summary>
    [JsonProperty("customerFieldInfoList")]
    public List<CustomFieldInfo>? CustomerFieldInfoList { get; set; }

    /// <summary>
    /// 培训记录附件
    /// </summary>
    [JsonProperty("attachmentInfoList")]
    public List<AttachmentInfo>? AttachmentInfoList { get; set; }
}

/// <summary>
/// 编辑员工绩效考核
/// </summary>
public class EditStaffPerformanceInfo
{
    /// <summary>
    /// 绩效考核周期
    /// </summary>
    [JsonProperty("performanceCycle")]
    public string? PerformanceCycle { get; set; }

    /// <summary>
    /// 绩效考核人
    /// </summary>
    [JsonProperty("performanceEvaluator")]
    public string? PerformanceEvaluator { get; set; }

    /// <summary>
    /// 绩效考核评分
    /// </summary>
    [JsonProperty("performanceScore")]
    public string? PerformanceScore { get; set; }

    /// <summary>
    /// 绩效考核等级
    /// </summary>
    [JsonProperty("performanceLevel")]
    public string? PerformanceLevel { get; set; }

    /// <summary>
    /// 绩效考核评语
    /// </summary>
    [JsonProperty("performanceComments")]
    public string? PerformanceComments { get; set; }

    /// <summary>
    /// 绩效考核流水号
    /// </summary>
    [JsonProperty("tradeSeq")]
    public string? TradeSeq { get; set; }

    /// <summary>
    /// 自定义字段信息
    /// </summary>
    [JsonProperty("customerFieldInfoList")]
    public List<CustomFieldInfo>? CustomerFieldInfoList { get; set; }

    /// <summary>
    /// 绩效考核附件
    /// </summary>
    [JsonProperty("attachmentInfoList")]
    public List<AttachmentInfo>? AttachmentInfoList { get; set; }
}

/// <summary>
/// 创建员工个税基础信息
/// </summary>
public class CreateStaffTaxBasicInfo
{
    /// <summary>
    /// 其他证件类型
    /// </summary>
    [JsonProperty("otherCertificateType")]
    public string? OtherCertificateType { get; set; }

    /// <summary>
    /// 其他证件号码
    /// </summary>
    [JsonProperty("otherCertificateNumber")]
    public string? OtherCertificateNumber { get; set; }

    /// <summary>
    /// 出生地
    /// </summary>
    [JsonProperty("birthLand")]
    public string? BirthLand { get; set; }

    /// <summary>
    /// 中文名
    /// </summary>
    [JsonProperty("chineseName")]
    public string? ChineseName { get; set; }

    /// <summary>
    /// 首次入境日期
    /// </summary>
    [JsonProperty("firstInboundDate")]
    public string? FirstInboundDate { get; set; }

    /// <summary>
    /// 预计离境日期
    /// </summary>
    [JsonProperty("planOutboundDate")]
    public string? PlanOutboundDate { get; set; }

    /// <summary>
    /// 最高学历
    /// </summary>
    [JsonProperty("highestDegree")]
    public string? HighestDegree { get; set; }

    /// <summary>
    /// 是否残疾
    /// </summary>
    [JsonProperty("isDisabled")]
    public string? IsDisabled { get; set; }

    /// <summary>
    /// 是否烈属
    /// </summary>
    [JsonProperty("isMartyr")]
    public string? IsMartyr { get; set; }

    /// <summary>
    /// 是否孤老
    /// </summary>
    [JsonProperty("isLonelyAndOld")]
    public string? IsLonelyAndOld { get; set; }

    /// <summary>
    /// 残疾证类型
    /// </summary>
    [JsonProperty("disabilityCertificateType")]
    public string? DisabilityCertificateType { get; set; }

    /// <summary>
    /// 残疾证号
    /// </summary>
    [JsonProperty("disabilityCertificateNumber")]
    public string? DisabilityCertificateNumber { get; set; }

    /// <summary>
    /// 烈属证号
    /// </summary>
    [JsonProperty("martyrCertificateNumber")]
    public string? MartyrCertificateNumber { get; set; }
}
