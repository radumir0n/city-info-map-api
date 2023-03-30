using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Locations")]
    public class Location
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public City City { get; set; }

        public int CityId { get; set; }
    }
}