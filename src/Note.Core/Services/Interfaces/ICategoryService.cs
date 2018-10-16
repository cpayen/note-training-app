using System.Collections.Generic;
using System.Threading.Tasks;
using Note.Core.Models.DTO.Note;

namespace Note.Core.Services
{
    public interface ICategoryService
    {
        Task<NoteCategoryDTO> CreateAsync(CreateNoteCategoryDTO dto);
        Task<bool> DeleteAsync(string id);
        Task<IEnumerable<NoteCategoryDTO>> GetAllAsync();
        Task<NoteCategoryDTO> GetAsync(string id);
        Task<NoteCategoryDTO> UpdateAsync(string id, UpdateNoteCategoryDTO dto);
    }
}