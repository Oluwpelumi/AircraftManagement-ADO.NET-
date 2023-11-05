using AircraftM.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM
{
    public class StartUp
    {
        public StartUp() 
        {
            CreateSchema();
            CreateAircraftTable();
            CreateAirportTable();
            CreateRoleTable();
            CreateProfileTable();
            CreateAddressTable();
            CreateUserTable();
            CreateFlightTable();
            CreateBookingTable();
            CreateManagerTable();
            CreatePilotTable();
            CreatePassengerTable();

        }

        public string connectionStrings = "server = localhost; user = root; database = AircraftMgt; password = Adewale24434$";
        public MySqlConnection Connection() => new MySqlConnection(connectionStrings);



        public void CreateSchema()
        {
            string connectionStrings = "server = localhost; user = root; password = Adewale24434$";

            using(var connection = new MySqlConnection(connectionStrings))
            {
                connection.Open();
                var querry = "create schema if not exists AircraftMgt";
                var command = new MySqlCommand(querry, connection);
                int row = command.ExecuteNonQuery();
                Console.WriteLine(row > 0 ? "schema created successfully" : "not created");
            }
        }

        public void CreateTable(string tableQuerry)
        {
            using(var connection = Connection())
            {
                connection.Open();
                var command = new MySqlCommand(tableQuerry, connection);
                int row = command.ExecuteNonQuery();
                Console.WriteLine(row != -1 ? "table created successfully" : "table not created");
            }
        }


        public void CreateAircraftTable()
        {
            var querry = "create table if not exists aircraft" +
                "(Id varchar(50) not null unique," +
                " Name varchar(50) not null unique, " +
                "EngineNumber varchar(50) not null unique, " +
                "Capacity int, " +
                "DateCreated Datetime, " +
                "primary key(Id))";
            CreateTable(querry);
        }

        public void CreateAirportTable()
        {
            var querry = "create table if not exists airport" +
                "(Id varchar(50) not null unique," +
                " Name varchar(50) not null unique," +
                " Location varchar(50) not null unique, " +
                "AirportType varchar(50) not null," +
                " DateCreated Datetime, " +
                "primary key(Id))";
            CreateTable(querry);
        }


        public void CreateBookingTable()
        {
            var querry = "create table if not exists booking" +
                "(Id varchar(50) not null unique," +
                " ReferenceNumber varchar(50) not null unique," +
                " SeatNumber int, " +
                "PassengerEmail varchar(50) not null," +
                "FlightReferenceNumber varchar(50) not null," +
                " AircraftName varchar(30) not null," +
                " DateCreated Datetime, " +
                "primary key(Id), " +
                "foreign key(FlightReferenceNumber) references Flight(ReferenceNumber)," +
                "foreign key(AircraftName) references Aircraft(Name))";
            CreateTable(querry);
        }
        //public void CreateBookingTable()
        //{
        //    var query = "CREATE TABLE IF NOT EXISTS booking (" +
        //        "Id VARCHAR(50) NOT NULL UNIQUE, " +
        //        "ReferenceNumber VARCHAR(50) NOT NULL UNIQUE, " +
        //        "SeatNumber INT, " +
        //        "PassengerEmail VARCHAR(50) NOT NULL, " +
        //        "FlightReferenceNumber VARCHAR(50) NOT NULL, " +
        //        "AircraftName VARCHAR(30) NOT NULL, " +
        //        "DateCreated DATETIME, " +
        //        "PRIMARY KEY (Id), " +
        //        "FOREIGN KEY (FlightReferenceNumber) REFERENCES Flight (ReferenceNumber), " +
        //        "FOREIGN KEY (AircraftName) REFERENCES Aircraft (Name))";

        //    CreateTable(query);
        //}


        public void CreateAddressTable()
        {
            var querry = "create table if not exists address" +
                "(Id varchar(50) not null unique," +
                " Number int, " +
                "Street varchar(50) not null," +
                "City varchar(50) not null," +
                " State varchar(50) not null , " +
                "PostalCode varchar(50) not null," +
                " DateCreated Datetime," +
                " primary key(Id))";
            CreateTable(querry);
        }

        public void CreateFlightTable()
        {
            var querry = "create table if not exists flight" +
                "(Id varchar(50) not null unique, " +
                "ReferenceNumber varchar(50) not null unique," +
                "TakeOffPoint varchar(50) not null," +
                " Destination varchar(50) not null ," +
                "TakeOfTime DateTime, " +
                " PilotStaffNumber varchar(50) not null, " +
                " AircraftName varchar(50) not null, " +
                "Price double," +
                "DateCreated Datetime," +
                " primary key(Id)," +
                "foreign key(AircraftName) references Aircraft(Name))";

            CreateTable(querry);
        }


        public void CreateManagerTable()
        {
            var querry = "create table if not exists manager" +
                "(Id varchar(50) not null unique," +
                "UserId varchar(50) not null," +
                "StaffNumber varchar(50) not null," +
                "Wallet double ," +
                " DateCreated Datetime," +
                " primary key(Id), " +
                "foreign key(UserId) references User(Id))";
            CreateTable(querry);
        }


        public void CreatePassengerTable()
        {
            var querry = "create table if not exists passenger" +
                "(Id varchar(50) not null unique," +
                "UserId varchar(50) not null," +
                "RegNumber varchar(50) not null," +
                "BookingId varchar(50) not null," +
                "Wallet double ," +
                " DateCreated Datetime," +
                " primary key(Id), " +
                "foreign key(UserId) references User(Id))";
            CreateTable(querry);
        }


        public void CreatePilotTable()
        {
            var querry = "create table if not exists pilot" +
                "(Id varchar(50) not null unique," +
                "UserId varchar(50) not null," +
                "StaffNumber varchar(50) not null," +
                "Wallet double ," +
                " DateCreated Datetime," +
                " primary key(Id)," +
                " foreign key(UserId) references User(Id))";
            CreateTable(querry);
        }

        public void CreateProfileTable()
        {
            var querry = "create table if not exists profile" +
                "(Id varchar(50) not null unique," +
                "FirstName varchar(50) not null," +
                "LastName varchar(50) not null," +
                "UserName varchar(50) not null," +
                "UserEmail varchar(50) not null," +
                "DOB Datetime," +
                "Gender varchar(50) not null," +
                " DateCreated Datetime," +
                " primary key(Id))";
            CreateTable(querry);
        }

        public void CreateRoleTable()
        {
            var querry = "create table if not exists role" +
                "(Id varchar(50) not null unique," +
                "Name varchar(50) not null," +
                "Description varchar(50) not null," +
                " DateCreated Datetime," +
                " primary key(Id))";
            CreateTable(querry);
        }

        public void CreateUserTable()
        {
            var querry = "create table if not exists user" +
                "(Id varchar(50) not null unique, " +
                "UserEmail varchar(50) not null unique, " +
                "Password varchar(50) not null," +
                "AddressId varchar(50) not null," +
                " ProfileId varchar(50) not null ," +
                " RoleId varchar(50) not null, " +
                "DateCreated Datetime," +
                " primary key(Id), " +
                "foreign key(AddressId) references Address(Id), " +
                "foreign key(ProfileId) references Profile(Id), " +
                "foreign key(RoleId) references Role(Id))";

            CreateTable(querry);
        }



    }
}
