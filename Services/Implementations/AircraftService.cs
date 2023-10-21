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

namespace AircraftM.Services.Implementations
{
    public class AircraftService : IAircraftService
    {
        IAircraftRepository _aircraftRepository = new AircraftRepository();
        public AircraftResponse<bool> DeleteAircraft(string engineNumber)
        {
            var aircraft = _aircraftRepository.Get(engineNumber);
            if (aircraft != null)
            {
                _aircraftRepository.Delete(engineNumber);
                return new AircraftResponse<bool>
                {
                    Data = true,
                    Message = "Successful",
                    Status = true
                };
            }
            return new AircraftResponse<bool>
            {
                Data = false,
                Message = "aircraft not found",
                Status = false
            };

        }

        public AircraftResponse<AircraftDto> GetAircraft(string engineNumber)
        {
            var aircraft = _aircraftRepository.Get(engineNumber);
            if (aircraft != null)
            {
                return new AircraftResponse<AircraftDto>
                {
                    Data = new AircraftDto
                    {
                        Id = aircraft.Id,
                        Capacity = aircraft.Capacity,
                        EngineNumber = aircraft.EngineNumber,
                        Name = aircraft.Name,
                        DateCreated = aircraft.DateCreated
                    },
                    Message = "Successful",
                    Status = true
                };
            }
            return new AircraftResponse<AircraftDto>
            {
                Data = null,
                Message = "aircraft not found",
                Status = false
            };
        }

        public AircraftResponse<List<AircraftDto>> GetAllAircrafts()
        {
            var aircrafts = _aircraftRepository.GetAll();
            if (aircrafts != null)
            {
                return new AircraftResponse<List<AircraftDto>>
                {
                    Status = true,
                    Message = "Successful",
                    Data = aircrafts.Select(aircraft => new AircraftDto
                    {
                        Id = aircraft.Id,
                        Capacity = aircraft.Capacity,
                        EngineNumber = aircraft.EngineNumber,
                        Name = aircraft.Name,
                        DateCreated = aircraft.DateCreated
                    }).ToList()
                };
            }
            return new AircraftResponse<List<AircraftDto>>
            {
                Data = null,
                Message = "No aircraft found",
                Status = false
            };
        }

        public AircraftResponse<AircraftDto> RegisterAircraft(AircraftRequestModel model)
        {
            var arft = _aircraftRepository.Get(model.EngineNumber);
            if (arft != null)
            {
                return new AircraftResponse<AircraftDto>
                {
                    Status = true,
                    Message = "Aircraft already exists",
                    Data = null
                };
            }
            Aircraft aircraft = new Aircraft
            {
                EngineNumber = model.EngineNumber,
                Capacity = model.Capacity,
                Name = model.Name
            };
            _aircraftRepository.Create(aircraft);
            return new AircraftResponse<AircraftDto>
            {
                Status = true,
                Message = "Aircraft Registered successfully",
                Data = new AircraftDto
                {
                    Id = aircraft.Id,
                    Capacity = aircraft.Capacity,
                    Name = aircraft.Name,
                    EngineNumber = aircraft.EngineNumber,
                    DateCreated = aircraft.DateCreated
                }
            };
        }
    }
}
