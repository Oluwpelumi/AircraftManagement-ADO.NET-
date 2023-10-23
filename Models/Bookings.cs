using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AircraftM.Models
{
    public class Bookings : AuditableEntities
    {
        public string ReferenceNumber{ get; set; }
        public int SeatNumber{ get; set; }
        public string PassengerEmail{ get; set; }
        public string FlightReferenceNumber{ get; set; }
    }
}
