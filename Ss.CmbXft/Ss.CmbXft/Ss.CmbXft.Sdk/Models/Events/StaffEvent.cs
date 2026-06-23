using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ss.CmbXft.Sdk.Models.Events;

/// <summary>
/// 员工事件模型（新增/修改员工事件）
/// </summary>
public class StaffEvent
{
    /// <summary>
    /// 座机号
    /// </summary>
    [JsonProperty("TELNBR")]
    public string? TelNbr { get; set; }

    /// <summary>
    /// 其他证件类型
    /// </summary>
    [JsonProperty("OCRTYP")]
    public string? OcrTyp { get; set; }

    /// <summary>
    /// 是否已婚
    /// </summary>
    [JsonProperty("MARSTS")]
    public string? MarSts { get; set; }

    /// <summary>
    /// 户籍所在地区/县
    /// </summary>
    [JsonProperty("HOMARE")]
    public string? HomAre { get; set; }

    /// <summary>
    /// 工龄扣减期
    /// </summary>
    [JsonProperty("WRKDDT")]
    public string? WrkDdt { get; set; }

    /// <summary>
    /// 其他证件类型(用户版本)
    /// </summary>
    [JsonProperty("OTHCERTYPE")]
    public string? OthCerType { get; set; }

    /// <summary>
    /// 现住址市
    /// </summary>
    [JsonProperty("USLCTY")]
    public string? UslCty { get; set; }

    /// <summary>
    /// 操作用户
    /// </summary>
    [JsonProperty("OPRUSR")]
    public string? OprUsr { get; set; }

    /// <summary>
    /// 员工类型
    /// </summary>
    [JsonProperty("STFTYP")]
    public string? StfTyp { get; set; }

    /// <summary>
    /// 开户地市
    /// </summary>
    [JsonProperty("BNKCTY")]
    public string? BnkCty { get; set; }

    /// <summary>
    /// 司龄扣减期
    /// </summary>
    [JsonProperty("CCSDDT")]
    public string? CcsDdt { get; set; }

    /// <summary>
    /// 个人社保账号
    /// </summary>
    [JsonProperty("SECACT")]
    public string? SecAct { get; set; }

    /// <summary>
    /// 工作所属公司
    /// </summary>
    [JsonProperty("EMPCRP")]
    public string? EmpCrp { get; set; }

    /// <summary>
    /// 职位编码
    /// </summary>
    [JsonProperty("JOBSEQ")]
    public string? JobSeq { get; set; }

    /// <summary>
    /// 联系地城市
    /// </summary>
    [JsonProperty("LNKCTY")]
    public string? LnkCty { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    [JsonProperty("STFSEX")]
    public string? StfSex { get; set; }

    /// <summary>
    /// 岗位名称
    /// </summary>
    [JsonProperty("POSNAM")]
    public string? PosNam { get; set; }

    /// <summary>
    /// 汇报上级
    /// </summary>
    [JsonProperty("REPSUP")]
    public string? RepSup { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    [JsonProperty("UPDATE")]
    public string? Update { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [JsonProperty("REMARK")]
    public string? Remark { get; set; }

    /// <summary>
    /// 国籍/地区
    /// </summary>
    [JsonProperty("NATNLT")]
    public string? NatnLt { get; set; }

    /// <summary>
    /// 烈属证号
    /// </summary>
    [JsonProperty("MTYNBR")]
    public string? MtyNbr { get; set; }

    /// <summary>
    /// 出生日期
    /// </summary>
    [JsonProperty("BIRDAT")]
    public string? BirDat { get; set; }

    /// <summary>
    /// 曾用名
    /// </summary>
    [JsonProperty("USDNAM")]
    public string? UsdNam { get; set; }

    /// <summary>
    /// 职级
    /// </summary>
    [JsonProperty("JOBRNK")]
    public string? JobRnk { get; set; }

    /// <summary>
    /// 是否烈属
    /// </summary>
    [JsonProperty("MTYSTS")]
    public string? MtySts { get; set; }

    /// <summary>
    /// 出生地
    /// </summary>
    [JsonProperty("BIRLAD")]
    public string? BirLad { get; set; }

    /// <summary>
    /// 工资卡号
    /// </summary>
    [JsonProperty("SALCAR")]
    public string? SalCar { get; set; }

    /// <summary>
    /// 证件长期是否有效
    /// </summary>
    [JsonProperty("CERISL")]
    public string? CerIsL { get; set; }

    /// <summary>
    /// 计划转正日期
    /// </summary>
    [JsonProperty("CORPLD")]
    public string? CorpLd { get; set; }

    /// <summary>
    /// 户籍所在地城市
    /// </summary>
    [JsonProperty("HOMCTY")]
    public string? HomCty { get; set; }

    /// <summary>
    /// 员工序号
    /// </summary>
    [JsonProperty("STFSEQ")]
    public string? StfSeq { get; set; }

    /// <summary>
    /// 计划试用期
    /// </summary>
    [JsonProperty("CORPLP")]
    public string? CorpLp { get; set; }

    /// <summary>
    /// 证件类型
    /// </summary>
    [JsonProperty("CERTYP")]
    public string? CerTyp { get; set; }

    /// <summary>
    /// 工作地
    /// </summary>
    [JsonProperty("WKPSEQ")]
    public string? WkpSeq { get; set; }

    /// <summary>
    /// 岗位id
    /// </summary>
    [JsonProperty("POSUID")]
    public string? PosUid { get; set; }

    /// <summary>
    /// 首次工作日期
    /// </summary>
    [JsonProperty("BGNDAT")]
    public string? BgnDat { get; set; }

    /// <summary>
    /// 开户行
    /// </summary>
    [JsonProperty("BNKTYP")]
    public string? BnkTyp { get; set; }

    /// <summary>
    /// 职位名称
    /// </summary>
    [JsonProperty("JOBNAM")]
    public string? JobNam { get; set; }

    /// <summary>
    /// 转正述职内容
    /// </summary>
    [JsonProperty("CORREP")]
    public string? CorRep { get; set; }

    /// <summary>
    /// 实际转正日期
    /// </summary>
    [JsonProperty("CORDAT")]
    public string? CorDat { get; set; }

    /// <summary>
    /// 部门
    /// </summary>
    [JsonProperty("ORGSEQ")]
    public string? OrgSeq { get; set; }

    /// <summary>
    /// 岗位code
    /// </summary>
    [JsonProperty("POSSEQ")]
    public string? PosSeq { get; set; }

    /// <summary>
    /// 业务分组
    /// </summary>
    [JsonProperty("GRPSEQ")]
    public string? GrpSeq { get; set; }

    /// <summary>
    /// 数币钱包ID
    /// </summary>
    [JsonProperty("DIGWET")]
    public string? DigWet { get; set; }

    /// <summary>
    /// 户籍所在地详细地址
    /// </summary>
    [JsonProperty("HOMDTL")]
    public string? HomDtl { get; set; }

    /// <summary>
    /// 残疾证号
    /// </summary>
    [JsonProperty("DIBNBR")]
    public string? DibNbr { get; set; }

    /// <summary>
    /// 员工状态
    /// </summary>
    [JsonProperty("STFSTS")]
    public string? StfSts { get; set; }

    /// <summary>
    /// 司龄开始日期
    /// </summary>
    [JsonProperty("CCSDAT")]
    public string? CcsDat { get; set; }

    /// <summary>
    /// 首次入境日期
    /// </summary>
    [JsonProperty("ETRDAT")]
    public string? EtrDat { get; set; }

    /// <summary>
    /// 民族
    /// </summary>
    [JsonProperty("NATION")]
    public string? Nation { get; set; }

    /// <summary>
    /// 现住址详细地址
    /// </summary>
    [JsonProperty("ADDRES")]
    public string? Addres { get; set; }

    /// <summary>
    /// 证件到期日期
    /// </summary>
    [JsonProperty("CERVAL")]
    public string? CerVal { get; set; }

    /// <summary>
    /// 是否残疾
    /// </summary>
    [JsonProperty("LOESTS")]
    public string? LoeSts { get; set; }

    /// <summary>
    /// 员工号
    /// </summary>
    [JsonProperty("STFNBR")]
    public string? StfNbr { get; set; }

    /// <summary>
    /// 离职类型
    /// </summary>
    [JsonProperty("QUTFLG")]
    public string? QutFlg { get; set; }

    /// <summary>
    /// 现住址省
    /// </summary>
    [JsonProperty("USLPRV")]
    public string? UslPrv { get; set; }

    /// <summary>
    /// 证件号码
    /// </summary>
    [JsonProperty("CERNBR")]
    public string? CerNbr { get; set; }

    /// <summary>
    /// 成本分摊比例
    /// </summary>
    [JsonProperty("CSTRAT")]
    public string? CstRat { get; set; }

    /// <summary>
    /// 是否已育
    /// </summary>
    [JsonProperty("AFESTS")]
    public string? AfeSts { get; set; }

    /// <summary>
    /// 现住址区/县
    /// </summary>
    [JsonProperty("USLDST")]
    public string? UslDst { get; set; }

    /// <summary>
    /// 身份证地址
    /// </summary>
    [JsonProperty("CERLOC")]
    public string? CerLoc { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    [JsonProperty("STFNAM")]
    public string? StfNam { get; set; }

    /// <summary>
    /// 预计离境日期
    /// </summary>
    [JsonProperty("DPTDAT")]
    public string? DptDat { get; set; }

    /// <summary>
    /// 个人邮箱
    /// </summary>
    [JsonProperty("SELEML")]
    public string? SelEml { get; set; }

    /// <summary>
    /// 是否残疾
    /// </summary>
    [JsonProperty("DIBSTS")]
    public string? DibSts { get; set; }

    /// <summary>
    /// 开户地省份
    /// </summary>
    [JsonProperty("BNKPRV")]
    public string? BnkPrv { get; set; }

    /// <summary>
    /// 离职备注
    /// </summary>
    [JsonProperty("QUTCOM")]
    public string? QutCom { get; set; }

    /// <summary>
    /// 中文名
    /// </summary>
    [JsonProperty("CHNNAM")]
    public string? ChnNam { get; set; }

    /// <summary>
    /// 转正备注
    /// </summary>
    [JsonProperty("CORMAK")]
    public string? CorMak { get; set; }

    /// <summary>
    /// 其他证件号
    /// </summary>
    [JsonProperty("OCRNBR")]
    public string? OcrNbr { get; set; }

    /// <summary>
    /// 部门名称
    /// </summary>
    [JsonProperty("ORGNAM")]
    public string? OrgNam { get; set; }

    /// <summary>
    /// 工作邮箱
    /// </summary>
    [JsonProperty("WRKEML")]
    public string? WrkEml { get; set; }

    /// <summary>
    /// 职位id
    /// </summary>
    [JsonProperty("JOBUID")]
    public string? JobUid { get; set; }

    /// <summary>
    /// 转正类型
    /// </summary>
    [JsonProperty("CORFLG")]
    public string? CorFlg { get; set; }

    /// <summary>
    /// 个税最高学历
    /// </summary>
    [JsonProperty("HIGSCH")]
    public string? HigSch { get; set; }

    /// <summary>
    /// 性别(用户版本)
    /// </summary>
    [JsonProperty("GENDER")]
    public string? Gender { get; set; }

    /// <summary>
    /// 事件生成时间
    /// </summary>
    [JsonProperty("OPRTSP")]
    public string? OprtSp { get; set; }

    /// <summary>
    /// 离职原因
    /// </summary>
    [JsonProperty("QUTRES")]
    public string? QutRes { get; set; }

    /// <summary>
    /// 户口性质
    /// </summary>
    [JsonProperty("HOMTYP")]
    public string? HomTyp { get; set; }

    /// <summary>
    /// 政治面貌
    /// </summary>
    [JsonProperty("PLTSTS")]
    public string? PltSts { get; set; }

    /// <summary>
    /// 联系地址区/县
    /// </summary>
    [JsonProperty("LNKDST")]
    public string? LnkDst { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    [JsonProperty("MOBNBR")]
    public string? MobNbr { get; set; }

    /// <summary>
    /// 运营机构
    /// </summary>
    [JsonProperty("OPRAGN")]
    public string? OprAgn { get; set; }

    /// <summary>
    /// 实际试用期
    /// </summary>
    [JsonProperty("PBTPRD")]
    public string? PbtPrd { get; set; }

    /// <summary>
    /// 证件类型(用户版本)
    /// </summary>
    [JsonProperty("CERTTYPE")]
    public string? CertType { get; set; }

    /// <summary>
    /// 转正评价
    /// </summary>
    [JsonProperty("CORMSG")]
    public string? CorMsg { get; set; }

    /// <summary>
    /// 赔偿方案
    /// </summary>
    [JsonProperty("CPSAMT")]
    public string? CpsAmt { get; set; }

    /// <summary>
    /// 户籍所在地省
    /// </summary>
    [JsonProperty("HOMPRV")]
    public string? HomPrv { get; set; }

    /// <summary>
    /// 联系地省
    /// </summary>
    [JsonProperty("LNKPRV")]
    public string? LnkPrv { get; set; }

    /// <summary>
    /// 个人公积金账号
    /// </summary>
    [JsonProperty("FUNACT")]
    public string? FunAct { get; set; }

    /// <summary>
    /// 联系地详细地址
    /// </summary>
    [JsonProperty("LNKADR")]
    public string? LnkAdr { get; set; }

    /// <summary>
    /// 企业号
    /// </summary>
    [JsonProperty("PRJCOD")]
    public string? PrjCod { get; set; }

    /// <summary>
    /// 入职日期
    /// </summary>
    [JsonProperty("ENTDAT")]
    public string? EntDat { get; set; }

    /// <summary>
    /// 自定义字段信息
    /// </summary>
    [JsonProperty("cus")]
    public List<StaffEventCustomField>? Cus { get; set; }

    /// <summary>
    /// 合同信息
    /// </summary>
    [JsonProperty("con")]
    public List<StaffEventContract>? Con { get; set; }

    /// <summary>
    /// 兼任信息
    /// </summary>
    [JsonProperty("atp")]
    public List<StaffEventAdjunct>? Atp { get; set; }

    /// <summary>
    /// 教育经历
    /// </summary>
    [JsonProperty("edu")]
    public List<StaffEventEducation>? Edu { get; set; }

    /// <summary>
    /// 专业证书
    /// </summary>
    [JsonProperty("certificate")]
    public List<StaffEventCertificate>? Certificate { get; set; }

    /// <summary>
    /// 职称
    /// </summary>
    [JsonProperty("title")]
    public List<StaffEventTitle>? Title { get; set; }

    /// <summary>
    /// 奖惩记录
    /// </summary>
    [JsonProperty("award")]
    public List<StaffEventAward>? Award { get; set; }

    /// <summary>
    /// 工作经历
    /// </summary>
    [JsonProperty("wrk")]
    public List<StaffEventWork>? Wrk { get; set; }

    /// <summary>
    /// 家庭成员
    /// </summary>
    [JsonProperty("family")]
    public List<StaffEventFamily>? Family { get; set; }

    /// <summary>
    /// 个税申报信息
    /// </summary>
    [JsonProperty("taxdetail")]
    public List<StaffEventTaxDetail>? TaxDetail { get; set; }
}

/// <summary>
/// 员工事件-自定义字段信息
/// </summary>
public class StaffEventCustomField
{
    /// <summary>
    /// 自定义字段值
    /// </summary>
    [JsonProperty("FLDVAL")]
    public string? FldVal { get; set; }

    /// <summary>
    /// 自定义字段id
    /// </summary>
    [JsonProperty("FLDKEY")]
    public string? FldKey { get; set; }

    /// <summary>
    /// 自定义字段流水号
    /// </summary>
    [JsonProperty("TRDSEQ")]
    public string? TrdSeq { get; set; }

    /// <summary>
    /// 自定义字段所在分组
    /// </summary>
    [JsonProperty("CLSKEY")]
    public string? ClsKey { get; set; }
}

/// <summary>
/// 员工事件-合同信息
/// </summary>
public class StaffEventContract
{
    /// <summary>
    /// 合同状态
    /// </summary>
    [JsonProperty("CONSTS")]
    public string? Consts { get; set; }

    /// <summary>
    /// 合同流水号
    /// </summary>
    [JsonProperty("TRASEQ")]
    public string? TraSeq { get; set; }

    /// <summary>
    /// 电子签进度
    /// </summary>
    [JsonProperty("ESNSTS")]
    public string? EsnSts { get; set; }

    /// <summary>
    /// 合同期限
    /// </summary>
    [JsonProperty("TIMLIM")]
    public string? TimLim { get; set; }

    /// <summary>
    /// 合同起始日
    /// </summary>
    [JsonProperty("NOWBGN")]
    public string? NowBgn { get; set; }

    /// <summary>
    /// 合同类型
    /// </summary>
    [JsonProperty("CONTYP")]
    public string? ConTyp { get; set; }

    /// <summary>
    /// 合同编号
    /// </summary>
    [JsonProperty("CONNBR")]
    public string? ConNbr { get; set; }

    /// <summary>
    /// 合同公司
    /// </summary>
    [JsonProperty("CMPSEQ")]
    public string? CmpSeq { get; set; }

    /// <summary>
    /// 合同到期日
    /// </summary>
    [JsonProperty("NOWEND")]
    public string? NowEnd { get; set; }

    /// <summary>
    /// 签署方式
    /// </summary>
    [JsonProperty("SGNTYP")]
    public string? SgnTyp { get; set; }

    /// <summary>
    /// 续签次数
    /// </summary>
    [JsonProperty("RENTIM")]
    public int? RenTim { get; set; }
}

/// <summary>
/// 员工事件-兼任信息
/// </summary>
public class StaffEventAdjunct
{
    /// <summary>
    /// 兼任流水号
    /// </summary>
    [JsonProperty("ATPSEQ")]
    public string? AtpSeq { get; set; }

    /// <summary>
    /// 兼任岗位id
    /// </summary>
    [JsonProperty("ATPPST")]
    public string? AtpPst { get; set; }

    /// <summary>
    /// 兼任岗位code
    /// </summary>
    [JsonProperty("ATPPOS")]
    public string? AtpPos { get; set; }

    /// <summary>
    /// 兼任部门
    /// </summary>
    [JsonProperty("ATPORG")]
    public string? AtpOrg { get; set; }

    /// <summary>
    /// 员工序号
    /// </summary>
    [JsonProperty("STFSEQ")]
    public string? StfSeq { get; set; }
}

/// <summary>
/// 员工事件-教育经历
/// </summary>
public class StaffEventEducation
{
    /// <summary>
    /// 毕业院校
    /// </summary>
    [JsonProperty("GRASCH")]
    public string? GraSch { get; set; }

    /// <summary>
    /// 毕业时间
    /// </summary>
    [JsonProperty("GRADAT")]
    public string? GraDat { get; set; }

    /// <summary>
    /// 专业
    /// </summary>
    [JsonProperty("MAJORI")]
    public string? Majori { get; set; }

    /// <summary>
    /// 教育经历流水号
    /// </summary>
    [JsonProperty("EDUNBR")]
    public string? EduNbr { get; set; }

    /// <summary>
    /// 学历
    /// </summary>
    [JsonProperty("BCKGRD")]
    public string? BckGrd { get; set; }
}

/// <summary>
/// 员工事件-专业证书
/// </summary>
public class StaffEventCertificate
{
    /// <summary>
    /// 证书名称
    /// </summary>
    [JsonProperty("CETNAM")]
    public string? CetNam { get; set; }

    /// <summary>
    /// 有效期到期日
    /// </summary>
    [JsonProperty("VALEND")]
    public string? ValEnd { get; set; }

    /// <summary>
    /// 发证日期
    /// </summary>
    [JsonProperty("ISSDAT")]
    public string? IssDat { get; set; }

    /// <summary>
    /// 专业证书流水号
    /// </summary>
    [JsonProperty("CETSEQ")]
    public string? CetSeq { get; set; }

    /// <summary>
    /// 证书机构
    /// </summary>
    [JsonProperty("CETAUT")]
    public string? CetAut { get; set; }

    /// <summary>
    /// 有效期起始日
    /// </summary>
    [JsonProperty("VALSTA")]
    public string? ValSta { get; set; }
}

/// <summary>
/// 员工事件-职称
/// </summary>
public class StaffEventTitle
{
    /// <summary>
    /// 职称流水号
    /// </summary>
    [JsonProperty("TTLSEQ")]
    public string? TtlSeq { get; set; }

    /// <summary>
    /// 职称名称
    /// </summary>
    [JsonProperty("TTLNAM")]
    public string? TtlNam { get; set; }

    /// <summary>
    /// 职称级别
    /// </summary>
    [JsonProperty("TTLVEL")]
    public string? TtlVel { get; set; }

    /// <summary>
    /// 职称评定机构
    /// </summary>
    [JsonProperty("RATAGC")]
    public string? RatAgc { get; set; }

    /// <summary>
    /// 职称获得日期
    /// </summary>
    [JsonProperty("GETDAT")]
    public string? GetDat { get; set; }

    /// <summary>
    /// 职称证书编号
    /// </summary>
    [JsonProperty("TTLNBR")]
    public string? TtlNbr { get; set; }

    /// <summary>
    /// 是否最高职称
    /// </summary>
    [JsonProperty("HIGHST")]
    public string? HighSt { get; set; }
}

/// <summary>
/// 员工事件-奖惩记录
/// </summary>
public class StaffEventAward
{
    /// <summary>
    /// 奖惩记录流水号
    /// </summary>
    [JsonProperty("RPRSEQ")]
    public string? RprSeq { get; set; }

    /// <summary>
    /// 奖惩事项
    /// </summary>
    [JsonProperty("REWPNS")]
    public string? RewPns { get; set; }

    /// <summary>
    /// 奖惩时间
    /// </summary>
    [JsonProperty("RECDAT")]
    public string? RecDat { get; set; }

    /// <summary>
    /// 奖惩单位
    /// </summary>
    [JsonProperty("CURUNT")]
    public string? CurUnt { get; set; }

    /// <summary>
    /// 奖惩详情
    /// </summary>
    [JsonProperty("DETAIL")]
    public string? Detail { get; set; }
}

/// <summary>
/// 员工事件-工作经历
/// </summary>
public class StaffEventWork
{
    /// <summary>
    /// 证明人联系电话
    /// </summary>
    [JsonProperty("RETMNR")]
    public string? RetMnr { get; set; }

    /// <summary>
    /// 工作结束日期
    /// </summary>
    [JsonProperty("WENDAT")]
    public string? WenDat { get; set; }

    /// <summary>
    /// 工作经历流水号
    /// </summary>
    [JsonProperty("WRKNBR")]
    public string? WrkNbr { get; set; }

    /// <summary>
    /// 工作开始日期
    /// </summary>
    [JsonProperty("WBGDAT")]
    public string? WbgDat { get; set; }

    /// <summary>
    /// 证明人
    /// </summary>
    [JsonProperty("RETERE")]
    public string? RetEre { get; set; }

    /// <summary>
    /// 离职薪资
    /// </summary>
    [JsonProperty("LEASAL")]
    public string? LeaSal { get; set; }

    /// <summary>
    /// 曾任岗位
    /// </summary>
    [JsonProperty("LSTPST")]
    public string? LstPst { get; set; }

    /// <summary>
    /// 过往离职原因
    /// </summary>
    [JsonProperty("LEAREA")]
    public string? LeaRea { get; set; }

    /// <summary>
    /// 工作单位
    /// </summary>
    [JsonProperty("LSTCOM")]
    public string? LstCom { get; set; }
}

/// <summary>
/// 员工事件-家庭成员
/// </summary>
public class StaffEventFamily
{
    /// <summary>
    /// 职位/岗位
    /// </summary>
    [JsonProperty("WRKJOB")]
    public string? WrkJob { get; set; }

    /// <summary>
    /// 家庭成员关系
    /// </summary>
    [JsonProperty("RLTSHP")]
    public string? RltShp { get; set; }

    /// <summary>
    /// 家庭成员流水号
    /// </summary>
    [JsonProperty("MBESEQ")]
    public string? MbeSeq { get; set; }

    /// <summary>
    /// 家庭成员电话
    /// </summary>
    [JsonProperty("CONTEL")]
    public string? ConTel { get; set; }

    /// <summary>
    /// 家庭成员生日
    /// </summary>
    [JsonProperty("BIRDAY")]
    public string? BirDay { get; set; }

    /// <summary>
    /// 现工作单位
    /// </summary>
    [JsonProperty("WRKCOM")]
    public string? WrkCom { get; set; }

    /// <summary>
    /// 家庭成员姓名
    /// </summary>
    [JsonProperty("MBENAM")]
    public string? MbeNam { get; set; }
}

/// <summary>
/// 员工事件-个税申报信息
/// </summary>
public class StaffEventTaxDetail
{
    /// <summary>
    /// 任职受雇从业日期
    /// </summary>
    [JsonProperty("EMPDAT")]
    public string? EmpDat { get; set; }

    /// <summary>
    /// 任职受雇从业类型
    /// </summary>
    [JsonProperty("EMPTYP")]
    public string? EmpTyp { get; set; }

    /// <summary>
    /// 个人投资总额
    /// </summary>
    [JsonProperty("INVAMT")]
    public decimal? InvAmt { get; set; }

    /// <summary>
    /// 个人投资比例
    /// </summary>
    [JsonProperty("INVPRP")]
    public decimal? InvPrp { get; set; }

    /// <summary>
    /// 是否扣除减除费用
    /// </summary>
    [JsonProperty("KCJCFY")]
    public string? KcjCfy { get; set; }

    /// <summary>
    /// 个税人员状态
    /// </summary>
    [JsonProperty("NRMSTS")]
    public string? NrmSts { get; set; }

    /// <summary>
    /// 个税状态
    /// </summary>
    [JsonProperty("RECSTS")]
    public string? RecSts { get; set; }

    /// <summary>
    /// 企业/税局
    /// </summary>
    [JsonProperty("REGSEQ")]
    public string? RegSeq { get; set; }

    /// <summary>
    /// 涉税事由
    /// </summary>
    [JsonProperty("RELTYP")]
    public string? RelTyp { get; set; }

    /// <summary>
    /// 职务
    /// </summary>
    [JsonProperty("STFTTL")]
    public string? StfTtl { get; set; }

    /// <summary>
    /// 个税通知手机号
    /// </summary>
    [JsonProperty("TAXMOB")]
    public string? TaxMob { get; set; }

    /// <summary>
    /// 税局离职日期
    /// </summary>
    [JsonProperty("TAXQDT")]
    public string? TaxQdt { get; set; }

    /// <summary>
    /// 个税备注
    /// </summary>
    [JsonProperty("TAXRMK")]
    public string? TaxRmk { get; set; }

    /// <summary>
    /// 入职年度就业情形
    /// </summary>
    [JsonProperty("WRKFST")]
    public string? WrkFst { get; set; }
}
