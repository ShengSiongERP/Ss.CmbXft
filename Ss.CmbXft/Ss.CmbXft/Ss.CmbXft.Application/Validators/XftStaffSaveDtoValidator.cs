using FluentValidation;
using Ss.CmbXft.Application.Dtos.Xft;

namespace Ss.CmbXft.Application.Validators;

/// <summary>
/// 员工保存请求验证器
/// </summary>
public class XftStaffSaveDtoValidator : BaseValidator<XftStaffSaveDto>
{
    public XftStaffSaveDtoValidator()
    {
        // 员工姓名验证
        RuleFor(x => x.StfName)
            .NotEmpty()
            .WithMessage("员工姓名不能为空")
            .MaximumLength(100)
            .WithMessage("员工姓名长度不能超过100个字符")
            .MinimumLength(2)
            .WithMessage("员工姓名至少需要2个字符")
            .Must(name => !string.IsNullOrWhiteSpace(name))
            .WithMessage("员工姓名不能只包含空白字符");

        // 手机号验证
        RuleFor(x => x.MobileNumber)
            .NotEmpty()
            .WithMessage("手机号不能为空")
            .MaximumLength(20)
            .WithMessage("手机号长度不能超过20个字符")
            .Matches(@"^1[3-9]\d{9}$")
            .WithMessage("手机号格式不正确");

        // 员工序号验证
        RuleFor(x => x.StaffSeq)
            .NotEmpty()
            .WithMessage("员工序号不能为空")
            .MaximumLength(50)
            .WithMessage("员工序号长度不能超过50个字符");

        // 企业号验证
        RuleFor(x => x.EnterpriseId)
            .MaximumLength(50)
            .WithMessage("企业号长度不能超过50个字符")
            .When(x => !string.IsNullOrEmpty(x.EnterpriseId));

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

        // 员工JSON验证
        RuleFor(x => x.StaffJson)
            .MaximumLength(10000)
            .WithMessage("员工JSON数据长度不能超过10000个字符")
            .When(x => !string.IsNullOrEmpty(x.StaffJson));
    }
}