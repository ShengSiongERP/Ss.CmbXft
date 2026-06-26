using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ss.CmbXft.Application.Dtos.Sserp.Product;
using Ss.CmbXft.Application.Extensions;
using Ss.CmbXft.Application.Interfaces.Services.Sserp.SsMember;
using Ss.CmbXft.Common.Models;
using Ss.CmbXft.Infrastructure.EntityFrameworkCore;
using Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Interfaces;
using Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Model.GoodsSync;

namespace Ss.CmbXft.Infrastructure.Services;

/// <summary>
/// 商品同步(erp数据库)
/// </summary>
public class ProductSyncAppService : IProductSyncAppService
{
    private const int TAX_CODE_ATTRIBUTE_ID = 18; // TaxCode 属性ID

    private readonly SserpDbContext _db;
    private readonly ISsMemberGoodsService _goodsService;
    private readonly ILogger<ProductSyncAppService> _logger;

    public ProductSyncAppService(
        SserpDbContext db,
        ISsMemberGoodsService goodsService,
        ILogger<ProductSyncAppService> logger)
    {
        _db = db;
        _goodsService = goodsService;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<ApiResult> SyncProductsAsync(ProductSyncQueryDto query, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(query);

        // 构建基础查询
        var productsQuery = BuildProductQuery(query);

        // 获取总数
        var totalCount = await productsQuery.CountAsync(ct);
        if (totalCount == 0)
        {
            _logger.LogWarning("没有找到符合条件的商品，跳过同步");
            return ApiResult.Error(ApiResultEnum.Fail, "没有找到符合条件的商品");
        }

        // 计算实际需要同步的数量：受 MaxSyncCount 和剩余可同步数量限制
        var remainingCount = Math.Max(0, totalCount - query.Skip);
        var maxSync = query.MaxCount.HasValue
            ? Math.Min(query.MaxCount.Value, remainingCount)
            : remainingCount;

        if (maxSync == 0)
        {
            _logger.LogWarning("指定页码超出范围（跳过 {Skip} 条，总共有 {Total} 条），没有需要同步的商品",
                query.Skip, totalCount);
            return ApiResult.Error(ApiResultEnum.Fail,
                $"指定页码超出范围（跳过 {query.Skip} 条，总共有 {totalCount} 条）");
        }

        _logger.LogInformation(
            "找到 {TotalCount} 个商品，从第 {PageIndex} 页开始（跳过 {Skip} 条），每页 {PageSize} 条，最多同步 {MaxSync} 个",
            totalCount, query.PageIndex, query.Skip, query.PageSize, maxSync);

        // 一次性获取所有需要同步的产品数据
        _logger.LogInformation("正在加载所有需要同步的产品数据...");
        var allProducts = await productsQuery
            .Skip(query.Skip)
            .Take(maxSync)
            .ToListAsync(ct);
        _logger.LogInformation("产品数据加载完成，共 {ProductCount} 个产品", allProducts.Count);

        // 一次性获取所有这些产品的条码信息（约14万条数据）
        _logger.LogInformation("正在加载所有产品的条码信息...");
        var allProductCodes = allProducts.Select(p => p.ProductCode).ToList();
        var allBarcodes = await _db.Barcodes
            .Where(b => allProductCodes.Contains(b.ProductCode) && b.Status == 1)
            .Join(_db.Uoms, b => b.UOMID, u => u.UOMID, (b, u) => new { b, u })
            .Select(x => new
            {
                x.b.ProductCode,
                x.b.Barcode,
                x.u.UOMName
            })
            .ToListAsync(ct);

        // 按产品编码分组条码
        var allBarcodeDict = allBarcodes
            .GroupBy(x => x.ProductCode)
            .ToDictionary(g => g.Key, g => g.Select(x => new GoodsBarcode
            {
                Barcode = x.Barcode,
                UomName = x.UOMName
            }).ToList());

        _logger.LogInformation("条码信息加载完成，共 {BarcodeCount} 条条码数据", allBarcodes.Count);

        // 分批处理内存中的数据
        var totalSynced = 0;
        var totalFailed = 0;
        var batchIndex = 0;
        var currentIndex = 0;

        while (currentIndex < allProducts.Count)
        {
            ct.ThrowIfCancellationRequested();

            var batchSize = Math.Min(query.PageSize, allProducts.Count - currentIndex);
            var batchProducts = allProducts.Skip(currentIndex).Take(batchSize).ToList();

            if (batchProducts.Count == 0)
            {
                break;
            }

            currentIndex += batchSize;

            batchIndex++;
            _logger.LogInformation("正在处理第 {Current} 批，本批 {Count} 个商品",
                batchIndex, batchProducts.Count);

            // 映射到 GoodsInfo
            var goodsInfoList = batchProducts.Select(p => new GoodsInfo
            {
                ProductCode = p.ProductCode,
                ProductDescription = p.ProductDescription ?? p.ProductCode,
                Department = p.DepartmentName,
                Category = p.CategoryName,
                SubCategory = p.SubCategoryName,
                Segment = p.SegmentName,
                ItemClass = p.ItemClassName,
                Brand = p.BrandName,
                Uom = p.UomName,
                Status = p.Status,
                TaxRate = p.TaxRate.HasValue ? (object)p.TaxRate.Value : null,
                TaxCode = p.TaxCode,
                Barcodes = allBarcodeDict.GetValueOrDefault(p.ProductCode, new List<GoodsBarcode>())
            }).ToList();

#if DEBUG
            //if (goodsInfoList.Count > 0)
            //{
            //    goodsInfoList = goodsInfoList.Take(1).ToList();
            //}
#endif

            // 调用第三方服务同步
            var response = await _goodsService.SyncGoodsAsync(goodsInfoList, ct);

            if (response.IsSuccess)
            {
                totalSynced += goodsInfoList.Count;
                _logger.LogInformation("第 {Current} 批同步成功，已同步 {Synced}/{Total} 个商品",
                    batchIndex, totalSynced, maxSync);
            }
            else
            {
                totalFailed += goodsInfoList.Count;
                _logger.LogError("第 {Current} 批同步失败: {Message}",
                    batchIndex, response.GetErrorMessage());
            }
            //请求太快会返回429 Too Many Attempts.
            await Task.Delay(500, ct);
        }

        if (totalFailed > 0)
        {
            return ApiResult.Error(ApiResultEnum.ServerError,
                $"同步完成：成功 {totalSynced} 个，失败 {totalFailed} 个");
        }

        return ApiResult.Success($"成功同步 {totalSynced} 个商品");
    }

    /// <summary>
    /// 构建产品查询（参考 V_ProductMasterWithoutBarcode 视图结构）
    /// </summary>
    private IQueryable<ProductSyncInfo> BuildProductQuery(ProductSyncQueryDto query)
    {
        // 从 T_Product 开始，关联所有需要的表
        // 注意：TaxCode 需要通过后续的内存查询或子查询获取
        var productsQuery = from p in _db.Products
                            join pg in _db.ProductGroups on p.ProductGroupID equals pg.ProductGroupID into pgJoin
                            from pg in pgJoin.DefaultIfEmpty()
                            join d in _db.Departments on pg.DepartmentID equals d.DepartmentID into dJoin
                            from d in dJoin.DefaultIfEmpty()
                            join c in _db.Categories on pg.CategoryID equals c.CategoryID into cJoin
                            from c in cJoin.DefaultIfEmpty()
                            join sc in _db.SubCategories on pg.SubCategoryID equals sc.SubCategoryID into scJoin
                            from sc in scJoin.DefaultIfEmpty()
                            join s in _db.Segments on pg.SegmentID equals s.SegmentID into sJoin
                            from s in sJoin.DefaultIfEmpty()
                            join ic in _db.ItemClasses on pg.ItemClassID equals ic.ItemClassID into icJoin
                            from ic in icJoin.DefaultIfEmpty()
                            join t in _db.Taxes on p.TaxID equals t.TaxID into tJoin
                            from t in tJoin.DefaultIfEmpty()
                            join b in _db.Brands on p.BrandID equals b.BrandID into bJoin
                            from b in bJoin.DefaultIfEmpty()
                            join u in _db.Uoms on p.DefaultUOMID equals u.UOMID into uJoin
                            from u in uJoin.DefaultIfEmpty()
                                // 简单的 LEFT JOIN ProductAttribute - 通过 where 条件过滤 AttributeID = 18
                            join pa in _db.ProductAttributes.Where(x => x.AttributeId == TAX_CODE_ATTRIBUTE_ID)
                                on p.ProductCode equals pa.ProductCode into paJoin
                            from pa in paJoin.DefaultIfEmpty()
                            select new ProductSyncInfo
                            {
                                ProductCode = p.ProductCode,
                                ProductDescription = p.ProductDescription1 ?? p.ProductDescription2 ?? p.ProductDescription3,
                                DepartmentName = d.DepartmentName,
                                CategoryName = c.CategoryName,
                                SubCategoryName = sc.SubCategoryName,
                                SegmentName = s.SegmentName,
                                ItemClassName = ic.ItemClassName,
                                BrandName = b.BrandName,
                                UomName = u.UOMName,
                                Status = p.Status,
                                TaxRate = t.TaxRate,
                                TaxCode = pa.Value,
                                ModifiedOn = p.ModifiedOn,
                                CreatedOn = p.CreatedOn
                            };

        // 应用筛选条件
        return productsQuery
            .WhereIfNotNull(query.Status, p => p.Status == query.Status!.Value)
            .WhereInSplit(query.ProductCode, p => p.ProductCode)
            .WhereIfNotWhiteSpace(query.ProductDescription, p => p.ProductDescription != null && p.ProductDescription.Contains(query.ProductDescription!))
            .WhereIfNotWhiteSpace(query.Department, p => p.DepartmentName != null && p.DepartmentName.Contains(query.Department!))
            .WhereIfNotWhiteSpace(query.Category, p => p.CategoryName != null && p.CategoryName.Contains(query.Category!))
            .WhereIfNotWhiteSpace(query.SubCategory, p => p.SubCategoryName != null && p.SubCategoryName.Contains(query.SubCategory!))
            .WhereIfNotWhiteSpace(query.Segment, p => p.SegmentName != null && p.SegmentName.Contains(query.Segment!))
            .WhereIfNotWhiteSpace(query.ItemClass, p => p.ItemClassName != null && p.ItemClassName.Contains(query.ItemClass!))
            .WhereIfNotWhiteSpace(query.Brand, p => p.BrandName != null && p.BrandName.Contains(query.Brand!))
            .WhereIfNotNull(query.ModifiedSince, p => p.ModifiedOn.HasValue && p.ModifiedOn.Value >= query.ModifiedSince!.Value)
            .WhereIfNotNull(query.CreatedSince, p => p.CreatedOn >= query.CreatedSince!.Value);
    }

    /// <summary>
    /// 产品同步信息（内部使用）
    /// </summary>
    private class ProductSyncInfo
    {
        public string ProductCode { get; set; } = string.Empty;
        public string? ProductDescription { get; set; }
        public string? DepartmentName { get; set; }
        public string? CategoryName { get; set; }
        public string? SubCategoryName { get; set; }
        public string? SegmentName { get; set; }
        public string? ItemClassName { get; set; }
        public string? BrandName { get; set; }
        public string? UomName { get; set; }
        public short? Status { get; set; }
        public decimal? TaxRate { get; set; }
        public string? TaxCode { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
