using AircraftM;
using AircraftM.Menu;
using AircraftM.Models;
using AircraftM.Repositories.Interfaces;
using MySql.Data.MySqlClient;

namespace AircraftM.Repositories.Implementations
{
    public class AirportRepository : IAirportRepository
    {
        public string connectionStrings = "server = localhost; user = root; database = AircraftMgt; password = Adewale24434$";
        public MySqlConnection Connection() => new MySqlConnection(connectionStrings);


        public Airport Create(Airport airport)
        {
            using (var connect = Connection())
            {
                connect.Open();
                var dateCreated = airport.DateCreated.ToString("yyyy-MM-dd HH:mm:ss");
                var querry = $"Insert into airport (Id, Name, Location, AirportType, DateCreated) values('{airport.Id}', '{airport.Name}', '{airport.Location}', '{airport.AirportType}','{dateCreated}');";
                var command = new MySqlCommand(querry, connect);
                var row = command.ExecuteNonQuery();
                if (row != -1)
                {
                    return airport;
                }
                
                return null;
            }
        }

        public bool Delete(string name)
        {
            using (var connect = Connection())
            {
                connect.Open();
                var querry = $"DELETE FROM airport WHERE Name = @name;";
                var command = new MySqlCommand(querry, connect);
                command.Parameters.AddWithValue("@name", name);
                var rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public Airport Get(string name)
        {
            using (var connect = Connection())
            {
                connect.Open();
                var command = new MySqlCommand($"select * from airport where Name = @name;", connect);
                command.Parameters.AddWithValue("@name", name);
                var row = command.ExecuteReader();
                Airport airport = null;
                while (row.Read())
                {
                    airport = new Airport();
                    airport.Id = Convert.ToString(row[0]);
                    airport.Name = Convert.ToString(row[1]);
                    airport.Location = Convert.ToString(row[2]);
                    airport.AirportType = Convert.ToString(row[3]);
                    airport.DateCreated = Convert.ToDateTime(row[4]);
                }
                return airport;
            };
        }

        public List<Airport> GetAll()
        {
            List<Airport> airports = new List<Airport>();
            using (var connect = Connection())
            {
                connect.Open();
                var command = new MySqlCommand($"Select * From airport;", connect);
                var row = command.ExecuteReader();
                while (row.Read())
                {
                    Airport airport = new();
                    airport.Id = Convert.ToString(row[0]);
                    airport.Name = Convert.ToString(row[1]);
                    airport.Location = Convert.ToString(row[2]);
                    airport.AirportType = Convert.ToString(row[3]);
                    airport.DateCreated = Convert.ToDateTime(row[4]);

                    airports.Add(airport);
                }

            }
            return airports;
        }
    }
}