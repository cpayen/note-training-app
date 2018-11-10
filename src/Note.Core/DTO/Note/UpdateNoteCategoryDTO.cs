using System.ComponentModel.DataAnnotations;

namespace Note.Core.DTO.Note
{
    public class UpdateNoteCategoryDTO
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [RegularExpression("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$", ErrorMessage = "Invalid color format")]
        public string Color { get; set; }
    }
}
