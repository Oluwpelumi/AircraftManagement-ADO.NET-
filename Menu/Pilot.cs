using AircraftM.Services.Implementations;
using AircraftM.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.Menu
{
    public class Pilot
    {
        IAircraftService _aircraftService = new AircraftService();
        IFlightService _flightService = new FlightService();

        public void PilotMenu()
        {
            bool opt = true;
            while (opt)
            {
                Console.WriteLine("Enter 1 to view all Aircrafts\nEnter 2 to view all avialable flights\nEnter 3 to exit");
                // int input = int.Parse(Console.ReadLine());
                if (int.TryParse(Console.ReadLine(), out int input))
                {
                    if (input == 1)
                    {
                        ViewAllAircraftsMenu();
                    }
                    else if (input == 2)
                    {
                        ViewAllFlightsMenu();
                    }
                    else if (input == 3)
                    {
                        opt = false;
                    }
                    else if (input < 0)
                    {
                        System.Console.WriteLine("The input value you entered is a negative number");
                    }
                    else
                    {
                        System.Console.WriteLine("The value you entered is out of range");
                    }
                }
                else
                {
                    System.Console.WriteLine("Invalid Input!!");
                    // Thread.Sleep(2000);
                }
            }
        }



        public void ViewAllAircraftsMenu()
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



        public void ViewAllFlightsMenu()
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
