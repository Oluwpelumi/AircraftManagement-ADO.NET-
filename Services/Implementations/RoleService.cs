using AircraftM.DTOs;
using AircraftM.Models;
using AircraftM.Repositories.Implementations;
using AircraftM.Repositories.Interfaces;
using AircraftM.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.Services.Implementations
{
    public class RoleService : IRoleService
    {
        IRoleRepository _roleRepository = new RoleRepository();
        public RoleResponse<RoleDto> AddRole(RoleRequestModel model)
        {
            var rl = _roleRepository.Get(model.Name);
            if (rl != null)
            {
                return new RoleResponse<RoleDto>
                {
                    Status = true,
                    Message = "role already exists",
                    Data = null
                };
            }
            Role role = new Role
            {
                Name = model.Name,
                Description = model.Description
            };
            _roleRepository.Register(role);
            return new RoleResponse<RoleDto>
            {
                Status = true,
                Message = "role Registered successfully",
                Data = new RoleDto
                {
                    Id = role.Id,
                    Name = model.Name,
                    Description = model.Description,
                    DateCreated = role.DateCreated
                }
            };
        }

        public RoleResponse<bool> DeleteRole(string name)
        {
            var role = _roleRepository.Get(name);
            if (role != null)
            {
                _roleRepository.Delete(name);
                return new RoleResponse<bool>
                {
                    Data = true,
                    Message = "Successful",
                    Status = true
                };
            }
            return new RoleResponse<bool>
            {
                Data = false,
                Message = "role not found",
                Status = false
            };
        }

        public RoleResponse<List<RoleDto>> GetAllRoles()
        {
            var roles = _roleRepository.GetAll();
            if (roles != null)
            {
                return new RoleResponse<List<RoleDto>>
                {
                    Status = true,
                    Message = "Successful",
                    Data = roles.Select(role => new RoleDto
                    {
                        Id = role.Id,
                        Name = role.Name,
                        Description = role.Description,
                        DateCreated = role.DateCreated
                    }).ToList()
                };
            }
            return new RoleResponse<List<RoleDto>>
            {
                Data = null,
                Message = "No role found",
                Status = false
            };
        }

        public RoleResponse<RoleDto> GetRole(string name)
        {
            var role = _roleRepository.Get(name);
            if (role != null)
            {
                return new RoleResponse<RoleDto>
                {
                    Data = new RoleDto
                    {
                        Id = role.Id,
                        Name = role.Name,
                        Description = role.Description,
                        DateCreated = role.DateCreated
                    },
                    Message = "Successful",
                    Status = true
                };
            }
            return new RoleResponse<RoleDto>
            {
                Data = null,
                Message = "role not found",
                Status = false
            };
        }
    }
}
