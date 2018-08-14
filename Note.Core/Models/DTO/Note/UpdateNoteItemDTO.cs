using System.ComponentModel.DataAnnotations;

namespace Note.Core.Models.DTO.Note
{
    public class UpdateNoteItemDTO
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public NoteItemStatus Status { get; set; }
    }
}
