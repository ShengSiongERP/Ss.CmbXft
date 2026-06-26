using Mapster;
using Ss.CmbXft.Application.Dtos;
using Ss.CmbXft.Domain.Entities;

namespace Ss.CmbXft.Application.Mappings;

/// <summary>
/// XftStaff 实体 Mapster 映射配置
/// </summary>
public class XftStaffMappingRegister : IRegister
{
    /// <inheritdoc />
    public void Register(TypeAdapterConfig config)
    {
        //config.NewConfig<XftStaff, XftStaffDto>();
        //config.NewConfig<CreateXftStaffDto, XftStaff>();
        //config.NewConfig<UpdateXftStaffDto, XftStaff>();
    }
}