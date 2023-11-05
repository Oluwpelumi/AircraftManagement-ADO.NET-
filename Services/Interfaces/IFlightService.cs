using AircraftM.DTOs;
using AircraftM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.Services.Interfaces
{
    public interface IFlightService
    {
        FlightResponse<FlightDto> ScheduleFlight(FlightRequestModel model);
        FlightResponse<FlightDto> GetFlight(string referenceNumber);
        FlightResponse<List<FlightDto>> GetAllFlights();
        FlightResponse<bool> DeleteFlight(string referenceNumber);
    }
}
