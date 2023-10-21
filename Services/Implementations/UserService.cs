using AircraftM.DTOs;
using AircraftM.Models;
using AircraftM.Repositories.Implementations;
using AircraftM.Repositories.Interfaces;
using AircraftM.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.Services.Implementations
{
    public class UserService : IUserService
    {
        IUserRepository _userRepository = new UserRepository();
        IProfileRepository _profileRepository = new ProfileRepository();
        IAddressRepository _addressRepository = new AddressRepository();
        IRoleRepository _roleRepository = new RoleRepository();
        public UserResponse<List<UserDto>> GetAllUsers()
        {
            var users = _userRepository.GetAllUsers();
            if (users != null)
            {
                return new UserResponse<List<UserDto>>
                {
                    Message = "Successful",
                    Status = true,
                    Data = users.Select(user => new UserDto
                    {
                        Id = user.Id,
                        AddressId = user.AddressId,
                        ProfileId = user.ProfileId,
                        RoleId = user.RoleId,
                        UserEmail = user.UserEmail,
                        DateCreated = user.DateCreated
                    }).ToList()
                };
            }
            return new UserResponse<List<UserDto>>
            {
                Data = null,
                Message = "No user found",
                Status = false
            };
        }

        public UserResponse<UserDto> GetUser(string userEmail)
        {
            var user = _userRepository.Get(userEmail);
            var profile = _profileRepository.Get(userEmail);
            var address = _addressRepository.Get(user.AddressId);
            if (user != null)
            {
                var role = _roleRepository.GetById(user.RoleId);
                return new UserResponse<UserDto>
                {
                    Data = new UserDto
                    {
                        Id = user.Id,
                        FirstName = profile.FirstName,
                        LastName = profile.LastName,
                        Gender = profile.Gender,
                        DOB = profile.DOB,
                        UserName = profile.UserName,
                        UserEmail = profile.UserEmail,
                        State = address.State,
                        City = address.City,
                        Street = address.Street,
                        PostalCode = address.PostalCode,
                        Number = address.Number,
                        AddressId = address.Id,
                        ProfileId = profile.Id,
                        RoleName = role.Name,
                        RoleId = role.Id,
                        RoleDescription = role.Description,
                        DateCreated = user.DateCreated
                    },
                    Message = "Successful",
                    Status = true
                };
            }
            return new UserResponse<UserDto>
            {
                Data = null,
                Message = "user not found",
                Status = false
            };
        }

        public UserResponse<UserDto> Login(UserLoginModel model)
        {
            var user = _userRepository.Get(model.UserEmail);
            if (user != null)
            {
                return new UserResponse<UserDto>
                {
                    Status = false,
                    Message = "Login Successful",
                    Data = new UserDto
                    {
                        Id = user.Id,
                        AddressId = user.AddressId,
                        ProfileId = user.ProfileId,
                        RoleId = user.RoleId,
                        UserEmail = user.UserEmail,
                        DateCreated = user.DateCreated
                    }
                };
            }
            return new UserResponse<UserDto>
            {
                Status = false,
                Message = "Unable to login user",
                Data = null
            };
        }
    }
}
