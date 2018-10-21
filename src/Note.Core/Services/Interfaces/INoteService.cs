using System.Collections.Generic;
using System.Threading.Tasks;
using Note.Core.Models.DTO.Note;

namespace Note.Core.Services
{
    public interface INoteService
    {
        Task<NoteListDTO> CreateNotListAsync(CreateNoteListDTO dto);
        Task<NoteListDTO> CreateNoteItemAsync(string noteId, CreateNoteItemDTO dto);
        Task<bool> DeleteNoteListAsync(string id);
        Task<NoteListDTO> DeleteNoteItemAsync(string noteId, string itemId);
        Task<IEnumerable<NoteListDTO>> GetAllNoteListsAsync();
        Task<NoteListDTO> GetNoteListAsync(string id);
        Task<NoteListDTO> UpdateNoteListAsync(string id, UpdateNoteListDTO dto);
        Task<NoteListDTO> UpdateNoteItemAsync(string noteId, string itemId, UpdateNoteItemDTO dto);
    }
}