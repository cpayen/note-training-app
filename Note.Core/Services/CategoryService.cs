﻿using AutoMapper;
using Note.Core.Exceptions;
using Note.Core.Helpers;
using Note.Core.Models;
using Note.Core.Models.DTO.Note;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Note.Core.Services
{
    public class CategoryService
    {
        protected readonly Repository<NoteCategory> _repository;
        protected readonly ICurrentUserService _currentUserService;
        protected readonly IMapper _mapper;

        public CategoryService(Repository<NoteCategory> repository, ICurrentUserService currentUserService, IMapper mapper)
        {
            _repository = repository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NoteCategoryDTO>> GetAllAsync()
        {
            var items = await _repository.GetItemsAsync();
            return _mapper.Map<List<NoteCategoryDTO>>(items);
        }

        public async Task<NoteCategoryDTO> GetAsync(string id)
        {
            var item = await _repository.GetItemAsync(id);
            if (item == null)
            {
                throw new NotFoundException("Category not found.");
            }
            return _mapper.Map<NoteCategoryDTO>(item);
        }

        public async Task<NoteCategoryDTO> CreateAsync(CreateNoteCategoryDTO dto)
        {
            var item = EntityHelper<NoteCategory>.Create();

            _mapper.Map(dto, item);
            item.CreatedAt = DateTime.Now;
            item.CreatedBy = _currentUserService.GetName();

            var createdItem = await _repository.CreateItemAsync(item);
            return _mapper.Map<NoteCategoryDTO>(createdItem);
        }

        public async Task<NoteCategoryDTO> UpdateAsync(string id, UpdateNoteCategoryDTO dto)
        {
            var item = await _repository.GetItemAsync(id);
            if (item == null)
            {
                throw new NotFoundException("Category not found.");
            }

            _mapper.Map(dto, item);
            item.UpdatedAt = DateTime.Now;
            item.UpdatedBy = _currentUserService.GetName();

            var updatedItem = await _repository.UpdateItemAsync(id, item);
            return _mapper.Map<NoteCategoryDTO>(updatedItem);
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