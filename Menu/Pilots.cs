using AircraftM.DTOs;
using AircraftM.Services.Implementations;
using AircraftM.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ZstdSharp.Unsafe;

namespace AircraftM.Menu
{
    public class Pilots
    {
        IPilotService _pilotService = new PilotService();
        IUserService _userService = new UserService();

        public void PilotMgtMenu()
        {

            bool check = true;
            while (check)
            {
                Console.WriteLine("Enter 1 to register a pilot\nEnter 2 to view all pilots\nEnter 3 to delete a pilot\nEnter 4 to exit");
                if (int.TryParse(Console.ReadLine(), out int opt4))
                {
                    if (opt4 == 1)
                    {
                        RegisterPilotMenu();
                    }
                    else if (opt4 == 2)
                    {
                        ViewAllPilotMenu();
                    }
                    else if (opt4 == 3)
                    {
                        DeletePilotMenu();
                    }
                    else if (opt4 == 4)
                    {
                        check = false;
                    }
                }
                else
                {
                    System.Console.WriteLine("Invalid Input!!");
                }
            }

        }

        public void RegisterPilotMenu()
        {
            try
            {
                Console.WriteLine("enter pilot first-name");
                string firstName = Console.ReadLine();
                Console.WriteLine("enter pilot last-name");
                string lastName = Console.ReadLine();
                Console.WriteLine("enter pilot user-name");
                string userName = Console.ReadLine();
                Console.WriteLine("enter pilot email");
                string email = Console.ReadLine();
                Console.WriteLine("enter the pilot's password:");
                string password = Console.ReadLine();
                Console.WriteLine("enter the pilot's date of birth");
                DateTime DOB = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("enter the gender");
                string gender = Console.ReadLine();
                Console.WriteLine("enter your House No");
                int houseNumber = int.Parse(Console.ReadLine());
                Console.WriteLine("enter your street name");
                string streetName = Console.ReadLine();
                Console.WriteLine("enter your city");
                string city = Console.ReadLine();
                Console.WriteLine("enter your state");
                string state = Console.ReadLine();
                Console.WriteLine("enter your postalCode");
                string postalCode = Console.ReadLine();


                var model = new PilotRequestModel
                {
                    FirstName = firstName,
                    LastName = lastName,
                    UserName = userName,
                    UserEmail = email,
                    Password = password,
                    RoleName = "pilot",
                    DOB = DOB,
                    Gender = gender,
                    Number = houseNumber,
                    Street = streetName,
                    City = city,
                    State = state,
                    PostalCode = postalCode,
                    Wallet = 0
                };

                var register = _pilotService.RegisterPilot(model);

                if (register.Status)
                {
                    System.Console.WriteLine(register.Message);
                }
                else
                {
                    Console.WriteLine(register.Message);
                    RegisterPilotMenu();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        public void ViewAllPilotMenu()
        {
            var pilots = _pilotService.GetAllPilots();
            if (pilots.Status)
            {
                foreach (var pilot in pilots.Data)
                {
                    System.Console.WriteLine($"ID:{pilot.Id}\t U-ID:{pilot.UserId}\t STAFF-NO:{pilot.StaffNumber}\t WLT:{pilot.Wallet}\t DC:{pilot.DateCreated}");
                }
            }
            else
            {
                Console.WriteLine(pilots.Message);
            }
        }

        public void DeletePilotMenu()
        {
            ViewAllPilotMenu();
            System.Console.WriteLine("Enter the staffNumber of the pilot you want to delete: ");
            string staffNumber = Console.ReadLine();

            var delete = _pilotService.DeletePilot(staffNumber);
            if ((delete.Status))
            {
                Console.WriteLine(delete.Message);
                PilotMgtMenu();
            }
            else
            {
                Console.WriteLine(delete.Message);
            }
        }
    }
}
