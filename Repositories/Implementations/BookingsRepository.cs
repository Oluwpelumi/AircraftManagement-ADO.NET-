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
    public class BookingsRepository : IBookingsRepository
    {
        StartUp db = new();
        public bool Delete(string id)
        {
            using (var connect = db.Connection())
            {
                connect.Open();
                var querry = $"DELETE FROM booking WHERE ID = @id;";
                var command = new MySqlCommand(querry, connect);
                command.Parameters.AddWithValue("@id", id);
                var rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return true;
                }
                return false;
            }
        }


        public Bookings Get(string id)
        {
            using (var connect = db.Connection())
            {
                connect.Open();
                var command = new MySqlCommand($"select * from booking where Id = @id;", connect);
                command.Parameters.AddWithValue("@id", id);
                var row = command.ExecuteReader();
                Bookings bk = new();
                while (row.Read())
                {
                    bk.Id = Convert.ToString(row[0]);
                    bk.ReferenceNumber = Convert.ToString(row[1]);
                    bk.SeatNumber = Convert.ToInt32(row[2]);
                    bk.PassengerEmail = Convert.ToString(row[3]);
                    bk.FlightId = Convert.ToString(row[4]);
                    bk.DateCreated = Convert.ToDateTime(row[5]);
                }
                return bk;
            }
        }

        public List<Bookings> GetAll()
        {
            List<Bookings> bookings = new List<Bookings>();
            using (var connect = db.Connection())
            {
                connect.Open();
                var command = new MySqlCommand($"Select * From booking;", connect);
                var row = command.ExecuteReader();
                while (row.Read())
                {
                    Bookings bk = new();
                    bk.Id = Convert.ToString(row[0]);
                    bk.ReferenceNumber = Convert.ToString(row[1]);
                    bk.SeatNumber = Convert.ToInt32(row[2]);
                    bk.PassengerEmail = Convert.ToString(row[3]);
                    bk.FlightId = Convert.ToString(row[4]);
                    bk.DateCreated = Convert.ToDateTime(row[5]);

                    bookings.Add(bk);
                }

            }
            return bookings;
        }

        public Bookings Make(Bookings bk)
        {
            using (var connect = db.Connection())
            {
                connect.Open();
                var querry = $"Insert into booking (Id, ReferenceNumber, SeatNumber, PassengerEmail, FlightId, DateCreated) values('{bk.Id}', '{bk.ReferenceNumber}', '{bk.SeatNumber}', '{bk.PassengerEmail}',  '{bk.FlightId}','{bk.DateCreated}');";
                var command = new MySqlCommand(querry, connect);
                var row = command.ExecuteNonQuery();
                if (row != -1)
                {
                    return bk;
                }
                return null;
            }
        }
    }
}
