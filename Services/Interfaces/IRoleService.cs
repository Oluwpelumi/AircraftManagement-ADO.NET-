using AircraftM.DTOs;
using AircraftM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.Services.Interfaces
{
    public interface IRoleService
    {
        RoleResponse<RoleDto> AddRole(RoleRequestModel model);
        RoleResponse<RoleDto> GetRole(string name);
        RoleResponse<RoleDto> GetRoleById(string id);
        RoleResponse<List<RoleDto>> GetAllRoles();
        RoleResponse<bool> DeleteRole(string name);
    }
}
