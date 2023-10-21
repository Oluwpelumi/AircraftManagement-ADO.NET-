using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.DTOs
{
    public record AddressDto
    {
        public string Id { get; init; }
        public int Number { get; init; }
        public string Street { get; init; }
        public string City { get; init; }
        public string State { get; init; }
        public string PostalCode { get; init; }
        public DateTime DateCreated { get; init; }
    }

    public record AddressRequestModel
    {
        public int Number { get; init; }
        public string Street { get; init; }
        public string City { get; init; }
        public string State { get; init; }
        public string PostalCode { get; init; }
    }

    public class AddressResponse<T>
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public T Data { get; set; }
    }
}
