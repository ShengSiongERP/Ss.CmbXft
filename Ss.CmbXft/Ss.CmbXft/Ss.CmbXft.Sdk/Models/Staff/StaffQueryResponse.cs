using System;
using System.Collections.Generic;

namespace Ss.CmbXft.Sdk.Models.Staff;

/// <summary>
/// 员工信息查询响应Body
/// </summary>
public class StaffQueryResponseBody
{
    /// <summary>
    /// 当前页
    /// </summary>
    public int? CurrentPage { get; set; }

    /// <summary>
    /// 每页查询数量
    /// </summary>
    public int? PageSize { get; set; }

    /// <summary>
    /// 数据总量
    /// </summary>
    public int? TotalSize { get; set; }

    /// <summary>
    /// 查询结果
    /// </summary>
    public List<StaffInfo> Records { get; set; } = new();
}

/// <summary>
/// 员工信息
/// </summary>
public class StaffInfo
{
    /// <summary>
    /// 员工序号
    /// </summary>
    public string? StaffSeq { get; set; }

    /// <summary>
    /// 基本信息、个人信息
    /// </summary>
    public StaffBasicInfo? StaffBasicInfo { get; set; }

    /// <summary>
    /// 在职分组、离职分组
    /// </summary>
    public StaffHrmInfo? StaffHrmInfo { get; set; }

    /// <summary>
    /// 工资社保
    /// </summary>
    public StaffWagesAndSocialSecurityInfo? StaffWagesAndSocialSecurityInfo { get; set; }

    /// <summary>
    /// 紧急联系人
    /// </summary>
    public StaffEmergencyContact? StaffEmergencyContact { get; set; }

    /// <summary>
    /// 集团服务信息
    /// </summary>
    public StaffGroupServiceInfo? StaffGroupServiceInfo { get; set; }

    /// <summary>
    /// 个税基础信息
    /// </summary>
    public StaffTaxBasicInfo? StaffTaxBasicInfo { get; set; }

    /// <summary>
    /// 奖惩记录
    /// </summary>
    public List<StaffAwardInfo>? StaffAwardInfoList { get; set; }

    /// <summary>
    /// 工作经历
    /// </summary>
    public List<StaffWorkInfo>? StaffWorkInfoList { get; set; }

    /// <summary>
    /// 教育经历
    /// </summary>
    public List<StaffEducationInfo>? StaffEducationInfoList { get; set; }

    /// <summary>
    /// 专业证书
    /// </summary>
    public List<StaffCertificateInfo>? StaffCertificateInfoList { get; set; }

    /// <summary>
    /// 职称
    /// </summary>
    public List<StaffJobTitleInfo>? StaffJobTitleInfoList { get; set; }

    /// <summary>
    /// 家庭成员
    /// </summary>
    public List<StaffFamilyMemberInfo>? StaffFamilyMemberInfoList { get; set; }

    /// <summary>
    /// 兼任信息
    /// </summary>
    public List<StaffAdjunctInfo>? StaffAdjunctInfoList { get; set; }

    /// <summary>
    /// 个人材料
    /// </summary>
    public List<StaffAttachmentInfo>? StaffAttachmentInfoList { get; set; }

    /// <summary>
    /// 合同信息
    /// </summary>
    public List<StaffContractInfo>? StaffContractInfoList { get; set; }

    /// <summary>
    /// 个税申报信息
    /// </summary>
    public List<StaffTaxDeclarationInfo>? StaffTaxDeclarationInfoList { get; set; }

    /// <summary>
    /// 培训记录
    /// </summary>
    public List<StaffTrainRecordInfo>? StaffTrainRecordInfoList { get; set; }

    /// <summary>
    /// 绩效考核
    /// </summary>
    public List<StaffPerformanceInfo>? StaffPerformanceInfoList { get; set; }

    /// <summary>
    /// 自定义字段信息
    /// </summary>
    public List<CustomerFieldInfo>? CustomerFieldInfoList { get; set; }
}

/// <summary>
/// 基本信息、个人信息
/// </summary>
public class StaffBasicInfo
{
    /// <summary>
    /// 企业号
    /// </summary>
    public string? EnterpriseId { get; set; }

    /// <summary>
    /// 员工序号
    /// </summary>
    public string? StfSeq { get; set; }

    /// <summary>
    /// 员工类型
    /// </summary>
    public string? StfType { get; set; }

    /// <summary>
    /// 员工状态 0试用1正式2已离职3待离职
    /// </summary>
    public string? StfStatus { get; set; }

    /// <summary>
    /// 员工姓名
    /// </summary>
    public string? StfName { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    public string? MobileNumber { get; set; }

    /// <summary>
    /// 证件类型
    /// </summary>
    public string? CertificateType { get; set; }

    /// <summary>
    /// 证件号码
    /// </summary>
    public string? CertificateNumber { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    public string? Sex { get; set; }

    /// <summary>
    /// 国籍/地区
    /// </summary>
    public string? Nationality { get; set; }

    /// <summary>
    /// 部门
    /// </summary>
    public string? OrgSeq { get; set; }

    /// <summary>
    /// 岗位编号
    /// </summary>
    public string? PosCode { get; set; }

    /// <summary>
    /// 职位编号
    /// </summary>
    public string? JobCode { get; set; }

    /// <summary>
    /// 职级
    /// </summary>
    public string? JobRankSeq { get; set; }

    /// <summary>
    /// 员工号
    /// </summary>
    public string? StfNumber { get; set; }

    /// <summary>
    /// 座机
    /// </summary>
    public string? TelephoneNumber { get; set; }

    /// <summary>
    /// 工作邮箱
    /// </summary>
    public string? WorkEmail { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 出生日期
    /// </summary>
    public DateTime? Birthday { get; set; }

    /// <summary>
    /// 婚姻状态
    /// </summary>
    public string? HasMarried { get; set; }

    /// <summary>
    /// 是否已育
    /// </summary>
    public string? HasNurtured { get; set; }

    /// <summary>
    /// 民族
    /// </summary>
    public string? Nation { get; set; }

    /// <summary>
    /// 政治面貌
    /// </summary>
    public string? PoliticalAppearance { get; set; }

    /// <summary>
    /// 现住址省
    /// </summary>
    public string? PresentAddressProvince { get; set; }

    /// <summary>
    /// 现住址城市
    /// </summary>
    public string? PresentAddressCity { get; set; }

    /// <summary>
    /// 现住址区/县
    /// </summary>
    public string? PresentAddressDistrict { get; set; }

    /// <summary>
    /// 现住址详细地址
    /// </summary>
    public string? PresentAddressDetail { get; set; }

    /// <summary>
    /// 户籍省
    /// </summary>
    public string? HouseholdAddressProvince { get; set; }

    /// <summary>
    /// 户籍城市
    /// </summary>
    public string? HouseholdAddressCity { get; set; }

    /// <summary>
    /// 户籍区/县
    /// </summary>
    public string? HouseholdAddressDistrict { get; set; }

    /// <summary>
    /// 户籍详细地址
    /// </summary>
    public string? HouseholdAddressDetail { get; set; }

    /// <summary>
    /// 户口性质
    /// </summary>
    public string? HouseholdType { get; set; }

    /// <summary>
    /// 身份证地址
    /// </summary>
    public string? CertificateAddress { get; set; }

    /// <summary>
    /// 证件是否长期有效
    /// </summary>
    public string? IsCertificateLongTermEffective { get; set; }

    /// <summary>
    /// 证件签发日期
    /// </summary>
    public DateTime? CertificateValidBeginDate { get; set; }

    /// <summary>
    /// 证件到期日期
    /// </summary>
    public DateTime? CertificateValidEndDate { get; set; }

    /// <summary>
    /// 个人邮箱
    /// </summary>
    public string? IndividualEmail { get; set; }

    /// <summary>
    /// 首次工作日期
    /// </summary>
    public DateTime? FirstWorkDate { get; set; }

    /// <summary>
    /// 工龄扣减期
    /// </summary>
    public int? WorkYearDeduction { get; set; }

    /// <summary>
    /// 工作地
    /// </summary>
    public string? WorkplaceLocationSeq { get; set; }

    /// <summary>
    /// 联系地址省
    /// </summary>
    public string? ContactAddressProvince { get; set; }

    /// <summary>
    /// 联系地址城市
    /// </summary>
    public string? ContactAddressCity { get; set; }

    /// <summary>
    /// 联系地址区/县
    /// </summary>
    public string? ContactAddressDistrict { get; set; }

    /// <summary>
    /// 联系地址详细地址
    /// </summary>
    public string? ContactAddressDetail { get; set; }

    /// <summary>
    /// 业务分组
    /// </summary>
    public string? BusinessGroupSeq { get; set; }

    /// <summary>
    /// 曾用名
    /// </summary>
    public string? FormerName { get; set; }

    /// <summary>
    /// 汇报上级
    /// </summary>
    public string? ReportSuperiorStfSeq { get; set; }

    /// <summary>
    /// 岗位id
    /// </summary>
    public string? PosId { get; set; }

    /// <summary>
    /// 职位id
    /// </summary>
    public string? JobId { get; set; }

    /// <summary>
    /// 成本中心
    /// </summary>
    public string? CostCenter { get; set; }

    /// <summary>
    /// 成本分摊比例
    /// </summary>
    public decimal? CostAllocationRatio { get; set; }

    /// <summary>
    /// 工作所属公司
    /// </summary>
    public string? EmployerCorporation { get; set; }

    /// <summary>
    /// 自定义字段信息
    /// </summary>
    public List<CustomerFieldInfo>? CustomerFieldInfoList { get; set; }
}

/// <summary>
/// 在职分组、离职分组
/// </summary>
public class StaffHrmInfo
{
    /// <summary>
    /// 入职日期
    /// </summary>
    public DateTime? EntryDate { get; set; }

    /// <summary>
    /// 实际试用期
    /// </summary>
    public string? ProbationPeriod { get; set; }

    /// <summary>
    /// 计划转正日期
    /// </summary>
    public DateTime? PlanPositiveDate { get; set; }

    /// <summary>
    /// 在职分组
    /// </summary>
    public string? OnJobGroupSeq { get; set; }

    /// <summary>
    /// 离职分组
    /// </summary>
    public string? LeaveJobGroupSeq { get; set; }

    /// <summary>
    /// 实际试用期
    /// </summary>
    public string? ActutalProbationPeriod { get; set; }

    /// <summary>
    /// 实际转正日期
    /// </summary>
    public DateTime? ActualPositiveDate { get; set; }

    /// <summary>
    /// 实际离职日期
    /// </summary>
    public DateTime? ActualQuitDate { get; set; }

    /// <summary>
    /// 离职类型
    /// </summary>
    public string? QuitType { get; set; }

    /// <summary>
    /// 离职原因
    /// </summary>
    public string? QuitReason { get; set; }

    /// <summary>
    /// 离职申请日期
    /// </summary>
    public DateTime? ApplyQuitDate { get; set; }

    /// <summary>
    /// 计划离职日期
    /// </summary>
    public DateTime? PlanQuitDate { get; set; }

    /// <summary>
    /// 薪资结算日期
    /// </summary>
    public DateTime? SalarySettleDate { get; set; }

    /// <summary>
    /// 离职备注
    /// </summary>
    public string? QuitRemark { get; set; }

    /// <summary>
    /// 是否加入黑名单
    /// </summary>
    public string? InBlackList { get; set; }

    /// <summary>
    /// 加入黑名单备注
    /// </summary>
    public string? ReasonForBlackList { get; set; }

    /// <summary>
    /// 申请转正日期
    /// </summary>
    public DateTime? ApplyPositiveDate { get; set; }

    /// <summary>
    /// 计划试用期
    /// </summary>
    public string? PlanProbationPeriod { get; set; }

    /// <summary>
    /// 司龄开始日期
    /// </summary>
    public DateTime? SeniorityBeginDate { get; set; }

    /// <summary>
    /// 司龄扣减期
    /// </summary>
    public int? SeniorityDeduction { get; set; }

    /// <summary>
    /// 是否重入职
    /// </summary>
    public string? ReEntry { get; set; }

    /// <summary>
    /// 员工记录序号
    /// </summary>
    public string? StaffRecordOrder { get; set; }

    /// <summary>
    /// 自定义字段信息
    /// </summary>
    public List<CustomerFieldInfo>? CustomerFieldInfoList { get; set; }
}

/// <summary>
/// 工资社保
/// </summary>
public class StaffWagesAndSocialSecurityInfo
{
    /// <summary>
    /// 工资卡号
    /// </summary>
    public string? BankCardAccount { get; set; }

    /// <summary>
    /// 开户行
    /// </summary>
    public string? BankName { get; set; }

    /// <summary>
    /// 开户行省
    /// </summary>
    public string? BankOfProvince { get; set; }

    /// <summary>
    /// 开户行市
    /// </summary>
    public string? BankOfCity { get; set; }

    /// <summary>
    /// 员工序号
    /// </summary>
    public string? StfSeq { get; set; }

    /// <summary>
    /// 企业号
    /// </summary>
    public string? EnterpriseId { get; set; }

    /// <summary>
    /// 数币钱包ID
    /// </summary>
    public string? DigitalWallet { get; set; }

    /// <summary>
    /// 运营机构
    /// </summary>
    public string? DigitalBank { get; set; }

    /// <summary>
    /// 个人社保账号
    /// </summary>
    public string? SocialSecurityAccount { get; set; }

    /// <summary>
    /// 个人公积金账号
    /// </summary>
    public string? AccumulationFundAccount { get; set; }

    /// <summary>
    /// 工资卡流水号
    /// </summary>
    public string? TradeSeq { get; set; }

    /// <summary>
    /// 数币id流水号
    /// </summary>
    public string? DigitalTradeSeq { get; set; }

    /// <summary>
    /// 自定义字段信息
    /// </summary>
    public List<CustomerFieldInfo>? CustomerFieldInfoList { get; set; }
}

/// <summary>
/// 紧急联系人
/// </summary>
public class StaffEmergencyContact
{
    /// <summary>
    /// 紧急联系人
    /// </summary>
    public string? ContactName { get; set; }

    /// <summary>
    /// 紧急联系人电话
    /// </summary>
    public string? ContactTelephoneNumber { get; set; }

    /// <summary>
    /// 自定义字段信息
    /// </summary>
    public List<CustomerFieldInfo>? CustomerFieldInfoList { get; set; }
}

/// <summary>
/// 集团服务信息
/// </summary>
public class StaffGroupServiceInfo
{
    /// <summary>
    /// 企业号
    /// </summary>
    public string? EnterpriseId { get; set; }

    /// <summary>
    /// 员工序号
    /// </summary>
    public string? StfSeq { get; set; }

    /// <summary>
    /// 集团入职日期
    /// </summary>
    public DateTime? GroupJoinDate { get; set; }

    /// <summary>
    /// 集团司龄扣减期
    /// </summary>
    public int? GroupServiceTimeDeduction { get; set; }

    /// <summary>
    /// 自定义字段信息
    /// </summary>
    public List<CustomerFieldInfo>? CustomerFieldInfoList { get; set; }
}

/// <summary>
/// 个税基础信息
/// </summary>
public class StaffTaxBasicInfo
{
    /// <summary>
    /// 其他证件类型
    /// </summary>
    public string? OtherCertificateType { get; set; }

    /// <summary>
    /// 其他证件号
    /// </summary>
    public string? OtherCertificateNumber { get; set; }

    /// <summary>
    /// 出生地
    /// </summary>
    public string? BirthLand { get; set; }

    /// <summary>
    /// 中文名
    /// </summary>
    public string? ChineseName { get; set; }

    /// <summary>
    /// 首次入境日期
    /// </summary>
    public DateTime? FirstInboundDate { get; set; }

    /// <summary>
    /// 预计离境日期
    /// </summary>
    public DateTime? PlanOutboundDate { get; set; }

    /// <summary>
    /// 最高学历
    /// </summary>
    public string? HighestDegree { get; set; }

    /// <summary>
    /// 是否残疾
    /// </summary>
    public string? IsDisabled { get; set; }

    /// <summary>
    /// 是否烈属
    /// </summary>
    public string? IsMartyr { get; set; }

    /// <summary>
    /// 是否孤老
    /// </summary>
    public string? IsLonelyAndOld { get; set; }

    /// <summary>
    /// 残疾证号
    /// </summary>
    public string? DisabilityCertificateNumber { get; set; }

    /// <summary>
    /// 烈属郑号
    /// </summary>
    public string? MartyrCertificateNumber { get; set; }

    /// <summary>
    /// 员工序号
    /// </summary>
    public string? StfSeq { get; set; }

    /// <summary>
    /// 企业号
    /// </summary>
    public string? EnterpriseId { get; set; }
}

/// <summary>
/// 自定义字段信息
/// </summary>
public class CustomerFieldInfo
{
    /// <summary>
    /// 企业号
    /// </summary>
    public string? EnterpriseId { get; set; }

    /// <summary>
    /// 员工序号
    /// </summary>
    public string? StfSeq { get; set; }

    /// <summary>
    /// 所属分组
    /// </summary>
    public string? ClassKey { get; set; }

    /// <summary>
    /// 自定义字段id
    /// </summary>
    public string? FieldKey { get; set; }

    /// <summary>
    /// 字段值
    /// </summary>
    public string? FieldValue { get; set; }
}

/// <summary>
/// 奖惩记录
/// </summary>
public class StaffAwardInfo
{
    /// <summary>
    /// 流水号
    /// </summary>
    public string? TradeSeq { get; set; }

    /// <summary>
    /// 奖惩事项
    /// </summary>
    public string? RewardAndPunishmentMatters { get; set; }

    /// <summary>
    /// 奖惩时间
    /// </summary>
    public DateTime? RewardAndPunishmentDate { get; set; }

    /// <summary>
    /// 奖惩单位
    /// </summary>
    public string? RewardAndPunishmentCompany { get; set; }

    /// <summary>
    /// 奖惩详情
    /// </summary>
    public string? RewardAndPunishmentDetail { get; set; }

    /// <summary>
    /// 企业号
    /// </summary>
    public string? EnterpriseId { get; set; }

    /// <summary>
    /// 员工序号
    /// </summary>
    public string? StfSeq { get; set; }

    /// <summary>
    /// 自定义字段
    /// </summary>
    public List<CustomerFieldInfo>? CustomerFieldInfoList { get; set; }
}

/// <summary>
/// 工作经历
/// </summary>
public class StaffWorkInfo
{
    /// <summary>
    /// 企业号
    /// </summary>
    public string? EnterpriseId { get; set; }

    /// <summary>
    /// 员工序号
    /// </summary>
    public string? StfSeq { get; set; }

    /// <summary>
    /// 流水号
    /// </summary>
    public string? TradeSeq { get; set; }

    /// <summary>
    /// 工作单位
    /// </summary>
    public string? LastCompanyName { get; set; }

    /// <summary>
    /// 曾任岗位
    /// </summary>
    public string? LastPositionName { get; set; }

    /// <summary>
    /// 工作开始日期
    /// </summary>
    public DateTime? WorkBeginDate { get; set; }

    /// <summary>
    /// 工作结束日期
    /// </summary>
    public DateTime? WorkEndDate { get; set; }

    /// <summary>
    /// 证明人
    /// </summary>
    public string? ProverName { get; set; }

    /// <summary>
    /// 证明人联系电话
    /// </summary>
    public string? ProverTelephoneNumber { get; set; }

    /// <summary>
    /// 过往离职原因
    /// </summary>
    public string? LeaveReason { get; set; }

    /// <summary>
    /// 离职薪资
    /// </summary>
    public string? LeaveSalary { get; set; }

    /// <summary>
    /// 上家工作经历标志
    /// </summary>
    public string? CurrentFlag { get; set; }

    /// <summary>
    /// 自定义字段信息
    /// </summary>
    public List<CustomerFieldInfo>? CustomerFieldInfoList { get; set; }
}

/// <summary>
/// 教育经历
/// </summary>
public class StaffEducationInfo
{
    /// <summary>
    /// 企业号
    /// </summary>
    public string? EnterpriseId { get; set; }

    /// <summary>
    /// 员工序号
    /// </summary>
    public string? StfSeq { get; set; }

    /// <summary>
    /// 流水号
    /// </summary>
    public string? TradeSeq { get; set; }

    /// <summary>
    /// 毕业院校
    /// </summary>
    public string? GraduateSchool { get; set; }

    /// <summary>
    /// 学历
    /// </summary>
    public string? Degree { get; set; }

    /// <summary>
    /// 专业
    /// </summary>
    public string? Specialty { get; set; }

    /// <summary>
    /// 毕业日期
    /// </summary>
    public DateTime? GraduateDate { get; set; }

    /// <summary>
    /// 自定义字段信息
    /// </summary>
    public List<CustomerFieldInfo>? CustomerFieldInfoList { get; set; }

    /// <summary>
    /// 最高学历标志
    /// </summary>
    public string? CurrentFlag { get; set; }
}

/// <summary>
/// 专业证书
/// </summary>
public class StaffCertificateInfo
{
    /// <summary>
    /// 企业号
    /// </summary>
    public string? EnterpriseId { get; set; }

    /// <summary>
    /// 员工序号
    /// </summary>
    public string? StfSeq { get; set; }

    /// <summary>
    /// 流水号
    /// </summary>
    public string? TradeSeq { get; set; }

    /// <summary>
    /// 证书名称
    /// </summary>
    public string? CertificateName { get; set; }

    /// <summary>
    /// 证书机构
    /// </summary>
    public string? IssuingAuthority { get; set; }

    /// <summary>
    /// 发证日期
    /// </summary>
    public DateTime? IssuingDate { get; set; }

    /// <summary>
    /// 有效期起始日
    /// </summary>
    public DateTime? ValidBeginDate { get; set; }

    /// <summary>
    /// 有效期到期日
    /// </summary>
    public DateTime? ValidEndDate { get; set; }

    /// <summary>
    /// 自定义字段信息
    /// </summary>
    public List<CustomerFieldInfo>? CustomerFieldInfoList { get; set; }
}

/// <summary>
/// 职称
/// </summary>
public class StaffJobTitleInfo
{
    /// <summary>
    /// 流水号
    /// </summary>
    public string? TradeSeq { get; set; }

    /// <summary>
    /// 职称名称
    /// </summary>
    public string? TitleName { get; set; }

    /// <summary>
    /// 职称级别
    /// </summary>
    public string? TitleLevel { get; set; }

    /// <summary>
    /// 职称评定机构
    /// </summary>
    public string? AssessmentAuthority { get; set; }

    /// <summary>
    /// 职称获得日期
    /// </summary>
    public DateTime? ObtainDate { get; set; }

    /// <summary>
    /// 职称证书编号
    /// </summary>
    public string? TitleCertificateNumber { get; set; }

    /// <summary>
    /// 是否最高职称
    /// </summary>
    public string? IsHighestTitle { get; set; }

    /// <summary>
    /// 企业号
    /// </summary>
    public string? EnterpriseId { get; set; }

    /// <summary>
    /// 员工序号
    /// </summary>
    public string? StfSeq { get; set; }

    /// <summary>
    /// 自定义字段信息
    /// </summary>
    public List<CustomerFieldInfo>? CustomerFieldInfoList { get; set; }
}

/// <summary>
/// 家庭成员
/// </summary>
public class StaffFamilyMemberInfo
{
    /// <summary>
    /// 企业号
    /// </summary>
    public string? EnterpriseId { get; set; }

    /// <summary>
    /// 员工序号
    /// </summary>
    public string? StfSeq { get; set; }

    /// <summary>
    /// 流水号
    /// </summary>
    public string? TradeSeq { get; set; }

    /// <summary>
    /// 家庭成员关系
    /// </summary>
    public string? Relation { get; set; }

    /// <summary>
    /// 家庭成员姓名
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 家庭成员生日
    /// </summary>
    public DateTime? BirthDate { get; set; }

    /// <summary>
    /// 现工作单位
    /// </summary>
    public string? CurrentWorkCompany { get; set; }

    /// <summary>
    /// 职位/岗位
    /// </summary>
    public string? Position { get; set; }

    /// <summary>
    /// 家庭成员电话
    /// </summary>
    public string? ContactNumber { get; set; }

    /// <summary>
    /// 自定义字段信息
    /// </summary>
    public List<CustomerFieldInfo>? CustomerFieldInfoList { get; set; }
}

/// <summary>
/// 兼任信息
/// </summary>
public class StaffAdjunctInfo
{
    /// <summary>
    /// 流水号
    /// </summary>
    public string? TradeSeq { get; set; }

    /// <summary>
    /// 兼任部门
    /// </summary>
    public string? AdjunctOrgSeq { get; set; }

    /// <summary>
    /// 兼任岗位code
    /// </summary>
    public string? AdjunctPosCode { get; set; }

    /// <summary>
    /// 兼任岗位id
    /// </summary>
    public string? AdjunctPosId { get; set; }

    /// <summary>
    /// 员工序号
    /// </summary>
    public string? StfSeq { get; set; }

    /// <summary>
    /// 企业号
    /// </summary>
    public string? EnterpriseId { get; set; }

    /// <summary>
    /// 自定义分组字段信息
    /// </summary>
    public List<CustomerFieldInfo>? CustomerFieldInfoList { get; set; }
}

/// <summary>
/// 个人材料
/// </summary>
public class StaffAttachmentInfo
{
    /// <summary>
    /// 个人材料字段key
    /// </summary>
    public string? AttachmentFieldKey { get; set; }

    /// <summary>
    /// 内部序号
    /// </summary>
    public int? InnerSeq { get; set; }

    /// <summary>
    /// 个人材料文件id
    /// </summary>
    public string? AttachmentId { get; set; }

    /// <summary>
    /// 个人材料文件名称
    /// </summary>
    public string? AttachmentName { get; set; }

    /// <summary>
    /// 业务流水号
    /// </summary>
    public string? BusinessNumber { get; set; }

    /// <summary>
    /// 员工序号
    /// </summary>
    public string? StfSeq { get; set; }

    /// <summary>
    /// 企业号
    /// </summary>
    public string? EnterpriseId { get; set; }
}

/// <summary>
/// 合同信息
/// </summary>
public class StaffContractInfo
{
    /// <summary>
    /// 员工序号
    /// </summary>
    public string? StfSeq { get; set; }

    /// <summary>
    /// 企业号
    /// </summary>
    public string? EnterpriseId { get; set; }

    /// <summary>
    /// 流水号
    /// </summary>
    public string? TradeSeq { get; set; }

    /// <summary>
    /// 合同公司
    /// </summary>
    public string? ContractCompany { get; set; }

    /// <summary>
    /// 合同编号
    /// </summary>
    public string? ContractNumber { get; set; }

    /// <summary>
    /// 合同类型
    /// </summary>
    public string? ContractType { get; set; }

    /// <summary>
    /// 合同期限
    /// </summary>
    public string? ContractDuration { get; set; }

    /// <summary>
    /// 合同起始日
    /// </summary>
    public DateTime? ContractBeginDate { get; set; }

    /// <summary>
    /// 合同到期日
    /// </summary>
    public DateTime? ContractEndDate { get; set; }

    /// <summary>
    /// 合同状态
    /// </summary>
    public string? ContractState { get; set; }

    /// <summary>
    /// 当前合同标志
    /// </summary>
    public string? CurrentFlag { get; set; }

    /// <summary>
    /// 自定义字段信息
    /// </summary>
    public List<CustomerFieldInfo>? CustomerFieldInfoList { get; set; }
}

/// <summary>
/// 个税申报信息
/// </summary>
public class StaffTaxDeclarationInfo
{
    /// <summary>
    /// 税局登记序号
    /// </summary>
    public string? RegisterSequence { get; set; }

    /// <summary>
    /// 任职受雇从业类型
    /// </summary>
    public string? EmployedType { get; set; }

    /// <summary>
    /// 任职受雇从业日期
    /// </summary>
    public DateTime? EmployedDate { get; set; }

    /// <summary>
    /// 入职年度就业情形
    /// </summary>
    public string? EmploymentSituation { get; set; }

    /// <summary>
    /// 税局离职日期
    /// </summary>
    public DateTime? TaxOfficeQuitDate { get; set; }

    /// <summary>
    /// 职务
    /// </summary>
    public string? Duty { get; set; }

    /// <summary>
    /// 涉税事由
    /// </summary>
    public string? TaxRelatedReason { get; set; }

    /// <summary>
    /// 个人投资总额
    /// </summary>
    public decimal? TotalPersonalInvestment { get; set; }

    /// <summary>
    /// 个人投资比例
    /// </summary>
    public decimal? IndividualInvestmentPercentage { get; set; }

    /// <summary>
    /// 是否扣除减除费用
    /// </summary>
    public string? IsDeductionsDeducted { get; set; }

    /// <summary>
    /// 个税备注
    /// </summary>
    public string? TaxRemark { get; set; }

    /// <summary>
    /// 个税通知手机号
    /// </summary>
    public string? TaxMobileNumber { get; set; }
}

/// <summary>
/// 培训记录
/// </summary>
public class StaffTrainRecordInfo
{
    /// <summary>
    /// 课程名称
    /// </summary>
    public string? CourseName { get; set; }

    /// <summary>
    /// 课程编号
    /// </summary>
    public string? CourseNumber { get; set; }

    /// <summary>
    /// 培训类型
    /// </summary>
    public string? TrainType { get; set; }

    /// <summary>
    /// 培训方式
    /// </summary>
    public string? TrainWay { get; set; }

    /// <summary>
    /// 培训机构
    /// </summary>
    public string? TrainInstitution { get; set; }

    /// <summary>
    /// 讲师
    /// </summary>
    public string? Lecturer { get; set; }

    /// <summary>
    /// 培训开始日期
    /// </summary>
    public DateTime? TrainBegin { get; set; }

    /// <summary>
    /// 培训结束日期
    /// </summary>
    public DateTime? TrainEnd { get; set; }

    /// <summary>
    /// 培训地点
    /// </summary>
    public string? TrainLocation { get; set; }

    /// <summary>
    /// 考试时间
    /// </summary>
    public DateTime? ExamDate { get; set; }

    /// <summary>
    /// 考试结果
    /// </summary>
    public string? ExamResult { get; set; }

    /// <summary>
    /// 流水号
    /// </summary>
    public string? TradeSeq { get; set; }

    /// <summary>
    /// 自定义字段信息
    /// </summary>
    public List<CustomerFieldInfo>? CustomerFieldInfoList { get; set; }
}

/// <summary>
/// 绩效考核
/// </summary>
public class StaffPerformanceInfo
{
    /// <summary>
    /// 绩效考核周期
    /// </summary>
    public string? PerformanceCycle { get; set; }

    /// <summary>
    /// 绩效考核人
    /// </summary>
    public string? PerformanceEvaluator { get; set; }

    /// <summary>
    /// 绩效考核评分
    /// </summary>
    public string? PerformanceScore { get; set; }

    /// <summary>
    /// 绩效考核等级
    /// </summary>
    public string? PerformanceLevel { get; set; }

    /// <summary>
    /// 绩效考核评语
    /// </summary>
    public string? PerformanceComments { get; set; }

    /// <summary>
    /// 流水号
    /// </summary>
    public string? TradeSeq { get; set; }

    /// <summary>
    /// 自定义字段信息
    /// </summary>
    public List<CustomerFieldInfo>? CustomerFieldInfoList { get; set; }
}
