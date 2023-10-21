using AircraftM.DTOs;
using AircraftM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.Services.Interfaces
{
    public interface IAirportService
    {
        AirportResponse<AirportDto> RegisterAirport(AirportRequestModel model);
        AirportResponse<AirportDto> GetAirport(string name);
        AirportResponse<List<AirportDto>> GetAllAirports();
        AirportResponse<bool> DeleteAirport(string name);
    }
}
