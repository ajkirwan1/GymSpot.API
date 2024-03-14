namespace GymSpot.API.Models.DTOs.UserDTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public int PhoneNumber { get; set; }
        public string? Role { get; set; }
        public RegionDTO Region { get; set; }
    }
}
