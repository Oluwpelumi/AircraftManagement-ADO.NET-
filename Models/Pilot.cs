using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.Models
{
    public class Pilot : AuditableEntities
    {
        public string UserId { get; set; }
        public string StaffNumber{ get; set; }
        public double Wallet { get; set; }
    }
}
