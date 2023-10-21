using AircraftM.DTOs;
using AircraftM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.Services.Interfaces
{
    public interface IProfileService
    {
        ProfileResponse<ProfileDto> GetProfile(string userEmail);
        ProfileResponse<List<ProfileDto>> GetAllProfiles();
        ProfileResponse<bool> DeleteProfile(string userEmail);
    }
}
