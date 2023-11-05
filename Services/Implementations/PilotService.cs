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
    public class PilotService : IPilotService
    {
        IPilotRepository _pilotRepository = new PilotRepository();
        IUserRepository _userRepository = new UserRepository();
        IProfileRepository _profileRepository = new ProfileRepository();
        IAddressRepository _addressRepository = new AddressRepository();
        IRoleRepository _roleRepository = new RoleRepository();
        public PilotResponse<bool> DeletePilot(string staffNumber)
        {
            var pilot = _pilotRepository.Get(staffNumber);
            if (pilot != null)
            {
                var user = _userRepository.GetById(pilot.UserId);
                _pilotRepository.Delete(staffNumber);
                _userRepository.Delete(user.UserEmail);
                _profileRepository.Delete(user.UserEmail);
                _addressRepository.Delete(user.AddressId);
                return new PilotResponse<bool>
                {
                    Data = true,
                    Message = $"The pilot with the staff-Number {pilot.StaffNumber} has been deleted Successfully",
                    Status = true
                };
            }
            return new PilotResponse<bool>
            {
                Data = false,
                Message = "pilot not found",
                Status = false
            };
        }

        public PilotResponse<bool> FundWallet(string staffNumber, double amount)
        {
            var pilot = _pilotRepository.Get(staffNumber);
            var user = _userRepository.GetById(pilot.UserId);
            if (pilot != null)
            {
                _pilotRepository.UpdateWallet(user.UserEmail, amount);
                _userRepository.UpdateWallet(user.UserEmail, amount);
                return new PilotResponse<bool>
                {
                    Message = "Wallet Updated Successfully",
                    Data = true,
                    Status = true
                };
            }
            return new PilotResponse<bool>
            {
                Message = "Unable to fund wallet",
                Data = false,
                Status = false
            };
        }

        public PilotResponse<List<PilotDto>> GetAllPilots()
        {
            var pilots = _pilotRepository.GetAll();
            if (pilots != null)
            {
                return new PilotResponse<List<PilotDto>>
                {
                    Status = true,
                    Message = "Successful",
                    Data = pilots.Select(pilot => new PilotDto
                    {
                        Id = pilot.Id,
                        UserId = pilot.UserId,
                        StaffNumber = pilot.StaffNumber,
                        Wallet = pilot.Wallet,
                        DateCreated = pilot.DateCreated
                    }).ToList()
                };
            }
            return new PilotResponse<List<PilotDto>>
            {
                Data = null,
                Message = "No pilot found",
                Status = false
            };
        }

        public PilotResponse<PilotDto> GetPilot(string staffNumber)
        {
            var pilot = _pilotRepository.Get(staffNumber);
            if (pilot != null)
            {
                var user = _userRepository.GetById(pilot.UserId);
                var address = _addressRepository.Get(user.AddressId);
                var profile = _profileRepository.Get(user.UserEmail);
                return new PilotResponse<PilotDto>
                {
                    Data = new PilotDto
                    {
                        Id = pilot.Id,
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
                        UserId = pilot.UserId,
                        StaffNumber = pilot.StaffNumber,
                        Wallet = pilot.Wallet,
                        DateCreated = pilot.DateCreated
                    },
                    Message = "Successful",
                    Status = true
                };
            }
            return new PilotResponse<PilotDto>
            {
                Data = null,
                Message = "pilot not found",
                Status = false
            };
        }

        public PilotResponse<PilotDto> RegisterPilot(PilotRequestModel model)
        {
            var user = _userRepository.Get(model.UserEmail);
            if (user != null)
            {
                return new PilotResponse<PilotDto>
                {
                    Status = true,
                    Message = "pilot already exists",
                    Data = null
                };
            }
            Address address = new Address
            {
                State = model.State,
                City = model.City,
                Street = model.Street,
                PostalCode = model.PostalCode,
                Number = model.Number
            };
            Profile profile = new Profile
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                UserEmail = model.UserEmail,
                Gender = model.Gender,
                DOB = model.DOB
            };
            User user1 = new User
            {
                UserEmail = model.UserEmail,
                Password = model.Password,
                AddressId = address.Id,
                ProfileId = profile.Id,
                RoleId = _roleRepository.Get("pilot").Id
            };
            Pilot pilot = new Pilot
            {
                UserId = user1.Id,
                Wallet = model.Wallet,
                StaffNumber = profile.LastName + "/" + new Random().Next(100, 999)
            };
            _addressRepository.Create(address);
            _profileRepository.Create(profile);
            _userRepository.Create(user1);
            _pilotRepository.Register(pilot);
            return new PilotResponse<PilotDto>
            {
                Status = true,
                Message = "pilot Registered successfully",
                Data = new PilotDto
                {
                    Id = pilot.Id,
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
                    UserId = pilot.UserId,
                    StaffNumber = pilot.StaffNumber,
                    Wallet = pilot.Wallet,
                    DateCreated = pilot.DateCreated
                }
            };
        }
    }
}
