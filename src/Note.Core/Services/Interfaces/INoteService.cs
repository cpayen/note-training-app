using System.Collections.Generic;
using System.Threading.Tasks;
using Note.Core.Models.DTO.Note;

namespace Note.Core.Services
{
    public interface INoteService
    {
        Task<NoteListDTO> CreateAsync(CreateNoteListDTO dto);
        Task<NoteListDTO> CreateItemAsync(string noteId, CreateNoteItemDTO dto);
        Task<bool> DeleteAsync(string id);
        Task<NoteListDTO> DeleteItemAsync(string noteId, string itemId);
        Task<IEnumerable<NoteListDTO>> GetAllAsync();
        Task<NoteListDTO> GetAsync(string id);
        Task<NoteListDTO> UpdateAsync(string id, UpdateNoteListDTO dto);
        Task<NoteListDTO> UpdateItemAsync(string noteId, string itemId, UpdateNoteItemDTO dto);
    }
}