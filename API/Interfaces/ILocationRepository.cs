using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface ILocationRepository
    {
        Task<IEnumerable<LocationDto>> GetLocationsByCityIdAsync(int cityId);

        Task<LocationDto> GetLocationByIdAsync(int id);

        Task<LocationDto> AddLocationAsync(LocationDto cityDetails, int cityId);

        Task<LocationDto> EditLocationAsync(LocationDto cityDetails, int cityId);

        Task DeleteLocationAsync(int locationId);
    }
}