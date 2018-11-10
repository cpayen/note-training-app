using Note.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Note.Core.DTO.AppUser
{
    public class CreateAppUserDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public AppUserRole Role { get; set; }
    }
}
