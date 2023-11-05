using AircraftM.DTOs;
using AircraftM.Models;
using AircraftM.Services.Implementations;
using AircraftM.Services.Interfaces;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.Menu
{
    public class Main
    {
        IUserService _userService = new UserService();
        IPassengerService _passengerService = new PassengerService();

        static void CenterText(string text)
        {
            int screenWidth = Console.WindowWidth;
            int padding = (screenWidth - text.Length) / 2;
            Console.WriteLine(text.PadLeft(padding + text.Length));
        }

        public void MainMenu()
        {
            bool opt5 = true;
            while (opt5)
            {
                Console.WriteLine("Enter 1 to register\nEnter 2 to login\nEnter 3 to exit");
                bool check = int.TryParse(Console.ReadLine(), out int opt);
                if (check)
                {
                    if (opt == 1)
                    {
                        RegisterMenu();
                    }
                    else if (opt == 2)
                    {
                        LoginMenu();
                    }
                    else if (opt == 3)
                    {
                        System.Console.WriteLine("Thanks for coming");
                        opt5 = false;
                    }
                    else
                    {
                        System.Console.WriteLine("wrong input, not within the range of the given options: ");
                    }
                }

                else
                {
                    Console.WriteLine("invalid input, Your input is not an integer value");
                }
            }
        }
        public void RegisterMenu()
        {
            try
            {
                Console.WriteLine("enter your last-name");
                string lastName = Console.ReadLine();
                Console.WriteLine("enter your first-name");
                string firstName = Console.ReadLine();
                Console.WriteLine("enter your user-name");
                string userName = Console.ReadLine();
                Console.WriteLine("enter your email");
                string email = Console.ReadLine();
                Console.WriteLine("enter your password");
                string password = Console.ReadLine();
                Console.WriteLine("enter your date of birth");
                DateTime DOB = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("enter your gender");
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

                var requestModel = new PassengerRequestModel
                {
                    LastName = lastName,
                    FirstName = firstName,
                    UserName = userName,
                    UserEmail = email,
                    Password = password,
                    DOB = DOB,
                    Gender = gender,
                    Number = houseNumber,
                    Street = streetName,
                    City = city,
                    State = state,
                    PostalCode = postalCode,
                    RoleName = "passenger",
                    Wallet = 0
                };

                var response = _passengerService.RegisterPassenger(requestModel);

                if (response.Data != null)
                {
                    Console.WriteLine(response.Message);
                    LoginMenu();
                }
                else
                {
                    Console.WriteLine(response.Message);
                    RegisterMenu();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
        public void LoginMenu()
        {
            System.Console.WriteLine("Enter your credentials below to login: ");
            Console.WriteLine("enter your email");
            string email = Console.ReadLine();
            Console.WriteLine("enter your password");
            string password = Console.ReadLine();

            var userLogin = new UserLoginModel
            {
                UserEmail = email,
                Password = password
            };
            var user = _userService.Login(userLogin);

            if (user.Status == false)
            {
                Console.WriteLine(user.Message);
                MainMenu();
            }
            
            if (user.Data.RoleName == "manager")
            {
                Manager m = new Manager();
                m.ManagerMenu();
            }
            else if (user.Data.RoleName == "passenger")
            {
                Passenger p = new Passenger();
                p.PassengerMenu();
            }
            else if (user.Data.RoleName == "pilot")
            {
                Pilot p = new Pilot();
                p.PilotMenu();
            }
            else if (user.Data.RoleName == "superAdmin")
            {
                SuperAdmin sp = new SuperAdmin();
                sp.SuperMenu();
            }
        }
    }
}
