using AircraftM.DTOs;
using AircraftM.Models;
using AircraftM.Services.Implementations;
using AircraftM.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.Menu
{
    public class Bookings
    {
        IAircraftService _aircraftService = new AircraftService();
        IFlightService _flightService = new FlightService();
        IUserService _userService = new UserService();
        IPassengerService _passengerService = new PassengerService();
        IBookingService _bookingService = new BookingService();

        public void BookingMgtMenu()
        {
            bool opt2 = true;
            while (opt2)
            {
                Console.WriteLine("Enter 1 to make booking\nEnter 2 to view a booking / all bookings\nEnter 3 to cancel booking\nEnter 4 to exit");

                if (int.TryParse(Console.ReadLine(), out int opt))
                {
                    if (opt == 1)
                    {
                        MakeBookingMenu();
                    }
                    else if (opt == 2)
                    {
                        ViewBookingsMenu();
                    }
                    else if (opt == 3)
                    {
                        CancelBookingsMenu();
                    }
                    else if (opt == 4)
                    {
                        opt2 = false;
                    }
                }
                else
                {
                    System.Console.WriteLine("Invalid Input!!");
                    // Thread.Sleep(2000);
                }

            }

        }




        public void CancelBookingsMenu()
        {
            Console.WriteLine("You have to be a registered and certified manager before you can delete bookings....");
            Console.WriteLine();
            System.Console.WriteLine("Enter your e-mail to cancel a booked a flight");
            string email = Console.ReadLine();
            var user = _userService.GetUser(email);
            if (user.Status)
            {
                if (user.Data.RoleName == "manager")
                {
                    System.Console.WriteLine("Enter your booking-REFNUM to cancel your booking");
                    string bk_ref = Console.ReadLine();
                    var bk = _bookingService.GetBooking(bk_ref);
                    if (bk != null)
                    {
                        _bookingService.CancelBooking(bk.Data.ReferenceNumber);
                        Console.WriteLine(bk.Message);
                    }
                    else
                    {
                        Console.WriteLine(bk.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Sorry! You are not eligible to cancel a booked flight..");
                }
            }
            else
            {
                Console.WriteLine(user.Message);
            }
            
        }


        public void MakeBookingMenu()
        {
            var flights = _flightService.GetAllFlights();
            if (flights.Status == false)
            {
                System.Console.WriteLine(flights.Message);
                Passenger p = new Passenger();
                p.PassengerMenu();
            }
            else
            {
                try
                {
                    foreach (var item in flights.Data)
                    {
                        System.Console.WriteLine($"ID:{item.Id}\t REF-NO:{item.ReferenceNumber}\t T.O.P:{item.TakeOffPoint}\t DES:{item.Destination}\t T.O.T:{item.TakeOfTime}\t PLT STF-NO:{item.PilotStaffNumber}\t ACFT-NAME:{item.AircraftName}\t PR:{item.Price}\t DC:{item.DateCreated}");
                    }
                    System.Console.WriteLine("Enter your e-mail to book a flight");
                    string email = Console.ReadLine();
                    System.Console.WriteLine("Enter the Flight's Reference number");
                    string flightRef = Console.ReadLine();

                    var bookingModel = new BookingRequestModel
                    {
                        FlightReferenceNumber = flightRef,
                        PassengerEmail = email
                    };
                    var make = _bookingService.MakeBooking(bookingModel);
                    if (make.Status)
                    {
                        Console.WriteLine(make.Message);
                    }
                    else
                    {
                        Console.WriteLine(make.Message);
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }


        public void ViewBookingsMenu()
        {
            System.Console.WriteLine("Enter 1 to view booking of a particular passenger\nEnter 2 to view all bookings\nEnter 3 to exit: ");
            int Input = int.Parse(Console.ReadLine());
            var bookings = _bookingService.GetAllBookings();
            if (Input == 1)
            {
                if (bookings.Data.Count <= 0)
                {
                    System.Console.WriteLine("No booking made yet");
                }
                else
                {
                    System.Console.WriteLine("Enter the reference number for the booking: ");
                    string reff = Console.ReadLine();
                    var bk = _bookingService.GetBooking(reff);
                    if (bk.Status)
                    {
                        System.Console.WriteLine(bk.Message);
                    }
                    else
                    {
                        System.Console.WriteLine(bk.Message);
                    }
                }

            }
            else if (Input == 2)
            {
                Console.WriteLine("You have to be a registered and certified manager before you can view all bookings....");
                Console.WriteLine();
                System.Console.WriteLine("Enter your e-mail to view all booked a flights");
                string email = Console.ReadLine();
                var user = _userService.GetUser(email);
                if (user.Status)
                {
                    if (user.Data.RoleName == "manager")
                    {
                        if (bookings.Data.Count <= 0)
                        {
                            System.Console.WriteLine("No bookings found");
                        }
                        else
                        {
                            foreach (var booking in bookings.Data)
                            {
                                System.Console.WriteLine($"ID:{booking.Id}\t REF-NO:{booking.ReferenceNumber}\t SEAT-NO:{booking.SeatNumber}\t P/EMAIL:{booking.PassengerEmail}\t FLIGHTEREF-NO:{booking.FlightReferenceNumber}\t ACFT-NAME:{booking.AircraftName}\t DC:{booking.DateCreated}");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Sorry! You are not eligible to view all booked flight..");
                    }
                }
                else
                {
                    Console.WriteLine(user.Message);
                }
            }
            else
            {
                System.Console.WriteLine("invalid input! ");
            }
        }

    }
}
