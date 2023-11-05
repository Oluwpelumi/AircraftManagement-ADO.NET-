using AircraftM.DTOs;
using AircraftM.Models;
using AircraftM.Repositories.Implementations;
using AircraftM.Repositories.Interfaces;
using AircraftM.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AircraftM.Services.Implementations
{
    public class FlightService : IFlightService
    {
        IFlightRepository _flightRepository = new FlightRepository();
        IAircraftRepository _aircraftRepository = new AircraftRepository();
        public FlightResponse<FlightDto> ScheduleFlight(FlightRequestModel model)
        {
            var aircrafts = _aircraftRepository.GetAll();
            bool check = aircrafts.Any(aircraft => aircraft.Name == model.AircraftName);
            if (check)
            {
                var flgt = _flightRepository.Get(model.ReferenceNumber);
                if (flgt != null)
                {
                    return new FlightResponse<FlightDto>
                    {
                        Status = true,
                        Message = "flight already exists",
                        Data = null
                    };
                }
                Flight flight = new Flight
                {
                    ReferenceNumber = model.ReferenceNumber,
                    TakeOffPoint = model.TakeOffPoint,
                    Destination = model.Destination,
                    TakeOfTime = model.TakeOfTime,
                    PilotStaffNumber = model.PilotStaffNumber,
                    AircraftName = model.AircraftName,
                    Price = model.Price,
                };
                _flightRepository.Book(flight);
                return new FlightResponse<FlightDto>
                {
                    Status = true,
                    Message = $"flight with the reference-number {model.ReferenceNumber} has been Registered successfully",
                    Data = new FlightDto
                    {
                        Id = flight.Id,
                        ReferenceNumber = model.ReferenceNumber,
                        TakeOffPoint = model.TakeOffPoint,
                        Destination = model.Destination,
                        TakeOfTime = model.TakeOfTime,
                        PilotStaffNumber = model.PilotStaffNumber,
                        AircraftName = model.AircraftName,
                        Price = model.Price,
                        DateCreated = flight.DateCreated
                    }
                };
            }
            else
            {
                return new FlightResponse<FlightDto>
                {
                    Status = true,
                    Message = $"There is an existing flight schedule for the aircraft{model.AircraftName} already",
                    Data = null
                };
            }
        }

        public FlightResponse<bool> DeleteFlight(string referenceNumber)
        {
            var flight = _flightRepository.Get(referenceNumber);
            if (flight != null)
            {
                _flightRepository.Delete(referenceNumber);
                return new FlightResponse<bool>
                {
                    Data = true,
                    Message = $"The flight with the reference number {flight.ReferenceNumber} has been cancelled Successfully",
                    Status = true
                };
            }
            return new FlightResponse<bool>
            {
                Data = false,
                Message = "flight not found",
                Status = false
            };
        }

        public FlightResponse<List<FlightDto>> GetAllFlights()
        {
            var flights = _flightRepository.GetAll();
            if (flights != null)
            {
                return new FlightResponse<List<FlightDto>>
                {
                    Status = true,
                    Message = "Successful",
                    Data = flights.Select(flight => new FlightDto
                    {
                        Id = flight.Id,
                        ReferenceNumber = flight.ReferenceNumber,
                        TakeOffPoint = flight.TakeOffPoint,
                        Destination = flight.Destination,
                        TakeOfTime = flight.TakeOfTime,
                        PilotStaffNumber = flight.PilotStaffNumber,
                        AircraftName = flight.AircraftName,
                        Price = flight.Price,
                        DateCreated = flight.DateCreated
                    }).ToList()
                };
            }
            return new FlightResponse<List<FlightDto>>
            {
                Data = null,
                Message = "No flight found",
                Status = false
            };
        }

        public FlightResponse<FlightDto> GetFlight(string referenceNumber)
        {
            var flight = _flightRepository.Get(referenceNumber);
            if (flight != null)
            {
                return new FlightResponse<FlightDto>
                {
                    Data = new FlightDto
                    {
                        Id = flight.Id,
                        ReferenceNumber = flight.ReferenceNumber,
                        TakeOffPoint = flight.TakeOffPoint,
                        Destination = flight.Destination,
                        TakeOfTime = flight.TakeOfTime,
                        PilotStaffNumber = flight.PilotStaffNumber,
                        AircraftName = flight.AircraftName,
                        Price = flight.Price,
                        DateCreated = flight.DateCreated
                    },
                    Message = "Successful",
                    Status = true
                };
            }
            return new FlightResponse<FlightDto>
            {
                Data = null,
                Message = "flight not found",
                Status = false
            };
        }
    }
}
