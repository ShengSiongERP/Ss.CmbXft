using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Ss.CmbXft.Domain.Entities;
using Ss.CmbXft.Domain.Entities.Sserp;
using Ss.CmbXft.Domain.Repositories;
using Ss.CmbXft.Sdk.Models.Organization;
using Ss.CmbXft.Sdk.Models.Post;
using Ss.CmbXft.Sdk.Models.Staff;
using Ss.CmbXft.Sdk.Services;
using Yitter.IdGenerator;
using SdkStaffInfo = Ss.CmbXft.Sdk.Models.Staff.StaffInfo;

namespace Ss.CmbXft.Application.Services;

/// <summary>
/// 员工数据数据库同步服务实现
/// </summary>
public class XftErpSyncService : IXftErpSyncService
{
    private readonly ISserpUnitOfWork _secondaryUnitOfWork;
    private readonly ISserpRepository<SserpERPTxnEmployee, long> _sserpERPTxnEmployeeRepository;
    private readonly ISserpRepository<AbpUser, Guid> _abpUserRepository;
    private readonly ISserpRepository<AbpUserRole, Guid> _abpUserRoleRepository;
    private readonly ISserpRepository<AbpRole, Guid> _abpRoleRepository;
    private readonly IStaffService _staffService;
    private readonly IOrganizationService _organizationService;
    private readonly IPostService _postService;
    private readonly ILogger<XftErpSyncService> _logger;
    private readonly IPasswordHasher<AbpUser> _passwordHasher;

    public XftErpSyncService(
        ISserpRepository<SserpERPTxnEmployee, long> secondaryRepository,
        ISserpRepository<AbpUser, Guid> abpUserRepository,
        ISserpRepository<AbpUserRole, Guid> abpUserRoleRepository,
        ISserpRepository<AbpRole, Guid> abpRoleRepository,
        ISserpUnitOfWork secondaryUnitOfWork,
        IStaffService staffService,
        IOrganizationService organizationService,
        IPostService postService,
        ILogger<XftErpSyncService> logger,
        IPasswordHasher<AbpUser> passwordHasher)
    {
        _sserpERPTxnEmployeeRepository = secondaryRepository;
        _abpUserRepository = abpUserRepository;
        _abpUserRoleRepository = abpUserRoleRepository;
        _abpRoleRepository = abpRoleRepository;
        _secondaryUnitOfWork = secondaryUnitOfWork;
        _staffService = staffService;
        _organizationService = organizationService;
        _postService = postService;
        _logger = logger;
        _passwordHasher = passwordHasher;
    }

    //还原ERP_Txn_Employee表数据用sql
    //TRUNCATE TABLE ERP_Txn_Employee;
    //INSERT INTO ERP_Txn_Employee SELECT * FROM ERP_Txn_Employee_copy1;
    //还原用户角色
    //TRUNCATE TABLE AbpUserRoles;
    //INSERT INTO AbpUserRoles SELECT * FROM AbpUserRoles_copy1;
    //删除新增用户 筛选出删除数据 分条删除
    //select * FROM AbpUsers WHERE CAST(CreationTime AS DATE) = '2026-06-22';
    //delete from AbpUsers where id='F4168F3F-2321-4DC8-AB08-01DDF1EF7B6D';
    //delete from AbpUsers where id='F4168F3F-2321-4DC8-AB08-01DDF1EF7B6D';

    /// <summary>
    /// 从薪福通同步员工数据到本地数据库
    /// </summary>
    public async Task<int> SyncStaffAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("开始从薪福通同步员工数据到数据库");
        #region 获取数据 机构 岗位（用于映射中文名称）
        var organizationDict = await LoadOrganizationDataAsync(cancellationToken);
        var postDict = await LoadPostDataAsync(cancellationToken);
        #endregion

        var allXftStaffs = new List<SdkStaffInfo>();
        var currentPage = 1;
        int pageSize = 1000;
#if DEBUG
        //pageSize = 10;
#endif

        bool hasMoreData = true;

        try
        {
            #region 获取数据 机构 岗位（用于映射中文名称）

            while (hasMoreData)
            {
                _logger.LogInformation("开始获取第 {Page} 页数据，每页 {PageSize} 条", currentPage, pageSize);

                var request = new StaffQueryRequest
                {
                    CurrentPage = currentPage,
                    PageSize = pageSize,
                    QueryResultType = QueryResultType.CreateGroupQuery(
                        "S01BASIC",    //员工基本信息	staffBasicInfo
                        "S02ONJOB",    //在职信息	staffHrmInfo
                        "S03PERSN",    //个人信息	staffBasicInfo
                        "S01BASIC",    //兼任信息	staffAdjunctInfoList
                        "S04SAISR",    //工资社保	staffWagesAndSocialSecurityInfo
                        "S05CONTACT",  //合同信息	staffContractInfoList
                        "S06EMCNT",    //紧急联系人信息	staffEmergencyContact
                        "S19GRPSVS",   //集团服务信息	staffGroupServiceInfo
                        "S07EDUCA",    //教育经历	staffEducationInfoList
                        "S08WRKEPR",   //工作经历	staffWorkInfoList
                        "S02QUITMSG",  //离职信息	staffHrmInfo
                        "S09TXOTH",    //个税基本信息	staffTaxBasicInfo
                        "S09TXSTA",    //个税申报信息	staffTaxDeclarationInfoList
                        "S09FMLMEM",   //家庭成员信息	staffFamilyMemberInfoList
                        "S09PFSCER",   //专业证书	staffCertificateInfoList
                        "S09RWDPNS",   //奖惩记录	staffAwardInfoList
                        "S09TTITLE",   //职称	staffJobTitleInfoList
                        "S09TUTRAN",   //培训记录	staffTrainRecordInfoList
                        "S09TUVPFE",   //绩效考核	staffPerformanceInfoList
                        "S10PSNPFL"    //个人材料	staffAttachmentInfoList
                    ),
                };

                var response = await _staffService.QueryStaffAsync(request, cancellationToken);

                if (response?.Body?.Records == null || response.Body.Records.Count == 0)
                {
                    _logger.LogInformation("第 {Page} 页无数据，获取结束", currentPage);
                    hasMoreData = false;
                    break;
                }

                var staffs = response.Body.Records.Where(r => r.StaffBasicInfo != null).ToList();
                if (!staffs.Any())
                {
                    _logger.LogInformation("第 {Page} 页无有效数据", currentPage);
                    hasMoreData = false;
                    break;
                }

                allXftStaffs.AddRange(staffs);

                // 检查是否还有更多数据
                hasMoreData = (currentPage * pageSize) < response.Body.TotalSize;
#if DEBUG
                //hasMoreData = false; //调试时只同步一页数据
#endif
                currentPage++;
            }

            _logger.LogInformation("共获取 {Total} 条员工数据，开始同步到数据库", allXftStaffs.Count);
            #endregion
            //var test = allXftStaffs.FirstOrDefault(x=>x.StaffBasicInfo);
            // 同步到主数据库和ERP数据库
            await SyncToSecondaryDatabaseAsync(allXftStaffs, organizationDict, postDict, cancellationToken);

            _logger.LogInformation("员工数据同步完成，共同步 {Total} 条", allXftStaffs.Count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "员工数据同步失败");
            throw;
        }

        return allXftStaffs.Count;
    }

    ///// <summary>
    ///// 同步数据到主数据库（XftStaff）
    ///// </summary>
    //private async Task SyncToPrimaryDatabaseAsync(List<SdkStaffInfo> xftStaffs, CancellationToken cancellationToken)
    //{
    //    await _unitOfWork.BeginTransactionAsync(cancellationToken);
    //    try
    //    {
    //        // 获取所有员工编码（用于查询和比对）
    //        var staffSeqs = xftStaffs
    //            .Select(r => r.StaffSeq)
    //            .Where(s => !string.IsNullOrEmpty(s))
    //            .Distinct()
    //            .ToList();

    //        // 查询主数据库中所有员工（包括已删除的）
    //        var allExistingStaff = await _xftStaffRepository.GetListAsync(
    //            predicate: e => !string.IsNullOrEmpty(e.StaffSeq),
    //            null, null,
    //            false, true, cancellationToken);

    //        var existingStaffDict = allExistingStaff
    //            .Where(e => !string.IsNullOrEmpty(e.StaffSeq))
    //            .ToDictionary(e => e.StaffSeq!);

    //        var toInsert = new List<XftStaff>();
    //        var toUpdate = new List<XftStaff>();
    //        var totalInserted = 0;
    //        var totalUpdated = 0;

    //        foreach (var staffInfo in xftStaffs)
    //        {
    //            if (string.IsNullOrEmpty(staffInfo.StaffSeq))
    //            {
    //                _logger.LogWarning("跳过员工序号为空的员工数据");
    //                continue;
    //            }

    //            var staffJson = JsonConvert.SerializeObject(staffInfo, Formatting.None,
    //                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

    //            if (existingStaffDict.TryGetValue(staffInfo.StaffSeq, out var exist))
    //            {
    //                // 更新现有记录（如果已删除则恢复）
    //                exist.EnterpriseId = staffInfo.StaffBasicInfo?.EnterpriseId;
    //                exist.StaffSeq = staffInfo.StaffSeq;
    //                exist.StfType = staffInfo.StaffBasicInfo?.StfType;
    //                exist.StfStatus = staffInfo.StaffBasicInfo?.StfStatus;
    //                exist.StfName = staffInfo.StaffBasicInfo?.StfName;
    //                exist.MobileNumber = staffInfo.StaffBasicInfo?.MobileNumber;
    //                exist.StaffJson = staffJson;
    //                exist.IsDeleted = false; // 恢复删除状态
    //                exist.UpdateTime = DateTime.Now;
    //                toUpdate.Add(exist);
    //            }
    //            else
    //            {
    //                // 创建新记录
    //                var newEntity = new XftStaff
    //                {
    //                    Id = YitIdHelper.NextId(),
    //                    EnterpriseId = staffInfo.StaffBasicInfo?.EnterpriseId,
    //                    StaffSeq = staffInfo.StaffSeq,
    //                    StfType = staffInfo.StaffBasicInfo?.StfType,
    //                    StfStatus = staffInfo.StaffBasicInfo?.StfStatus,
    //                    StfName = staffInfo.StaffBasicInfo?.StfName,
    //                    MobileNumber = staffInfo.StaffBasicInfo?.MobileNumber,
    //                    StaffJson = staffJson
    //                };
    //                toInsert.Add(newEntity);
    //            }
    //        }

    //        // 批量插入
    //        if (toInsert.Any())
    //        {
    //            await _xftStaffRepository.AddRangeAsync(toInsert, cancellationToken);
    //            totalInserted += toInsert.Count;
    //            _logger.LogInformation("主数据库批量插入 {Count} 条员工数据", toInsert.Count);
    //        }

    //        // 批量更新
    //        if (toUpdate.Any())
    //        {
    //            _xftStaffRepository.Update(toUpdate);
    //            totalUpdated += toUpdate.Count;
    //            _logger.LogInformation("主数据库批量更新 {Count} 条员工数据", toUpdate.Count);
    //        }

    //        // 处理删除逻辑：将本地存在但远程不存在的员工标记为删除
    //        var toDelete = allExistingStaff
    //            .Where(e => !string.IsNullOrEmpty(e.StaffSeq) &&
    //                        !staffSeqs.Contains(e.StaffSeq) &&
    //                        !e.IsDeleted)
    //            .ToList();

    //        if (toDelete.Any())
    //        {
    //            foreach (var staff in toDelete)
    //            {
    //                staff.IsDeleted = true;
    //                staff.UpdateTime = DateTime.Now;
    //            }
    //            _xftStaffRepository.Update(toDelete);
    //            _logger.LogInformation("主数据库将 {Count} 条不存在的员工标记为删除", toDelete.Count);
    //        }

    //        // 保存更改
    //        await _unitOfWork.SaveChangesAsync(cancellationToken);
    //        await _unitOfWork.CommitTransactionAsync(cancellationToken);

    //        _logger.LogInformation(
    //            "主数据库员工数据同步完成，新增 {Inserted} 条，更新 {Updated} 条，删除 {Deleted} 条",
    //            totalInserted, totalUpdated, toDelete.Count);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "主数据库员工数据同步失败，已回滚事务");
    //        await _unitOfWork.RollbackTransactionAsync(cancellationToken);
    //        throw;
    //    }
    //}

    /// <summary>
    /// 同步数据到ERP数据库（SserpERPTxnEmployee）(关系SserpERPTxnEmployee的EmployeeCode=staff_req 0000000001)
    /// </summary>
    private async Task SyncToSecondaryDatabaseAsync(
        List<SdkStaffInfo> xftStaffs,
        Dictionary<string, OrganizationInfo> organizationDict,
        Dictionary<string, string> postDict,
        CancellationToken cancellationToken)
    {
        await _secondaryUnitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            // 获取所有员工编码（用于查询和比对）
            var employeeCodes = xftStaffs
                .Select(r => NormalizeEmployeeCode(r.StaffSeq))
                .Where(s => !string.IsNullOrEmpty(s))
                .Distinct()
                .ToList();

            // 查询ERP数据库中所有员工（包括Status=0的）
            var allExistingEmployees = await _sserpERPTxnEmployeeRepository.GetListAsync(
                predicate: e => !string.IsNullOrEmpty(e.EmployeeCode),
                null, null,
                false, false, cancellationToken);

            var existingEmployeeDict = allExistingEmployees
                .Where(e => !string.IsNullOrEmpty(e.EmployeeCode))
                .ToDictionary(e => NormalizeEmployeeCode(e.EmployeeCode));

            var toInsert = new List<SserpERPTxnEmployee>();
            var toUpdate = new List<SserpERPTxnEmployee>();
            var totalInserted = 0;
            var totalUpdated = 0;

            // 收集员工同步信息（用于后续AbpUser同步和角色匹配：EmployeeNo, Name, OrgNamePath）
            var employeeSyncInfoList = new List<(string EmployeeNo, string Name, string? OrgNamePath)>();
            // 收集需要禁用的AbpUsers（UserName列表）
            var usersToDisable = new List<string>();

            //只有staffInfo.StaffBasicInfo.stfStatus=1和0的进行创建正常用户
            //staffInfo.StaffBasicInfo.stfStatus=2的用户对原有employee更改Status=0（禁用）
            foreach (var staffInfo in xftStaffs)
            {
                if (string.IsNullOrEmpty(staffInfo.StaffSeq))
                {
                    _logger.LogWarning("跳过员工序号为空的员工数据");
                    continue;
                }

                var basicInfo = staffInfo.StaffBasicInfo;
                var normalizedStaffSeq = NormalizeEmployeeCode(staffInfo.StaffSeq);

                // 从自定义字段中获取值
                var employeeNo = GetCustomFieldValue(staffInfo.StaffHrmInfo?.CustomerFieldInfoList, "FLD1100052") ?? string.Empty;
                var englishName = GetCustomFieldValue(staffInfo.StaffBasicInfo?.CustomerFieldInfoList, "FLD1100053") ?? string.Empty;
                var isAccessAllOutletValue = GetCustomFieldValue(staffInfo.StaffHrmInfo?.CustomerFieldInfoList, "FLD1100055");
                var isAccessAllOutlet = ParseIsAccessAllOutlet(isAccessAllOutletValue);

                var department = string.IsNullOrEmpty(basicInfo?.OrgSeq) ? basicInfo?.OrgSeq : organizationDict.TryGetValue(basicInfo.OrgSeq, out var orgInfo) ? orgInfo.Name : basicInfo.OrgSeq;
                var position = string.IsNullOrEmpty(basicInfo?.PosCode) ? basicInfo?.PosCode : postDict.TryGetValue(basicInfo.PosCode, out var name2) ? name2 : basicInfo.PosCode;

                // 根据组织机构的namePath映射WorkingLocationCode和AccessGroupCode
                var orgNamePath = organizationDict.TryGetValue(basicInfo?.OrgSeq ?? string.Empty, out var org) ? org.NamePath : null;
                var (workingLocationCode, accessGroupCode) = MapLocationAndAccessGroup(orgNamePath);

                // 判断员工状态
                var stfStatus = basicInfo?.StfStatus;
                var isActiveUser = stfStatus == "0" || stfStatus == "1"; // 0试用1正式2已离职3待离职
                var isDisabledUser = stfStatus == "2"; // 2为禁用用户

                if (existingEmployeeDict.TryGetValue(normalizedStaffSeq, out var exist))
                {
                    // 更新现有记录
                    exist.EmployeeNo = employeeNo;
                    exist.EnglishName = englishName;
                    exist.ID = basicInfo?.CertificateNumber ?? string.Empty;
                    exist.Department = department;
                    exist.Position = position;
                    exist.IsAccessAllOutlet = isAccessAllOutlet;
                    exist.ModifyUser = "SYSTEM";
                    exist.ModifyDate = DateTime.Now;

                    if (isDisabledUser)
                    {
                        // 禁用用户
                        exist.Status = 0;
                        // 收集需要禁用的AbpUser
                        if (!string.IsNullOrEmpty(employeeNo))
                        {
                            usersToDisable.Add(employeeNo);
                        }
                    }
                    else if (isActiveUser)
                    {
                        // 正常用户
                        exist.Status = 1;
                        // 收集员工同步信息（用于后续AbpUser同步和角色匹配）
                        if (!string.IsNullOrEmpty(employeeNo))
                        {
                            employeeSyncInfoList.Add((employeeNo, basicInfo?.StfName ?? string.Empty, orgNamePath));
                        }
                    }

                    toUpdate.Add(exist);
                }
                else
                {
                    // 只有stfStatus=0或1的才创建新记录
                    if (isActiveUser)
                    {
                        var newEntity = new SserpERPTxnEmployee
                        {
                            EmployeeCode = normalizedStaffSeq.PadLeft(7, '0'),
                            EmployeeNo = employeeNo,
                            Name = basicInfo?.StfName ?? string.Empty,
                            EnglishName = englishName,
                            Sex = ParseSex(basicInfo?.Sex),
                            AGE = CalculateAge(basicInfo?.Birthday),
                            BIRTHDATE = basicInfo?.Birthday,
                            ID = basicInfo?.CertificateNumber ?? string.Empty,
                            Department = department,
                            Position = position,
                            Status = 1, // 正常用户
                            IsAccessAllOutlet = isAccessAllOutlet,
                            WorkingLocationCode = workingLocationCode,
                            AccessGroupCode = accessGroupCode,
                            CreateUser = "SYSTEM",
                            CreateDate = DateTime.Now
                        };
                        toInsert.Add(newEntity);

                        // 收集员工同步信息（用于后续AbpUser同步和角色匹配）
                        if (!string.IsNullOrEmpty(employeeNo))
                        {
                            employeeSyncInfoList.Add((employeeNo, basicInfo?.StfName ?? string.Empty, orgNamePath));
                        }
                    }
                }
            }

            // 批量插入
            if (toInsert.Any())
            {
                await _sserpERPTxnEmployeeRepository.AddRangeAsync(toInsert, cancellationToken);
                totalInserted += toInsert.Count;
                _logger.LogInformation("ERP数据库批量插入 {Count} 条员工数据", toInsert.Count);
            }

            // 批量更新
            if (toUpdate.Any())
            {
                _sserpERPTxnEmployeeRepository.Update(toUpdate);
                totalUpdated += toUpdate.Count;
                _logger.LogInformation("ERP数据库批量更新 {Count} 条员工数据", toUpdate.Count);
            }

            // 处理删除逻辑：将本地存在但远程不存在的员工Status改为0
            var toDelete = allExistingEmployees.Where(e => !string.IsNullOrEmpty(e.EmployeeCode) && !employeeCodes.Contains(NormalizeEmployeeCode(e.EmployeeCode)) && e.Status != 0).ToList();

            if (toDelete.Any())
            {
                foreach (var employee in toDelete)
                {
                    employee.Status = 0;
                    employee.ModifyUser = "SYSTEM";
                    employee.ModifyDate = DateTime.Now;
                }
                _sserpERPTxnEmployeeRepository.Update(toDelete);
                _logger.LogInformation("ERP数据库将 {Count} 条不存在的员工状态改为0（删除）", toDelete.Count);
            }

            // ===== 同步Employee到AbpUser并匹配角色 =====
            if (employeeSyncInfoList.Any() || usersToDisable.Any())
            {
                var abpUserSyncResult = await SyncAbpUsersWithRolesAsync(employeeSyncInfoList, usersToDisable, cancellationToken);
                _logger.LogInformation("AbpUser同步完成，新增用户 {Inserted} 条，禁用用户 {Disabled} 条，新增角色关联 {RoleInserted} 条",
                    abpUserSyncResult.InsertedUsers, abpUserSyncResult.DisabledUsers, abpUserSyncResult.InsertedRoles);
            }

            // 保存更改
            await _secondaryUnitOfWork.SaveChangesAsync(cancellationToken);
            await _secondaryUnitOfWork.CommitTransactionAsync(cancellationToken);

            _logger.LogInformation("ERP数据库员工数据同步完成，新增 {Inserted} 条，更新 {Updated} 条，删除 {Deleted} 条",
                totalInserted, totalUpdated, toDelete.Count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ERP数据库员工数据同步失败，已回滚事务");
            await _secondaryUnitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    /// <summary>
    /// 加载组织机构数据并构建字典（Id -> OrganizationInfo）
    /// </summary>
    private async Task<Dictionary<string, OrganizationInfo>> LoadOrganizationDataAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("开始加载组织机构数据");
        var dict = new Dictionary<string, OrganizationInfo>();
        var currentPage = 1;
        const int pageSize = 2000;
        bool hasMoreData = true;

        try
        {
            while (hasMoreData)
            {
                var request = new OrganizationListRequest
                {
                    CurrentPage = currentPage,
                    PageSize = pageSize
                };

                var response = await _organizationService.GetOrganizationListAsync(request, cancellationToken);

                if (response?.Body?.Records == null || response.Body.Records.Count == 0)
                {
                    hasMoreData = false;
                    break;
                }

                foreach (var org in response.Body.Records)
                {
                    if (!string.IsNullOrEmpty(org.Id))
                    {
                        dict[org.Id] = org;
                    }
                }

                hasMoreData = (currentPage * pageSize) < response.Body.TotalSize;
                currentPage++;
            }

            _logger.LogInformation("组织机构数据加载完成，共 {Count} 条", dict.Count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "加载组织机构数据失败");
        }

        return dict;
    }

    /// <summary>
    /// 加载岗位数据并构建字典（CodeNumber -> PositionName）
    /// </summary>
    private async Task<Dictionary<string, string>> LoadPostDataAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("开始加载岗位数据");
        var dict = new Dictionary<string, string>();
        var currentPage = 1;
        const int pageSize = 1000;
        bool hasMoreData = true;

        try
        {
            while (hasMoreData)
            {
                var request = new PostQueryRequest
                {
                    CurrentPage = currentPage,
                    PageSize = pageSize
                };

                var response = await _postService.QueryPostAsync(request, cancellationToken);

                if (response?.Body?.Records == null || response.Body.Records.Count == 0)
                {
                    hasMoreData = false;
                    break;
                }

                foreach (var post in response.Body.Records)
                {
                    if (!string.IsNullOrEmpty(post.CodeNumber))
                    {
                        dict[post.CodeNumber] = post.PositionName;
                    }
                }

                hasMoreData = (currentPage * pageSize) < response.Body.TotalSize;
                currentPage++;
            }

            _logger.LogInformation("岗位数据加载完成，共 {Count} 条", dict.Count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "加载岗位数据失败");
        }

        return dict;
    }

    /// <summary>
    /// 标准化员工编码（去除前导零）
    /// </summary>
    private string NormalizeEmployeeCode(string? employeeCode)
    {
        if (string.IsNullOrEmpty(employeeCode))
            return string.Empty;

        return employeeCode.TrimStart('0');
    }

    /// <summary>
    /// 从自定义字段列表中获取指定字段的值
    /// </summary>
    private string? GetCustomFieldValue(List<CustomerFieldInfo>? customFieldList, string fieldKey)
    {
        if (customFieldList == null || !customFieldList.Any())
            return null;

        return customFieldList.FirstOrDefault(f => f.FieldKey == fieldKey)?.FieldValue;
    }

    /// <summary>
    /// 解析是否可访问所有门店
    /// </summary>
    private bool? ParseIsAccessAllOutlet(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return null;

        return value switch
        {
            "是" => true,
            "否" => false,
            _ => null
        };
    }

    /// <summary>
    /// 解析性别（XFT: 男0女1 -> ERP: 男1女2）
    /// </summary>
    private short ParseSex(string? sex)
    {
        if (string.IsNullOrEmpty(sex))
            return 1; // 默认为男

        return sex switch
        {
            "0" => 1, // XFT女 -> ERP女
            "1" => 2, // XFT男 -> ERP男
            _ => 1    // 默认为男
        };
    }

    /// <summary>
    /// 计算年龄
    /// </summary>
    private int? CalculateAge(DateTime? birthDate)
    {
        if (!birthDate.HasValue)
            return null;

        var today = DateTime.Today;
        var age = today.Year - birthDate.Value.Year;

        if (today < birthDate.Value.AddYears(age))
            age--;

        return age;
    }

    /// <summary>
    /// 根据组织机构的namePath映射WorkingLocationCode和AccessGroupCode
    /// </summary>
    /// <param name="namePath">组织机构名称路径</param>
    /// <returns>返回WorkingLocationCode和AccessGroupCode的元组，如果未匹配则返回(null, null)</returns>
    private (string? WorkingLocationCode, string? AccessGroupCode) MapLocationAndAccessGroup(string? namePath)
    {
        if (string.IsNullOrEmpty(namePath))
            return (null, null);

        // 店铺映射规则
        var storeMapping = new Dictionary<string, (string WorkingLocationCode, string AccessGroupCode)>
        {
            { "盛高店", ("KM01", "0001") },
            { "经典店", ("KM02", "0002") },
            { "慧谷店", ("KM03", "0003") },
            { "鑫都店", ("KM04", "0004") },
            { "北城店", ("KM05", "0005") },
            { "广福店", ("KM06", "0006") }
        };

        // 检查namePath是否包含店铺名称
        foreach (var mapping in storeMapping)
        {
            if (namePath.Contains(mapping.Key))
            {
                return mapping.Value;
            }
        }

        return ("KM06", "0006");
    }

    /// <summary>
    /// 同步员工到AbpUser并匹配角色
    /// 匹配规则：EmployeeNo 与 AbpUser.UserName 忽略大小写匹配
    /// 新增用户根据组织机构namePath匹配角色
    /// 禁用的用户（stfStatus=2）设置IsActive=false
    /// </summary>
    private async Task<(int InsertedUsers, int DisabledUsers, int InsertedRoles)> SyncAbpUsersWithRolesAsync(
        List<(string EmployeeNo, string Name, string? OrgNamePath)> employeeSyncInfoList,
        List<string> usersToDisable,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("开始同步Employee到AbpUser，共 {Count} 条待处理，{DisableCount} 条需要禁用", employeeSyncInfoList.Count, usersToDisable.Count);

        // 查询所有AbpUser
        var allAbpUsers = await _abpUserRepository.GetListAsync(
            predicate: u => !string.IsNullOrEmpty(u.UserName),
            null, null,
            false, false, cancellationToken);

        // 构建已存在用户的字典（NormalizedUserName大写 -> AbpUser）
        var existingUserDict = new Dictionary<string, AbpUser>();
        foreach (var u in allAbpUsers.Where(u => !string.IsNullOrEmpty(u.NormalizedUserName)))
        {
            var key = u.NormalizedUserName!.ToUpperInvariant();
            if (!existingUserDict.ContainsKey(key))
                existingUserDict[key] = u;
        }

        // 查询所有AbpUserRole（用于判断新增用户是否已有角色关联）
        var allUserRoles = await _abpUserRoleRepository.GetListAsync(
            predicate: null, null, null,
            false, false, cancellationToken);

        var existingUserRoleSet = allUserRoles
            .Select(ur => (ur.UserId, ur.RoleId))
            .ToHashSet();

        // 查询所有AbpRole，构建角色名称字典（NormalizedRoleName大写 -> RoleId）
        var allRoles = await _abpRoleRepository.GetListAsync(
            predicate: null, null, null,
            false, false, cancellationToken);
        var roleNameToIdDict = new Dictionary<string, Guid>();
        foreach (var role in allRoles.Where(r => !string.IsNullOrEmpty(r.Name)))
        {
            roleNameToIdDict[role.Name!.ToUpperInvariant()] = role.Id;
        }

        var toInsertUsers = new List<AbpUser>();
        var toDisableUsers = new List<AbpUser>();
        var toInsertUserRoles = new List<AbpUserRole>();

        // 将usersToDisable转换为HashSet以便快速查找
        var usersToDisableSet = new HashSet<string>(usersToDisable.Select(u => u.ToUpperInvariant()));

        foreach (var (employeeNo, name, orgNamePath) in employeeSyncInfoList)
        {
            var normalizedUserName = employeeNo.ToUpperInvariant();

            if (existingUserDict.TryGetValue(normalizedUserName, out var existingUser))
            {
                // 已存在：判断是否需要禁用
                if (usersToDisableSet.Contains(normalizedUserName) && existingUser.IsActive)
                {
                    // 需要禁用的用户
                    existingUser.IsActive = false;
                    existingUser.LastModificationTime = DateTime.Now;
                    toDisableUsers.Add(existingUser);
                }
                // 已存在且不需要禁用的用户：不做更新
            }
            else
            {
                // 不存在：新增AbpUser
                var newUserId = Guid.NewGuid();

                var newUser = new AbpUser
                {
                    Id = newUserId,
                    TenantId = null,
                    UserName = employeeNo,
                    NormalizedUserName = normalizedUserName,
                    Name = name,
                    Surname = string.Empty,
                    Email = $"{employeeNo}@test.com",
                    NormalizedEmail = $"{normalizedUserName}@TEST.COM",
                    EmailConfirmed = false,
                    SecurityStamp = newUserId.ToString("n"),
                    IsExternal = false,
                    PhoneNumber = string.Empty,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    ExtraProperties = "{}",
                    ConcurrencyStamp = newUserId.ToString("n"),
                    CreationTime = DateTime.Now,
                    CreatorId = newUserId,
                    LastModificationTime = null,
                    LastModifierId = null,
                    IsDeleted = false,
                    DeleterId = null,
                    DeletionTime = null,
                    BranchId = null,
                    IsFirstTimeLogin = true,
                    IsActive = true
                };
                // 使用ABP vNext加密方式：密码=账号(employeeNo)
                newUser.PasswordHash = _passwordHasher.HashPassword(newUser, employeeNo);
                toInsertUsers.Add(newUser);

                // 只有新增用户才匹配角色和创建角色关联
                var matchedRoleName = MatchRoleByOrgNamePath(orgNamePath);
                var matchedRoleId = matchedRoleName != null && roleNameToIdDict.TryGetValue(matchedRoleName.ToUpperInvariant(), out var roleId)
                    ? (Guid?)roleId : null;
                if (matchedRoleId.HasValue)
                {
                    var userRoleKey = (newUserId, matchedRoleId.Value);
                    if (!existingUserRoleSet.Contains(userRoleKey))
                    {
                        toInsertUserRoles.Add(new AbpUserRole
                        {
                            UserId = newUserId,
                            RoleId = matchedRoleId.Value,
                            TenantId = null
                        });
                        existingUserRoleSet.Add(userRoleKey);
                    }
                }
            }
        }

        // 批量新增AbpUser
        if (toInsertUsers.Any())
        {
            await _abpUserRepository.AddRangeAsync(toInsertUsers, cancellationToken);
            _logger.LogInformation("AbpUser批量新增 {Count} 条", toInsertUsers.Count);
        }

        // 批量禁用AbpUser
        if (toDisableUsers.Any())
        {
            _abpUserRepository.Update(toDisableUsers);
            _logger.LogInformation("AbpUser批量禁用 {Count} 条", toDisableUsers.Count);
        }

        // 批量新增AbpUserRole
        if (toInsertUserRoles.Any())
        {
            //保存一次用户  防止AbpUserRole外键缺失
            await _secondaryUnitOfWork.SaveChangesAsync(cancellationToken);

            await _abpUserRoleRepository.AddRangeAsync(toInsertUserRoles, cancellationToken);
            _logger.LogInformation("AbpUserRole批量新增 {Count} 条", toInsertUserRoles.Count);
        }

        return (toInsertUsers.Count, toDisableUsers.Count, toInsertUserRoles.Count);
    }

    /// <summary>
    /// 根据组织机构namePath匹配角色名称
    /// 匹配规则（按优先级）：
    ///   1. namePath包含"百货食品采购部" → "Buyer"
    ///   2. namePath包含"财务部" → "Finance Team Memeber"
    ///   3. namePath包含分店名+营运部 → "Branch Manager"
    ///      分店：盛高店、经典店、慧谷店、鑫都店、北城店、广福店
    ///   匹配不到返回null
    /// </summary>
    private string? MatchRoleByOrgNamePath(string? namePath)
    {
        if (string.IsNullOrEmpty(namePath))
            return null;

        // 百货食品采购部 → Buyer
        if (namePath.Contains("百货食品采购部"))
            return "Buyer";

        // 财务部 → Finance Team Memeber
        if (namePath.Contains("财务部"))
            return "Finance Team Memeber";

        // 分店/XXX店/营运部 → Operation Team member
        var storeNames = new[] { "盛高店", "经典店", "慧谷店", "鑫都店", "北城店", "广福店" };
        if (storeNames.Any(s => namePath.Contains(s)) && namePath.Contains("营运部"))
            return "Branch Manager";

        return null;
    }
}
