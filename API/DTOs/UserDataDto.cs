namespace API.DTOs
{
    public class UserDataDto
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public ICollection<CityDto> Cities { get; set; }
    }
}