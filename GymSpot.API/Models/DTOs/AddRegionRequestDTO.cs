using System.ComponentModel.DataAnnotations;

namespace GymSpot.API.Models.DTOs
{
    public class AddRegionRequestDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code needs to be three characters")]
        [MaxLength(3, ErrorMessage = "Code needs to be three characters")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Name must be less than 100 characters")]
        public string Name { get; set; }
    }
}
