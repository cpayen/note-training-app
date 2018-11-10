using Note.Core.Data;
using Note.Core.Helpers;
using Note.Core.Entities;
using Note.Core.DTO.Login;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Note.Core.Services
{
    public class AuthService : IAuthService
    {
        protected readonly IRepository<AppUser> _appUserRepository;

        public AuthService(IRepository<AppUser> appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        public async Task<List<Claim>> LoginAsync(LoginDTO dto)
        {
            var users = await _appUserRepository.GetItemsAsync(o => o.Email == dto.Email);
            var user = users.FirstOrDefault();
            if(user != null)
            {
                string hash = SecurityHelper.EncryptPassword(dto.Password, user.Salt);
                if(hash == user.Password)
                {
                    return new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.Role.ToString())
                    };
                }
            }

            return null;
        }
    }
}
