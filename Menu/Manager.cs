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
    public class Manager
    {
        IAircraftService _aircraftService = new AircraftService();
        IFlightService _flightService = new FlightService();
        IPilotService _pilotService = new PilotService();
        IUserService _userService = new UserService();
        IPassengerService _passengerService = new PassengerService();

        public void ManagerMenu()
        {
            bool opt3 = true;
            while (opt3)
            {
                Console.WriteLine("Enter 1 for Airport Mgt\nEnter 2 for Aircraft Mgt\nEnter 3 for Flight Mgt\nEnter 4 for Passenger Mgt\nEnter 5 for Pilot Mgt\nEnter 6 for Bookings Mgt\nEnter 7 to exit");
                
                if (int.TryParse(Console.ReadLine(), out int input))
                {
                    if (input == 1)
                    {
                        Airports airport = new Airports();
                        airport.AirPortMgtMenu();
                    }
                    else if (input == 2)
                    {
                        Aircrafts aircraft = new Aircrafts();
                        aircraft.AircraftMgtMenu();
                    }
                    else if (input == 3)
                    {
                        Flights flt = new Flights();
                        flt.FlightMgtMenu();
                    }
                    else if (input == 4)
                    {
                        PassengerMgtMenu();
                    }
                    else if (input == 5)
                    {
                        Pilots plt = new Pilots();
                        plt.PilotMgtMenu();
                    }
                    else if (input == 6)
                    {
                        Bookings bk = new Bookings();
                        bk.BookingMgtMenu();
                    }
                    else if (input == 7)
                    {
                        opt3 = false;
                    }
                    else if (input > 7)
                    {
                        System.Console.WriteLine("The integer value you enter is not within the options");
                    }
                }
                else
                {
                    System.Console.WriteLine("Invalid Input! Your input is not an integer value");
                    // Thread.Sleep(2000);
                }
            }

        }


        public void PassengerMgtMenu()
        {
            bool opt = true;
            while (opt)
            {
                System.Console.WriteLine("Enter 1 to view all passengers\nEnter 2 to remove passenger\nEnter 3 to go back to the Manager menu");
                bool chk = int.TryParse(Console.ReadLine(), out int k);
                if (chk)
                {
                    switch (k)
                    {
                        case 1:
                            ViewAllPassengers();
                            break;

                        case 2:
                            RemovePassenger();
                            break;

                        case 3:
                            opt = false;
                            break;
                    }
                }
                else
                {
                    System.Console.WriteLine("Invalid input!");
                }
            }
        }
        public void ViewAllPassengers()
        {
            var passengers = _passengerService.GetAllPassengers();
            if (passengers.Status)
            {
                foreach (var passenger in passengers.Data)
                {
                    System.Console.WriteLine($"ID:{passenger.Id}\t U-ID:{passenger.UserId}\t REG-NO:{passenger.RegNumber}\t WLT:{passenger.Wallet}\t BK-ID:{passenger.BookingId}\t DC:{passenger.DateCreated}");
                }
            }
            else
            {
                Console.WriteLine(passengers.Message);
            }
        }

        public void RemovePassenger()
        {
            ViewAllPassengers();
            System.Console.WriteLine("Enter the regNumber of the passenger you want to delete: ");
            string regNumber = Console.ReadLine();
            var response = _passengerService.DeletePassenger(regNumber);
            if (response.Status)
            {
                System.Console.WriteLine(response.Message);
            }
            else
            {
                System.Console.WriteLine($"Passenger with the regNumber {regNumber} does not exist! ");
            }
        }
    }
}
