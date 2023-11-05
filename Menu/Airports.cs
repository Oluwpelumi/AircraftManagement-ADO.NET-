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
    public class Airports
    {
        IAirportService _airportService = new AirportService();
        public void AirPortMgtMenu()
        {
            bool opt2 = true;
            while (opt2)
            {
                Console.WriteLine("Enter 1 to register airport\nEnter 2 to view all airports\nEnter 3 to delete an airport\nEnter 4 to exit");
                // int opt = int.Parse();
                if (int.TryParse(Console.ReadLine(), out int opt))
                {
                    if (opt == 1)
                    {
                        RegisterAirportMenu();
                    }
                    else if (opt == 2)
                    {
                        ViewAllAirportMenu();
                    }
                    else if (opt == 3)
                    {
                        DeleteAirportMenu();
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


        public void RegisterAirportMenu()
        {
            Console.WriteLine("enter airport name");
            string name = Console.ReadLine();
            Console.WriteLine("enter airport location");
            string location = Console.ReadLine();
            Console.WriteLine("enter the airport-type: local or international");
            string airportType = Console.ReadLine();

            var registerModel = new AirportRequestModel
            {
                AirportType = airportType,
                Location = location,
                Name = name
            };

            var register = _airportService.RegisterAirport(registerModel);

            if (register.Status)
            {
                System.Console.WriteLine(register.Message);
            }
            else
            {
                Console.WriteLine(register.Message);
                RegisterAirportMenu();
            }
        }

        public void ViewAllAirportMenu()
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


        public void DeleteAirportMenu()
        {
            var airports = _airportService.GetAllAirports();
            foreach(var airport in airports.Data)
                {
                System.Console.WriteLine($"{airport.Id}\t{airport.Name}\t{airport.Location}\t{airport.AirportType}\t{airport.DateCreated}");
            }
            System.Console.WriteLine("Enter the name of the airport you want to delete: ");
            string name = Console.ReadLine();

            var delete = _airportService.DeleteAirport(name);
            if (delete.Status)
            {
                System.Console.WriteLine(delete.Message);
                AirPortMgtMenu();
            }
            else
            {
                System.Console.WriteLine(delete.Message);
            }
        }
    }
}
