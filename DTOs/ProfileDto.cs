using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.DTOs
{
    public record ProfileDto
    {
        public string Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string UserName { get; init; }
        public string UserEmail { get; init; }
        public DateTime DOB { get; init; }
        public string Gender { get; init; }
        public DateTime DateCreated { get; init; }
    }


    public record ProfileRequestModel
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string UserName { get; init; }
        public string UserEmail { get; init; }
        public DateTime DOB { get; init; }
        public string Gender { get; init; }
    }

    public class ProfileResponse<T>
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public T Data { get; set; }
    }
}
