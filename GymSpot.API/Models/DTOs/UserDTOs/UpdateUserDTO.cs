namespace GymSpot.API.Models.DTOs.UserDTOs
{
    public class UpdateUserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int PhoneNumber { get; set; }
        public string Role { get; set; }

        public Guid RegionId { get; set; }

    }
}
