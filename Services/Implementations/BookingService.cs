using AircraftM.DTOs;
using AircraftM.Models;
using AircraftM.Repositories.Implementations;
using AircraftM.Repositories.Interfaces;
using AircraftM.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.Services.Implementations
{
    public class BookingService : IBookingService
    {
        IPassengerRepository _passengerRepository = new PassengerRepository();
        IUserRepository _userRepository = new UserRepository();
        IAircraftRepository _aircraftRepository = new AircraftRepository();
        IFlightRepository _flightRepository = new FlightRepository();   
        IBookingsRepository _bookingsRepository = new BookingsRepository(); 


        public BookingResponse<bool> CancelBooking(string referenceNumber)
        {
            var bk = _bookingsRepository.Get(referenceNumber);
            if (bk != null)
            {
                _bookingsRepository.Delete(bk.Id);
                return new BookingResponse<bool>
                {
                    Data = true,
                    Message = $"Bookig with the Id{bk.Id} has been cancelled successfully.",
                    Status = true
                };
            }
            return new BookingResponse<bool>
            {
                Data = false,
                Message = $"Bookig with the Id{bk.Id} does not exist.",
                Status = false
            };
        }

        public BookingResponse<List<BookingDto>> GetAllBookings()
        {
            var bookings = _bookingsRepository.GetAll();
            if (bookings != null)
            {
                return new BookingResponse<List<BookingDto>>
                {
                    Data = bookings.Select(booking => new BookingDto
                    {
                        Id = booking.Id,
                        ReferenceNumber = booking.ReferenceNumber,
                        FlightReferenceNumber = booking.FlightReferenceNumber,
                        PassengerEmail = booking.PassengerEmail,
                        SeatNumber = booking.SeatNumber,
                        AircraftName = booking.AircraftName,
                        DateCreated =booking.DateCreated
                    }).ToList(),
                    Message = "Successful",
                    Status = true
                };
            }
            return new BookingResponse<List<BookingDto>>
            {
                Data = null,
                Message = "No bookings yet",
                Status = false
            };
        }

        public BookingResponse<BookingDto> GetBooking(string referenceNumber)
        {
            var book = _bookingsRepository.Get(referenceNumber);
            if (book != null)
            {
                return new BookingResponse<BookingDto>
                {
                    Data = new BookingDto
                    {
                        Id = book.Id,
                        ReferenceNumber = book.ReferenceNumber,
                        SeatNumber = book.SeatNumber,
                        PassengerEmail = book.PassengerEmail,
                        FlightReferenceNumber = book.FlightReferenceNumber,
                        AircraftName = book.AircraftName,
                        DateCreated = book.DateCreated
                    },
                    Message = "Successful",
                    Status = true
                };
            }
            return new BookingResponse<BookingDto>
            {
                Data = null,
                Message = "No booking Found",
                Status = false
            };
        }

        public BookingResponse<BookingDto> MakeBooking(BookingRequestModel model)
        {
            var user = _userRepository.Get(model.PassengerEmail);
            var passenger = _passengerRepository.GetAll().SingleOrDefault(a => a.UserId == user.Id);
            var flight = _flightRepository.Get(model.FlightReferenceNumber);
            var aircraft = _aircraftRepository.GetByName(flight.AircraftName);
            //var flights = _flightRepository.GetAll();
            var bookings = _bookingsRepository.GetAll().Where(bk => bk.AircraftName == aircraft.Name).ToList();
            if (user != null)
            {
                if (flight != null)
                {
                    if (flight.Price <= passenger.Wallet && bookings.Count < aircraft.Capacity)
                    {
                        passenger.Wallet -= flight.Price;
                        _passengerRepository.UpdateWallet(passenger.RegNumber, passenger.Wallet);
                        Bookings bk = new Bookings
                        {
                            AircraftName = aircraft.Name,
                            FlightReferenceNumber = model.FlightReferenceNumber,
                            PassengerEmail = model.PassengerEmail,
                            SeatNumber = bookings.Count + 1,
                            ReferenceNumber = "BLK" + "/" + new Random().Next(1,99) + "/" + bookings.Count+1
                        };
                        _bookingsRepository.Make(bk);
                        _passengerRepository.UpdateBookingId(passenger.RegNumber, bk.Id);
                        return new BookingResponse<BookingDto>
                        {
                            Data = new BookingDto
                            {
                                AircraftName = bk.AircraftName,
                                Destination = flight.Destination,
                                FlightReferenceNumber = bk.FlightReferenceNumber,
                                PassengerEmail = bk.PassengerEmail,
                                PilotStaffNumber = flight.PilotStaffNumber,
                                Price = flight.Price,
                                ReferenceNumber = bk.ReferenceNumber,
                                SeatNumber = bk.SeatNumber,
                                TakeOffPoint = flight.TakeOffPoint,
                                TakeOfTime = flight.TakeOfTime
                            },
                            Message = "Booking successful",
                            Status = true
                        };
                    }
                    else
                    {
                        string message = "";
                        if (flight.Price > passenger.Wallet)
                        {
                            message = "Insufficient Balance";
                        }
                        else if (bookings.Count >= aircraft.Capacity)
                        {
                            message = $"The aircraft{aircraft.Name} is filled up already .......  Unable to book flight";
                        }
                        return new BookingResponse<BookingDto>
                        {
                            Data = null,
                            Status = false,
                            Message = message
                        };
                    }
                }
                else
                {
                    return new BookingResponse<BookingDto>
                    {
                        Data = null,
                        Status = false,
                        Message = $"No flight with the reference-number {model.FlightReferenceNumber}"
                    };
                }
            }
            else
            {
                return new BookingResponse<BookingDto>
                {
                    Data = null,
                    Status = false,
                    Message = "Invalid Email Address"
                };
            }

        }
    }
}