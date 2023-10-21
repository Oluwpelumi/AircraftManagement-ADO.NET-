
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.DTOs
{
    public record UserDto
    {
        public string Id { get; init; }
        public string UserEmail { get; set; }
        public int Number { get; init; }
        public string Street { get; init; }
        public string City { get; init; }
        public string State { get; init; }
        public string PostalCode { get; init; }
        public string AddressId { get; set; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string UserName { get; init; }
        public DateTime DOB { get; init; }
        public string Gender { get; init; }
        public string ProfileId { get; set; }
        public string RoleName { get; init; }
        public string RoleDescription { get; init; }
        public string RoleId { get; set; }
        public DateTime DateCreated { get; init; }
    }


    public record UserLoginModel
    {
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public string RoleName { get; init; }
    }

    public class UserResponse<T>
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public T Data { get; set; }
    }
}
