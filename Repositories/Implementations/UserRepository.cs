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
    public class UserRepository : IUserRepository
    {
        StartUp db = new StartUp();
        

        public User Create(User user)
        {
            using (var connect = db.Connection())
            {
                connect.Open();
                var querry = $"Insert into user (Id, UserEmail, Password, AddressId, ProfileId, RoleId, DateCreated) values('{user.Id}', '{user.UserEmail}', '{user.Password}', '{user.AddressId}', '{user.ProfileId}', '{user.RoleId}','{user.DateCreated}');";
                var command = new MySqlCommand(querry, connect);
                var row = command.ExecuteNonQuery();
                if (row != -1)
                {
                    return user;
                }
                return null;
            }
        }

        public bool Delete(string userEmail)
        {
            using (var connect = db.Connection())
            {
                connect.Open();
                var querry = $"DELETE FROM user WHERE UserEmail = @userEmail;";
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

        public bool UpdateWallet(string userEmail, double newWalletAmount)
        {
            using (var connect = db.Connection())
            {
                connect.Open();

                var query = $"UPDATE user SET Wallet = @NewWalletAmount WHERE UserEmail = @userEmail;";
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


        public User Get(string userEmail)
        {
            using (var connect = db.Connection())
            {
                connect.Open();
                var command = new MySqlCommand($"select * from user where UserEmail = @userEmail;", connect);
                command.Parameters.AddWithValue("@userEmail", userEmail);
                var row = command.ExecuteReader();
                User user = new();
                while (row.Read())
                {
                    user.Id = Convert.ToString(row[0]);
                    user.UserEmail = Convert.ToString(row[1]);
                    user.Password = Convert.ToString(row[2]);
                    user.AddressId = Convert.ToString(row[3]);
                    user.ProfileId = Convert.ToString(row[4]);
                    user.RoleId = Convert.ToString(row[5]);
                    user.DateCreated = Convert.ToDateTime(row[6]);
                }
                return user;
            }
        }


        public User GetById(string id)
        {
            using (var connect = db.Connection())
            {
                connect.Open();
                var command = new MySqlCommand($"select * from user where Id = @id;", connect);
                command.Parameters.AddWithValue("@id", id);
                var row = command.ExecuteReader();
                User user = new();
                while (row.Read())
                {
                    user.Id = Convert.ToString(row[0]);
                    user.UserEmail = Convert.ToString(row[1]);
                    user.Password = Convert.ToString(row[2]);
                    user.AddressId = Convert.ToString(row[3]);
                    user.ProfileId = Convert.ToString(row[4]);
                    user.RoleId = Convert.ToString(row[5]);
                    user.DateCreated = Convert.ToDateTime(row[6]);
                }
                return user;
            }
        }

        public List<User> GetAllUsers()
        {
            List<User> useres = new List<User>();
            using (var connect = db.Connection())
            {
                connect.Open();
                var command = new MySqlCommand($"Select * From user;", connect);
                var row = command.ExecuteReader();
                while (row.Read())
                {
                    User user = new();
                    user.Id = Convert.ToString(row[0]);
                    user.UserEmail = Convert.ToString(row[1]);
                    user.Password = Convert.ToString(row[2]);
                    user.AddressId = Convert.ToString(row[3]);
                    user.ProfileId = Convert.ToString(row[4]);
                    user.RoleId = Convert.ToString(row[5]);
                    user.DateCreated = Convert.ToDateTime(row[6]);

                    useres.Add(user);
                }

            }
            return useres;
        }
    }
}
