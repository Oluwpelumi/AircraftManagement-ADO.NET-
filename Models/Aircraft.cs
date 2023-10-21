using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.Models
{
    public class Aircraft : AuditableEntities
    {
        public string Name { get; set; }
        public string EngineNumber { get; set; }
        public int Capacity { get; set; }
    }
}
