using Ss.CmbXft.Common.Models.Enums;
using Ss.CmbXft.Common.Models.Request;

namespace Ss.CmbXft.Application.Dtos.Perm;

/// <summary>菜单返回Res</summary>
public class PermMenuRes
{
    public long Id { get; set; }
    public long ParentId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public string Component { get; set; } = string.Empty;
    public string? Redirect { get; set; }
    public string? Icon { get; set; }
    public int Sort { get; set; }
    public int Type { get; set; }
    public string? Permission { get; set; }
    public bool Hidden { get; set; }
    public bool AlwaysShow { get; set; }
    public bool NoCache { get; set; }
    public bool Affix { get; set; }
    public bool Breadcrumb { get; set; }
    public string? ActiveMenu { get; set; }
    public bool IsIframe { get; set; }
    public string? ExternalUrl { get; set; }
    public bool Enable { get; set; }
    public string? Remark { get; set; }
    public DateTime CreateTime { get; set; }
    public DateTime? UpdateTime { get; set; }
    public List<PermMenuRes> Children { get; set; } = new();
}

/// <summary>菜单保存请求</summary>
public class PermMenuSaveReq
{
    public string Title { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public string Component { get; set; } = string.Empty;
    public string? Redirect { get; set; }
    public string? Icon { get; set; }
    public long ParentId { get; set; } = 0;
    public int Sort { get; set; } = 0;
    public int Type { get; set; }
    public string? Permission { get; set; }
    public bool Hidden { get; set; } = false;
    public bool AlwaysShow { get; set; } = false;
    public bool NoCache { get; set; } = false;
    public bool Affix { get; set; } = false;
    public bool? Breadcrumb { get; set; } = true;
    public string? ActiveMenu { get; set; }
    public bool IsIframe { get; set; } = false;
    public string? ExternalUrl { get; set; }
    public bool Enable { get; set; } = true;
    public string? Remark { get; set; }
}

/// <summary>菜单分页查询请求</summary>
public class PermMenuPageReq : PageQueryRequestBase { }

/// <summary>菜单列表查询请求</summary>
public class PermMenuQueryReq : QueryRequestBase { }

/// <summary>菜单树查询请求</summary>
public class PermMenuTreeReq : BaseRequest { }
