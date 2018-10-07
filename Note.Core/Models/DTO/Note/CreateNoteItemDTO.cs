using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Note.Core.Models.DTO.Note
{
    public class CreateNoteItemDTO
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public List<NoteCategoryDTO> Categories { get; set; }
    }
}
