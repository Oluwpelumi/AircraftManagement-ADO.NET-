using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.DTOs
{
    public record FlightDto
    {
        public string Id { get; init; }
        public string ReferenceNumber { get; init; }
        public string TakeOffPoint { get; init; }
        public string Destination { get; init; }
        public DateTime TakeOfTime { get; init; }
        public string PilotStaffNumber { get; init; }
        public string AircraftName { get; init; }
        public double Price { get; init; }
        public DateTime DateCreated { get; init; }
    }


    public record FlightRequestModel
    {
        public string ReferenceNumber { get; init; }
        public string TakeOffPoint { get; init; }
        public string Destination { get; init; }
        public DateTime TakeOfTime { get; init; }
        public string PilotStaffNumber { get; init; }
        public string AircraftName { get; init; }
        public double Price { get; init; }
    }


    public class FlightResponse<T>
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public T Data { get; set; }
    }
}
