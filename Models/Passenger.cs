using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.Models
{
    public class Passenger : AuditableEntities
    {
        public string UserId { get; set; }
        public string RegNumber { get; set; }
        public string BookingId { get; set; }
        public double Wallet { get; set; }
    }
}
