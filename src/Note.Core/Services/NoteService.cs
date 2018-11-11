using AutoMapper;
using Note.Core.Data;
using Note.Core.Enums;
using Note.Core.Exceptions;
using Note.Core.Entities;
using Note.Core.DTO.Note;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.Core.Services
{
    public class NoteService : INoteService
    {
        protected readonly IRepository<NoteList> _repository;
        protected readonly ICurrentUserInfo _currentUserService;
        protected readonly IMapper _mapper;

        public NoteService(IRepository<NoteList> repository, ICurrentUserInfo currentUserService, IMapper mapper)
        {
            _repository = repository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NoteListDTO>> GetAllNoteListsAsync()
        {
            var items = await _repository.GetItemsAsync();
            return _mapper.Map<List<NoteListDTO>>(items);
        }

        public async Task<NoteListDTO> GetNoteListAsync(string id)
        {
            var item = await _repository.GetItemAsync(id);
            if (item == null)
            {
                throw new NotFoundException("Note list not found.");
            }
            return _mapper.Map<NoteListDTO>(item);
        }

        public async Task<NoteListDTO> CreateNotListAsync(CreateNoteListDTO dto)
        {
            var item = _mapper.Map<NoteList>(dto);
            item.Status = NoteListStatus.Enabled;

            var createdItem = await _repository.CreateItemAsync(item);
            return _mapper.Map<NoteListDTO>(createdItem);
        }
        
        public async Task<NoteListDTO> UpdateNoteListAsync(string id, UpdateNoteListDTO dto)
        {
            var item = await _repository.GetItemAsync(id);
            if (item == null)
            {
                throw new NotFoundException("Note list not found.");
            }

            _mapper.Map(dto, item);

            var updatedItem = await _repository.UpdateItemAsync(id, item);
            return _mapper.Map<NoteListDTO>(updatedItem);
        }

        public async Task<bool> DeleteNoteListAsync(string id)
        {
            var item = await _repository.GetItemAsync(id);
            if (item == null)
            {
                throw new NotFoundException("Note list not found.");
            }

            //TODO: Delete NoteItems

            return await _repository.DeleteItemAsync(id);
        }

        public async Task<NoteListDTO> CreateNoteItemAsync(string noteId, CreateNoteItemDTO dto)
        {
            var noteList = await _repository.GetItemAsync(noteId);
            if (noteList == null)
            {
                throw new NotFoundException("Note list not found.");
            }

            var item = _mapper.Map<NoteItem>(dto);
            item.Status = NoteItemStatus.Pending;

            if (noteList.Items == null)
            {
                noteList.Items = new List<NoteItem>();
            }

            noteList.Items.Add(item);

            var updatedList = await _repository.UpdateItemAsync(noteId, noteList);
            return _mapper.Map<NoteListDTO>(updatedList);
        }

        public async Task<NoteListDTO> UpdateNoteItemAsync(string noteId, string itemId, UpdateNoteItemDTO dto)
        {
            var noteList = await _repository.GetItemAsync(noteId);
            if (noteList == null)
            {
                throw new NotFoundException("Note list not found.");
            }

            var item = noteList.Items.Where(o => o.Id == itemId).FirstOrDefault();
            if (item == null)
            {
                throw new NotFoundException("Note item not found.");
            }

            _mapper.Map(dto, item);

            var updatedList = await _repository.UpdateItemAsync(noteId, noteList);
            return _mapper.Map<NoteListDTO>(updatedList);
        }

        public async Task<NoteListDTO> DeleteNoteItemAsync(string noteId, string itemId)
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
            return _mapper.Map<NoteListDTO>(updatedList);
        }
    }
}
