using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.DTOs
{
    public record RoleDto
    {
        public string Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public DateTime DateCreated { get; init; }
    }

    public record RoleRequestModel
    {
        public string Name { get; init; }
        public string Description { get; init; }
    }


    public class RoleResponse<T>
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public T Data { get; set; }
    }
}


