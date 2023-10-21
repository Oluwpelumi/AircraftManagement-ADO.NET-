using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.DTOs
{
    public record AircraftDto
    {
        public string Id { get; init; }
        public string Name { get; init; }
        public string EngineNumber { get; init; }
        public int Capacity { get; init; }
        public DateTime DateCreated { get; init; }
    }

    public record AircraftRequestModel
    {
        public string Name { get; init; }
        public string EngineNumber { get; init; }
        public int Capacity { get; init; }
    }

    public class AircraftResponse<T>
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public T Data { get; set; }
    }
}
