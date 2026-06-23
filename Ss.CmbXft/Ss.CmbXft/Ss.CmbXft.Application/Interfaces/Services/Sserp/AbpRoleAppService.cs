using Mapster;
using Microsoft.Extensions.Logging;
using Ss.CmbXft.Application.Dtos.Sserp;
using Ss.CmbXft.Common.Models;
using Ss.CmbXft.Domain.Entities.Sserp;
using Ss.CmbXft.Domain.Repositories;

namespace Ss.CmbXft.Application.Services;

/// <summary>
/// Abp角色应用服务实现
/// </summary>
public class AbpRoleAppService : IAbpRoleAppService
{
    private readonly ISserpRepository<AbpRole, Guid> _roleRepository;
    private readonly ILogger<AbpRoleAppService> _logger;

    public AbpRoleAppService(
        ISserpRepository<AbpRole, Guid> roleRepository,
        ILogger<AbpRoleAppService> logger)
    {
        _roleRepository = roleRepository;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<PageResult<AbpRoleDto>> GetPageAsync(AbpRoleQueryDto query)
    {
        ArgumentNullException.ThrowIfNull(query);

        // 构建过滤表达式
        var result = await _roleRepository.GetPagedListAsync(
            predicate: r =>
                (string.IsNullOrWhiteSpace(query.Name) || r.Name.Contains(query.Name)) &&
                (string.IsNullOrWhiteSpace(query.NormalizedName) || r.NormalizedName.Contains(query.NormalizedName)) &&
                (!query.IsDefault.HasValue || r.IsDefault == query.IsDefault.Value) &&
                (!query.IsStatic.HasValue || r.IsStatic == query.IsStatic.Value) &&
                (!query.IsPublic.HasValue || r.IsPublic == query.IsPublic.Value),
            sorting: "Name ASC",
            pageIndex: query.PageIndex,
            pageSize: query.PageSize
        );

        var items = result.Items.Adapt<List<AbpRoleDto>>();
        return PageResult<AbpRoleDto>.Create(result.TotalCount, result.PageIndex, result.PageSize, items);
    }

    /// <inheritdoc />
    public async Task<List<AbpRoleDto>> GetListAsync()
    {
        var roles = await _roleRepository.GetListAsync(orderBy: null);

        return roles.OrderBy(r => r.Name).Adapt<List<AbpRoleDto>>();
    }

    /// <inheritdoc />
    public async Task<AbpRoleDto?> GetAsync(Guid id)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        if (role == null) return null;

        return role.Adapt<AbpRoleDto>();
    }
}