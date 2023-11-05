using AircraftM.DTOs;
using AircraftM.Models;
using AircraftM.Services.Implementations;
using AircraftM.Services.Interfaces;
using Org.BouncyCastle.Crypto.Prng;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.Menu
{
    public class SuperAdmin
    {
        IManagerService _managerService = new ManagerService();
        IUserService _userService = new UserService();
        public void SuperMenu()
        {
            bool opt2 = true;
            while (opt2)
            {
                System.Console.WriteLine("enter 1 to register manager\nenter 2 to view all managers\nenter 3 to delete manager\nenter 4 to logout");
                // int opt = int.Parse(Console.ReadLine());
                bool opt = int.TryParse(Console.ReadLine(), out int num);
                if (opt == true)
                {
                    if (num == 1)
                    {
                        RegisterManagerMenu();
                    }
                    else if (num == 2)
                    {
                        ViewAllManagersMenu();
                    }
                    else if (num == 3)
                    {
                        DeleteManagerMenu();
                    }
                    else if (num == 4)
                    {
                        opt2 = false;
                    }
                    else
                    {
                        System.Console.WriteLine("The value you input is out of range.....try again");
                        SuperMenu();
                    }
                }
                else
                {
                    System.Console.WriteLine("Invalid Input");
                }
            }
        }

        public void RegisterManagerMenu()
        {
            try
            {
                Console.WriteLine("enter manager first-name");
                string firstName = Console.ReadLine();
                Console.WriteLine("enter manager last-name");
                string lastName = Console.ReadLine();
                Console.WriteLine("enter manager user-name");
                string userName = Console.ReadLine();
                Console.WriteLine("enter your email");
                string email = Console.ReadLine();
                Console.WriteLine("enter the manager's password:");
                string password = Console.ReadLine();
                Console.WriteLine("enter the manager's date of birth");
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


                var model = new ManagerRequestModel
                {
                    FirstName = firstName,
                    LastName = lastName,
                    UserName = userName,
                    UserEmail = email,
                    Password = password,
                    RoleName = "manager",
                    DOB = DOB,
                    Gender = gender,
                    Number = houseNumber,
                    Street = streetName,
                    City = city,
                    State = state,
                    PostalCode = postalCode,
                    Wallet = 0
                };



                var response = _managerService.RegisterManager(model);
                if (response.Status)
                {
                    Console.WriteLine(response.Message);
                }
                else
                {
                    Console.WriteLine(response.Message);
                    SuperMenu();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public void ViewAllManagersMenu()
        {
            var managers = _managerService.GetAllManagers();
            if (managers.Status)
            {
                foreach (var manager in managers.Data)
                {
                
                    System.Console.WriteLine($"Id:{manager.Id}\t UserId:{manager.UserId}\t StaffNO:{manager.StaffNumber}\t Wallet:{manager.Wallet}\t DateCreated:{manager.DateCreated}");
                }
            }
            else
            {
                Console.WriteLine(managers.Message);
            }
        }




        public void DeleteManagerMenu()
        {

            ViewAllManagersMenu();

            System.Console.WriteLine("Enter the staff Number of the manager you want to delete: ");
            string staffnum = Console.ReadLine();
            var delete = _managerService.DeleteManager(staffnum);
            if (delete.Status)
            {
                System.Console.WriteLine(delete.Message);
                SuperMenu();
            }
            else
            {
                System.Console.WriteLine(delete.Message);
            }
        }

    }
}
