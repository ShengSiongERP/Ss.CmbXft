using Mapster;
using Ss.CmbXft.Application.Dtos.Sserp;
using Ss.CmbXft.Domain.Entities.Sserp;

namespace Ss.CmbXft.Application.Mappings;

/// <summary>
/// AbpRole 实体 Mapster 映射配置
/// </summary>
public class AbpRoleMappingRegister : IRegister
{
    /// <inheritdoc />
    public void Register(TypeAdapterConfig config)
    {
        // AbpRole → AbpRoleDto（完整映射）
        config.NewConfig<AbpRole, AbpRoleDto>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.TenantId, src => src.TenantId)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.NormalizedName, src => src.NormalizedName)
            .Map(dest => dest.IsDefault, src => src.IsDefault)
            .Map(dest => dest.IsStatic, src => src.IsStatic)
            .Map(dest => dest.IsPublic, src => src.IsPublic)
            .Map(dest => dest.ExtraProperties, src => src.ExtraProperties)
            .Map(dest => dest.ConcurrencyStamp, src => src.ConcurrencyStamp);
    }
}