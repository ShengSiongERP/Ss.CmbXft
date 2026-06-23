using FluentValidation;
using Ss.CmbXft.Common.Models;

namespace Ss.CmbXft.Application.Validators;

public interface IFluentValidation
{
}
/// <summary>
/// 验证器基类
/// </summary>
/// <typeparam name="T">要验证的类型</typeparam>
public abstract class BaseValidator<T> : AbstractValidator<T>, IFluentValidation where T : class
{
    /// <summary>
    /// 构造函数
    /// </summary>
    protected BaseValidator()
    {
        // 设置级联模式：遇到第一个错误时停止
        RuleLevelCascadeMode = CascadeMode.Stop;

        // 设置类级级联模式：遇到第一个错误时停止
        ClassLevelCascadeMode = CascadeMode.Stop;
    }
    /// <summary>
    /// 分页参数通用校验
    /// 适用于所有继承 PagedRequestBase 的 DTO
    /// </summary>
    protected void RuleForPaging()
    {
        // 只有 T 是 PagedRequestBase 才校验
        if (typeof(PagedRequestBase).IsAssignableFrom(typeof(T)))
        {
            RuleFor(x => (x as PagedRequestBase)!.PageIndex)
                .GreaterThan(0).WithMessage("页码必须大于 0")
                .When(x => x is PagedRequestBase);

            RuleFor(x => (x as PagedRequestBase)!.PageSize)
                .InclusiveBetween(1, 1000).WithMessage("每页条数必须在 1~1000 之间")
                .When(x => x is PagedRequestBase);
        }
    }
    /// <summary>
    /// 验证字符串不为空且不包含空白字符
    /// </summary>
    protected IRuleBuilderOptions<T, string> NotNullOrWhiteSpace(IRuleBuilder<T, string> ruleBuilder, string errorMessage = "该字段不能为空或只包含空白字符")
    {
        return ruleBuilder
            .NotEmpty().WithMessage(errorMessage)
            .Must(value => !string.IsNullOrWhiteSpace(value)).WithMessage(errorMessage);
    }

    /// <summary>
    /// 验证字符串长度
    /// </summary>
    protected IRuleBuilderOptions<T, string> LengthBetween(IRuleBuilder<T, string> ruleBuilder, int min, int max, string? errorMessage = null)
    {
        var message = errorMessage ?? $"长度必须在{min}到{max}个字符之间";
        return ruleBuilder
            .MinimumLength(min).WithMessage(message)
            .MaximumLength(max).WithMessage(message);
    }

    /// <summary>
    /// 验证数值范围
    /// </summary>
    protected IRuleBuilderOptions<T, TProperty> Range<TProperty>(IRuleBuilder<T, TProperty> ruleBuilder, TProperty min, TProperty max, string? errorMessage = null)
        where TProperty : IComparable<TProperty>, IComparable
    {
        var message = errorMessage ?? $"必须在{min}到{max}之间";
        return ruleBuilder.InclusiveBetween(min, max).WithMessage(message);
    }
    /// <summary>
    /// 最小值校验（大于等于 min）
    /// </summary>
    protected IRuleBuilderOptions<T, TProp> Min<TProp>(
        IRuleBuilderInitial<T, TProp> ruleBuilder,
        TProp minValue,
        string? message = null)
        where TProp : IComparable<TProp>, IComparable
    {
        var msg = message ?? $"不能小于 {minValue}";
        return ruleBuilder.GreaterThanOrEqualTo(minValue).WithMessage(msg);
    }

    /// <summary>
    /// 最大值校验（小于等于 max）
    /// </summary>
    protected IRuleBuilderOptions<T, TProp> Max<TProp>(
        IRuleBuilderInitial<T, TProp> ruleBuilder,
        TProp maxValue,
        string? message = null)
        where TProp : IComparable<TProp>, IComparable
    {
        var msg = message ?? $"不能大于 {maxValue}";
        return ruleBuilder.LessThanOrEqualTo(maxValue).WithMessage(msg);
    }
    /// <summary>
    /// 验证枚举值
    /// </summary>
    protected IRuleBuilderOptions<T, TEnum> IsValidEnum<TEnum>(IRuleBuilder<T, TEnum> ruleBuilder, string? errorMessage = null)
        where TEnum : struct, Enum
    {
        var message = errorMessage ?? $"枚举值无效";
        return ruleBuilder
            .Must(value => Enum.IsDefined(typeof(TEnum), value))
            .WithMessage(message);
    }
}

///// <summary>
///// 分页请求基类验证器
///// </summary>
//public class PagedRequestBaseValidator : BaseValidator<PagedRequestBase>
//{
//    public PagedRequestBaseValidator()
//    {
//        // 页码验证
//        RuleFor(x => x.PageIndex)
//            .GreaterThan(0).WithMessage("页码必须大于0")
//            .LessThanOrEqualTo(100000).WithMessage("页码不能超过100000");

//        // 每页大小验证
//        RuleFor(x => x.PageSize)
//            .GreaterThan(0).WithMessage("每页数量必须大于0")
//            .LessThanOrEqualTo(1000).WithMessage("每页数量不能超过1000");
//    }
//}