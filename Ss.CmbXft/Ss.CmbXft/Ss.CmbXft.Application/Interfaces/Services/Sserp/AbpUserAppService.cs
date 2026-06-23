using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Ss.CmbXft.Application.Dtos.Sserp;
using Ss.CmbXft.Common.Models;
using Ss.CmbXft.Domain.Entities.Sserp;
using Ss.CmbXft.Domain.Repositories;

namespace Ss.CmbXft.Application.Services;

/// <summary>
/// Abp用户应用服务实现
/// </summary>
public class AbpUserAppService : IAbpUserAppService
{
    private readonly ISserpRepository<AbpUser, Guid> _userRepository;
    private readonly ISserpUnitOfWork _unitOfWork;
    private readonly ILogger<AbpUserAppService> _logger;

    public AbpUserAppService(
        ISserpRepository<AbpUser, Guid> userRepository,
        ISserpUnitOfWork unitOfWork,
        ILogger<AbpUserAppService> logger)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<PageResult<AbpUserDto>> GetPageAsync(AbpUserQueryDto query)
    {
        ArgumentNullException.ThrowIfNull(query);

        var result = await _userRepository.GetPagedListAsync(
            predicate: u =>
                (string.IsNullOrWhiteSpace(query.UserName) || u.UserName.Contains(query.UserName)) &&
                (string.IsNullOrWhiteSpace(query.Email) || u.Email.Contains(query.Email)) &&
                (string.IsNullOrWhiteSpace(query.Name) || (u.Name != null && u.Name.Contains(query.Name))) &&
                (string.IsNullOrWhiteSpace(query.Surname) || (u.Surname != null && u.Surname.Contains(query.Surname))) &&
                (string.IsNullOrWhiteSpace(query.PhoneNumber) || (u.PhoneNumber != null && u.PhoneNumber.Contains(query.PhoneNumber))) &&
                (!query.IsActive.HasValue || u.IsActive == query.IsActive.Value) &&
                (!query.BranchId.HasValue || u.BranchId == query.BranchId.Value) &&
                (!query.IsDeleted.HasValue || u.IsDeleted == query.IsDeleted.Value),
            sorting: "CreationTime DESC",
            pageIndex: query.PageIndex,
            pageSize: query.PageSize
        );

        var items = result.Items.Select(u => MapToDto(u)).ToList();
        return PageResult<AbpUserDto>.Create(result.TotalCount, result.PageIndex, result.PageSize, items);
    }

    /// <inheritdoc />
    public async Task<List<AbpUserDto>> GetListAsync()
    {
        var users = await _userRepository.GetListAsync(orderBy: null);
        return users.Select(u => MapToDto(u)).ToList();
    }

    /// <inheritdoc />
    public async Task<AbpUserDto?> GetAsync(Guid id)
    {
        // AbpUser 主键是 Id，Guid 类型，需要使用 isTracking: true 走 FindAsync 路径
        var entity = await _userRepository.GetByIdAsync(id, isTracking: true);
        if (entity == null) return null;
        return MapToDto(entity);
    }

    /// <inheritdoc />
    public async Task<AbpUserDto> CreateAsync(AbpUserSaveDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        // 检查用户ID唯一性
        var exists = await _userRepository.FirstOrDefaultAsync(u => u.Id == dto.Id);
        if (exists != null)
        {
            throw new InvalidOperationException($"用户ID '{dto.Id}' 已存在");
        }

        var entity = new AbpUser
        {
            Id = dto.Id,
            TenantId = dto.TenantId,
            UserName = dto.UserName.Trim(),
            NormalizedUserName = dto.NormalizedUserName.Trim(),
            Name = dto.Name?.Trim(),
            Surname = dto.Surname?.Trim(),
            Email = dto.Email.Trim(),
            NormalizedEmail = dto.NormalizedEmail.Trim(),
            EmailConfirmed = dto.EmailConfirmed,
            PasswordHash = dto.PasswordHash,
            SecurityStamp = dto.SecurityStamp.Trim(),
            IsExternal = dto.IsExternal,
            PhoneNumber = dto.PhoneNumber?.Trim(),
            PhoneNumberConfirmed = dto.PhoneNumberConfirmed,
            TwoFactorEnabled = dto.TwoFactorEnabled,
            LockoutEnd = dto.LockoutEnd,
            LockoutEnabled = dto.LockoutEnabled,
            AccessFailedCount = dto.AccessFailedCount,
            ExtraProperties = dto.ExtraProperties,
            ConcurrencyStamp = dto.ConcurrencyStamp?.Trim(),
            CreationTime = DateTime.UtcNow,
            BranchId = dto.BranchId,
            IsFirstTimeLogin = dto.IsFirstTimeLogin,
            IsActive = dto.IsActive
        };

        await _userRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        return MapToDto(entity);
    }

    /// <inheritdoc />
    public async Task<AbpUserDto> UpdateAsync(Guid id, AbpUserSaveDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var entity = await _userRepository.GetByIdAsync(id, isTracking: true);
        if (entity == null)
        {
            throw new InvalidOperationException($"用户ID '{id}' 不存在");
        }

        // 更新字段
        entity.TenantId = dto.TenantId;
        entity.UserName = dto.UserName.Trim();
        entity.NormalizedUserName = dto.NormalizedUserName.Trim();
        entity.Name = dto.Name?.Trim();
        entity.Surname = dto.Surname?.Trim();
        entity.Email = dto.Email.Trim();
        entity.NormalizedEmail = dto.NormalizedEmail.Trim();
        entity.EmailConfirmed = dto.EmailConfirmed;
        entity.PasswordHash = dto.PasswordHash;
        entity.SecurityStamp = dto.SecurityStamp.Trim();
        entity.IsExternal = dto.IsExternal;
        entity.PhoneNumber = dto.PhoneNumber?.Trim();
        entity.PhoneNumberConfirmed = dto.PhoneNumberConfirmed;
        entity.TwoFactorEnabled = dto.TwoFactorEnabled;
        entity.LockoutEnd = dto.LockoutEnd;
        entity.LockoutEnabled = dto.LockoutEnabled;
        entity.AccessFailedCount = dto.AccessFailedCount;
        entity.ExtraProperties = dto.ExtraProperties;
        entity.ConcurrencyStamp = dto.ConcurrencyStamp?.Trim();
        entity.LastModificationTime = DateTime.UtcNow;
        entity.BranchId = dto.BranchId;
        entity.IsFirstTimeLogin = dto.IsFirstTimeLogin;
        entity.IsActive = dto.IsActive;

        _userRepository.Update(entity);
        await _unitOfWork.SaveChangesAsync();

        return MapToDto(entity);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Guid id)
    {
        var entity = await _userRepository.GetByIdAsync(id, isTracking: true);
        if (entity == null)
        {
            throw new InvalidOperationException($"用户ID '{id}' 不存在");
        }

        await _userRepository.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task DeleteBatchAsync(List<Guid> ids)
    {
        ArgumentNullException.ThrowIfNull(ids);
        if (ids.Count == 0)
        {
            throw new ArgumentException("请选择要删除的用户");
        }

        foreach (var id in ids)
        {
            var entity = await _userRepository.GetByIdAsync(id, isTracking: true);
            if (entity != null)
            {
                await _userRepository.DeleteAsync(id);
            }
        }

        await _unitOfWork.SaveChangesAsync();
    }

    /// <summary>
    /// 实体映射到 DTO
    /// </summary>
    private static AbpUserDto MapToDto(AbpUser entity)
    {
        return new AbpUserDto
        {
            Id = entity.Id,
            TenantId = entity.TenantId,
            UserName = entity.UserName,
            NormalizedUserName = entity.NormalizedUserName,
            Name = entity.Name,
            Surname = entity.Surname,
            Email = entity.Email,
            NormalizedEmail = entity.NormalizedEmail,
            EmailConfirmed = entity.EmailConfirmed,
            PasswordHash = entity.PasswordHash,
            SecurityStamp = entity.SecurityStamp,
            IsExternal = entity.IsExternal,
            PhoneNumber = entity.PhoneNumber,
            PhoneNumberConfirmed = entity.PhoneNumberConfirmed,
            TwoFactorEnabled = entity.TwoFactorEnabled,
            LockoutEnd = entity.LockoutEnd,
            LockoutEnabled = entity.LockoutEnabled,
            AccessFailedCount = entity.AccessFailedCount,
            ExtraProperties = entity.ExtraProperties,
            ConcurrencyStamp = entity.ConcurrencyStamp,
            CreationTime = entity.CreationTime,
            CreatorId = entity.CreatorId,
            LastModificationTime = entity.LastModificationTime,
            LastModifierId = entity.LastModifierId,
            IsDeleted = entity.IsDeleted,
            DeleterId = entity.DeleterId,
            DeletionTime = entity.DeletionTime,
            BranchId = entity.BranchId,
            IsFirstTimeLogin = entity.IsFirstTimeLogin,
            IsActive = entity.IsActive
        };
    }
}