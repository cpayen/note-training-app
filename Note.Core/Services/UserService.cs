using Note.Core.Exceptions;
using Note.Core.Helpers;
using Note.Core.Models;
using Note.Core.Models.DTO.AppUser;
using Note.Core.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Note.Core.Services
{
    public class UserService
    {
        protected readonly Repository<AppUser> _repository;
        protected readonly ICurrentUserService _currentUserService;

        public UserService(Repository<AppUser> repository, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task<IEnumerable<AppUserDTO>> GetAllAsync()
        {
            var items = await _repository.GetItemsAsync();
            return items.ToDTOList();
        }

        public async Task<AppUserDTO> GetAsync(string id)
        {
            var item = await _repository.GetItemAsync(id);
            if (item == null)
            {
                throw new NotFoundException("User not found.");
            }
            return item.ToDTO();
        }

        public async Task<AppUserDTO> CreateAsync(CreateAppUserDTO dto)
        {
            var item = EntityHelper<AppUser>.Create();

            item.Name = dto.Name;
            item.Email = dto.Email;
            item.Role = dto.Role;
            item.CreatedAt = DateTime.Now;
            item.CreatedBy = _currentUserService.GetName();

            item.Salt = SecurityHelper.GetNewSalt();
            item.Password = SecurityHelper.EncryptPassword(dto.Password, item.Salt);

            var createdUser = await _repository.CreateItemAsync(item);
            return createdUser.ToDTO();
        }

        public async Task<AppUserDTO> UpdateAsync(string id, UpdateAppUserDTO dto)
        {
            var item = await _repository.GetItemAsync(id);
            if(item == null)
            {
                throw new NotFoundException("User not found.");
            }

            item.Name = dto.Name;
            item.Email = dto.Email;
            item.Role = dto.Role;
            item.UpdatedAt = DateTime.Now;
            item.UpdatedBy = _currentUserService.GetName();

            var updatedUser = await _repository.UpdateItemAsync(id, item);
            return updatedUser.ToDTO();
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var item = await _repository.GetItemAsync(id);
            if (item == null)
            {
                throw new NotFoundException("User not found.");
            }

            return await _repository.DeleteItemAsync(id);
        }
    }
}
