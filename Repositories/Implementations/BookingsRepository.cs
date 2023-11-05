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
        public string connectionStrings = "server = localhost; user = root; database = AircraftMgt; password = Adewale24434$";
        public MySqlConnection Connection() => new MySqlConnection(connectionStrings);



        public bool Delete(string id)
        {
            using (var connect = Connection())
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


        public Bookings Get(string referenceNumber)
        {
            using (var connect = Connection())
            {
                connect.Open();
                var command = new MySqlCommand($"select * from booking where ReferenceNumber = @referenceNumber;", connect);
                command.Parameters.AddWithValue("@referenceNumber", referenceNumber);
                var row = command.ExecuteReader();
                Bookings bk = null;
                while (row.Read())
                {
                    bk = new Bookings();
                    bk.Id = Convert.ToString(row[0]);
                    bk.ReferenceNumber = Convert.ToString(row[1]);
                    bk.SeatNumber = Convert.ToInt32(row[2]);
                    bk.PassengerEmail = Convert.ToString(row[3]);
                    bk.FlightReferenceNumber = Convert.ToString(row[4]);
                    bk.AircraftName = Convert.ToString(row[5]);
                    bk.DateCreated = Convert.ToDateTime(row[6]);
                }
                return bk;
            }
        }

        public List<Bookings> GetAll()
        {
            List<Bookings> bookings = new List<Bookings>();
            using (var connect = Connection())
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
                    bk.FlightReferenceNumber = Convert.ToString(row[4]);
                    bk.AircraftName = Convert.ToString(row[5]);
                    bk.DateCreated = Convert.ToDateTime(row[6]);

                    bookings.Add(bk);
                }

            }
            return bookings;
        }

        public Bookings Make(Bookings bk)
        {
            using (var connect = Connection())
            {
                connect.Open();
                var dateCreated = bk.DateCreated.ToString("yyyy-MM-dd HH:mm:ss");
                var querry = $"Insert into booking (Id, ReferenceNumber, SeatNumber, PassengerEmail, FlightReferenceNumber, AircraftName, DateCreated) values('{bk.Id}', '{bk.ReferenceNumber}', '{bk.SeatNumber}', '{bk.PassengerEmail}',  '{bk.FlightReferenceNumber}', '{bk.AircraftName}','{dateCreated}');";
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
 