# Ss.CmbXft 数据同步功能说明

## 一、项目结构

```
Ss.CmbXft/
├── Ss.CmbXft.Api/              → Web API 层（Controllers）
├── Ss.CmbXft.Application/      → 应用层（DTO、接口定义、业务服务）
├── Ss.CmbXft.Domain/           → 领域层（实体）
├── Ss.CmbXft.Infrastructure/   → 基础设施层（EF Core、第三方服务、Job）
├── Ss.CmbXft.Common/           → 公共层（通用模型、分页基类）
└── Ss.CmbXft.Sdk/              → 薪福通 SDK（XFT API 封装）
```

## 二、数据库

| 配置键 | 数据库 | 用途 |
|--------|--------|------|
| `Sserp` | SQL Server `SSERP_NEXT` | ERP 库（员工、商品、税、品牌等） |
| `Sspos` | SQL Server `HQSSPOS` | POS 库（门店、优惠券等） |

## 三、同步功能总览

```
┌──────────┐     同步1        ┌──────────────┐
│  薪福通   │ ──────────────→ │    AbpUser  │
│  XFT API  │                 │  SserpERPTxnEmployee  │
└──────────┘                  └──────────────┘

┌──────────┐     同步2      ┌──────────────┐
│  POS 库   │ ──────────────→│              │
│ HQSSPOS  │  POS_MLocation  │              │
└──────────┘               │              │
                            │  昇菘会员系统   │
┌──────────┐     同步3      │  member.      │
│  POS 库   │ ──────────────→│ shengsiongcn  │
│ HQSSPOS  │  POS_Mst_Voucher│ .com         │
└──────────┘               │              │
                            │              │
┌──────────┐     同步4      │              │
│  ERP 库   │ ──────────────→│              │
│SSERP_NEXT│  T_Product 等   └──────────────┘
└──────────┘
```

---

## 同步 1：薪福通员工 → 本地数据库

**功能说明**：从招商薪福通平台拉取员工数据，写入本地数据库。

| 项目 | 说明 |
|------|------|
| **数据来源** | 薪福通 API（`XftStaffService`，基于 SDK 封装） |
| **写入目标** | ① PostgreSQL `xft_staff` 表（主库）<br>② SQL Server `SserpERPTxnEmployee` 表（ERP 库）<br>③ 同时维护 `AbpUser` / `AbpUserRole` |
| **同步策略** | 分页拉取（每页 1000 条），支持软删除（缺失记录标记为已删除） |
| **同步数据** | 员工基本信息、任职、个人、薪资、合同、联系方式、学历、工作经历、税务等 |

### 关键代码文件

| 文件 | 职责 |
|------|------|
| `Ss.CmbXft.Application/Interfaces/Services/Xft/IXftErpSyncService.cs` | 同步服务接口 |
| `Ss.CmbXft.Application/Interfaces/Services/Xft/XftErpSyncService.cs` | 同步服务实现（核心逻辑） |
| `Ss.CmbXft.Domain/Entities/Sserp/SserpERPTxnEmployee.cs` | ERP 员工实体 |
| `Ss.CmbXft.Api/Controllers/Xft/XftStaffApiController.cs` | 手动触发接口 |
| `Ss.CmbXft.Infrastructure/Jobs/XftStaffSyncJob.cs` | 定时任务 |

### API 接口

| 方法 | 路径 | 说明 |
|------|------|------|
| POST | `/api/xft_staff/sync-to-both` | 手动触发同步到数据库 |
| POST | `/api/xft_staff/api_list` | 分页查询员工信息 |
| POST | `/api/xft_staff/api_dict` | 员工数据字典查询 |

### Job 配置

```json
"XftStaffSyncJob": {
  "Enabled": false,
  "CronExpression": "0 0 2 * * ?"   // 每天凌晨 2:00
}
```

---

## 同步 2：POS 门店 → 昇菘会员系统

**功能说明**：从 POS 数据库读取门店数据，推送至昇菘会员系统。

| 项目 | 说明 |
|------|------|
| **数据来源** | SQL Server `HQSSPOS` 库 → `POS_MLocation` 表 |
| **写入目标** | 昇菘会员系统 API：`POST /api/erp/store_sync` |
| **同步字段** | `LocationCode` → StoreId, `LocationName` → StoreName, `Address1` → Address |
| **筛选条件** | 默认只同步 `Active = true` 的门店 |

### 关键代码文件

| 文件 | 职责 |
|------|------|
| `Ss.CmbXft.Application/Dtos/Sspos/StoreSyncDto.cs` | 查询条件 DTO（继承 `PagedRequestBase`） |
| `Ss.CmbXft.Infrastructure/Services/StoreSyncAppService.cs` | 同步服务实现 |
| `Ss.CmbXft.Infrastructure/ThirdParty/SsMember/Services/SsMemberStoreService.cs` | 昇菘会员 API 客户端 |
| `Ss.CmbXft.Api/Controllers/Sserp/SsMemberSyncController.cs` | 手动触发接口 |
| `Ss.CmbXft.Infrastructure/Jobs/StoreSyncJob.cs` | 定时任务 |

### API 接口

| 方法 | 路径 | 说明 |
|------|------|------|
| POST | `/api/sserp/ssmember_sync/sync_store` | 手动触发门店同步（body 可传筛选条件） |

请求示例：
```json
{
  "locationCode": "S001,S002",
  "active": true,
  "pageIndex": 1,
  "pageSize": 20
}
```

### Job 配置

```json
"StoreSyncJob": {
  "Enabled": false,
  "CronExpression": "0 0 4 * * ?"   // 每天凌晨 4:00
}
```

---

## 同步 3：POS 优惠券 → 昇菘会员系统

**功能说明**：从 POS 数据库读取优惠券（代金券）数据，推送至昇菘会员系统。

| 项目 | 说明 |
|------|------|
| **数据来源** | SQL Server `HQSSPOS` 库 → `POS_Mst_Voucher` 表 |
| **写入目标** | 昇菘会员系统 API：`POST /api/erp/coupon_sync` |
| **同步字段** | VoucherCode→CodeId, VoucherName→Name, VoucherAmount→Money, ValidFrom→StartTime, ValidTo→FinishTime 等 |
| **筛选条件** | 默认只同步 `Active = true` 的优惠券 |

### 关键代码文件

| 文件 | 职责 |
|------|------|
| `Ss.CmbXft.Application/Dtos/Sspos/CouponSyncDto.cs` | 查询条件 DTO（继承 `PagedRequestBase`） |
| `Ss.CmbXft.Infrastructure/Services/CouponSyncAppService.cs` | 同步服务实现 |
| `Ss.CmbXft.Infrastructure/ThirdParty/SsMember/Services/SsMemberCouponService.cs` | 昇菘会员 API 客户端 |
| `Ss.CmbXft.Api/Controllers/Sserp/SsMemberSyncController.cs` | 手动触发接口 |
| `Ss.CmbXft.Infrastructure/Jobs/CouponSyncJob.cs` | 定时任务 |

### API 接口

| 方法 | 路径 | 说明 |
|------|------|------|
| POST | `/api/sserp/ssmember_sync/sync_voucher` | 手动触发优惠券同步（body 可传筛选条件） |

请求示例：
```json
{
  "voucherCode": "V001",
  "active": true,
  "pageIndex": 1,
  "pageSize": 20
}
```

### Job 配置

```json
"CouponSyncJob": {
  "Enabled": false,
  "CronExpression": "0 0 4 * * ?"   // 每天凌晨 4:00
}
```

---

## 同步 4：ERP 商品 → 昇菘会员系统

**功能说明**：从 ERP 数据库读取商品主数据（含条码），推送至昇菘会员系统。

| 项目 | 说明 |
|------|------|
| **数据来源** | SQL Server `SSERP_NEXT` 库 |
| **关联表** | `T_Product` LEFT JOIN `ProductGroups` → `Departments`、`Categories`、`SubCategories`、`Segments`、`ItemClasses`、`Taxes`、`Brands`、`Uoms`、`ProductAttributes`、`Barcodes` |
| **写入目标** | 昇菘会员系统 API：`POST /api/erp/goods_sync` |
| **筛选条件** | 默认 `Status = 1`（启用），支持按编码、描述、部门、品牌等筛选 |
| **分页控制** | 继承 `PagedRequestBase`，通过 `PageIndex` / `PageSize` 控制分批，`MaxSyncCount` 限制总量 |

### 关键代码文件

| 文件 | 职责 |
|------|------|
| `Ss.CmbXft.Application/Dtos/Sserp/Product/ProductSyncDto.cs` | 查询条件 DTO（含 `MaxSyncCount`） |
| `Ss.CmbXft.Infrastructure/Services/ProductSyncAppService.cs` | 同步服务实现（含 SQL JOIN 查询构建） |
| `Ss.CmbXft.Infrastructure/ThirdParty/SsMember/Services/SsMemberGoodsService.cs` | 昇菘会员 API 客户端 |
| `Ss.CmbXft.Api/Controllers/Sserp/SsMemberSyncController.cs` | 手动触发接口 |
| `Ss.CmbXft.Infrastructure/Jobs/ProductSyncJob.cs` | 定时任务 |

### API 接口

| 方法 | 路径 | 说明 |
|------|------|------|
| POST | `/api/sserp/ssmember_sync/sync_goods` | 手动触发商品同步（body 传筛选+分页条件） |

请求示例：
```json
{
  "productCode": "P001,P002",
  "status": 1,
  "pageIndex": 1,
  "pageSize": 50,
  "maxSyncCount": 200
}
```

| 参数 | 说明 |
|------|------|
| `pageIndex` | 从第几页开始同步（默认 1） |
| `pageSize` | 每批处理数量（默认 20，最大 1000） |
| `maxSyncCount` | 最大同步数量，不传则不限。传 20 则最多同步 20 条 |

### Job 配置

```json
"ProductSyncJob": {
  "Enabled": false,
  "CronExpression": "0 0 4 * * ?"   // 每天凌晨 4:00
}
```

---

## 四、Quartz Job 调度总览

所有 Job 默认 `Enabled: false`，需在 `appsettings.json` 中手动开启。

| Job | 类 | Cron | 时区 | 说明 |
|-----|----|------|------|------|
| XftStaffSyncJob | `XftStaffSyncJob` | `0 0 2 * * ?` | CST | 凌晨 2 点同步薪福通员工 |
| ProductSyncJob | `ProductSyncJob` | `0 0 4 * * ?` | CST | 凌晨 4 点同步商品 |
| StoreSyncJob | `StoreSyncJob` | `0 0 4 * * ?` | CST | 凌晨 4 点同步门店 |
| CouponSyncJob | `CouponSyncJob` | `0 0 4 * * ?` | CST | 凌晨 4 点同步优惠券 |

**公共特性**：
- 所有 Job 继承 `JobBase`，统一日志、计时、异常处理
- `[DisallowConcurrentExecution]` 防止并发执行
- `RAMJobStore` 纯内存存储，适合单实例部署
- 支持通过 `appsettings.json` 动态配置 Cron 表达式
- Job 注册入口：`QuartzServiceExtensions.AddQuartzJobs()`

---

## 五、昇菘会员系统对接

所有同步到昇菘会员系统的请求统一走 `ISsMemberClient`，自动附加签名和时间戳。

| 配置项 | 值 |
|--------|-----|
| BaseUrl | `https://member.shengsiongcn.com/` |
| AppKey | `31e4cc47aab6d066b0bbaa11c9b7a028`（MD5 of "shengsiong"） |
| Timeout | 30 秒 |

| 同步类型 | API 路径 |
|----------|----------|
| 商品同步 | `POST /api/erp/goods_sync` |
| 门店同步 | `POST /api/erp/store_sync` |
| 优惠券同步 | `POST /api/erp/coupon_sync` |
