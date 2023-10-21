using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.DTOs
{
    public record AirportDto
    {
        public string Id { get; init; }
        public string Name { get; init; }
        public string Location { get; init; }
        public string AirportType { get; init; }
        public DateTime DateCreated { get; init; }
    }


    public record AirportRequestModel
    {
        public string Name { get; init; }
        public string Location { get; init; }
        public string AirportType { get; init; }
    }

    public class AirportResponse<T>
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public T Data { get; set; }
    }
}
