using FluentValidation;
using Ss.CmbXft.Application.Dtos.Sserp;

namespace Ss.CmbXft.Application.Validators;

/// <summary>
/// Abp用户保存请求验证器
/// </summary>
public class AbpUserSaveDtoValidator : BaseValidator<AbpUserSaveDto>
{
    public AbpUserSaveDtoValidator()
    {
        // 用户ID验证
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("用户ID不能为空");

        // 用户名验证
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("用户名不能为空")
            .MaximumLength(256)
            .WithMessage("用户名长度不能超过256个字符")
            .Matches(@"^[a-zA-Z0-9_\-\.@]+$")
            .WithMessage("用户名只能包含字母、数字、下划线、连字符、点和@符号");

        // 规范化用户名验证
        RuleFor(x => x.NormalizedUserName)
            .NotEmpty()
            .WithMessage("规范化用户名不能为空")
            .MaximumLength(256)
            .WithMessage("规范化用户名长度不能超过256个字符");

        // 名字验证
        RuleFor(x => x.Name)
            .MaximumLength(64)
            .WithMessage("名字长度不能超过64个字符")
            .When(x => !string.IsNullOrEmpty(x.Name));

        // 姓氏验证
        RuleFor(x => x.Surname)
            .MaximumLength(64)
            .WithMessage("姓氏长度不能超过64个字符")
            .When(x => !string.IsNullOrEmpty(x.Surname));

        // 邮箱验证
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("邮箱不能为空")
            .MaximumLength(256)
            .WithMessage("邮箱长度不能超过256个字符")
            .EmailAddress()
            .WithMessage("邮箱格式不正确");

        // 规范化邮箱验证
        RuleFor(x => x.NormalizedEmail)
            .NotEmpty()
            .WithMessage("规范化邮箱不能为空")
            .MaximumLength(256)
            .WithMessage("规范化邮箱长度不能超过256个字符");

        // 密码哈希验证
        RuleFor(x => x.PasswordHash)
            .MaximumLength(256)
            .WithMessage("密码哈希长度不能超过256个字符")
            .When(x => !string.IsNullOrEmpty(x.PasswordHash));

        // 安全戳验证
        RuleFor(x => x.SecurityStamp)
            .NotEmpty()
            .WithMessage("安全戳不能为空")
            .MaximumLength(256)
            .WithMessage("安全戳长度不能超过256个字符");

        // 电话号码验证
        RuleFor(x => x.PhoneNumber)
            .MaximumLength(16)
            .WithMessage("电话号码长度不能超过16个字符")
            .Matches(@"^[0-9+\-\s]*$")
            .WithMessage("电话号码只能包含数字、+、-和空格")
            .When(x => !string.IsNullOrEmpty(x.PhoneNumber));

        // 访问失败次数验证
        RuleFor(x => x.AccessFailedCount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("访问失败次数不能小于0");

        // 扩展属性验证
        RuleFor(x => x.ExtraProperties)
            .MaximumLength(int.MaxValue)
            .WithMessage("扩展属性长度超出限制")
            .When(x => !string.IsNullOrEmpty(x.ExtraProperties));

        // 并发戳验证
        RuleFor(x => x.ConcurrencyStamp)
            .MaximumLength(40)
            .WithMessage("并发戳长度不能超过40个字符")
            .When(x => !string.IsNullOrEmpty(x.ConcurrencyStamp));

        // 分店ID验证
        RuleFor(x => x.BranchId)
            .GreaterThan(0)
            .WithMessage("分店ID必须大于0")
            .When(x => x.BranchId.HasValue);
    }
}