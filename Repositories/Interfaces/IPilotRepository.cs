using AircraftM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.Repositories.Interfaces
{
    public interface IPilotRepository
    {
        Pilot Register(Pilot pilot);
        Pilot Get(string staffNumber);
        Pilot GetById(string id);
        List<Pilot> GetAll();
        bool Delete(string staffNumber);
        bool UpdateWallet(string userEmail, double newWalletAmount);
    }
}
