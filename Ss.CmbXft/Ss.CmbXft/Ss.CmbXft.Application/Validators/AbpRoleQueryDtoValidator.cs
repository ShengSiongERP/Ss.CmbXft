using FluentValidation;
using Ss.CmbXft.Application.Dtos.Sserp;

namespace Ss.CmbXft.Application.Validators;

/// <summary>
/// Abp角色查询请求验证器
/// </summary>
public class AbpRoleQueryDtoValidator : BaseValidator<AbpRoleQueryDto>
{
    public AbpRoleQueryDtoValidator()
    {
        // 角色名称验证（可选）
        RuleFor(x => x.Name)
            .MaximumLength(256)
            .WithMessage("角色名称长度不能超过256个字符")
            .When(x => !string.IsNullOrEmpty(x.Name));

        // 规范化角色名称验证（可选）
        RuleFor(x => x.NormalizedName)
            .MaximumLength(256)
            .WithMessage("规范化角色名称长度不能超过256个字符")
            .When(x => !string.IsNullOrEmpty(x.NormalizedName));

        // 分页参数验证
        RuleFor(x => x.PageIndex)
            .GreaterThanOrEqualTo(1)
            .WithMessage("页码必须大于或等于1");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithMessage("每页数量必须大于或等于1")
            .LessThanOrEqualTo(100)
            .WithMessage("每页数量不能超过100");
    }
}