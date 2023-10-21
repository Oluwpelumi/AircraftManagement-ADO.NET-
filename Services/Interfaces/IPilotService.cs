using AircraftM.DTOs;
using AircraftM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.Services.Interfaces
{
    public interface IPilotService
    {
        PilotResponse<PilotDto> RegisterPilot(PilotRequestModel model);
        PilotResponse<PilotDto> GetPilot(string staffNumber);
        PilotResponse<List<PilotDto>> GetAllPilots();
        PilotResponse<bool> DeletePilot(string staffNumber);
        PilotResponse<bool> FundWallet(string staffNumber, double amount);
    
}
