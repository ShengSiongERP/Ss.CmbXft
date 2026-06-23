using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ss.CmbXft.Sdk.Models.Staff;

/// <summary>
/// 自定义字段信息
/// </summary>
public class CustomFieldInfo
{
    /// <summary>
    /// 分组key
    /// </summary>
    [JsonProperty("classKey")]
    public string? ClassKey { get; set; }

    /// <summary>
    /// 字段id
    /// </summary>
    [JsonProperty("fieldKey")]
    public string? FieldKey { get; set; }

    /// <summary>
    /// 字段值
    /// </summary>
    [JsonProperty("fieldValue")]
    public string? FieldValue { get; set; }

    /// <summary>
    /// 业务流水号
    /// </summary>
    [JsonProperty("businessSeq")]
    public string? BusinessSeq { get; set; }
}

/// <summary>
/// 员工基本信息
/// </summary>
public class CreateStaffBasicInfo
{
    /// <summary>
    /// 员工类型
    /// </summary>
    [JsonProperty("stfType")]
    public string? StfType { get; set; }

    /// <summary>
    /// 员工状态
    /// </summary>
    [JsonProperty("stfStatus")]
    public string? StfStatus { get; set; }

    /// <summary>
    /// 员工姓名
    /// </summary>
    [JsonProperty("stfName")]
    public string? StfName { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    [JsonProperty("mobileNumber")]
    public string? MobileNumber { get; set; }

    /// <summary>
    /// 证件类型
    /// </summary>
    [JsonProperty("certificateType")]
    public string? CertificateType { get; set; }

    /// <summary>
    /// 证件号码
    /// </summary>
    [JsonProperty("certificateNumber")]
    public string? CertificateNumber { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    [JsonProperty("sex")]
    public string? Sex { get; set; }

    /// <summary>
    /// 国籍/地区
    /// </summary>
    [JsonProperty("nationality")]
    public string? Nationality { get; set; }

    /// <summary>
    /// 部门
    /// </summary>
    [JsonProperty("orgSeq")]
    public string? OrgSeq { get; set; }

    /// <summary>
    /// 岗位编码
    /// </summary>
    [JsonProperty("posCode")]
    public string? PosCode { get; set; }

    /// <summary>
    /// 职位编码
    /// </summary>
    [JsonProperty("jobCode")]
    public string? JobCode { get; set; }

    /// <summary>
    /// 职级
    /// </summary>
    [JsonProperty("jobRankSeq")]
    public string? JobRankSeq { get; set; }

    /// <summary>
    /// 员工号
    /// </summary>
    [JsonProperty("stfNumber")]
    public string? StfNumber { get; set; }

    /// <summary>
    /// 座机
    /// </summary>
    [JsonProperty("telephoneNumber")]
    public string? TelephoneNumber { get; set; }

    /// <summary>
    /// 工作邮箱
    /// </summary>
    [JsonProperty("workEmail")]
    public string? WorkEmail { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [JsonProperty("remark")]
    public string? Remark { get; set; }

    /// <summary>
    /// 出生日期
    /// </summary>
    [JsonProperty("birthday")]
    public string? Birthday { get; set; }

    /// <summary>
    /// 是否已婚
    /// </summary>
    [JsonProperty("hasMarried")]
    public string? HasMarried { get; set; }

    /// <summary>
    /// 是否已育
    /// </summary>
    [JsonProperty("hasNurtured")]
    public string? HasNurtured { get; set; }

    /// <summary>
    /// 民族
    /// </summary>
    [JsonProperty("nation")]
    public string? Nation { get; set; }

    /// <summary>
    /// 政治面貌
    /// </summary>
    [JsonProperty("politicalAppearance")]
    public string? PoliticalAppearance { get; set; }

    /// <summary>
    /// 现住址省
    /// </summary>
    [JsonProperty("presentAddressProvince")]
    public string? PresentAddressProvince { get; set; }

    /// <summary>
    /// 现住址城市
    /// </summary>
    [JsonProperty("presentAddressCity")]
    public string? PresentAddressCity { get; set; }

    /// <summary>
    /// 现住址区/县
    /// </summary>
    [JsonProperty("presentAddressDistrict")]
    public string? PresentAddressDistrict { get; set; }

    /// <summary>
    /// 现住址详细地址
    /// </summary>
    [JsonProperty("presentAddressDetail")]
    public string? PresentAddressDetail { get; set; }

    /// <summary>
    /// 户籍省
    /// </summary>
    [JsonProperty("householdAddressProvince")]
    public string? HouseholdAddressProvince { get; set; }

    /// <summary>
    /// 户籍城市
    /// </summary>
    [JsonProperty("householdAddressCity")]
    public string? HouseholdAddressCity { get; set; }

    /// <summary>
    /// 户籍区/县
    /// </summary>
    [JsonProperty("householdAddressDistrict")]
    public string? HouseholdAddressDistrict { get; set; }

    /// <summary>
    /// 户籍详细地址
    /// </summary>
    [JsonProperty("householdAddressDetail")]
    public string? HouseholdAddressDetail { get; set; }

    /// <summary>
    /// 户口性质
    /// </summary>
    [JsonProperty("householdType")]
    public string? HouseholdType { get; set; }

    /// <summary>
    /// 身份证地址
    /// </summary>
    [JsonProperty("certificateAddress")]
    public string? CertificateAddress { get; set; }

    /// <summary>
    /// 证件是否长期有效
    /// </summary>
    [JsonProperty("isCertificateLongTermEffective")]
    public string? IsCertificateLongTermEffective { get; set; }

    /// <summary>
    /// 证件签发日期
    /// </summary>
    [JsonProperty("certificateValidBeginDate")]
    public string? CertificateValidBeginDate { get; set; }

    /// <summary>
    /// 证件到期日期
    /// </summary>
    [JsonProperty("certificateValidEndDate")]
    public string? CertificateValidEndDate { get; set; }

    /// <summary>
    /// 个人邮箱
    /// </summary>
    [JsonProperty("individualEmail")]
    public string? IndividualEmail { get; set; }

    /// <summary>
    /// 首次工作日期
    /// </summary>
    [JsonProperty("firstWorkDate")]
    public string? FirstWorkDate { get; set; }

    /// <summary>
    /// 工作地
    /// </summary>
    [JsonProperty("workplaceLocationSeq")]
    public string? WorkplaceLocationSeq { get; set; }

    /// <summary>
    /// 联系地址省
    /// </summary>
    [JsonProperty("contactAddressProvince")]
    public string? ContactAddressProvince { get; set; }

    /// <summary>
    /// 联系地址城市
    /// </summary>
    [JsonProperty("contactAddressCity")]
    public string? ContactAddressCity { get; set; }

    /// <summary>
    /// 联系地址区/县
    /// </summary>
    [JsonProperty("contactAddressDistrict")]
    public string? ContactAddressDistrict { get; set; }

    /// <summary>
    /// 联系地址详细地址
    /// </summary>
    [JsonProperty("contactAddressDetail")]
    public string? ContactAddressDetail { get; set; }

    /// <summary>
    /// 业务分组
    /// </summary>
    [JsonProperty("businessGroupSeq")]
    public string? BusinessGroupSeq { get; set; }

    /// <summary>
    /// 曾用名
    /// </summary>
    [JsonProperty("formerName")]
    public string? FormerName { get; set; }

    /// <summary>
    /// 汇报上级
    /// </summary>
    [JsonProperty("reportSuperiorStfSeq")]
    public string? ReportSuperiorStfSeq { get; set; }

    /// <summary>
    /// 自定义字段信息
    /// </summary>
    [JsonProperty("customerFieldInfoList")]
    public List<CustomFieldInfo>? CustomerFieldInfoList { get; set; }
}

/// <summary>
/// 员工人事信息
/// </summary>
public class CreateStaffHrmInfo
{
    /// <summary>
    /// 入职日期
    /// </summary>
    [JsonProperty("entryDate")]
    public string? EntryDate { get; set; }

    /// <summary>
    /// 实际试用期
    /// </summary>
    [JsonProperty("actutalProbationPeriod")]
    public string? ActutalProbationPeriod { get; set; }

    /// <summary>
    /// 实际转正日期
    /// </summary>
    [JsonProperty("actualPositiveDate")]
    public string? ActualPositiveDate { get; set; }

    /// <summary>
    /// 实际离职日期
    /// </summary>
    [JsonProperty("actualQuitDate")]
    public string? ActualQuitDate { get; set; }

    /// <summary>
    /// 离职类型
    /// </summary>
    [JsonProperty("quitType")]
    public string? QuitType { get; set; }

    /// <summary>
    /// 离职原因
    /// </summary>
    [JsonProperty("quitReason")]
    public string? QuitReason { get; set; }

    /// <summary>
    /// 离职申请日期
    /// </summary>
    [JsonProperty("applyQuitDate")]
    public string? ApplyQuitDate { get; set; }

    /// <summary>
    /// 计划离职日期
    /// </summary>
    [JsonProperty("planQuitDate")]
    public string? PlanQuitDate { get; set; }

    /// <summary>
    /// 薪资结算日期
    /// </summary>
    [JsonProperty("salarySettleDate")]
    public string? SalarySettleDate { get; set; }

    /// <summary>
    /// 离职备注
    /// </summary>
    [JsonProperty("quitRemark")]
    public string? QuitRemark { get; set; }

    /// <summary>
    /// 是否加入黑名单
    /// </summary>
    [JsonProperty("inBlackList")]
    public string? InBlackList { get; set; }

    /// <summary>
    /// 加入黑名单备注
    /// </summary>
    [JsonProperty("reasonForBlackList")]
    public string? ReasonForBlackList { get; set; }

    /// <summary>
    /// 申请转正日期
    /// </summary>
    [JsonProperty("applyPositiveDate")]
    public string? ApplyPositiveDate { get; set; }

    /// <summary>
    /// 计划转正日期
    /// </summary>
    [JsonProperty("planPositiveDate")]
    public string? PlanPositiveDate { get; set; }

    /// <summary>
    /// 计划试用期
    /// </summary>
    [JsonProperty("planProbationPeriod")]
    public string? PlanProbationPeriod { get; set; }

    /// <summary>
    /// 司龄开始日期
    /// </summary>
    [JsonProperty("seniorityBeginDate")]
    public string? SeniorityBeginDate { get; set; }

    /// <summary>
    /// 自定义字段信息
    /// </summary>
    [JsonProperty("customerFieldInfoList")]
    public List<CustomFieldInfo>? CustomerFieldInfoList { get; set; }
}

/// <summary>
/// 员工薪资社保信息
/// </summary>
public class CreateStaffWagesAndSocialSecurityInfo
{
    /// <summary>
    /// 工资卡号
    /// </summary>
    [JsonProperty("bankCardAccount")]
    public string? BankCardAccount { get; set; }

    /// <summary>
    /// 开户行
    /// </summary>
    [JsonProperty("bankName")]
    public string? BankName { get; set; }

    /// <summary>
    /// 开户行省
    /// </summary>
    [JsonProperty("bankOfProvince")]
    public string? BankOfProvince { get; set; }

    /// <summary>
    /// 开户行市
    /// </summary>
    [JsonProperty("bankOfCity")]
    public string? BankOfCity { get; set; }

    /// <summary>
    /// 数币钱包
    /// </summary>
    [JsonProperty("digitalWallet")]
    public string? DigitalWallet { get; set; }

    /// <summary>
    /// 数币银行
    /// </summary>
    [JsonProperty("digitalBank")]
    public string? DigitalBank { get; set; }

    /// <summary>
    /// 个人社保账号
    /// </summary>
    [JsonProperty("socialSecurityAccount")]
    public string? SocialSecurityAccount { get; set; }

    /// <summary>
    /// 个人公积金账号
    /// </summary>
    [JsonProperty("accumulationFundAccount")]
    public string? AccumulationFundAccount { get; set; }

    /// <summary>
    /// 自定义字段信息
    /// </summary>
    [JsonProperty("customerFieldInfoList")]
    public List<CustomFieldInfo>? CustomerFieldInfoList { get; set; }
}

/// <summary>
/// 员工紧急联系人
/// </summary>
public class CreateStaffEmergencyContact
{
    /// <summary>
    /// 紧急联系人
    /// </summary>
    [JsonProperty("contactName")]
    public string? ContactName { get; set; }

    /// <summary>
    /// 紧急联系人电话
    /// </summary>
    [JsonProperty("contactTelephoneNumber")]
    public string? ContactTelephoneNumber { get; set; }

    /// <summary>
    /// 自定义字段信息
    /// </summary>
    [JsonProperty("customerFieldInfoList")]
    public List<CustomFieldInfo>? CustomerFieldInfoList { get; set; }
}

/// <summary>
/// 员工奖惩信息
/// </summary>
public class CreateStaffAwardInfo
{
    /// <summary>
    /// 奖惩事项
    /// </summary>
    [JsonProperty("rewardAndPunishmentMatters")]
    public string? RewardAndPunishmentMatters { get; set; }

    /// <summary>
    /// 奖惩时间
    /// </summary>
    [JsonProperty("rewardAndPunishmentDate")]
    public string? RewardAndPunishmentDate { get; set; }

    /// <summary>
    /// 奖惩单位
    /// </summary>
    [JsonProperty("rewardAndPunishmentCompany")]
    public string? RewardAndPunishmentCompany { get; set; }

    /// <summary>
    /// 奖惩详情
    /// </summary>
    [JsonProperty("rewardAndPunishmentDetail")]
    public string? RewardAndPunishmentDetail { get; set; }

    /// <summary>
    /// 自定义字段信息
    /// </summary>
    [JsonProperty("customerFieldInfoList")]
    public List<CustomFieldInfo>? CustomerFieldInfoList { get; set; }
}

/// <summary>
/// 员工工作经历
/// </summary>
public class CreateStaffWorkInfo
{
    /// <summary>
    /// 工作单位
    /// </summary>
    [JsonProperty("lastCompanyName")]
    public string? LastCompanyName { get; set; }

    /// <summary>
    /// 曾任岗位
    /// </summary>
    [JsonProperty("lastPositionName")]
    public string? LastPositionName { get; set; }

    /// <summary>
    /// 工作开始日期
    /// </summary>
    [JsonProperty("workBeginDate")]
    public string? WorkBeginDate { get; set; }

    /// <summary>
    /// 工作结束日期
    /// </summary>
    [JsonProperty("workEndDate")]
    public string? WorkEndDate { get; set; }

    /// <summary>
    /// 证明人
    /// </summary>
    [JsonProperty("proverName")]
    public string? ProverName { get; set; }

    /// <summary>
    /// 证明人联系电话
    /// </summary>
    [JsonProperty("proverTelephoneNumber")]
    public string? ProverTelephoneNumber { get; set; }

    /// <summary>
    /// 过往离职原因
    /// </summary>
    [JsonProperty("leaveReason")]
    public string? LeaveReason { get; set; }

    /// <summary>
    /// 离职薪资
    /// </summary>
    [JsonProperty("leaveSalary")]
    public string? LeaveSalary { get; set; }

    /// <summary>
    /// 自定义字段信息
    /// </summary>
    [JsonProperty("customerFieldInfoList")]
    public List<CustomFieldInfo>? CustomerFieldInfoList { get; set; }
}

/// <summary>
/// 员工教育经历
/// </summary>
public class CreateStaffEducationInfo
{
    /// <summary>
    /// 毕业院校
    /// </summary>
    [JsonProperty("graduateSchool")]
    public string? GraduateSchool { get; set; }

    /// <summary>
    /// 学历
    /// </summary>
    [JsonProperty("degree")]
    public string? Degree { get; set; }

    /// <summary>
    /// 专业
    /// </summary>
    [JsonProperty("specialty")]
    public string? Specialty { get; set; }

    /// <summary>
    /// 毕业日期
    /// </summary>
    [JsonProperty("graduateDate")]
    public string? GraduateDate { get; set; }

    /// <summary>
    /// 自定义字段信息
    /// </summary>
    [JsonProperty("customerFieldInfoList")]
    public List<CustomFieldInfo>? CustomerFieldInfoList { get; set; }
}

/// <summary>
/// 员工专业证书
/// </summary>
public class CreateStaffCertificateInfo
{
    /// <summary>
    /// 证书名称
    /// </summary>
    [JsonProperty("certificateName")]
    public string? CertificateName { get; set; }

    /// <summary>
    /// 证书机构
    /// </summary>
    [JsonProperty("issuingAuthority")]
    public string? IssuingAuthority { get; set; }

    /// <summary>
    /// 发证日期
    /// </summary>
    [JsonProperty("issuingDate")]
    public string? IssuingDate { get; set; }

    /// <summary>
    /// 有效期起始日
    /// </summary>
    [JsonProperty("validBeginDate")]
    public string? ValidBeginDate { get; set; }

    /// <summary>
    /// 有效期到期日
    /// </summary>
    [JsonProperty("validEndDate")]
    public string? ValidEndDate { get; set; }

    /// <summary>
    /// 自定义字段信息
    /// </summary>
    [JsonProperty("customerFieldInfoList")]
    public List<CustomFieldInfo>? CustomerFieldInfoList { get; set; }
}

/// <summary>
/// 员工职称信息
/// </summary>
public class CreateStaffJobTitleInfo
{
    /// <summary>
    /// 职称名称
    /// </summary>
    [JsonProperty("titleName")]
    public string? TitleName { get; set; }

    /// <summary>
    /// 职称级别
    /// </summary>
    [JsonProperty("titleLevel")]
    public string? TitleLevel { get; set; }

    /// <summary>
    /// 职称评定机构
    /// </summary>
    [JsonProperty("assessmentAuthority")]
    public string? AssessmentAuthority { get; set; }

    /// <summary>
    /// 职称获得日期
    /// </summary>
    [JsonProperty("obtainDate")]
    public string? ObtainDate { get; set; }

    /// <summary>
    /// 职称证书编号
    /// </summary>
    [JsonProperty("titleCertificateNumber")]
    public string? TitleCertificateNumber { get; set; }

    /// <summary>
    /// 是否最高职称
    /// </summary>
    [JsonProperty("isHighestTitle")]
    public string? IsHighestTitle { get; set; }

    /// <summary>
    /// 自定义字段信息
    /// </summary>
    [JsonProperty("customerFieldInfoList")]
    public List<CustomFieldInfo>? CustomerFieldInfoList { get; set; }
}

/// <summary>
/// 员工家庭成员
/// </summary>
public class CreateStaffFamilyMemberInfo
{
    /// <summary>
    /// 家庭成员关系
    /// </summary>
    [JsonProperty("relation")]
    public string? Relation { get; set; }

    /// <summary>
    /// 家庭成员姓名
    /// </summary>
    [JsonProperty("name")]
    public string? Name { get; set; }

    /// <summary>
    /// 家庭成员生日
    /// </summary>
    [JsonProperty("birthDate")]
    public string? BirthDate { get; set; }

    /// <summary>
    /// 现工作单位
    /// </summary>
    [JsonProperty("currentWorkCompany")]
    public string? CurrentWorkCompany { get; set; }

    /// <summary>
    /// 职位/岗位
    /// </summary>
    [JsonProperty("position")]
    public string? Position { get; set; }

    /// <summary>
    /// 家庭成员电话
    /// </summary>
    [JsonProperty("contactNumber")]
    public string? ContactNumber { get; set; }

    /// <summary>
    /// 自定义字段信息
    /// </summary>
    [JsonProperty("customerFieldInfoList")]
    public List<CustomFieldInfo>? CustomerFieldInfoList { get; set; }
}

/// <summary>
/// 员工兼任信息
/// </summary>
public class CreateStaffAdjunctInfo
{
    /// <summary>
    /// 兼任部门
    /// </summary>
    [JsonProperty("adjunctOrgSeq")]
    public string? AdjunctOrgSeq { get; set; }
}

/// <summary>
/// 附件信息
/// </summary>
public class AttachmentInfo
{
    /// <summary>
    /// 附件字段key
    /// </summary>
    [JsonProperty("attachmentFieldKey")]
    public string? AttachmentFieldKey { get; set; }

    /// <summary>
    /// 内部序号
    /// </summary>
    [JsonProperty("innerSeq")]
    public string? InnerSeq { get; set; }

    /// <summary>
    /// 附件文件id
    /// </summary>
    [JsonProperty("attachmentId")]
    public string? AttachmentId { get; set; }

    /// <summary>
    /// 附件文件名称
    /// </summary>
    [JsonProperty("attachmentName")]
    public string? AttachmentName { get; set; }

    /// <summary>
    /// 业务流水号
    /// </summary>
    [JsonProperty("businessNumber")]
    public string? BusinessNumber { get; set; }
}

/// <summary>
/// 员工合同信息
/// </summary>
public class CreateStaffContractInfo
{
    /// <summary>
    /// 合同公司
    /// </summary>
    [JsonProperty("contractCompany")]
    public string? ContractCompany { get; set; }

    /// <summary>
    /// 合同编号
    /// </summary>
    [JsonProperty("contractNumber")]
    public string? ContractNumber { get; set; }

    /// <summary>
    /// 合同类型
    /// </summary>
    [JsonProperty("contractType")]
    public string? ContractType { get; set; }

    /// <summary>
    /// 合同期限
    /// </summary>
    [JsonProperty("contractDuration")]
    public string? ContractDuration { get; set; }

    /// <summary>
    /// 合同起始日
    /// </summary>
    [JsonProperty("contractBeginDate")]
    public string? ContractBeginDate { get; set; }

    /// <summary>
    /// 合同到期日
    /// </summary>
    [JsonProperty("contractEndDate")]
    public string? ContractEndDate { get; set; }

    /// <summary>
    /// 自定义字段信息
    /// </summary>
    [JsonProperty("customerFieldInfoList")]
    public List<CustomFieldInfo>? CustomerFieldInfoList { get; set; }

    /// <summary>
    /// 附件信息
    /// </summary>
    [JsonProperty("attachmentInfoList")]
    public List<AttachmentInfo>? AttachmentInfoList { get; set; }
}

/// <summary>
/// 创建员工请求项
/// </summary>
public class CreateStaffRequestItem
{
    [JsonProperty("staffBasicInfo")]
    public CreateStaffBasicInfo? StaffBasicInfo { get; set; }

    [JsonProperty("staffHrmInfo")]
    public CreateStaffHrmInfo? StaffHrmInfo { get; set; }

    [JsonProperty("staffWagesAndSocialSecurityInfo")]
    public CreateStaffWagesAndSocialSecurityInfo? StaffWagesAndSocialSecurityInfo { get; set; }

    [JsonProperty("staffEmergencyContact")]
    public CreateStaffEmergencyContact? StaffEmergencyContact { get; set; }

    [JsonProperty("staffAwardInfoList")]
    public List<CreateStaffAwardInfo>? StaffAwardInfoList { get; set; }

    [JsonProperty("staffWorkInfoList")]
    public List<CreateStaffWorkInfo>? StaffWorkInfoList { get; set; }

    [JsonProperty("staffEducationInfoList")]
    public List<CreateStaffEducationInfo>? StaffEducationInfoList { get; set; }

    [JsonProperty("staffCertificateInfoList")]
    public List<CreateStaffCertificateInfo>? StaffCertificateInfoList { get; set; }

    [JsonProperty("staffJobTitleInfoList")]
    public List<CreateStaffJobTitleInfo>? StaffJobTitleInfoList { get; set; }

    [JsonProperty("staffFamilyMemberInfoList")]
    public List<CreateStaffFamilyMemberInfo>? StaffFamilyMemberInfoList { get; set; }

    [JsonProperty("staffAdjunctInfoList")]
    public List<CreateStaffAdjunctInfo>? StaffAdjunctInfoList { get; set; }

    [JsonProperty("customerFieldInfoList")]
    public List<CustomFieldInfo>? CustomerFieldInfoList { get; set; }

    [JsonProperty("staffAttachmentInfoList")]
    public List<AttachmentInfo>? StaffAttachmentInfoList { get; set; }

    [JsonProperty("staffContractInfoList")]
    public List<CreateStaffContractInfo>? StaffContractInfoList { get; set; }
}

/// <summary>
/// 创建员工请求
/// </summary>
public class CreateStaffRequest : List<CreateStaffRequestItem>
{
}