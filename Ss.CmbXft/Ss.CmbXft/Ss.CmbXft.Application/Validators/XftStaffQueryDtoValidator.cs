using FluentValidation;
using Ss.CmbXft.Application.Dtos.Xft;

namespace Ss.CmbXft.Application.Validators;

/// <summary>
/// 员工查询请求验证器
/// </summary>
public class XftStaffQueryDtoValidator : BaseValidator<XftStaffQueryDto>
{
    public XftStaffQueryDtoValidator()
    {
        RuleForPaging();

        // 员工序号验证
        RuleFor(x => x.StaffSeq)
            .MaximumLength(50)
            .WithMessage("员工序号长度不能超过50个字符")
            .When(x => !string.IsNullOrEmpty(x.StaffSeq));

        // 员工姓名验证
        RuleFor(x => x.StfName)
            .MaximumLength(100)
            .WithMessage("员工姓名长度不能超过100个字符")
            .When(x => !string.IsNullOrEmpty(x.StfName));

        // 手机号验证
        RuleFor(x => x.MobileNumber)
            .MaximumLength(20)
            .WithMessage("手机号长度不能超过20个字符")
            .Matches(@"^1[3-9]\d{9}$")
            .WithMessage("手机号格式不正确")
            .When(x => !string.IsNullOrEmpty(x.MobileNumber));

        // 员工类型验证
        RuleFor(x => x.StfType)
            .MaximumLength(50)
            .WithMessage("员工类型长度不能超过50个字符")
            .When(x => !string.IsNullOrEmpty(x.StfType));

        // 员工状态验证
        RuleFor(x => x.StfStatus)
            .MaximumLength(50)
            .WithMessage("员工状态长度不能超过50个字符")
            .When(x => !string.IsNullOrEmpty(x.StfStatus));

        // 企业号验证
        RuleFor(x => x.EnterpriseId)
            .MaximumLength(50)
            .WithMessage("企业号长度不能超过50个字符")
            .When(x => !string.IsNullOrEmpty(x.EnterpriseId));
    }
}