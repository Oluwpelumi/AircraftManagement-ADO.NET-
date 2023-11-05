using AircraftM.DTOs;
using AircraftM.Models;
using AircraftM.Repositories.Implementations;
using AircraftM.Repositories.Interfaces;
using AircraftM.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AircraftM.Services.Implementations
{
    public class AirportService : IAirportService
    {
        IAirportRepository _airportRepository = new AirportRepository();
        public AirportResponse<bool> DeleteAirport(string name)
        {
            var airport = _airportRepository.Get(name);
            if (airport != null)
            {
                _airportRepository.Delete(name);
                return new AirportResponse<bool>
                {
                    Data = true,
                    Message = $"The airport {airport.Name} has been deleted Successfully",
                    Status = true
                };
            }
            return new AirportResponse<bool>
            {
                Data = false,
                Message = "airport not found",
                Status = false
            };
        }

        public AirportResponse<AirportDto> GetAirport(string name)
        {
            var airport = _airportRepository.Get(name);
            if (airport != null)
            {
                return new AirportResponse<AirportDto>
                {
                    Data = new AirportDto
                    {
                        Id = airport.Id,
                        Name = airport.Name,
                        Location = airport.Location,
                        AirportType = airport.AirportType,
                        DateCreated = airport.DateCreated
                    },
                    Message = "Successful",
                    Status = true
                };
            }
            return new AirportResponse<AirportDto>
            {
                Data = null,
                Message = "airport not found",
                Status = false
            };
        }

        public AirportResponse<List<AirportDto>> GetAllAirports()
        {
            var airports = _airportRepository.GetAll();
            if (airports != null)
            {
                return new AirportResponse<List<AirportDto>>
                {
                    Status = true,
                    Message = "Successful",
                    Data = airports.Select(airport => new AirportDto
                    {
                        Id = airport.Id,
                        Name = airport.Name,
                        Location = airport.Location,
                        AirportType = airport.AirportType,
                        DateCreated = airport.DateCreated
                    }).ToList()
                };
            }
            return new AirportResponse<List<AirportDto>>
            {
                Data = null,
                Message = "No airport found",
                Status = false
            };
        }

        public AirportResponse<AirportDto> RegisterAirport(AirportRequestModel model)
        {
            var airp = _airportRepository.Get(model.Name);
            if (airp != null)
            {
                return new AirportResponse<AirportDto>
                {
                    Status = true,
                    Message = "airport already exists",
                    Data = null
                };
            }
            Airport airport = new Airport
            {
                Name = model.Name,
                Location = model.Location,
                AirportType = model.AirportType,
            };
            _airportRepository.Create(airport);
            return new AirportResponse<AirportDto>
            {
                Status = true,
                Message = $"airport{airport.Name} Registered successfully",
                Data = new AirportDto
                {
                    Id = airport.Id,
                    Name = airport.Name,
                    Location = airport.Location,
                    AirportType = airport.AirportType,
                    DateCreated = airport.DateCreated
                }
            };
        }
    }
}
