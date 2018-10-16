using System.Collections.Generic;
using System.Threading.Tasks;
using Note.Core.Models.DTO.AppUser;

namespace Note.Core.Services
{
    public interface IUserService
    {
        Task<AppUserDTO> CreateAsync(CreateAppUserDTO dto);
        Task<bool> DeleteAsync(string id);
        Task<IEnumerable<AppUserDTO>> GetAllAsync();
        Task<AppUserDTO> GetAsync(string id);
        Task<AppUserDTO> UpdateAsync(string id, UpdateAppUserDTO dto);
    }
}