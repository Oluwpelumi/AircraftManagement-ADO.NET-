using AircraftM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Role Register(Role role);
        Role Get(string name);
        Role GetById(string id);
        List<Role> GetAll();
        bool Delete(string name);
    }
}
