using AircraftM.DTOs;
using AircraftM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.Services.Interfaces
{
    public interface IAircraftService
    {
        AircraftResponse<AircraftDto> RegisterAircraft(AircraftRequestModel model);
        AircraftResponse<AircraftDto> GetAircraft(string engineNumber);
        AircraftResponse<AircraftDto> GetAircraftByName(string name);
        AircraftResponse<List<AircraftDto>> GetAllAircrafts();
        AircraftResponse<bool> DeleteAircraft(string engineNumber);
    }
}
