using Note.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Note.Core.Entities.DTO.Note
{
    public class UpdateNoteListDTO
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public NoteListStatus Status { get; set; }
    }
}
