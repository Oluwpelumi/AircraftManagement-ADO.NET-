using AircraftM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.Repositories.Interfaces
{
    public interface IFlightRepository
    {
        Flight Book(Flight flight);
        Flight Get(string referenceNumber);
        List<Flight> GetAll();
        bool Delete(string referenceNumber);
    }
}
