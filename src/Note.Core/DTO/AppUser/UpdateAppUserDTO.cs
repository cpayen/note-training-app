using Note.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Note.Core.DTO.AppUser
{
    public class UpdateAppUserDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public AppUserRole Role { get; set; }
    }
}
