using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;
        public LocationsController(ILocationRepository locationRepository, IMapper mapper) 
        {
            _mapper = mapper;
            _locationRepository = locationRepository;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<LocationDto>>> GetLocationsByCityId([FromQuery] int cityId) 
        {
            return Ok(await _locationRepository.GetLocationsByCityIdAsync(cityId));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LocationDto>> GetLocationById([FromQuery] int id) 
        {
            return Ok(await _locationRepository.GetLocationByIdAsync(id));
        }

        [HttpPost()]
        public async Task<ActionResult<LocationDto>> AddLocationToCity([FromBody] LocationDto locationDetails, [FromQuery] int cityId) 
        {
            return Ok(await _locationRepository.AddLocationAsync(locationDetails, cityId));
        }

        [HttpPut()]
        public async Task<ActionResult<LocationDto>> EditLocationFromCity([FromBody] LocationDto locationDetails, [FromQuery] int locationId) 
        {
            return Ok(await _locationRepository.EditLocationAsync(locationDetails, locationId));
        }

        [HttpDelete()]
        public async Task DeleteLocationFromCity([FromQuery] int locationId) 
        {
            await _locationRepository.DeleteLocationAsync(locationId);
        }
    }
}