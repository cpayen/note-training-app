using System.ComponentModel.DataAnnotations;

namespace Note.Core.Entities.DTO.Note
{
    public class CreateNoteCategoryDTO
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [RegularExpression("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$", ErrorMessage = "Invalid color format")]
        public string Color { get; set; }
    }
}
