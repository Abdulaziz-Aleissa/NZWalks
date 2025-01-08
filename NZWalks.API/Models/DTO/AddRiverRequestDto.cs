using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class AddRiverRequestDto
    {
        [Required]
        [MinLength(3,ErrorMessage ="Name must be minimum 3 characters")]
        [MaxLength(12, ErrorMessage = "Name must be maximum 12 characters")]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
