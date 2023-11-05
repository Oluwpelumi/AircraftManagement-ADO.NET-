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
        public string connectionStrings = "server = localhost; user = root; database = AircraftMgt; password = Adewale24434$";
        public MySqlConnection Connection() => new MySqlConnection(connectionStrings);


        public bool Delete(string regNumber)
        {
            using (var connect = Connection())
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
            using (var connect = Connection())
            {
                connect.Open();
                var command = new MySqlCommand($"select * from passenger where RegNumber = @regNumber;", connect);
                command.Parameters.AddWithValue("@regNumber", regNumber);
                var row = command.ExecuteReader();
                Passenger passenger = null;
                while (row.Read())
                {
                    passenger = new Passenger();
                    passenger.Id = Convert.ToString(row[0]);
                    passenger.UserId = Convert.ToString(row[1]);
                    passenger.RegNumber = Convert.ToString(row[2]);
                    passenger.BookingId = Convert.ToString(row[3]);
                    passenger.Wallet = Convert.ToDouble(row[4]);
                    passenger.DateCreated = Convert.ToDateTime(row[5]);
                }
                return passenger;
            }
        }
        
        public Passenger GetById(string id)
        {
            using (var connect = Connection())
            {
                connect.Open();
                var command = new MySqlCommand($"select * from passenger where Id = @id;", connect);
                command.Parameters.AddWithValue("@id", id);
                var row = command.ExecuteReader();
                Passenger passenger = null;
                while (row.Read())
                {
                    passenger = new Passenger();
                    passenger.Id = Convert.ToString(row[0]);
                    passenger.UserId = Convert.ToString(row[1]);
                    passenger.RegNumber = Convert.ToString(row[2]);
                    passenger.BookingId = Convert.ToString(row[3]);
                    passenger.Wallet = Convert.ToDouble(row[4]);
                    passenger.DateCreated = Convert.ToDateTime(row[5]);
                }
                return passenger;
            }
        }


        public List<Passenger> GetAllPassengersInThisAircraft(string aircraftName)
        {
            List<Passenger> passengers = new List<Passenger>();

            using (var connect = Connection())
            {
                connect.Open();

                var command = new MySqlCommand($"SELECT * FROM passenger WHERE AircraftName = @aircraftName;", connect);
                command.Parameters.AddWithValue("@aircraftName", aircraftName);
                var row = command.ExecuteReader();
                Passenger passenger = null;
                while (row.Read())
                {
                    passenger = new Passenger();
                    passenger.Id = Convert.ToString(row[0]);
                    passenger.UserId = Convert.ToString(row[1]);
                    passenger.RegNumber = Convert.ToString(row[2]);
                    passenger.BookingId = Convert.ToString(row[3]);
                    passenger.Wallet = Convert.ToDouble(row[4]);
                    passenger.DateCreated = Convert.ToDateTime(row[5]);

                    passengers.Add(passenger);
                }
            }
            return passengers;
        }


        //public List<Passenger> GetAllPassengersInThisAircraft(string aircraftName)
        //{
        //    List<Passenger> passengers = new List<Passenger>();

        //    using (var connect = Connection())
        //    {
        //        connect.Open();

        //        var command = new MySqlCommand($"SELECT * FROM passenger WHERE AircraftName = @aircraftName;", connect);
        //        command.Parameters.AddWithValue("@aircraftName", aircraftName);
        //        var row = command.ExecuteReader();
        //        while (row.Read())
        //        {
        //            Passenger passenger = new Passenger
        //            {
        //                Id = row.GetString(row.GetOrdinal("Id")),
        //                UserId = row.GetString(row.GetOrdinal("UserId")),
        //            };

        //            passengers.Add(passenger);
        //        }
        //    }

        //    return passengers;
        //}

        public List<Passenger> GetAll()
        {
            List<Passenger> passengers = new List<Passenger>();
            using (var connect = Connection())
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
                    passenger.BookingId = Convert.ToString(row[3]);
                    passenger.Wallet = Convert.ToDouble(row[4]);
                    passenger.DateCreated = Convert.ToDateTime(row[5]);

                    passengers.Add(passenger);
                }

            }
            return passengers;
        }

        public Passenger Register(Passenger passenger)
        {
            using (var connect = Connection())
            {
                connect.Open();
                var dateCreated = passenger.DateCreated.ToString("yyyy-MM-dd HH:mm:ss");
                var querry = $"Insert into passenger (Id, UserId, RegNumber,BookingId, Wallet, DateCreated) values('{passenger.Id}', '{passenger.UserId}', '{passenger.RegNumber}','{passenger.BookingId}', '{passenger.Wallet}', '{dateCreated}');";
                var command = new MySqlCommand(querry, connect);
                var row = command.ExecuteNonQuery();
                if (row != -1)
                {
                    return passenger;
                }
                return null;
            }
        }

        public bool UpdateWallet(string regNumber, double newWalletAmount)
        {
            using (var connect = Connection())
            {
                connect.Open();

                var query = $"UPDATE passenger SET Wallet = @NewWalletAmount WHERE RegNumber = @regNumber;";
                var command = new MySqlCommand(query, connect);

                command.Parameters.AddWithValue("@NewWalletAmount", newWalletAmount);
                command.Parameters.AddWithValue("@RegNumber", regNumber);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return true;
                }
                return false;
            }
        }


        public bool UpdateBookingId(string regNumber, string bookingId)
        {
            using (var connect = Connection())
            {
                connect.Open();

                var query = $"UPDATE passenger SET BookingId = @bookingId WHERE RegNumber = @regNumber;";
                var command = new MySqlCommand(query, connect);

                command.Parameters.AddWithValue("@BookingId", bookingId);
                command.Parameters.AddWithValue("@RegNumber", regNumber);

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
