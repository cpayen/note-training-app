using AutoMapper;
using Note.Core.Data;
using Note.Core.DTO.Login;
using Note.Core.Entities;
using Note.Core.Helpers;
using System.Linq;
using System.Threading.Tasks;

namespace Note.Core.Services
{
    public class AuthService : IAuthService
    {
        protected readonly IRepository<AppUser> _appUserRepository;
        protected readonly IMapper _mapper;

        public AuthService(IRepository<AppUser> appUserRepository, IMapper mapper)
        {
            _appUserRepository = appUserRepository;
            _mapper = mapper;
        }

        public async Task<AuthenticatedUserDTO> LoginAsync(LoginDTO dto)
        {
            var users = await _appUserRepository.GetItemsAsync(o => o.Email == dto.Email);
            var user = users.FirstOrDefault();
            
            if (user != null)
            {
                string hash = SecurityHelper.EncryptPassword(dto.Password, user.Salt);
                if (hash == user.Password)
                {
                    return _mapper.Map<AuthenticatedUserDTO>(user);
                }
            }

            return null;
        }
    }
}
