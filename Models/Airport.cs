using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.Models
{
    public class Airport : AuditableEntities
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string AirportType { get; set; }
    }
}
