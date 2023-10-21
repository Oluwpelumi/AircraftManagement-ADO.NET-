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
using System.Xml.Linq;

namespace AircraftM.Services.Implementations
{
    public class ManagerService : IManagerService
    {
        IManagerRepository _managerRepository = new ManagerRepository();
        IUserRepository _userRepository = new UserRepository();
        IProfileRepository _profileRepository = new ProfileRepository();
        IAddressRepository _addressRepository = new AddressRepository();
        IRoleRepository _roleRepository = new RoleRepository();

        public ManagerResponse<bool> DeleteManager(string staffNumber)
        {
            var manager = _managerRepository.Get(staffNumber);
            if (manager != null)
            {
                _managerRepository.Delete(staffNumber);
                return new ManagerResponse<bool>
                {
                    Data = true,
                    Message = "Successful",
                    Status = true
                };
            }
            return new ManagerResponse<bool>
            {
                Data = false,
                Message = "manager not found",
                Status = false
            };
        }

        public ManagerResponse<bool> FundWallet(string staffNumber, double amount)
        {
            var manager = _managerRepository.Get(staffNumber);
            var user = _userRepository.GetById(manager.UserId);
            if (manager != null)
            {
                _managerRepository.UpdateWallet(user.UserEmail, amount);
                _userRepository.UpdateWallet(user.UserEmail, amount);
                return new ManagerResponse<bool>
                {
                    Message = "Wallet Updated Successfully",
                    Data = true,
                    Status = true
                };
            }
            return new ManagerResponse<bool>
            {
                Message = "Unable to fund wallet",
                Data = false,
                Status = false
            };
        }

        public ManagerResponse<List<ManagerDto>> GetAllManagers()
        {
            var managers = _managerRepository.GetAll();
            if (managers != null)
            {
                return new ManagerResponse<List<ManagerDto>>
                {
                    Status = true,
                    Message = "Successful",
                    Data = managers.Select(manager => new ManagerDto
                    {
                        Id = manager.Id,
                        UserId = manager.UserId,
                        StaffNumber = manager.StaffNumber,
                        Wallet = manager.Wallet,
                        DateCreated = manager.DateCreated
                    }).ToList()
                };
            }
            return new ManagerResponse<List<ManagerDto>>
            {
                Data = null,
                Message = "No manager found",
                Status = false
            };
        }



        public ManagerResponse<ManagerDto> GetManager(string staffNumber)
        {
            var manager = _managerRepository.Get(staffNumber);
            if (manager != null)
            {
                var user = _userRepository.GetById(manager.UserId);
                var address = _addressRepository.Get(user.AddressId);
                var profile = _profileRepository.Get(user.ProfileId);
                return new ManagerResponse<ManagerDto>
                {
                    Data = new ManagerDto
                    {
                        Id = manager.Id,
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
                        UserId = manager.UserId,
                        StaffNumber = manager.StaffNumber,
                        Wallet = manager.Wallet,
                        DateCreated = manager.DateCreated
                    },
                    Message = "Successful",
                    Status = true
                };
            }
            return new ManagerResponse<ManagerDto>
            {
                Data = null,
                Message = "manager not found",
                Status = false
            };
        }

        public ManagerResponse<ManagerDto> RegisterManager(ManagerRequestModel model)
        {
            var user = _userRepository.Get(model.UserEmail);
            //var mngr = _managerRepository.GetById(user.Id);
            if (user != null)
            {
                return new ManagerResponse<ManagerDto>
                {
                    Status = true,
                    Message = "manager already exists",
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
            Role role = new Role
            {
                Name = model.RoleName
            };
            User user1 = new User
            {
                UserEmail = model.UserEmail,
                Password = model.Password,
                AddressId = address.Id,
                ProfileId = profile.Id,
                RoleId = _roleRepository.Get("MANAGER").Id
            };
            Manager manager = new Manager
            {
                UserId = user1.Id,
                Wallet = model.Wallet,
                StaffNumber = profile.LastName + "/" + new Random().Next(100, 999)
            };
            _addressRepository.Create(address);
            _profileRepository.Create(profile);
            _userRepository.Create(user1);
            _managerRepository.Register(manager);
            return new ManagerResponse<ManagerDto>
            {
                Status = true,
                Message = "manager Registered successfully",
                Data = new ManagerDto
                {
                    Id = manager.Id,
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
                    UserId = manager.UserId,
                    StaffNumber = manager.StaffNumber,
                    Wallet = manager.Wallet,
                    DateCreated = manager.DateCreated
                }
            };
        }
    }
}
