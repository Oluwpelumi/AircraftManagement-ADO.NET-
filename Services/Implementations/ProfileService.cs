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
    public class ProfileService : IProfileService
    {
        IProfileRepository _profileRepository = new ProfileRepository();

        public ProfileResponse<bool> DeleteProfile(string userEmail)
        {
            var profile = _profileRepository.Get(userEmail);
            if (profile != null)
            {   

                _profileRepository.Delete(userEmail);
                return new ProfileResponse<bool>
                {
                    Data = true,
                    Message = "Successful",
                    Status = true
                };
            }
            return new ProfileResponse<bool>
            {
                Data = false,
                Message = "profile not found",
                Status = false
            };
        }

        public ProfileResponse<List<ProfileDto>> GetAllProfiles()
        {
            var profiles = _profileRepository.GetAll();
            if (profiles != null)
            {
                return new ProfileResponse<List<ProfileDto>>
                {
                    Status = true,
                    Message = "Successful",
                    Data = profiles.Select(profile => new ProfileDto
                    {
                        Id = profile.Id,
                        FirstName = profile.FirstName,
                        LastName = profile.LastName,
                        Gender = profile.Gender,
                        DOB = profile.DOB,
                        UserName = profile.UserName,
                        UserEmail = profile.UserEmail,
                        DateCreated = profile.DateCreated
                    }).ToList()
                };
            }
            return new ProfileResponse<List<ProfileDto>>
            {
                Data = null,
                Message = "No profile found",
                Status = false
            };
        }

        public ProfileResponse<ProfileDto> GetProfile(string userEmail)
        {
            var profile = _profileRepository.Get(userEmail);
            if (profile != null)
            {
                return new ProfileResponse<ProfileDto>
                {
                    Data = new ProfileDto
                    {
                        Id = profile.Id,
                        FirstName = profile.FirstName,
                        LastName = profile.LastName,
                        Gender = profile.Gender,
                        DOB = profile.DOB,
                        UserName = profile.UserName,
                        UserEmail = profile.UserEmail,
                        DateCreated = profile.DateCreated
                    },
                    Message = "Successful",
                    Status = true
                };
            }
            return new ProfileResponse<ProfileDto>
            {
                Data = null,
                Message = "profile not found",
                Status = false
            };
        }
    }
}
