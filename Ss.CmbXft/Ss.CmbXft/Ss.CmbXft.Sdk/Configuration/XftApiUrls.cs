namespace Ss.CmbXft.Sdk.Configuration;

/// <summary>
/// API URL信息
/// </summary>
public sealed class ApiUrl
{
    /// <summary>
    /// 测试环境URL路径
    /// </summary>
    public string TestPath { get; }

    /// <summary>
    /// 生产环境URL路径
    /// </summary>
    public string ProductionPath { get; }

    /// <summary>
    /// 初始化 <see cref="ApiUrl"/> 类的新实例
    /// </summary>
    /// <param name="testPath">测试环境URL路径</param>
    /// <param name="productionPath">生产环境URL路径</param>
    public ApiUrl(string testPath, string productionPath)
    {
        TestPath = testPath;
        ProductionPath = productionPath;
    }

    /// <summary>
    /// 根据环境获取URL路径
    /// </summary>
    /// <param name="environment">环境类型</param>
    /// <returns>URL路径</returns>
    public string Path(XftEnvironment environment)
    {
        return environment == XftEnvironment.Test ? TestPath : ProductionPath;
    }
}

/// <summary>
/// 薪福通接口地址
/// </summary>
public static class XftApiUrls
{
    /// <summary>
    /// 公共服务
    /// </summary>
    public static class Common
    {
    }

    /// <summary>
    /// 组织管理
    /// </summary>
    public static class Organization
    {
        /// <summary>
        /// 获取组织列表
        /// </summary>
        public static readonly ApiUrl GetOrganizationList = new ApiUrl(
            "https://api.cmburl.cn:8065/ORG/orgqry/xft-service-organization/org/v1/get/page",
            "https://api.cmbchina.com/ORG/orgqry/xft-service-organization/org/v1/get/page"
        );
    }
    /// <summary>
    /// 权限管理
    /// </summary>
    public static class Auth
    {
        /// <summary>
        /// 角色列表
        /// </summary>
        public static readonly ApiUrl RoleList = new ApiUrl(
            "https://api.cmburl.cn:8065/aut/aut/aut/role/list",
            "https://api.cmbchina.com/aut/OPSTAMOD/aut/role/list"
        );
    }
    /// <summary>
    /// 员工管理接口
    /// </summary>
    public static class Staff
    {
        /// <summary>
        /// 员工信息查询
        /// </summary>
        public static readonly ApiUrl Query = new ApiUrl(
            "https://api.cmburl.cn:8065/xfthrm/hrm2uat/xft-employeeprofile/employee/external/api/query/staffInfo",
            "https://api.cmbchina.com/hrm/hrm2/xft-employeeprofile/employee/external/api/query/staffInfo"
        );

        /// <summary>
        /// 员工信息新增
        /// </summary>
        public static readonly ApiUrl Create = new ApiUrl(
            "https://api.cmburl.cn:8065/xfthrm/hrm2uat/xft-employeeprofile/employee/staffGeneralApi/addStaff",
            "https://api.cmbchina.com/hrm/hrm2/xft-employeeprofile/employee/staffGeneralApi/addStaff"
        );

        /// <summary>
        /// 员工信息修改
        /// </summary>
        public static readonly ApiUrl Update = new ApiUrl(
            "https://api.cmburl.cn:8065/xfthrm/hrm2uat/xft-employeeprofile/employee/staff-general-api/modify-staff",
            "https://api.cmbchina.com/hrm/hrm2/xft-employeeprofile/employee/staff-general-api/modify-staff"
        );

        /// <summary>
        /// 员工信息删除
        /// </summary>
        public static readonly ApiUrl Delete = new ApiUrl(
            "https://api.cmburl.cn:8065/xfthrm/hrm2uat/xft-employeeprofile/openapi/employee/staff-general-api/remove-staff",
            "https://api.cmbchina.com/hrm/hrm2/xft-employeeprofile/openapi/employee/staff-general-api/remove-staff"
        );

        /// <summary>
        /// 员工数据字典通用查询
        /// </summary>
        public static readonly ApiUrl DataDictionary = new ApiUrl(
            "https://api.cmburl.cn:8065/xfthrm/hrm2uat/xft-employeeprofile/openapi/employee/setting-api/find-data-dictionary",
            "https://api.cmbchina.com/hrm/hrm2/xft-employeeprofile/openapi/employee/setting-api/find-data-dictionary"
        );
    }

    /// <summary>
    /// 考勤管理接口
    /// </summary>
    public static class Attendance
    {
        /// <summary>
        /// 查询日考勤统计
        /// </summary>
        public static readonly ApiUrl DayQuery = new ApiUrl(
            "https://api.cmburl.cn:8065/atd/atduat/xft-atn/sta-result/day/query",
            "https://api.cmbchina.com/atd/prd/xft-atn/sta-result/day/query"
        );

        /// <summary>
        /// 查询月考勤统计
        /// </summary>
        public static readonly ApiUrl MonthQuery = new ApiUrl(
            "https://api.cmburl.cn:8065/atd/atduat/xft-atn/sta-result/month/query",
            "https://api.cmbchina.com/atd/prd/xft-atn/sta-result/month/query"
        );

        /// <summary>
        /// 查询考勤统计项
        /// </summary>
        public static readonly ApiUrl ItemQuery = new ApiUrl(
            "https://api.cmburl.cn:8065/atd/atduat/xft-attendance-bfe/sta-item/query",
            "https://api.cmbchina.com/atd/prd/xft-attendance-bfe/sta-item/query"
        );

        /// <summary>
        /// 获取实时考勤
        /// </summary>
        public static readonly ApiUrl RealTimeQuery = new ApiUrl(
            "https://api.cmburl.cn:8065/atd/atduat/xft-atn/realtime-attendance/open-api-query",
            "https://api.cmbchina.com/atd/prd/xft-atn/realtime-attendance/open-api-query"
        );
    }

    /// <summary>
    /// 职位管理接口
    /// </summary>
    public static class Position
    {
        /// <summary>
        /// 分页查询职位
        /// </summary>
        public static readonly ApiUrl QueryPage = new ApiUrl(
            "https://api.cmburl.cn:8065/xfthrm/hrm2uat/organization-management/openapi/job/query/page",
            "https://api.cmbchina.com/hrm/hrm2/organization-management/openapi/job/query/page"
        );
    }

    /// <summary>
    /// 岗位管理接口
    /// </summary>
    public static class Post
    {
        /// <summary>
        /// 分页查询岗位
        /// </summary>
        public static readonly ApiUrl QueryPage = new ApiUrl(
            "https://api.cmburl.cn:8065/xfthrm/hrm2uat/organization-management/openapi/position/query/page",
            "https://api.cmbchina.com/hrm/hrm2/organization-management/openapi/position/query/page"
        );
    }
}
