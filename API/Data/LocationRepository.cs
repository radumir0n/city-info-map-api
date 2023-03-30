using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class LocationRepository : ILocationRepository
    {
        private readonly DataContext _context;
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public LocationRepository(DataContext context, ICityRepository cityRepository, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _cityRepository = cityRepository;
        }

        public async Task<IEnumerable<LocationDto>> GetLocationsByCityIdAsync(int cityId)
        {
            var city = await _cityRepository.GetCityById(cityId);

            var locations = await _context.Locations.Where(l => l.CityId == city.Id).ToListAsync();

            var locationsResult = _mapper.Map<IEnumerable<LocationDto>>(locations);
            
            return locationsResult;
        }

        public async Task<LocationDto> GetLocationByIdAsync(int id)
        {
            var location = await _context.Locations.Where(l => l.Id == id).FirstOrDefaultAsync();

            var locationResult = _mapper.Map<LocationDto>(location);
            
            return locationResult;
        }

        public async Task<LocationDto> AddLocationAsync(LocationDto locationDetails, int cityId)
        {
            var newLocation = new Location() 
            {
                Name = locationDetails.Name,
                Latitude = locationDetails.Latitude,
                Longitude = locationDetails.Longitude,
                Description = locationDetails.Description,
                Address = locationDetails.Address,
                CityId = cityId
            };

            await _context.Locations.AddAsync(newLocation);
            
            await _context.SaveChangesAsync();

            var locationResult = _mapper.Map<LocationDto>(newLocation);

            return locationResult;
        }

        public async Task<LocationDto> EditLocationAsync(LocationDto locationDetails, int locationId)
        {
            var currentLocation = await _context.Locations.Where(l => l.Id == locationId).FirstOrDefaultAsync();

            currentLocation.Name = locationDetails.Name;
            currentLocation.Latitude = locationDetails.Latitude;
            currentLocation.Longitude = locationDetails.Longitude;
            currentLocation.Description = locationDetails.Description;
            currentLocation.Address = locationDetails.Address;

            await _context.SaveChangesAsync();

            var locationResult = _mapper.Map<LocationDto>(currentLocation);

            return locationResult;
        }

        public async Task DeleteLocationAsync(int locationId)
        {
            var currentLocation = await _context.Locations.Where(l => l.Id == locationId).FirstOrDefaultAsync();

            _context.Locations.Remove(currentLocation);

            await _context.SaveChangesAsync();
        }
    }
}