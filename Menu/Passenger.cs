using AircraftM.Models;
using AircraftM.Services.Implementations;
using AircraftM.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.Menu
{
    public class Passenger
    {
        IAircraftService _aircraftService = new AircraftService();
        IAirportService _airportService = new AirportService();
        IFlightService _flightService = new FlightService();
        IPilotService _pilotService = new PilotService();
        IUserService _userService = new UserService();
        IPassengerService _passengerService = new PassengerService();
        IBookingService _bookingService = new BookingService();

        public void PassengerMenu()
        {
            bool opt3 = true;
            while (opt3)
            {
                Console.WriteLine("Enter 1 to Fund your wallet\nEnter 2 to view all Airports\nEnter 3 to view all Aircrafts\nEnter 4 to view all avialable Flights\nEnter 5 to check your wallet balance\nEnter 6 to book a Flight\nEnter 7 to exit");

                if (int.TryParse(Console.ReadLine(), out int input))
                {
                    if (input == 1)
                    {
                        FundWalletMenu();
                    }
                    else if (input == 2)
                    {
                        ViewAllAirports();
                    }
                    else if (input == 3)
                    {
                        ViewAllAircrafts();
                    }
                    else if (input == 4)
                    {
                        ViewAllFlights();
                    }
                    else if (input == 5)
                    {
                        CheckBalance();
                    }
                    else if (input == 6)
                    {
                        Bookings bk = new Bookings();
                        bk.MakeBookingMenu();
                    }
                    else if (input == 7)
                    {
                        opt3 = false;
                    }
                }
                else
                {
                    System.Console.WriteLine("Invalid Input!!");
                    // Thread.Sleep(2000);
                }
            }
        }



        public void CheckBalance()
        {
            System.Console.WriteLine("Enter your regNumber to fund your wallet:");
            string regNumber = Console.ReadLine();
            var passenger = _passengerService.GetPassenger(regNumber);
            Console.WriteLine($"Your wallet balance is: #{passenger.Data.Wallet}");
        }


        public void FundWalletMenu()
        {
            try
            {
                System.Console.WriteLine("Enter your regNumber to fund your wallet:");
                string regNumber = Console.ReadLine();
                System.Console.WriteLine("Enter the amount you want to add to your wallet:");
                double amount = double.Parse(Console.ReadLine());
                var passenger = _passengerService.GetPassenger(regNumber);
                if (passenger.Status)
                {
                    if (amount > 0)
                    {
                        var response = _passengerService.FundWallet(regNumber, amount);
                        if (response.Status)
                        {
                            Console.WriteLine(response.Message);
                        }
                        else
                        {
                            Console.WriteLine(response.Message);
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("The amount you input is a negative value!");
                    }
                }
                else
                {
                    System.Console.WriteLine(passenger.Message);
                }
            }
            catch (FormatException)
            {
                System.Console.WriteLine("Invalid input for the amount!");
            }
        }


        public void ViewAllAirports()
        {
            var airports = _airportService.GetAllAirports();
            if (airports.Status)
            {
                foreach (var airport in airports.Data)
                {
                    System.Console.WriteLine($"ID:{airport.Id}\t NAME:{airport.Name}\t LOC:{airport.Location}\t TYPE:{airport.AirportType}\t DC:{airport.DateCreated}");
                }
            }
            else
            {
                Console.WriteLine(airports.Message);
            }
        }

       

        public void ViewAllAircrafts()
        {
            var aircrafts = _aircraftService.GetAllAircrafts();
            if (aircrafts.Status)
            {
                foreach (var aircraft in aircrafts.Data)
                {
                    System.Console.WriteLine($"ID:{aircraft.Id}\t NAME:{aircraft.Name}\t ENG-NO:{aircraft.EngineNumber}\t CAP:{aircraft.Capacity}\t DC:{aircraft.DateCreated}");
                }
            }
            else
            {
                Console.WriteLine(aircrafts.Message);
            }
        }


        public void ViewAllFlights()
        {
            var flights = _flightService.GetAllFlights();
            if (flights.Status)
            {
                foreach (var item in flights.Data)
                {
                    System.Console.WriteLine($"ID:{item.Id}\t REF-NO:{item.ReferenceNumber}\t T.O.P:{item.TakeOffPoint}\t DES:{item.Destination}\t T.O.T:{item.TakeOfTime}\t PLT STF-NO:{item.PilotStaffNumber}\t ACFT-NAME:{item.AircraftName}\t PR:{item.Price}\t DC:{item.DateCreated}");
                }
            }
            else
            {
                Console.WriteLine(flights.Message);
            }
        }

    }
}
