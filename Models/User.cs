using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.Models
{
    public class User : AuditableEntities
    {
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public string AddressId { get; set; }
        public string ProfileId { get; set; }
        public string RoleId { get; set; }
    }
}
