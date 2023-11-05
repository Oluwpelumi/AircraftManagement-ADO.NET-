using AircraftM.Menu;
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
    public class FlightRepository : IFlightRepository
    {
        public string connectionStrings = "server = localhost; user = root; database = AircraftMgt; password = Adewale24434$";
        public MySqlConnection Connection() => new MySqlConnection(connectionStrings);



        public Flight Book(Flight flight)
        {
            using (var connect = Connection())
            {
                connect.Open();
                var dateCreated = flight.DateCreated.ToString("yyyy-MM-dd HH:mm:ss");
                var takeOffTime = flight.TakeOfTime.ToString("yyyy-MM-dd HH:mm:ss");
                var querry = $"Insert into flight (Id, ReferenceNumber, TakeOffPoint, Destination, TakeOfTime, PilotStaffNumber, AircraftName, Price, DateCreated) values('{flight.Id}', '{flight.ReferenceNumber}', '{flight.TakeOffPoint}', '{flight.Destination}', '{takeOffTime}', '{flight.PilotStaffNumber}', '{flight.AircraftName}', '{flight.Price}','{dateCreated}');";
                var command = new MySqlCommand(querry, connect);
                var row = command.ExecuteNonQuery();
                if (row != -1)
                {
                    return flight;
                }
                return null;
            }
        }

        public bool Delete(string referenceNumber)
        {
            using (var connect = Connection())
            {
                connect.Open();
                var querry = $"DELETE FROM flight WHERE ReferenceNumber = @referenceNumber;";
                var command = new MySqlCommand(querry, connect);
                command.Parameters.AddWithValue("@referenceNumber", referenceNumber);
                var rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public Flight Get(string referenceNumber)
        {
            using (var connect = Connection())
            {
                connect.Open();
                var command = new MySqlCommand($"select * from flight where ReferenceNumber = @referenceNumber;", connect);
                command.Parameters.AddWithValue("@referenceNumber", referenceNumber);
                var row = command.ExecuteReader();
                Flight flight = null;
                while (row.Read())
                {
                    flight = new Flight();
                    flight.Id = Convert.ToString(row[0]);
                    flight.ReferenceNumber = Convert.ToString(row[1]);
                    flight.TakeOffPoint = Convert.ToString(row[2]);
                    flight.Destination = Convert.ToString(row[3]);
                    flight.TakeOfTime = Convert.ToDateTime(row[4]);
                    flight.PilotStaffNumber = Convert.ToString(row[5]);
                    flight.AircraftName = Convert.ToString(row[6]);
                    flight.Price = Convert.ToDouble(row[7]);
                    flight.DateCreated = Convert.ToDateTime(row[8]);
                }
                return flight;
            }
        }

        public List<Flight> GetAll()
        {
            List<Flight> flights = new List<Flight>();
            using (var connect = Connection())
            {
                connect.Open();
                var command = new MySqlCommand($"Select * From flight;", connect);
                var row = command.ExecuteReader();
                while (row.Read())
                {
                    Flight flight = new();
                    flight.Id = Convert.ToString(row[0]);
                    flight.ReferenceNumber = Convert.ToString(row[1]);
                    flight.TakeOffPoint = Convert.ToString(row[2]);
                    flight.Destination = Convert.ToString(row[3]);
                    flight.TakeOfTime = Convert.ToDateTime(row[4]);
                    flight.PilotStaffNumber = Convert.ToString(row[5]);
                    flight.AircraftName = Convert.ToString(row[6]);
                    flight.Price = Convert.ToDouble(row[7]);
                    flight.DateCreated = Convert.ToDateTime(row[8]);

                    flights.Add(flight);
                }

            }
            return flights;
        }
    }
    
}
