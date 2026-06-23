using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Ss.CmbXft.Sdk.Models.Staff;

/// <summary>
/// 查询方式枚举
/// </summary>
public enum QueryMethod
{
    /// <summary>
    /// 等值查询
    /// </summary>
    Equal,

    /// <summary>
    /// 模糊查询
    /// </summary>
    Fuzzy,

    /// <summary>
    /// 范围查询
    /// </summary>
    Range,

    /// <summary>
    /// 日期查询
    /// </summary>
    Date
}

/// <summary>
/// 查询条件
/// </summary>
public class QueryFilter
{
    /// <summary>
    /// 查询条件的字段id，例如根据员工序号查询，则传stfSeq即可
    /// </summary>
    [JsonProperty("fieldKey")]
    public string FieldKey { get; set; } = string.Empty;

    /// <summary>
    /// 查询方式
    /// </summary>
    [JsonProperty("fieldQueryMethod")]
    public string FieldQueryMethod { get; set; } = string.Empty;

    /// <summary>
    /// 查询条件字段值
    /// </summary>
    [JsonProperty("fieldValue")]
    public string FieldValue { get; set; } = string.Empty;

    /// <summary>
    /// 初始化 <see cref="QueryFilter"/> 类的新实例
    /// </summary>
    public QueryFilter()
    {
    }

    /// <summary>
    /// 初始化 <see cref="QueryFilter"/> 类的新实例
    /// </summary>
    /// <param name="fieldKey">字段键</param>
    /// <param name="method">查询方式</param>
    /// <param name="fieldValue">字段值</param>
    public QueryFilter(string fieldKey, QueryMethod method, string fieldValue)
    {
        FieldKey = fieldKey;
        FieldQueryMethod = method.ToString().ToUpper();
        FieldValue = fieldValue;
    }
}

/// <summary>
/// 查询结果类型
/// </summary>
public class QueryResultType
{
    /// <summary>
    /// 查询类型：FIELD-按字段查询；GROUP-按分组查询
    /// </summary>
    [JsonProperty("queryType")]
    public string QueryType { get; set; } = string.Empty;

    /// <summary>
    /// 当查询类型为GROUP时，请在该参数内传入要查询的分组key
    /// </summary>
    [JsonProperty("queryClassKeyList")]
    public List<string>? QueryClassKeyList { get; set; }

    /// <summary>
    /// 当查询类型为FIELD时，请在该参数内传入要查询的字段id
    /// </summary>
    [JsonProperty("queryFieldList")]
    public List<string>? QueryFieldList { get; set; }

    /// <summary>
    /// 初始化 <see cref="QueryResultType"/> 类的新实例
    /// </summary>
    public QueryResultType()
    {
    }

    /// <summary>
    /// 创建分组查询
    /// </summary>
    /// <param name="classKeys">分组键列表</param>
    /// <returns>查询结果类型</returns>
    public static QueryResultType CreateGroupQuery(params string[] classKeys)
    {
        return new QueryResultType
        {
            QueryType = "GROUP",
            QueryClassKeyList = classKeys.ToList()
        };
    }

    /// <summary>
    /// 创建字段查询
    /// </summary>
    /// <param name="fieldKeys">字段键列表</param>
    /// <returns>查询结果类型</returns>
    public static QueryResultType CreateFieldQuery(params string[] fieldKeys)
    {
        return new QueryResultType
        {
            QueryType = "FIELD",
            QueryFieldList = fieldKeys.ToList()
        };
    }
}

/// <summary>
/// 员工信息查询请求
/// </summary>
public class StaffQueryRequest
{
    /// <summary>
    /// 查询条件对象，最多支持10个条件的与查询
    /// </summary>
    [JsonProperty("queryFilterList")]
    public List<QueryFilter> QueryFilterList { get; set; } = new();

    /// <summary>
    /// 查询类型
    /// </summary>
    [JsonProperty("queryResultType")]
    public QueryResultType QueryResultType { get; set; } = new();

    /// <summary>
    /// 当前页，起始从1开始
    /// </summary>
    [JsonProperty("currentPage")]
    public int? CurrentPage { get; set; } = 1;

    /// <summary>
    /// 每页查询数量，最大支持1000笔数据
    /// </summary>
    [JsonProperty("pageSize")]
    public int? PageSize { get; set; } = 10;

    /// <summary>
    /// 验证请求参数
    /// </summary>
    /// <param name="errors">输出错误信息列表</param>
    /// <returns>请求参数是否有效</returns>
    public bool Validate(out List<string> errors)
    {
        errors = new List<string>();

        if (QueryResultType?.QueryType == "GROUP" &&
            (QueryResultType.QueryClassKeyList == null || QueryResultType.QueryClassKeyList.Count == 0))
            errors.Add("分组查询时必须指定查询分组列表");

        if (QueryResultType?.QueryType == "FIELD" &&
            (QueryResultType.QueryFieldList == null || QueryResultType.QueryFieldList.Count == 0))
            errors.Add("字段查询时必须指定查询字段列表");

        if (CurrentPage < 1)
            errors.Add("当前页必须大于0");

        if (PageSize < 1 || PageSize > 2000)
            errors.Add("每页查询数量必须在1-2000之间");

        return errors.Count == 0;
    }
}
