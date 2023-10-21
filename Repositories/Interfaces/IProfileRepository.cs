using AircraftM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.Repositories.Interfaces
{
    public interface IProfileRepository
    {
        Profile Create(Profile profile);
        Profile Get(string userEmail);
        List<Profile> GetAll();
        bool Delete(string userEmail);
    }
}
