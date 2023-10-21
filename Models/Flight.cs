using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.Models
{
    public class Flight : AuditableEntities
    {
        public string Name{ get; set; }
        public string ReferenceNumber{ get; set; }
        public string TakeOffPoint{ get; set; }
        public string Destination{ get; set; }
        public DateTime TakeOfTime{ get; set; }
        public string PilotStaffNumber{ get; set; }
        public string AircraftName{ get; set; }
        public double Price{ get; set; }
    }
}
