using Note.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Note.Core.DTO.Note
{
    public class CreateDashboardDTO
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public DashboardVisibility Visibility { get; set; }
    }
}
