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
    public class Flights
    {
        IFlightService _flightService = new FlightService();
        IAircraftService _aircraftService = new AircraftService();
        IPilotService _pilotService = new PilotService();
        public void FlightMgtMenu()
        {
            bool opt2 = true;
            while (opt2)
            {
                Console.WriteLine("Enter 1 to register Flight\nEnter 2 to view all flights\nEnter 3 to cancel an flight\nEnter 4 to exit");
                // int opt = int.Parse();
                if (int.TryParse(Console.ReadLine(), out int opt))
                {
                    if (opt == 1)
                    {
                        RegisterFlightMenu();
                    }
                    else if (opt == 2)
                    {
                        ViewAllFlightMenu();
                    }
                    else if (opt == 3)
                    {
                        CancelFlightMenu();
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



        public void RegisterFlightMenu()
        {
            try
            {
                Console.WriteLine("enter the take-offpoint of the flight");
                string takeOffPoint = Console.ReadLine();
                Console.WriteLine("enter destination of the flight");
                string destination = Console.ReadLine();
                Console.WriteLine("enter the takeoff time of the flight");
                bool isValidate = DateTime.TryParse(Console.ReadLine(), out DateTime takeOffTime);
                Console.WriteLine("enter the pilot staffnumber of the flight");
                string pilotStaffNumber = Console.ReadLine();
                Console.WriteLine("enter flight's aircraft name");
                string aircraftName = Console.ReadLine();
                Console.WriteLine("enter the price of the flight");
                double price = double.Parse(Console.ReadLine());
                System.Console.WriteLine();


                var aircraft = _aircraftService.GetAircraftByName(aircraftName);
                var pilot = _pilotService.GetPilot(pilotStaffNumber);
                if (aircraft.Status)
                {
                    if (pilot.Status)
                    {
                        var model = new FlightRequestModel
                        {
                            ReferenceNumber = "FLT" + "/" + new Random().Next(1, 99),
                            TakeOffPoint = takeOffPoint,
                            TakeOfTime = takeOffTime,
                            Destination = destination,
                            AircraftName = aircraftName,
                            PilotStaffNumber = pilotStaffNumber,
                            Price = price,
                        };

                        var register = _flightService.ScheduleFlight(model);

                        if (register.Status)
                        {
                            Console.WriteLine(register.Message);
                        }
                        else
                        {
                            Console.WriteLine(register.Message);
                            FlightMgtMenu();
                        }
                    }
                    else
                    {
                        Console.WriteLine(pilot.Message);
                    }
                }
                else
                {
                    Console.WriteLine(aircraft.Message);
                }
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }


        public void ViewAllFlightMenu()
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
        public void CancelFlightMenu()
        {
            var flights = _flightService.GetAllFlights();
            foreach (var item in flights.Data)
            {
                System.Console.WriteLine($"ID:{item.Id}\t REF-NO:{item.ReferenceNumber}\t T.O.P:{item.TakeOffPoint}\t DES:{item.Destination}\t T.O.T:{item.TakeOfTime}\t PLT STF-NO:{item.PilotStaffNumber}\t ACFT-NAME:{item.AircraftName}\t PR:{item.Price}\t DC:{item.DateCreated}");
            }

            System.Console.WriteLine("Enter the reference number of the flight you wanna cancel: ");
            string refNum = Console.ReadLine();

            var response = _flightService.DeleteFlight(refNum);
            if (response.Status)
            {
                System.Console.WriteLine(response.Message);
                FlightMgtMenu();
            }
            else
            {
                System.Console.WriteLine(response.Message);
            }
        }
    }
}
