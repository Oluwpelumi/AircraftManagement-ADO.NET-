using AircraftM.DTOs;
using AircraftM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.Services.Interfaces
{
    public interface IPassengerService
    {
        PassengerResponse<PassengerDto> RegisterPassenger(PassengerRequestModel model);
        //PassengerResponse<PassengerDto> MakeBooking(PassengerMakeBookingRequestModel model);
        PassengerResponse<PassengerDto> GetPassenger(string regNumber);
        PassengerResponse<List<PassengerDto>> GetAllPassengers();
        PassengerResponse<bool> DeletePassenger(string regNumber);
        PassengerResponse<bool> FundWallet(string staffNumber, double amount);
    }
}
