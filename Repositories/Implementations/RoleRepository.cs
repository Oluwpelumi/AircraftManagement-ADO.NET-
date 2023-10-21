using AircraftM.Models;
using AircraftM.Repositories.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AircraftM.Repositories.Implementations
{
    public class RoleRepository : IRoleRepository
    {
        StartUp db = new();
        public bool Delete(string name)
        {
            using (var connect = db.Connection())
            {
                connect.Open();
                var querry = $"DELETE FROM role WHERE Name = @name;";
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

        public Role Get(string name)
        {
            using (var connect = db.Connection())
            {
                connect.Open();
                var command = new MySqlCommand($"select * from role where Name = @name;", connect);
                command.Parameters.AddWithValue("@name", name);
                var row = command.ExecuteReader();
                Role role = new();
                while (row.Read())
                {
                    role.Id = Convert.ToString(row[0]);
                    role.Name = Convert.ToString(row[1]);
                    role.Description = Convert.ToString(row[2]);
                    role.DateCreated = Convert.ToDateTime(row[3]);
                }
                return role;
            }
        }

        public List<Role> GetAll()
        {
            List<Role> roles = new List<Role>();
            using (var connect = db.Connection())
            {
                connect.Open();
                var command = new MySqlCommand($"Select * From role;", connect);
                var row = command.ExecuteReader();
                while (row.Read())
                {
                    Role role = new();
                    role.Id = Convert.ToString(row[0]);
                    role.Name = Convert.ToString(row[1]);
                    role.Description = Convert.ToString(row[2]);
                    role.DateCreated = Convert.ToDateTime(row[3]);

                    roles.Add(role);
                }

            }
            return roles;
        }

        public Role GetById(string id)
        {
            using (var connect = db.Connection())
            {
                connect.Open();
                var command = new MySqlCommand($"select * from role where Id = @id;", connect);
                command.Parameters.AddWithValue("@id", id);
                var row = command.ExecuteReader();
                Role role = new();
                while (row.Read())
                {
                    role.Id = Convert.ToString(row[0]);
                    role.Name = Convert.ToString(row[1]);
                    role.Description = Convert.ToString(row[2]);
                    role.DateCreated = Convert.ToDateTime(row[3]);
                }
                return role;
            }
        }

        public Role Register(Role role)
        {
            using (var connect = db.Connection())
            {
                connect.Open();
                var querry = $"Insert into role (Id, Name, Description, DateCreated) values('{role.Id}', '{role.Name}', '{role.Description}', '{role.DateCreated}');";
                var command = new MySqlCommand(querry, connect);
                var row = command.ExecuteNonQuery();
                if (row != -1)
                {
                    return role;
                }
                return null;
            }
        }
    }
}
