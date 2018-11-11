using Note.Core.Enums;

namespace Note.Core.DTO.Login
{
    public class AuthenticatedUserDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public AppUserRole Role { get; set; }
        public string Token { get; set; }
    }
}
