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
    public class AircraftRepository : IAircraftRepository
    {
        public string connectionStrings = "server = localhost; user = root; database = AircraftMgt; password = Adewale24434$";
        public MySqlConnection Connection() => new MySqlConnection(connectionStrings);


        public Aircraft Create(Aircraft aircraft)
        {
            using (var connect = Connection())
            {
                connect.Open();
                var dateCreated = aircraft.DateCreated.ToString("yyyy-MM-dd HH:mm:ss");
                var querry = $"Insert into aircraft (Id, Name, EngineNumber, Capacity, DateCreated) values('{aircraft.Id}', '{aircraft.Name}', '{aircraft.EngineNumber}', '{aircraft.Capacity}', '{dateCreated}');";
                var command = new MySqlCommand(querry, connect);
                var row = command.ExecuteNonQuery();
                if (row != -1)
                {
                    return aircraft;
                }
                return null;
            }
        }

        public bool Delete(string engineNumber)
        {
            using (var connect = Connection())
            {
                connect.Open();
                var querry = $"DELETE FROM aircraft WHERE EngineNumber = @engineNumber;";
                var command = new MySqlCommand(querry, connect);
                command.Parameters.AddWithValue("@engineNumber", engineNumber);
                var rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public Aircraft Get(string engineNumber)
        {
            using (var connect = Connection())
            {
                connect.Open();
                var command = new MySqlCommand($"select * from aircraft where EngineNumber = @engineNumber;", connect);
                command.Parameters.AddWithValue("@engineNumber", engineNumber);
                var row = command.ExecuteReader();
                Aircraft aircraft = null;
                while (row.Read())
                {
                    aircraft = new Aircraft();
                    aircraft.Id = Convert.ToString(row[0]);
                    aircraft.Name = Convert.ToString(row[1]);
                    aircraft.EngineNumber = Convert.ToString(row[2]);
                    aircraft.Capacity = Convert.ToInt32(row[3]);
                    aircraft.DateCreated = Convert.ToDateTime(row[4]);
                }
                return aircraft;
            }
        }




        public List<Aircraft> GetAll()
        {
            List<Aircraft> aircrafts = new List<Aircraft>();
            using (var connect = Connection())
            {
                connect.Open();
                var command = new MySqlCommand($"Select * From aircraft;", connect);
                var row = command.ExecuteReader();
                while (row.Read())
                {
                    Aircraft aircraft = new();
                    aircraft.Id = Convert.ToString(row[0]);
                    aircraft.Name = Convert.ToString(row[1]);
                    aircraft.EngineNumber = Convert.ToString(row[2]);
                    aircraft.Capacity = Convert.ToInt32(row[3]);
                    aircraft.DateCreated = Convert.ToDateTime(row[4]);

                    aircrafts.Add(aircraft);   
                }

            }
            return aircrafts;
        }

        public Aircraft GetByName(string name)
        {
            using (var connect = Connection())
            {
                connect.Open();
                var command = new MySqlCommand($"select * from aircraft where Name = @name;", connect);
                command.Parameters.AddWithValue("@name", name);
                var row = command.ExecuteReader();
                Aircraft aircraft = null;
                while (row.Read())
                {
                    aircraft = new Aircraft();
                    aircraft.Id = Convert.ToString(row[0]);
                    aircraft.Name = Convert.ToString(row[1]);
                    aircraft.EngineNumber = Convert.ToString(row[2]);
                    aircraft.Capacity = Convert.ToInt32(row[3]);
                    aircraft.DateCreated = Convert.ToDateTime(row[4]);
                }
                return aircraft;
            }
        }
    }
}
