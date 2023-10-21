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
    public class PilotRepository : IPilotRepository
    {
        StartUp db = new StartUp();
        public bool Delete(string staffNumber)
        {
            using (var connect = db.Connection())
            {
                connect.Open();
                var querry = $"DELETE FROM pilot WHERE StaffNumber = @staffNumber;";
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

        public Pilot Get(string staffNumber)
        {
            using (var connect = db.Connection())
            {
                connect.Open();
                var command = new MySqlCommand($"select * from pilot where StaffNumber = @staffNumber;", connect);
                command.Parameters.AddWithValue("@staffNumber", staffNumber);
                var row = command.ExecuteReader();
                Pilot pilot = new();
                while (row.Read())
                {
                    pilot.Id = Convert.ToString(row[0]);
                    pilot.UserId = Convert.ToString(row[1]);
                    pilot.StaffNumber = Convert.ToString(row[2]);
                    pilot.Wallet = Convert.ToDouble(row[3]);
                    pilot.DateCreated = Convert.ToDateTime(row[4]);
                }
                return pilot;
            }
        }public Pilot GetById(string id)
        {
            using (var connect = db.Connection())
            {
                connect.Open();
                var command = new MySqlCommand($"select * from pilot where Id = @id;", connect);
                command.Parameters.AddWithValue("@id", id);
                var row = command.ExecuteReader();
                Pilot pilot = new();
                while (row.Read())
                {
                    pilot.Id = Convert.ToString(row[0]);
                    pilot.UserId = Convert.ToString(row[1]);
                    pilot.StaffNumber = Convert.ToString(row[2]);
                    pilot.Wallet = Convert.ToDouble(row[3]);
                    pilot.DateCreated = Convert.ToDateTime(row[4]);
                }
                return pilot;
            }
        }

        public List<Pilot> GetAll()
        {
            List<Pilot> pilots = new List<Pilot>();
            using (var connect = db.Connection())
            {
                connect.Open();
                var command = new MySqlCommand($"Select * From pilot;", connect);
                var row = command.ExecuteReader();
                while (row.Read())
                {
                    Pilot pilot = new();
                    pilot.Id = Convert.ToString(row[0]);
                    pilot.UserId = Convert.ToString(row[1]);
                    pilot.StaffNumber = Convert.ToString(row[2]);
                    pilot.Wallet = Convert.ToDouble(row[3]);
                    pilot.DateCreated = Convert.ToDateTime(row[4]);

                    pilots.Add(pilot);
                }

            }
            return pilots;
        }

        public Pilot Register(Pilot pilot)
        {
            using (var connect = db.Connection())
            {
                connect.Open();
                var querry = $"Insert into pilot (Id, UserId, StaffNumber, Wallet, DateCreated) values('{pilot.Id}', '{pilot.UserId}', '{pilot.StaffNumber}', '{pilot.Wallet}', '{pilot.DateCreated}');";
                var command = new MySqlCommand(querry, connect);
                var row = command.ExecuteNonQuery();
                if (row != -1)
                {
                    return pilot;
                }
                return null;
            }
        }

        public bool UpdateWallet(string userEmail, double newWalletAmount)
        {
            using (var connect = db.Connection())
            {
                connect.Open();

                var query = $"UPDATE pilot SET Wallet = @NewWalletAmount WHERE UserEmail = @userEmail;";
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
