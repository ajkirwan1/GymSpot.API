using System.ComponentModel.DataAnnotations;

namespace GymSpot.API.Models.DTOs.UserDTOs
{
    public class AddUserRequestDTO
    {
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public int PhoneNumber { get; set; }
        public string Role { get; set; }
        public Guid RegionId { get; set; }
    }
}
