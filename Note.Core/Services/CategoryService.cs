using Note.Core.Exceptions;
using Note.Core.Helpers;
using Note.Core.Models;
using Note.Core.Models.DTO.Note;
using Note.Core.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Note.Core.Services
{
    public class CategoryService
    {
        protected readonly Repository<NoteCategory> _repository;
        protected readonly ICurrentUserService _currentUserService;

        public CategoryService(Repository<NoteCategory> repository, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task<IEnumerable<NoteCategoryDTO>> GetAllAsync()
        {
            var items = await _repository.GetItemsAsync();
            return items.ToDTOList();
        }

        public async Task<NoteCategoryDTO> GetAsync(string id)
        {
            var item = await _repository.GetItemAsync(id);
            if (item == null)
            {
                throw new NotFoundException("Category not found.");
            }
            return item.ToDTO();
        }

        public async Task<NoteCategoryDTO> CreateAsync(CreateNoteCategoryDTO dto)
        {
            var item = EntityHelper<NoteCategory>.Create();

            item.Name = dto.Name;
            item.Description = dto.Description;
            item.Color = dto.Color;
            item.CreatedAt = DateTime.Now;
            item.CreatedBy = _currentUserService.GetName();

            var createdItem = await _repository.CreateItemAsync(item);
            return createdItem.ToDTO();
        }

        public async Task<NoteCategoryDTO> UpdateAsync(string id, UpdateNoteCategoryDTO dto)
        {
            var item = await _repository.GetItemAsync(id);
            if (item == null)
            {
                throw new NotFoundException("Category not found.");
            }

            item.Name = dto.Name;
            item.Description = dto.Description;
            item.Color = dto.Color;
            item.UpdatedAt = DateTime.Now;
            item.UpdatedBy = _currentUserService.GetName();

            var updatedItem = await _repository.UpdateItemAsync(id, item);
            return updatedItem.ToDTO();
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var item = await _repository.GetItemAsync(id);
            if (item == null)
            {
                throw new NotFoundException("Category not found.");
            }

            return await _repository.DeleteItemAsync(id);
        }
    }
}
