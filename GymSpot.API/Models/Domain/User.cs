namespace GymSpot.API.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int PhoneNumber { get; set; }
        public string Role { get; set; }

        public Guid RegionId { get; set; }

        // Navigation properties

        public Region Region { get; set; }
    }
}
