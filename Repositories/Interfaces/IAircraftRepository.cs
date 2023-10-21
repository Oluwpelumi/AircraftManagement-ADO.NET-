using AircraftM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.Repositories.Interfaces
{
    public interface IAircraftRepository
    {
        Aircraft Create(Aircraft aircraft);
        Aircraft Get(string engineNumber);
        Aircraft GetByName(string name);
        List<Aircraft> GetAll();
        bool Delete(string engineNumber);
    }
}
