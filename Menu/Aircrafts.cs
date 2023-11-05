using AircraftM.DTOs;
using AircraftM.Services.Implementations;
using AircraftM.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.Menu
{
    public class Aircrafts
    {
        IAircraftService _aircraftService = new AircraftService();
        public void AircraftMgtMenu()
        {
            bool check = true;
            while (check)
            {
                Console.WriteLine("Enter 1 to register aircraft\nEnter 2 to view all aircrafts\nEnter 3 to delete an aircraft\nEnter 4 to exit");
                if (int.TryParse(Console.ReadLine(), out int opt4))
                {
                    if (opt4 == 1)
                    {
                        RegisterAircraftMenu();
                    }
                    else if (opt4 == 2)
                    {
                        ViewAllAircraftMenu();
                    }
                    else if (opt4 == 3)
                    {
                        DeleteAircraftMenu();
                    }
                    else if (opt4 == 4)
                    {
                        check = false;
                    }
                }
                else
                {
                    System.Console.WriteLine("Invalid Input!!");
                    // Thread.Sleep(2000);
                }
            }

        }


        public void RegisterAircraftMenu()
        {
            try
            {
                Console.WriteLine("enter aircraft name");
                string name = Console.ReadLine();
                Console.WriteLine("enter the engine number of the aircraft");
                string engineNumber = Console.ReadLine();
                Console.WriteLine("enter the capacity of the aircraft");
                int capacity = int.Parse(Console.ReadLine());

                var aircraftModel = new AircraftRequestModel
                {
                    Name = name,
                    EngineNumber = engineNumber,
                    Capacity = capacity
                };

                var register = _aircraftService.RegisterAircraft(aircraftModel);

                if (register.Status == true)
                {
                    System.Console.WriteLine(register.Message);
                }
                else
                {
                    System.Console.WriteLine(register.Message);
                    RegisterAircraftMenu();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

        public void ViewAllAircraftMenu()
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


        public void DeleteAircraftMenu()
        {
            var aircrafts = _aircraftService.GetAllAircrafts();
            foreach (var aircraft in aircrafts.Data)
            {
                System.Console.WriteLine($"{aircraft.Id}\t{aircraft.Name}\t{aircraft.EngineNumber}\t{aircraft.Capacity}\t{aircraft.DateCreated}");
            }
            System.Console.WriteLine("Enter the engineNumber of the aircraft you want to delete: ");
            string engineNumber = Console.ReadLine();

            var delete = _aircraftService.DeleteAircraft(engineNumber);
            if (delete.Status)
            {
                System.Console.WriteLine(delete.Message);
                AircraftMgtMenu();
            }
            else
            {
                System.Console.WriteLine(delete.Message);
            }
        }
    }
}
