using AircraftM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.Repositories.Interfaces
{
    public interface IManagerRepository
    {
        Manager Register(Manager manager);
        Manager GetById(string userId);
        Manager Get(string staffNumber);
        List<Manager> GetAll();
        bool Delete(string staffNumber);
        bool UpdateWallet(string userEmail, double newWalletAmount);
    }
}
