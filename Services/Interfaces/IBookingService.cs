using AircraftM.DTOs;
using AircraftM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.Services.Interfaces
{
    public interface IBookingService
    {
        BookingResponse<Bookings> MakeBooking(BookingRequestModel model);
        BookingResponse<Bookings> GetBooking(string id);
        BookingResponse<List<Bookings>> GetAllBookings();
        BookingResponse<bool> DeleteBooking(string id);
    }
}
