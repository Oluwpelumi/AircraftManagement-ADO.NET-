using AircraftM.DTOs;
using AircraftM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.Services.Interfaces
{
    public interface IAddressService
    {
        AddressResponse<AddressDto> GetAddress(string id);
        AddressResponse<List<AddressDto>> GetAllAddresses();
        AddressResponse<bool> DeleteAddress(string id);
    }
}
