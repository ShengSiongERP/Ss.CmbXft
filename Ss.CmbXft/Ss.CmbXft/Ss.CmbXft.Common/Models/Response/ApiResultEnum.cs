namespace Ss.CmbXft.Common.Models;

/// <summary>
/// 统一响应状态码
/// <para>
/// 编号规则：
/// <list type="bullet">
///   <item>200       成功（HTTP 200 OK）</item>
///   <item>400 / 4xx 客户端错误（参数、认证、权限、资源等）</item>
///   <item>422 / 422xx 业务错误（HTTP 422 Unprocessable Entity）</item>
///   <item>500 / 500xx 服务端错误（HTTP 500 Internal Server Error）</item>
///   <item>502 / 504 第三方服务错误（HTTP 502 Bad Gateway / 504 Gateway Timeout）</item>
/// </list>
/// 子码规则：基础码 + 两位序号，如 40101 = 401 下的第 1 个子码
/// </para>
/// </summary>
public enum ApiResultEnum
{
    #region 成功

    /// <summary>操作成功（HTTP 200 OK）</summary>
    Success = 200,

    #endregion

    #region 客户端错误 (HTTP 4xx)

    /// <summary>通用客户端失败（HTTP 400 Bad Request）</summary>
    Fail = 400,

    /// <summary>参数验证失败（400 子码）</summary>
    ValidateError = 40001,

    /// <summary>未登录或登录已过期（HTTP 401 Unauthorized）</summary>
    Unauthorized = 401,

    /// <summary>Token 已过期（401 子码）</summary>
    TokenExpired = 40101,

    /// <summary>权限不足（HTTP 403 Forbidden）</summary>
    Forbidden = 403,

    /// <summary>资源不存在（HTTP 404 Not Found）</summary>
    NotFound = 404,

    /// <summary>请求方法不允许（HTTP 405 Method Not Allowed）</summary>
    MethodNotAllowed = 405,

    /// <summary>资源冲突，如重复创建（HTTP 409 Conflict）</summary>
    Conflict = 409,

    /// <summary>请求过于频繁（HTTP 429 Too Many Requests）</summary>
    TooManyRequests = 429,

    #endregion

    #region 业务错误 (HTTP 422 Unprocessable Entity)

    /// <summary>通用业务错误（HTTP 422 Unprocessable Entity）</summary>
    BusinessError = 422,

    /// <summary>数据状态异常，如已审核不可修改（422 子码）</summary>
    DataStatusError = 42201,

    /// <summary>数据版本冲突，如并发修改（422 子码）</summary>
    DataConflict = 42202,

    /// <summary>操作被拒绝，如流程未完成（422 子码）</summary>
    OperationRejected = 42203,

    #endregion

    #region 服务端错误 (HTTP 5xx)

    /// <summary>通用服务端错误（HTTP 500 Internal Server Error）</summary>
    ServerError = 500,

    /// <summary>数据库操作异常（500 子码）</summary>
    DbError = 50001,

    /// <summary>服务暂不可用（HTTP 503 Service Unavailable）</summary>
    ServiceUnavailable = 503,

    #endregion

    #region 第三方服务错误 (HTTP 502 / 504)

    /// <summary>通用第三方服务错误（HTTP 502 Bad Gateway）</summary>
    ThirdPartyError = 502,

    /// <summary>第三方服务超时（HTTP 504 Gateway Timeout）</summary>
    ThirdPartyTimeout = 504,

    /// <summary>第三方服务返回异常（502 子码）</summary>
    ThirdPartyResponseError = 50201,

    #endregion
}

/// <summary>
/// ApiResultEnum 扩展方法
/// </summary>
public static class ApiResultEnumExtensions
{
    /// <summary>
    /// 获取状态码字符串
    /// </summary>
    public static string GetCode(this ApiResultEnum value) => ((int)value).ToString();

    /// <summary>
    /// 获取默认消息
    /// </summary>
    public static string GetMsg(this ApiResultEnum value) => value switch
    {
        // 成功
        ApiResultEnum.Success => "操作成功",

        // 客户端错误
        ApiResultEnum.Fail => "操作失败",
        ApiResultEnum.ValidateError => "参数验证失败",
        ApiResultEnum.Unauthorized => "请先登录",
        ApiResultEnum.TokenExpired => "登录已过期，请重新登录",
        ApiResultEnum.Forbidden => "权限不足，无法访问",
        ApiResultEnum.NotFound => "资源不存在",
        ApiResultEnum.MethodNotAllowed => "请求方法不允许",
        ApiResultEnum.Conflict => "资源冲突",
        ApiResultEnum.TooManyRequests => "请求过于频繁，请稍后重试",

        // 业务错误
        ApiResultEnum.BusinessError => "业务处理失败",
        ApiResultEnum.DataStatusError => "数据状态异常，请刷新后重试",
        ApiResultEnum.DataConflict => "数据已被他人修改，请刷新后重试",
        ApiResultEnum.OperationRejected => "操作被拒绝",

        // 服务端错误
        ApiResultEnum.ServerError => "服务器繁忙，请稍后重试",
        ApiResultEnum.DbError => "数据操作异常",
        ApiResultEnum.ServiceUnavailable => "服务暂不可用",

        // 第三方服务错误
        ApiResultEnum.ThirdPartyError => "第三方服务异常",
        ApiResultEnum.ThirdPartyTimeout => "第三方服务响应超时",
        ApiResultEnum.ThirdPartyResponseError => "第三方服务返回异常",

        _ => "未知错误"
    };

    /// <summary>
    /// 简写：获取状态码字符串
    /// </summary>
    public static string Code(this ApiResultEnum value) => value.GetCode();

    /// <summary>
    /// 简写：获取默认消息
    /// </summary>
    public static string Msg(this ApiResultEnum value) => value.GetMsg();
}

/// <summary>
/// int / string → ApiResultEnum 转换
/// </summary>
public static class ApiResultEnumParseExtensions
{
    /// <summary>
    /// 将 int 值转换为 ApiResultEnum，无效值返回 <see cref="ApiResultEnum.Success"/>
    /// </summary>
    public static ApiResultEnum ToApiResultEnum(this int value)
    {
        return Enum.IsDefined(typeof(ApiResultEnum), value) ? (ApiResultEnum)value : default;
    }

    /// <summary>
    /// 将状态码字符串转换为 ApiResultEnum，无效值返回 <see cref="ApiResultEnum.Success"/>
    /// </summary>
    public static ApiResultEnum ToApiResultEnum(this string? code)
    {
        return int.TryParse(code, out int value) ? value.ToApiResultEnum() : default;
    }
}
