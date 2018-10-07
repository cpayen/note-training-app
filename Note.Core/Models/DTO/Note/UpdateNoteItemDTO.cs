using Note.Core.Enums;
using System.Collections.Generic;
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
        
        public List<NoteCategoryDTO> Categories { get; set; }
    }
}
