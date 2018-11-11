using System.Collections.Generic;
using System.Threading.Tasks;
using Note.Core.DTO.Note;

namespace Note.Core.Services
{
    public interface IDashboardService
    {
        Task<DashboardDTO> CreateDashboardAsync(CreateDashboardDTO dto);
        Task<bool> DeleteDashboardAsync(string id);
        Task<IEnumerable<DashboardDTO>> GetAllDashboardsAsync();
        Task<DashboardDTO> GetDashboardAsync(string id);
        Task<DashboardDTO> UpdateDashboardAsync(string id, UpdateDashboardDTO dto);
    }
}