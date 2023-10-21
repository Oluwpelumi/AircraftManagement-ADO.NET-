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
    public class PassengerRepository : IPassengerRepository
    {
        StartUp db = new StartUp();
        public bool Delete(string regNumber)
        {
            using (var connect = db.Connection())
            {
                connect.Open();
                var querry = $"DELETE FROM passenger WHERE RegNumber = @regNumber;";
                var command = new MySqlCommand(querry, connect);
                command.Parameters.AddWithValue("@regNumber", regNumber);
                var rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public Passenger Get(string regNumber)
        {
            using (var connect = db.Connection())
            {
                connect.Open();
                var command = new MySqlCommand($"select * from passenger where RegNumber = @regNumber;", connect);
                command.Parameters.AddWithValue("@regNumber", regNumber);
                var row = command.ExecuteReader();
                Passenger passenger = new();
                while (row.Read())
                {
                    passenger.Id = Convert.ToString(row[0]);
                    passenger.UserId = Convert.ToString(row[1]);
                    passenger.RegNumber = Convert.ToString(row[2]);
                    passenger.Wallet = Convert.ToDouble(row[3]);
                    passenger.DateCreated = Convert.ToDateTime(row[4]);
                }
                return passenger;
            }
        }public Passenger GetById(string id)
        {
            using (var connect = db.Connection())
            {
                connect.Open();
                var command = new MySqlCommand($"select * from passenger where Id = @id;", connect);
                command.Parameters.AddWithValue("@id", id);
                var row = command.ExecuteReader();
                Passenger passenger = new();
                while (row.Read())
                {
                    passenger.Id = Convert.ToString(row[0]);
                    passenger.UserId = Convert.ToString(row[1]);
                    passenger.RegNumber = Convert.ToString(row[2]);
                    passenger.Wallet = Convert.ToDouble(row[3]);
                    passenger.DateCreated = Convert.ToDateTime(row[4]);
                }
                return passenger;
            }
        }

        public List<Passenger> GetAll()
        {
            List<Passenger> passengers = new List<Passenger>();
            using (var connect = db.Connection())
            {
                connect.Open();
                var command = new MySqlCommand($"Select * From passenger;", connect);
                var row = command.ExecuteReader();
                while (row.Read())
                {
                    Passenger passenger = new();
                    passenger.Id = Convert.ToString(row[0]);
                    passenger.UserId = Convert.ToString(row[1]);
                    passenger.RegNumber = Convert.ToString(row[2]);
                    passenger.Wallet = Convert.ToDouble(row[3]);
                    passenger.DateCreated = Convert.ToDateTime(row[4]);

                    passengers.Add(passenger);
                }

            }
            return passengers;
        }

        public Passenger Register(Passenger passenger)
        {
            using (var connect = db.Connection())
            {
                connect.Open();
                var querry = $"Insert into passenger (Id, UserId, StaffNumber, Wallet, DateCreated) values('{passenger.Id}', '{passenger.UserId}', '{passenger.RegNumber}', '{passenger.Wallet}', '{passenger.DateCreated}');";
                var command = new MySqlCommand(querry, connect);
                var row = command.ExecuteNonQuery();
                if (row != -1)
                {
                    return passenger;
                }
                return null;
            }
        }

        public bool UpdateWallet(string userEmail, double newWalletAmount)
        {
            using (var connect = db.Connection())
            {
                connect.Open();

                var query = $"UPDATE passenger SET Wallet = @NewWalletAmount WHERE UserEmail = @userEmail;";
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
