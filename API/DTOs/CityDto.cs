using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class CityDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Latitude { get; set; }

        [Required]
        public string Longitude { get; set; }

        public ICollection<LocationDto> Locations { get; set; }
    }
}