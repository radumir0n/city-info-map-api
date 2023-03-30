using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface ICityRepository
    {
        Task<IEnumerable<CityDto>> GetCitiesByUserIdAsync(int id);

        Task<CityDto> AddCityAsync(CityDto cityDetails, int id);

        Task<CityDto> EditCityAsync(CityDto cityDetails, int id);

        Task DeleteCityAsync(int id);

        Task<City> GetCityById(int id);
    }
}