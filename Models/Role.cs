using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.Models
{
    public class Role : AuditableEntities
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
