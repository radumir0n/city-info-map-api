using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Cities")]
    public class City
    {
        public int Id { get; set; }

        
        public string Name { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public ICollection<Location> Locations { get; set; }

        public AppUser AppUser { get; set; }
        
        public int AppUserId { get; set; }
    }
}