using AircraftM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.Repositories.Interfaces
{
    public interface IBookingsRepository
    {
        Bookings Make(Bookings bk);
        Bookings Get(string id);
        List<Bookings> GetAll();
        bool Delete(string id);
    }
}
