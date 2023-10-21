using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.Models
{
    public class AuditableEntities
    {
        public string Id { get; set; } = Guid.NewGuid().ToString().Substring(0, 5);
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
