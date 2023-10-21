using AircraftM.Models;
using AircraftM.Repositories.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AircraftM.Repositories.Implementations
{
    public class ProfileRepository : IProfileRepository
    {
        StartUp db = new();
       
        public Profile Create(Profile profile)
        {
            using (var connect = db.Connection())
            {
                connect.Open();
                var querry = $"Insert into profile (Id, FirstName, LastName, UserName, UserEmail, DOB, Gender, DateCreated) values('{profile.Id}', '{profile.FirstName}', '{profile.LastName}', '{profile.UserName}', '{profile.UserEmail}', '{profile.DOB}', '{profile.Gender}','{profile.DateCreated}');";
                var command = new MySqlCommand(querry, connect);
                var row = command.ExecuteNonQuery();
                if (row != -1)
                {
                    return profile;
                }
                return null;
            }
        }

        public bool Delete(string userEmail)
        {
            using (var connect = db.Connection())
            {
                connect.Open();
                var querry = $"DELETE FROM profile WHERE UserEmail = @userEmail;";
                var command = new MySqlCommand(querry, connect);
                command.Parameters.AddWithValue("@userEmail", userEmail);
                var rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public Profile Get(string userEmail)
        {
            using (var connect = db.Connection())
            {
                connect.Open();
                var command = new MySqlCommand($"select * from profile where UserEmail = @userEmail;", connect);
                command.Parameters.AddWithValue("@userEmail", userEmail);
                var row = command.ExecuteReader();
                Profile profile = new();
                while (row.Read())
                {
                    profile.Id = Convert.ToString(row[0]);
                    profile.FirstName = Convert.ToString(row[1]);
                    profile.LastName = Convert.ToString(row[2]);
                    profile.UserName = Convert.ToString(row[3]);
                    profile.UserEmail = Convert.ToString(row[4]);
                    profile.DOB = Convert.ToDateTime(row[5]);
                    profile.Gender = Convert.ToString(row[6]);
                    profile.DateCreated = Convert.ToDateTime(row[7]);
                }
                return profile;
            }
        }

        public List<Profile> GetAll()
        {
            List<Profile> profiles = new List<Profile>();
            using (var connect = db.Connection())
            {
                connect.Open();
                var command = new MySqlCommand($"Select * From profile;", connect);
                var row = command.ExecuteReader();
                while (row.Read())
                {
                    Profile profile = new Profile();
                    profile.Id = Convert.ToString(row[0]);
                    profile.FirstName = Convert.ToString(row[1]);
                    profile.LastName = Convert.ToString(row[2]);
                    profile.UserName = Convert.ToString(row[3]);
                    profile.UserEmail = Convert.ToString(row[4]);
                    profile.DOB = Convert.ToDateTime(row[5]);
                    profile.Gender = Convert.ToString(row[6]);
                    profile.DateCreated = Convert.ToDateTime(row[7]);

                    profiles.Add(profile);
                }

            }
            return profiles;
        }
    }
}
