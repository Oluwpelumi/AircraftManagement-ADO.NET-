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
    public class AddressService : IAddressService
    {
        IAddressRepository _addressRepository = new AddressRepository();
        public AddressResponse<bool> DeleteAddress(string id)
        {
            var address = _addressRepository.Get(id);
            if (address ==  null)
            {
                return new AddressResponse<bool>
                {
                    Data = false,
                    Message = "Address not found",
                    Status = false
                };
            }
            _addressRepository.Delete(id);
            return new AddressResponse<bool>
            {
                Data = true,
                Message = "Successful",
                Status = true
            };
        }

        public AddressResponse<AddressDto> GetAddress(string id)
        {
            var address = _addressRepository.Get(id);
            if (address == null)
            {
                return new AddressResponse<AddressDto>
                {
                    Data = null,
                    Message = "Address not found",
                    Status = false
                };
            }
            return new AddressResponse<AddressDto>
            {
                Data = new AddressDto
                {
                    Id = address.Id,
                    State = address.State,
                    City = address.City,
                    Number = address.Number,
                    PostalCode = address.PostalCode,
                    Street = address.Street,
                    DateCreated = address.DateCreated
                },
                Message = "Successful",
                Status = true
            };
        }

        public AddressResponse<List<AddressDto>> GetAllAddresses()
        {
            var addresses = _addressRepository.GetAll();
            if (addresses == null)
            {
                return new AddressResponse<List<AddressDto>>
                {
                    Data = null,
                    Message = "No address found",
                    Status = false
                };
            }
            return new AddressResponse<List<AddressDto>>
            {
                Status = true,
                Message = "Successful",
                Data = addresses.Select(address => new AddressDto
                {
                    Id = address.Id,
                    State = address.State,
                    City = address.City,
                    Number = address.Number,
                    PostalCode = address.PostalCode,
                    Street = address.Street,
                    DateCreated = address.DateCreated
                }).ToList()
            };
        }
    }
}
