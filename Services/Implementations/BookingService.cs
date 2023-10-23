using AircraftM.DTOs;
using AircraftM.Models;
using AircraftM.Repositories.Implementations;
using AircraftM.Repositories.Interfaces;
using AircraftM.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public BookingResponse<bool> DeleteBooking(string id)
        {
            throw new NotImplementedException();
        }

        public BookingResponse<List<Bookings>> GetAllBookings()
        {
            throw new NotImplementedException();
        }

        public BookingResponse<Bookings> GetBooking(string id)
        {
            throw new NotImplementedException();
        }

        public BookingResponse<Bookings> MakeBooking(BookingRequestModel model)
        {
            var passenger = _passengerRepository.Get(model.PassengerEmail);
            var user = _userRepository.GetById(passenger.UserId);
            var flight = _flightRepository.Get(model.FlightReferenceNumber);
            var aircraft = _aircraftRepository.GetByName(flight.AircraftName);
            var flights = _flightRepository.GetAll();

            //var ps = new List<Passenger>();
            //foreach (var item in passengers)
            //{
            //    if (item.FlightId == flight.Id)
            //    {
            //        ps.Add(item);
            //    }
            //}

            //passenger.Wallet -= flight.Price;
            //passengerInterface.Update(passenger.UserEmail);
            //userInterface.Update(passengerEmail);
            //flight.Passengers.Add(passengerEmail);
            //string refNum = flight.Passengers.Count + "AirLine" + new Random().Next(1, 99);
            //var booking = new Booking(bookingDb.Count + 1, refNum, flight.Passengers.Count, passengerEmail, flightReferenceNumber);
            //bookingDb.Add(booking);
            //AddToFile(booking);
            //Console.WriteLine($"booking with ref {booking.ReferenceNumber} is successful, your seat number is {booking.SeatNumber}, you are going with aircraft {aircraft.Name}");
            //return booking;
            var check = flights.Any(ft => ft.AircraftName == aircraft.Name);
            if (check)
            {
                var flts = flights.Where(flt => flt.AircraftName == aircraft.Name).ToList();
                if (flts.Count < aircraft.Capacity)
                {
                    if (flight.Price <= passenger.Wallet)
                    {
                        passenger.Wallet -= flight.Price;
                        _passengerRepository.UpdateWallet(user.UserEmail, passenger.Wallet);
                        _userRepository.UpdateWallet(user.UserEmail, passenger.Wallet);

                        //string refNum = Guid.NewGuid().ToString().Substring(0, 4) + "FLT" + new Random().Next(0, 99);
                    }
                    else
                    {
                        Console.WriteLine("Insuficient Balance");
                    }
                }
                else
                {
                    Console.WriteLine("The Flight is filled up....Unable to book flight");
                }
            }
            else
            {
                return new BookingResponse<Bookings>
                {
                    Data = null,
                    Message = $"There is a flight scheduled for the aircraft{aircraft.Name} already",
                     Status = false
                };
            }

        }
    }
}
