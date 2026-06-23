using FluentValidation;
using Ss.CmbXft.Application.Dtos.Sserp;

namespace Ss.CmbXft.Application.Validators;

/// <summary>
/// Abp用户角色关联保存请求验证器
/// </summary>
public class AbpUserRoleSaveDtoValidator : BaseValidator<AbpUserRoleSaveDto>
{
    public AbpUserRoleSaveDtoValidator()
    {
        // 用户ID验证
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("用户ID不能为空");

        // 角色ID验证
        RuleFor(x => x.RoleId)
            .NotEmpty()
            .WithMessage("角色ID不能为空");
    }
}

/// <summary>
/// Abp用户角色关联查询请求验证器
/// </summary>
public class AbpUserRoleQueryDtoValidator : BaseValidator<AbpUserRoleQueryDto>
{
    public AbpUserRoleQueryDtoValidator()
    {
        RuleForPaging();
    }
}

/// <summary>
/// Abp用户角色关联批量分配请求验证器
/// </summary>
public class AbpUserRoleBatchAssignDtoValidator : BaseValidator<AbpUserRoleBatchAssignDto>
{
    public AbpUserRoleBatchAssignDtoValidator()
    {
        // 用户ID验证
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("用户ID不能为空");

        // 角色ID列表验证
        RuleFor(x => x.RoleIds)
            .NotNull()
            .WithMessage("角色ID列表不能为空")
            .Must(x => x.Count > 0)
            .WithMessage("角色ID列表至少包含一个角色ID");
    }
}