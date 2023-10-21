using AircraftM.DTOs;
using AircraftM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.Services.Interfaces
{
    public interface IUserService
    {
        UserResponse<UserDto> Login(UserLoginModel model);
        UserResponse<UserDto> GetUser(string userEmail);
        UserResponse<List<UserDto>> GetAllUsers();
    }
}
