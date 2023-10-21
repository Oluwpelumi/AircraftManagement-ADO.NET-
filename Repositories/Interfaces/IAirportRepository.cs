using AircraftM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.Repositories.Interfaces
{
    public interface IAirportRepository
    {
        Airport Create(Airport airport);
        Airport Get(string name);
        List<Airport> GetAll();
        bool Delete(string name);
    }
}
