using System;
using Ss.CmbXft.Domain.Base;

namespace Ss.CmbXft.Domain.Entities;

/// <summary>
/// 员工信息实体（从薪福通同步的完整信息）
/// </summary>
public class StaffInfo : AllEntityBase<long>
{
    /// <summary>
    /// 员工序号
    /// </summary>
    public string StfSeq { get; set; } = string.Empty;

    /// <summary>
    /// 员工号
    /// </summary>
    public string? StfNbr { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    public string? StfNam { get; set; }

    /// <summary>
    /// 中文名
    /// </summary>
    public string? ChnNam { get; set; }

    /// <summary>
    /// 曾用名
    /// </summary>
    public string? UsdNam { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    public string? StfSex { get; set; }

    /// <summary>
    /// 性别(用户版本)
    /// </summary>
    public string? Gender { get; set; }

    /// <summary>
    /// 出生日期
    /// </summary>
    public DateTime? BirDat { get; set; }

    /// <summary>
    /// 证件类型
    /// </summary>
    public string? CerTyp { get; set; }

    /// <summary>
    /// 证件类型(用户版本)
    /// </summary>
    public string? CertType { get; set; }

    /// <summary>
    /// 证件号码
    /// </summary>
    public string? CerNbr { get; set; }

    /// <summary>
    /// 证件到期日期
    /// </summary>
    public DateTime? CerVal { get; set; }

    /// <summary>
    /// 证件长期是否有效
    /// </summary>
    public string? CerIsL { get; set; }

    /// <summary>
    /// 身份证地址
    /// </summary>
    public string? CerLoc { get; set; }

    /// <summary>
    /// 其他证件类型
    /// </summary>
    public string? OcrTyp { get; set; }

    /// <summary>
    /// 其他证件类型(用户版本)
    /// </summary>
    public string? OthCerType { get; set; }

    /// <summary>
    /// 其他证件号
    /// </summary>
    public string? OcrNbr { get; set; }

    /// <summary>
    /// 民族
    /// </summary>
    public string? Nation { get; set; }

    /// <summary>
    /// 国籍/地区
    /// </summary>
    public string? NatnLt { get; set; }

    /// <summary>
    /// 出生地
    /// </summary>
    public string? BirLad { get; set; }

    /// <summary>
    /// 户籍所在地省
    /// </summary>
    public string? HomPrv { get; set; }

    /// <summary>
    /// 户籍所在地城市
    /// </summary>
    public string? HomCty { get; set; }

    /// <summary>
    /// 户籍所在地区/县
    /// </summary>
    public string? HomAre { get; set; }

    /// <summary>
    /// 户籍所在地详细地址
    /// </summary>
    public string? HomDtl { get; set; }

    /// <summary>
    /// 户口性质
    /// </summary>
    public string? HomTyp { get; set; }

    /// <summary>
    /// 现住址省
    /// </summary>
    public string? UslPrv { get; set; }

    /// <summary>
    /// 现住址市
    /// </summary>
    public string? UslCty { get; set; }

    /// <summary>
    /// 现住址区/县
    /// </summary>
    public string? UslDst { get; set; }

    /// <summary>
    /// 现住址详细地址
    /// </summary>
    public string? Addres { get; set; }

    /// <summary>
    /// 联系地省
    /// </summary>
    public string? LnkPrv { get; set; }

    /// <summary>
    /// 联系地城市
    /// </summary>
    public string? LnkCty { get; set; }

    /// <summary>
    /// 联系地址区/县
    /// </summary>
    public string? LnkDst { get; set; }

    /// <summary>
    /// 联系地详细地址
    /// </summary>
    public string? LnkAdr { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    public string? MobNbr { get; set; }

    /// <summary>
    /// 座机号
    /// </summary>
    public string? TelNbr { get; set; }

    /// <summary>
    /// 个人邮箱
    /// </summary>
    public string? SelEml { get; set; }

    /// <summary>
    /// 工作邮箱
    /// </summary>
    public string? WrkEml { get; set; }

    /// <summary>
    /// 是否已婚
    /// </summary>
    public string? MarSts { get; set; }

    /// <summary>
    /// 是否已育
    /// </summary>
    public string? AfeSts { get; set; }

    /// <summary>
    /// 政治面貌
    /// </summary>
    public string? PltSts { get; set; }

    /// <summary>
    /// 是否烈属
    /// </summary>
    public string? MtySts { get; set; }

    /// <summary>
    /// 烈属证号
    /// </summary>
    public string? MtyNbr { get; set; }

    /// <summary>
    /// 是否残疾
    /// </summary>
    public string? DibSts { get; set; }

    /// <summary>
    /// 残疾证号
    /// </summary>
    public string? DibNbr { get; set; }

    /// <summary>
    /// 个税最高学历
    /// </summary>
    public string? HigSch { get; set; }

    /// <summary>
    /// 入职日期
    /// </summary>
    public DateTime? EntDat { get; set; }

    /// <summary>
    /// 首次工作日期
    /// </summary>
    public DateTime? BgnDat { get; set; }

    /// <summary>
    /// 司龄开始日期
    /// </summary>
    public DateTime? CcsDat { get; set; }

    /// <summary>
    /// 计划转正日期
    /// </summary>
    public DateTime? CorpLd { get; set; }

    /// <summary>
    /// 实际转正日期
    /// </summary>
    public DateTime? CorDat { get; set; }

    /// <summary>
    /// 计划试用期
    /// </summary>
    public string? CorpLp { get; set; }

    /// <summary>
    /// 实际试用期
    /// </summary>
    public string? PbtPrd { get; set; }

    /// <summary>
    /// 转正类型
    /// </summary>
    public string? CorFlg { get; set; }

    /// <summary>
    /// 转正备注
    /// </summary>
    public string? CorMak { get; set; }

    /// <summary>
    /// 转正评价
    /// </summary>
    public string? CorMsg { get; set; }

    /// <summary>
    /// 转正述职内容
    /// </summary>
    public string? CorRep { get; set; }

    /// <summary>
    /// 员工类型
    /// </summary>
    public string? StfTyp { get; set; }

    /// <summary>
    /// 员工状态
    /// </summary>
    public string? StfSts { get; set; }

    /// <summary>
    /// 部门
    /// </summary>
    public string? OrgSeq { get; set; }

    /// <summary>
    /// 部门名称
    /// </summary>
    public string? OrgNam { get; set; }

    /// <summary>
    /// 工作所属公司
    /// </summary>
    public string? EmpCrp { get; set; }

    /// <summary>
    /// 岗位id
    /// </summary>
    public string? PosUid { get; set; }

    /// <summary>
    /// 岗位code
    /// </summary>
    public string? PosSeq { get; set; }

    /// <summary>
    /// 岗位名称
    /// </summary>
    public string? PosNam { get; set; }

    /// <summary>
    /// 职位id
    /// </summary>
    public string? JobUid { get; set; }

    /// <summary>
    /// 职位编码
    /// </summary>
    public string? JobSeq { get; set; }

    /// <summary>
    /// 职位名称
    /// </summary>
    public string? JobNam { get; set; }

    /// <summary>
    /// 职级
    /// </summary>
    public string? JobRnk { get; set; }

    /// <summary>
    /// 工作地
    /// </summary>
    public string? WkpSeq { get; set; }

    /// <summary>
    /// 汇报上级
    /// </summary>
    public string? RepSup { get; set; }

    /// <summary>
    /// 业务分组
    /// </summary>
    public string? GrpSeq { get; set; }

    /// <summary>
    /// 开户行
    /// </summary>
    public string? BnkTyp { get; set; }

    /// <summary>
    /// 开户地省份
    /// </summary>
    public string? BnkPrv { get; set; }

    /// <summary>
    /// 开户地市
    /// </summary>
    public string? BnkCty { get; set; }

    /// <summary>
    /// 工资卡号
    /// </summary>
    public string? SalCar { get; set; }

    /// <summary>
    /// 数币钱包ID
    /// </summary>
    public string? DigWet { get; set; }

    /// <summary>
    /// 个人社保账号
    /// </summary>
    public string? SecAct { get; set; }

    /// <summary>
    /// 个人公积金账号
    /// </summary>
    public string? FunAct { get; set; }

    /// <summary>
    /// 成本分摊比例
    /// </summary>
    public double? CstRat { get; set; }

    /// <summary>
    /// 工龄扣减期
    /// </summary>
    public string? WrkDdt { get; set; }

    /// <summary>
    /// 司龄扣减期
    /// </summary>
    public string? CcsDdt { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 企业号
    /// </summary>
    public string? PrjCod { get; set; }

    /// <summary>
    /// 运营机构
    /// </summary>
    public string? OprAgn { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? XftUpdateTime { get; set; }

    /// <summary>
    /// 离职单据号
    /// </summary>
    public string? QuitTrsSeq { get; set; }

    /// <summary>
    /// 离职操作类型
    /// LAUNCH 办理
    /// APPROVE_END 审批结束
    /// COF 确认离职
    /// EFFECTIVE 生效
    /// CANCEL 取消
    /// </summary>
    public string? QuitActionType { get; set; }

    /// <summary>
    /// 离职审批状态
    /// WAT 未发起
    /// ING 审核中
    /// PAS 审核通过
    /// RVK 审核撤销
    /// REJ 审核否决
    /// NPM 没有审批
    /// </summary>
    public string? QuitApproveStatus { get; set; }

    /// <summary>
    /// 离职事件更新时间
    /// </summary>
    public DateTime? QuitUpdateTime { get; set; }
}
