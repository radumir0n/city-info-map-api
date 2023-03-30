using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;
        public CitiesController(ICityRepository cityRepository, IMapper mapper) 
        {
            _mapper = mapper;
            _cityRepository = cityRepository;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetCitiesByUserId([FromQuery] int userId) 
        {
            var cities = await _cityRepository.GetCitiesByUserIdAsync(userId);

            var citiesResult = _mapper.Map<IEnumerable<CityDto>>(cities);

            return Ok(citiesResult);
        }

        [HttpPost()]
        public async Task<ActionResult<CityDto>> AddCity([FromBody] CityDto cityDetails, [FromQuery] int userId) 
        {
            return Ok(await _cityRepository.AddCityAsync(cityDetails, userId));
        }

        [HttpPut()]
        public async Task<ActionResult<CityDto>> EditCity([FromBody] CityDto cityDetails, [FromQuery] int cityId) 
        {
            return Ok(await _cityRepository.EditCityAsync(cityDetails, cityId));
        }

        [HttpDelete()]
        public async Task DeleteCity([FromQuery] int cityId) 
        {
            await _cityRepository.DeleteCityAsync(cityId);
        }
    }
}