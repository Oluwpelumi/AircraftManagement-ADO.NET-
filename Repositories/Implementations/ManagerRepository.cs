using AircraftM.Models;
using AircraftM.Repositories.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.Repositories.Implementations
{
    public class ManagerRepository : IManagerRepository
    {
        StartUp db = new();
        public bool Delete(string staffNumber)
        {
            using (var connect = db.Connection())
            {
                connect.Open();
                var querry = $"DELETE FROM manager WHERE StaffNumber = @staffNumber;";
                var command = new MySqlCommand(querry, connect);
                command.Parameters.AddWithValue("@staffNumber", staffNumber);
                var rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public Manager Get(string staffNumber)
        {
            using (var connect = db.Connection())
            {
                connect.Open();
                var command = new MySqlCommand($"select * from manager where StaffNumber = @staffNumber;", connect);
                command.Parameters.AddWithValue("@staffNumber", staffNumber);
                var row = command.ExecuteReader();
                Manager manager = new();
                while (row.Read())
                {
                    manager.Id = Convert.ToString(row[0]);
                    manager.UserId = Convert.ToString(row[1]);
                    manager.StaffNumber = Convert.ToString(row[2]);
                    manager.Wallet = Convert.ToDouble(row[3]);
                    manager.DateCreated = Convert.ToDateTime(row[4]);
                }
                return manager;
            }
        }


        public Manager GetById(string userId)
        {
            using (var connect = db.Connection())
            {
                connect.Open();
                var command = new MySqlCommand($"select * from manager where UserId = @userId;", connect);
                command.Parameters.AddWithValue("@userId", userId);
                var row = command.ExecuteReader();
                Manager manager = new();
                while (row.Read())
                {
                    manager.Id = Convert.ToString(row[0]);
                    manager.UserId = Convert.ToString(row[1]);
                    manager.StaffNumber = Convert.ToString(row[2]);
                    manager.Wallet = Convert.ToDouble(row[3]);
                    manager.DateCreated = Convert.ToDateTime(row[4]);
                }
                return manager;
            }
        }

        public List<Manager> GetAll()
        {
            List<Manager> managers = new List<Manager>();
            using (var connect = db.Connection())
            {
                connect.Open();
                var command = new MySqlCommand($"Select * From manager;", connect);
                var row = command.ExecuteReader();
                while (row.Read())
                {
                    Manager manager = new();
                    manager.Id = Convert.ToString(row[0]);
                    manager.UserId = Convert.ToString(row[1]);
                    manager.StaffNumber = Convert.ToString(row[2]);
                    manager.Wallet = Convert.ToDouble(row[3]);
                    manager.DateCreated = Convert.ToDateTime(row[4]);

                    managers.Add(manager);
                }

            }
            return managers;
        }
        public Manager Register(Manager manager)
        {
            using (var connect = db.Connection())
            {
                connect.Open();
                var querry = $"Insert into manager (Id, UserId, StaffNumber, Wallet, DateCreated) values('{manager.Id}', '{manager.UserId}', '{manager.StaffNumber}', '{manager.Wallet}', '{manager.DateCreated}');";
                var command = new MySqlCommand(querry, connect);
                var row = command.ExecuteNonQuery();
                if (row != -1)
                {
                    return manager;
                }
                return null;
            }

        }

        public bool UpdateWallet(string userEmail, double newWalletAmount)
        {
            using (var connect = db.Connection())
            {
                connect.Open();

                var query = $"UPDATE manager SET Wallet = @NewWalletAmount WHERE UserEmail = @userEmail;";
                var command = new MySqlCommand(query, connect);

                command.Parameters.AddWithValue("@NewWalletAmount", newWalletAmount);
                command.Parameters.AddWithValue("@userEmail", userEmail);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
