using AircraftM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.Repositories.Interfaces
{
    public interface IPassengerRepository
    {
        Passenger Register(Passenger passenger);
        Passenger GetById(string id);
        Passenger Get(string regNumber);
        List<Passenger> GetAll();
        bool Delete(string regNumber);
        bool UpdateWallet(string userEmail, double newWalletAmount);
    }
}
