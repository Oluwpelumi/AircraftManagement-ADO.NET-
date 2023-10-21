using AircraftM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User Create(User user);
        User Get(string userEmail);
        User GetById(string id);
        List<User> GetAllUsers();
        bool UpdateWallet(string userEmail, double newWalletAmount);
        bool Delete(string userEmail);
    }
}
