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
    public class AddressRepository : IAddressRepository
    {
        public string connectionStrings = "server = localhost; user = root; database = AircraftMgt; password = Adewale24434$";
        public MySqlConnection Connection() => new MySqlConnection(connectionStrings);


        public Address Create(Address address)
        {
            using (var connect = Connection())
            {
                connect.Open();
                var dateCreated = address.DateCreated.ToString("yyyy-MM-dd HH:mm:ss");
                var querry = $"Insert into address (Id, Number, Street, City, State, PostalCode, DateCreated) values('{address.Id}', '{address.Number}', '{address.Street}', '{address.City}', '{address.State}', '{address.PostalCode}','{dateCreated}');";
                var command = new MySqlCommand(querry, connect);
                var row = command.ExecuteNonQuery();
                if (row != -1)
                {
                    return address;
                }
                return null;
            }
        }

        public bool Delete(string id)
        {
            using (var connect = Connection())
            {
                connect.Open();
                var querry = $"DELETE FROM address WHERE ID = @id;";
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

        public Address Get(string id)
        {
            using (var connect = Connection())
            {
                connect.Open();
                var command = new MySqlCommand($"select * from address where Id = @id;", connect);
                command.Parameters.AddWithValue("@id", id);
                var row = command.ExecuteReader();
                Address address = null;
                while (row.Read())
                {
                    address = new Address();
                    address.Id = Convert.ToString(row[0]);
                    address.Number = Convert.ToInt32(row[1]);
                    address.Street = Convert.ToString(row[2]);
                    address.City = Convert.ToString(row[3]);
                    address.State = Convert.ToString(row[4]);
                    address.PostalCode = Convert.ToString(row[5]);
                    address.DateCreated = Convert.ToDateTime(row[6]);
                }
                return address;
            }
        }

        public List<Address> GetAll()
        {
            List<Address> addresses = new List<Address>();
            using (var connect = Connection())
            {
                connect.Open();
                var command = new MySqlCommand($"Select * From address;", connect);
                var row = command.ExecuteReader();
                while (row.Read())
                {
                    Address address = new ();
                    address.Id = Convert.ToString(row[0]);
                    address.Number = Convert.ToInt32(row[1]);
                    address.Street = Convert.ToString(row[2]);
                    address.City = Convert.ToString(row[3]);
                    address.State = Convert.ToString(row[4]);
                    address.PostalCode = Convert.ToString(row[5]);
                    address.DateCreated = Convert.ToDateTime(row[6]);

                    addresses.Add(address);
                }

            }
            return addresses;
        }
    }
}
