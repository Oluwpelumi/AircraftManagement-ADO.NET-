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
    public class PassengerService : IPassengerService
    {
        IPassengerRepository _passengerRepository = new PassengerRepository();
        IUserRepository _userRepository = new UserRepository();
        IProfileRepository _profileRepository = new ProfileRepository();
        IAddressRepository _addressRepository = new AddressRepository();
        IRoleRepository _roleRepository = new RoleRepository();
        IBookingsRepository _bookingsRepository = new BookingsRepository();

        public PassengerResponse<bool> DeletePassenger(string regNumber)
        {
            var passenger = _passengerRepository.Get(regNumber);
            if (passenger != null)
            {
                var user = _userRepository.GetById(passenger.UserId);
                _passengerRepository.Delete(regNumber);
                _userRepository.Delete(user.UserEmail);
                _profileRepository.Delete(user.UserEmail);
                _addressRepository.Delete(user.AddressId);
                return new PassengerResponse<bool>
                {
                    Data = true,
                    Message = $"The passenger with the reg-Number {passenger.RegNumber} has been deleted Successfully",
                    Status = true
                };
            }
            return new PassengerResponse<bool>
            {
                Data = false,
                Message = "passenger not found",
                Status = false
            };
        }

        public PassengerResponse<bool> FundWallet(string staffNumber, double amount)
        {
            var passenger = _passengerRepository.Get(staffNumber);
            if (passenger != null)
            {
                var user = _userRepository.GetById(passenger.UserId);
                _passengerRepository.UpdateWallet(staffNumber, amount);
                //_userRepository.UpdateWallet(user.UserEmail, amount);
                return new PassengerResponse<bool>
                {
                    Message = "Wallet Updated Successfully",
                    Data = true,
                    Status = true
                };
            }
            return new PassengerResponse<bool>
            {
                Message = "Unable to fund wallet",
                Data = false,
                Status = false
            };
        }

        public PassengerResponse<List<PassengerDto>> GetAllPaseengerSchedledForAParticularFlight(PassengerBookingIdRequestModel model)
        {
            var aircraftName = _bookingsRepository.Get(model.BookingId).AircraftName;
            var passengers = _passengerRepository.GetAllPassengersInThisAircraft(aircraftName);
            if (passengers != null)
            {
                return new PassengerResponse<List<PassengerDto>>
                {
                    Status = true,
                    Message = "Successful",
                    Data = passengers.Select(passenger => new PassengerDto
                    {
                        Id = passenger.Id,
                        UserId = passenger.UserId,
                        RegNumber = passenger.RegNumber,
                        Wallet = passenger.Wallet,
                        BookingId = passenger.BookingId,
                        DateCreated = passenger.DateCreated
                    }).ToList()
                };
            }
            return new PassengerResponse<List<PassengerDto>>
            {
                Data = null,
                Message = "No passenger found",
                Status = false
            };
        }

        public PassengerResponse<List<PassengerDto>> GetAllPassengers()
        {
            var passengers = _passengerRepository.GetAll();
            if (passengers != null)
            {
                return new PassengerResponse<List<PassengerDto>>
                {
                    Status = true,
                    Message = "Successful",
                    Data = passengers.Select(passenger => new PassengerDto
                    {
                        Id = passenger.Id,
                        UserId = passenger.UserId,
                        RegNumber = passenger.RegNumber,
                        Wallet = passenger.Wallet,
                        BookingId = passenger.BookingId,
                        DateCreated = passenger.DateCreated
                    }).ToList()
                };
            }
            return new PassengerResponse<List<PassengerDto>>
            {
                Data = null,
                Message = "No passenger found",
                Status = false
            };
        }

        public PassengerResponse<PassengerDto> GetPassenger(string regNumber)
        {
            var passenger = _passengerRepository.Get(regNumber);
            if (passenger != null)
            {
                var user = _userRepository.GetById(passenger.UserId);
                var address = _addressRepository.Get(user.AddressId);
                var profile = _profileRepository.Get(user.UserEmail);
                return new PassengerResponse<PassengerDto>
                {
                    Data = new PassengerDto
                    {
                        Id = passenger.Id,
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
                        UserId = passenger.UserId,
                        RegNumber = passenger.RegNumber,
                        BookingId = passenger.BookingId,
                        Wallet = passenger.Wallet,
                        DateCreated = passenger.DateCreated
                    },
                    Message = "Successful",
                    Status = true
                };
            }
            return new PassengerResponse<PassengerDto>
            {
                Data = null,
                Message = "passenger not found",
                Status = false
            };
        }



        public PassengerResponse<PassengerDto> RegisterPassenger(PassengerRequestModel model)
        {
            var user = _userRepository.Get(model.UserEmail);
            if (user != null)
            {
                return new PassengerResponse<PassengerDto>
                {
                    Status = true,
                    Message = "passenger already exists",
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
                RoleId = _roleRepository.Get("passenger").Id
            };

            Passenger passenger = new Passenger
            {
                UserId = user1.Id,
                Wallet = model.Wallet,
                BookingId = "0",
                RegNumber = profile.LastName + "/" + new Random().Next(100, 999)
            };
            _addressRepository.Create(address);
            _profileRepository.Create(profile);
            _userRepository.Create(user1);
            _passengerRepository.Register(passenger);
            return new PassengerResponse<PassengerDto>
            {
                Status = true,
                Message = "passenger Registered successfully",
                Data = new PassengerDto
                {
                    Id = passenger.Id,
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
                    UserId = passenger.UserId,
                    RegNumber = passenger.RegNumber,
                    Wallet = passenger.Wallet,
                    DateCreated = passenger.DateCreated
                }
            };
        }
    }
}
