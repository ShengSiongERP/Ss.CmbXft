using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Ss.CmbXft.Application.Dtos;
using Ss.CmbXft.Common.Models;
using Ss.CmbXft.Sdk.Models.Organization;
using Ss.CmbXft.Sdk.Services;

namespace Ss.CmbXft.Application.Services;

/// <summary>
/// 组织应用服务实现
/// </summary>
public class OrganizationAppService : IOrganizationAppService
{
    private readonly IOrganizationService _organizationService;
    private readonly ILogger<OrganizationAppService> _logger;

    /// <summary>
    /// 初始化 <see cref="OrganizationAppService"/> 类的新实例
    /// </summary>
    /// <param name="organizationService">组织服务</param>
    /// <param name="logger">日志记录器</param>
    public OrganizationAppService(
        IOrganizationService organizationService,
        ILogger<OrganizationAppService> logger)
    {
        _organizationService = organizationService ?? throw new ArgumentNullException(nameof(organizationService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// 分页获取组织列表
    /// </summary>
    public async Task<PageResult<OrgOrgDto>> GetOrganizationListAsync(GetOrganizationListRequestDto input)
    {
        if (input == null)
        {
            throw new ArgumentNullException(nameof(input));
        }

        _logger.LogInformation("开始获取组织列表: Page={Page}, PageSize={PageSize}",
            input.CurrentPage, input.PageSize);

        try
        {
            // 构建SDK请求
            var request = new OrganizationListRequest
            {
                Codes = input.Codes,
                Ids = input.Ids,
                Keyword = input.Keyword,
                ParentId = input.ParentId,
                Status = input.Status,
                CurrentPage = input.CurrentPage,
                PageSize = input.PageSize,
                ExtOptions = input.ExtOptions
            };

            // 调用SDK服务
            var response = await _organizationService.GetOrganizationListAsync(request);

            // 检查响应
            if (response.ReturnCode != "SUC0000")
            {
                _logger.LogError("获取组织列表失败: ReturnCode={ReturnCode}, ErrorMsg={ErrorMsg}",
                    response.ReturnCode, response.ErrorMsg);
                throw new Exception($"获取组织列表失败: {response.ErrorMsg ?? response.ReturnCode}");
            }

            if (response.Body == null)
            {
                _logger.LogWarning("获取组织列表响应Body为空");
                return new PageResult<OrgOrgDto>(0, (int)input.CurrentPage, (int)input.PageSize, new List<OrgOrgDto>());
            }

            // 转换为DTO
            var organizationDtos = response.Body.Records?.Select(r => new OrgOrgDto
            {
                Id = r.Id,
                Code = r.Code,
                EffectiveDate = r.EffectiveDate,
                IdPath = r.IdPath,
                Name = r.Name,
                NamePath = r.NamePath,
                Number = r.Number,
                OrderNumber = r.OrderNumber,
                ParentCode = r.ParentCode,
                ParentId = r.ParentId,
                Remark = r.Remark,
                Status = r.Status,
                Type = r.Type,
                Leaders = r.Leaders?.Select(l => new OrganizationLeaderDto
                {
                    EnterpriseUserId = l.EnterpriseUserId,
                    Name = l.Name,
                    StaffId = l.StaffId,
                    StaffNum = l.StaffNum
                }).ToList(),
                Approvers = r.Approvers?.Select(a => new OrganizationApproverDto
                {
                    EnterpriseUserId = a.EnterpriseUserId,
                    Name = a.Name,
                    StaffId = a.StaffId,
                    StaffNum = a.StaffNum
                }).ToList(),
                ExtData = r.ExtData?.Select(e => new OrganizationExtDataDto
                {
                    Code = e.Code,
                    Name = e.Name,
                    FieldType = e.FieldType,
                    Value = e.Value
                }).ToList(),
                IsLeaf = r.IsLeaf
            }).ToList() ?? new List<OrgOrgDto>();

            _logger.LogInformation("成功获取组织列表: TotalSize={TotalSize}, Count={Count}",
                response.Body.TotalSize, organizationDtos.Count);

            return new PageResult<OrgOrgDto>(
                response.Body.TotalSize ?? 0,
                (int)input.CurrentPage,
                (int)input.PageSize,
                organizationDtos
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取组织列表异常");
            throw;
        }
    }
}
