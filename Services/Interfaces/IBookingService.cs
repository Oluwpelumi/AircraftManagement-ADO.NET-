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
        BookingResponse<BookingDto> MakeBooking(BookingRequestModel model);
        BookingResponse<BookingDto> GetBooking(string referenceNumber);
        BookingResponse<List<BookingDto>> GetAllBookings();
        BookingResponse<bool> CancelBooking(string referenceNumber);
    }
}
