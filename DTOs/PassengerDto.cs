using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.DTOs
{
    public record PassengerDto
    {
        public string Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string UserName { get; init; }
        public string UserEmail { get; init; }
        public DateTime DOB { get; init; }
        public string Gender { get; init; }
        public int Number { get; init; }
        public string Street { get; init; }
        public string City { get; init; }
        public string State { get; init; }
        public string PostalCode { get; init; }
        public string UserId { get; init; }
        public string RegNumber { get; init; }
        public double Wallet { get; set; }
        public string BookingId { get; set; }
        public DateTime DateCreated { get; init; }
    }


    public record PassengerBookingIdRequestModel
    {
        public string BookingId { get; set; }
    }

    public record PassengerRequestModel
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string UserName { get; init; }
        public string UserEmail { get; init; }
        public string Password { get; init; }
        public string RoleName { get; init; }
        public DateTime DOB { get; init; }
        public string Gender { get; init; }
        public int Number { get; init; }
        public string Street { get; init; }
        public string City { get; init; }
        public string State { get; init; }
        public string PostalCode { get; init; }
        public double Wallet { get; set; }
    }


    //public record PassengerMakeBookingRequestModel
    //{
    //    public string BookingReferenceNumber { get; init; }
    //    public int SeatNumber { get; init; }
    //    public string PassengerEmail { get; init; }
    //    public string FlightReferenceNumber { get; init; }
    //    public string TakeOffPoint { get; init; }
    //    public string Destination { get; init; }
    //    public DateTime TakeOfTime { get; init; }
    //    public string PilotStaffNumber { get; init; }
    //    public string AircraftName { get; init; }
    //    public double Price { get; init; }
    //}

    public class PassengerResponse<T>
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public T Data { get; set; }
    }
}
