using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.DTOs
{
    public record ManagerDto
    {
        public string Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string UserName { get; init; }
        public string UserEmail { get; init; }
        public DateTime DOB { get; init; }
        public string Gender { get; init; }
        public int Number { get; init; }
        public string Street { get; init; }
        public string City { get; init; }
        public string State { get; init; }
        public string PostalCode { get; init; }
        public string UserId { get; init; }
        public string StaffNumber { get; init; }
        public double Wallet { get; set; }
        public DateTime DateCreated { get; init; }
    }

    public record ManagerRequestModel
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string UserName { get; init; }
        public string UserEmail { get; init; }
        public string Password { get; init; }
        public string RoleName { get; init; }
        public DateTime DOB { get; init; }
        public string Gender { get; init; }
        public int Number { get; init; }
        public string Street { get; init; }
        public string City { get; init; }
        public string State { get; init; }
        public string PostalCode { get; init; }
        public double Wallet { get; set; }
    }

    public class ManagerResponse<T>
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public T Data { get; set; }
    }
}
