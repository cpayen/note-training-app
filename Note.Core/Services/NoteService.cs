using Note.Core.Enums;
using Note.Core.Exceptions;
using Note.Core.Helpers;
using Note.Core.Models;
using Note.Core.Models.DTO.Note;
using Note.Core.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.Core.Services
{
    public class NoteService
    {
        protected readonly Repository<NoteList> _repository;
        protected readonly ICurrentUserService _currentUserService;

        public NoteService(Repository<NoteList> repository, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task<IEnumerable<NoteListDTO>> GetAllAsync()
        {
            var items = await _repository.GetItemsAsync();
            return items.ToDTOList();
        }

        public async Task<NoteListDTO> GetAsync(string id)
        {
            var item = await _repository.GetItemAsync(id);
            if (item == null)
            {
                throw new NotFoundException("Note list not found.");
            }
            return item.ToDTO();
        }

        public async Task<NoteListDTO> CreateAsync(CreateNoteListDTO dto)
        {
            var item = EntityHelper<NoteList>.Create();

            item.UserId = dto.UserId;
            item.Name = dto.Name;
            item.Description = dto.Description;
            item.Order = 0;
            item.Status = NoteListStatus.Enabled;
            item.CreatedAt = DateTime.Now;
            item.CreatedBy = _currentUserService.GetName();

            var createdItem = await _repository.CreateItemAsync(item);
            return createdItem.ToDTO();
        }
        
        public async Task<NoteListDTO> UpdateAsync(string id, UpdateNoteListDTO dto)
        {
            var item = await _repository.GetItemAsync(id);
            if (item == null)
            {
                throw new NotFoundException("Note list not found.");
            }

            item.Name = dto.Name;
            item.Description = dto.Description;
            item.Status = dto.Status;
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
                throw new NotFoundException("Note list not found.");
            }

            return await _repository.DeleteItemAsync(id);
        }

        public async Task<NoteListDTO> CreateItemAsync(string noteId, CreateNoteItemDTO itemDto)
        {
            var noteList = await _repository.GetItemAsync(noteId);
            if (noteList == null)
            {
                throw new NotFoundException("Note list not found.");
            }

            var noteItem = EntityHelper<NoteItem>.Create();

            noteItem.Name = itemDto.Name;
            noteItem.Description = itemDto.Description;
            noteItem.Order = 0;
            noteItem.Status = NoteItemStatus.Pending;
            noteItem.CreatedAt = DateTime.Now;
            noteItem.CreatedBy = _currentUserService.GetName();

            if (noteList.Items == null)
            {
                noteList.Items = new List<NoteItem>();
            }

            noteList.Items.Add(noteItem);

            var updatedList = await _repository.UpdateItemAsync(noteId, noteList);
            return updatedList.ToDTO();
        }

        public async Task<NoteListDTO> UpdateItemAsync(string noteId, string itemId, UpdateNoteItemDTO itemDto)
        {
            var noteList = await _repository.GetItemAsync(noteId);
            if (noteList == null)
            {
                throw new NotFoundException("Note list not found.");
            }

            var noteItem = noteList.Items.Where(o => o.Id == itemId).FirstOrDefault();
            if (noteItem == null)
            {
                throw new NotFoundException("Note item not found.");
            }

            noteItem.Name = itemDto.Name;
            noteItem.Description = itemDto.Description;
            noteItem.Status = itemDto.Status;
            noteItem.UpdatedAt = DateTime.Now;
            noteItem.UpdatedBy = _currentUserService.GetName();

            var updatedList = await _repository.UpdateItemAsync(noteId, noteList);
            return updatedList.ToDTO();
        }

        public async Task<NoteListDTO> DeleteItemAsync(string noteId, string itemId)
        {
            var noteList = await _repository.GetItemAsync(noteId);
            if (noteList == null)
            {
                throw new NotFoundException("Note list not found.");
            }

            var noteItem = noteList.Items.Where(o => o.Id == itemId).FirstOrDefault();
            if (noteItem == null)
            {
                throw new NotFoundException("Note item not found.");
            }

            noteList.Items.Remove(noteItem);

            var updatedList = await _repository.UpdateItemAsync(noteId, noteList);
            return updatedList.ToDTO();
        }
    }
}
