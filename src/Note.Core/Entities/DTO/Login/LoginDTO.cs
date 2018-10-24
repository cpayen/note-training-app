using System.ComponentModel.DataAnnotations;

namespace Note.Core.Entities.DTO.Login
{
    public class LoginDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
