using System.ComponentModel.DataAnnotations;

namespace Note.Core.Entities.DTO.Note
{
    public class CreateNoteListDTO
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
