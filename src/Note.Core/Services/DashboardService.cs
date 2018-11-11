using AutoMapper;
using Note.Core.Data;
using Note.Core.DTO.Note;
using Note.Core.Entities;
using Note.Core.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Note.Core.Services
{
    public class DashboardService : IDashboardService
    {
        protected readonly IRepository<Dashboard> _repository;
        protected readonly ICurrentUserInfo _currentUserService;
        protected readonly IMapper _mapper;

        public DashboardService(IRepository<Dashboard> repository, ICurrentUserInfo currentUserService, IMapper mapper)
        {
            _repository = repository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DashboardDTO>> GetAllDashboardsAsync()
        {
            var items = await _repository.GetItemsAsync();
            return _mapper.Map<List<DashboardDTO>>(items);
        }

        public async Task<DashboardDTO> GetDashboardAsync(string id)
        {
            var item = await _repository.GetItemAsync(id);
            if (item == null)
            {
                throw new NotFoundException("Dashboard not found.");
            }
            return _mapper.Map<DashboardDTO>(item);
        }

        public async Task<DashboardDTO> CreateDashboardAsync(CreateDashboardDTO dto)
        {
            var item = _mapper.Map<Dashboard>(dto);

            var createdItem = await _repository.CreateItemAsync(item);
            return _mapper.Map<DashboardDTO>(createdItem);
        }

        public async Task<DashboardDTO> UpdateDashboardAsync(string id, UpdateDashboardDTO dto)
        {
            var item = await _repository.GetItemAsync(id);
            if (item == null)
            {
                throw new NotFoundException("Dashboard not found.");
            }

            _mapper.Map(dto, item);

            var updatedItem = await _repository.UpdateItemAsync(id, item);
            return _mapper.Map<DashboardDTO>(updatedItem);
        }

        public async Task<bool> DeleteDashboardAsync(string id)
        {
            var item = await _repository.GetItemAsync(id);
            if (item == null)
            {
                throw new NotFoundException("Dashboard not found.");
            }

            //TODO: Delete NoteLists

            return await _repository.DeleteItemAsync(id);
        }
    }
}
