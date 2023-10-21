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
        StartUp db = new StartUp();
     
        public Flight Book(Flight flight)
        {
            using (var connect = db.Connection())
            {
                connect.Open();
                var querry = $"Insert into flight (Id, Name, ReferenceNumber, TakeOffPoint, Destination, TakeOfTime, PilotStaffNumber, AircraftName, Price, DateCreated) values('{flight.Id}', '{flight.Name}', '{flight.ReferenceNumber}', '{flight.TakeOffPoint}', '{flight.Destination}', '{flight.TakeOfTime}', '{flight.PilotStaffNumber}', '{flight.AircraftName}', '{flight.Price}','{flight.DateCreated}');";
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
            using (var connect = db.Connection())
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
            using (var connect = db.Connection())
            {
                connect.Open();
                var command = new MySqlCommand($"select * from flight where ReferenceNumber = @referenceNumber;", connect);
                command.Parameters.AddWithValue("@referenceNumber", referenceNumber);
                var row = command.ExecuteReader();
                Flight flight = new();
                while (row.Read())
                {
                    flight.Id = Convert.ToString(row[0]);
                    flight.Name = Convert.ToString(row[1]);
                    flight.ReferenceNumber = Convert.ToString(row[2]);
                    flight.TakeOffPoint = Convert.ToString(row[3]);
                    flight.Destination = Convert.ToString(row[4]);
                    flight.TakeOfTime = Convert.ToDateTime(row[5]);
                    flight.PilotStaffNumber = Convert.ToString(row[6]);
                    flight.AircraftName = Convert.ToString(row[7]);
                    flight.Price = Convert.ToDouble(row[8]);
                    flight.DateCreated = Convert.ToDateTime(row[9]);
                }
                return flight;
            }
        }

        public List<Flight> GetAll()
        {
            List<Flight> flights = new List<Flight>();
            using (var connect = db.Connection())
            {
                connect.Open();
                var command = new MySqlCommand($"Select * From flight;", connect);
                var row = command.ExecuteReader();
                while (row.Read())
                {
                    Flight flight = new();
                    flight.Id = Convert.ToString(row[0]);
                    flight.Name = Convert.ToString(row[1]);
                    flight.ReferenceNumber = Convert.ToString(row[2]);
                    flight.TakeOffPoint = Convert.ToString(row[3]);
                    flight.Destination = Convert.ToString(row[4]);
                    flight.TakeOfTime = Convert.ToDateTime(row[5]);
                    flight.PilotStaffNumber = Convert.ToString(row[6]);
                    flight.AircraftName = Convert.ToString(row[7]);
                    flight.Price = Convert.ToDouble(row[8]);
                    flight.DateCreated = Convert.ToDateTime(row[9]);

                    flights.Add(flight);
                }

            }
            return flights;
        }
    }
    
}
