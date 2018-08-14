using System.ComponentModel.DataAnnotations;

namespace Note.Core.Models.DTO.Note
{
    public class CreateNoteItemDTO
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
