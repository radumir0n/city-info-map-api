using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class CityRepository : ICityRepository
    {
        private readonly DataContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public CityRepository(DataContext context, IUserRepository userRepository, IMapper mapper)
        {
            _context = context;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CityDto>> GetCitiesByUserIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            if (user is null)
            {
                throw new Exception("User not found");
            }

            var cities = await _context.Cities.Where(c => c.AppUserId == user.Id).Include(c => c.Locations).ToListAsync();

            var citiesResult = _mapper.Map<IEnumerable<CityDto>>(cities);

            return citiesResult;
        }

        public async Task<CityDto> AddCityAsync(CityDto cityDetails, int id)
        {
            var newCity = new City() 
            {
                Name = cityDetails.Name,
                Latitude = cityDetails.Latitude,
                Longitude = cityDetails.Longitude,
                Locations = new Location[] {},
                AppUserId = id
            };

            await _context.Cities.AddAsync(newCity);
            
            await _context.SaveChangesAsync();

            var cityResult = _mapper.Map<CityDto>(newCity);

            return cityResult;
        }

        public async Task<CityDto> EditCityAsync(CityDto cityDetails, int id)
        {
            var currentCity = await GetCityById(id);

            currentCity.Name = cityDetails.Name;
            currentCity.Latitude = cityDetails.Latitude;
            currentCity.Longitude = cityDetails.Longitude;

            await _context.SaveChangesAsync();

            var cityResult = _mapper.Map<CityDto>(currentCity);

            return cityResult;
        }

        public async Task DeleteCityAsync(int id)
        {
            var currentCity = await GetCityById(id);

            _context.Cities.Remove(currentCity);

            await _context.SaveChangesAsync();
        }

        public async Task<City> GetCityById(int id)
        {
            var currentCity = await _context.Cities.Where(c => c.Id == id).FirstOrDefaultAsync();

            if (currentCity is null)
            {
                throw new Exception($"City with id {id} does not exist");
            }

            return currentCity;
        }
    }
}