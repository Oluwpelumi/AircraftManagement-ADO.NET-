using AircraftM.DTOs;
using AircraftM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.Services.Interfaces
{
    public interface IManagerService
    {
        ManagerResponse<ManagerDto> RegisterManager(ManagerRequestModel model);
        ManagerResponse<ManagerDto> GetManager(string staffNumber);
        ManagerResponse<List<ManagerDto>> GetAllManagers();
        ManagerResponse<bool> DeleteManager(string staffNumber);
        ManagerResponse<bool> FundWallet(string staffNumber, double amount);
    }
}
