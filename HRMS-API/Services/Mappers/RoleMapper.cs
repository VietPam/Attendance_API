using Domain.Entities.Accounts;
using Services.DTOs;

namespace Services.Mappers;
public static class RoleMapper
{
    public static RoleDTO ToDTO(this SqlRole entity)
    {
        return new RoleDTO(entity.Name,
                            entity.Code,
                            entity.Description);
    }
}
