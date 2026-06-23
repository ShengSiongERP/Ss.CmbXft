using FluentValidation;
using Ss.CmbXft.Application.Dtos.Sserp;

namespace Ss.CmbXft.Application.Validators;

/// <summary>
/// Abp用户查询请求验证器
/// </summary>
public class AbpUserQueryDtoValidator : BaseValidator<AbpUserQueryDto>
{
    public AbpUserQueryDtoValidator()
    {
        RuleForPaging();

        // 用户名验证
        RuleFor(x => x.UserName)
            .MaximumLength(256)
            .WithMessage("用户名长度不能超过256个字符")
            .When(x => !string.IsNullOrEmpty(x.UserName));

        // 邮箱验证
        RuleFor(x => x.Email)
            .MaximumLength(256)
            .WithMessage("邮箱长度不能超过256个字符")
            .EmailAddress()
            .WithMessage("邮箱格式不正确")
            .When(x => !string.IsNullOrEmpty(x.Email));

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

        // 电话号码验证
        RuleFor(x => x.PhoneNumber)
            .MaximumLength(16)
            .WithMessage("电话号码长度不能超过16个字符")
            .Matches(@"^[0-9+\-\s]*$")
            .WithMessage("电话号码只能包含数字、+、-和空格")
            .When(x => !string.IsNullOrEmpty(x.PhoneNumber));

        // 分店ID验证
        RuleFor(x => x.BranchId)
            .GreaterThan(0)
            .WithMessage("分店ID必须大于0")
            .When(x => x.BranchId.HasValue);
    }
}