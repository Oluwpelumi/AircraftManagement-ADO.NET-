using AircraftM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.Repositories.Interfaces
{
    public interface IAddressRepository
    {
        Address Create(Address address);
        Address Get(string id);
        List<Address> GetAll();
        bool Delete(string id);
    }
}
