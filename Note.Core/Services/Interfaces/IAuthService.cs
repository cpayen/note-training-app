using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Note.Core.Models.DTO.Login;

namespace Note.Core.Services
{
    public interface IAuthService
    {
        Task<List<Claim>> LoginAsync(LoginDTO dto);
    }
}