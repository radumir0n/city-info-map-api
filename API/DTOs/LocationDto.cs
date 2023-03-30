using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class LocationDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Latitude { get; set; }

        [Required]
        public string Longitude { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Address { get; set; }
    }
}