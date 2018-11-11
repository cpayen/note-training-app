using Note.Core.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Note.Core.DTO.Note
{
    public class UpdateDashboardDTO
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public DashboardVisibility Visibility { get; set; }

        public IEnumerable<string> ListIds { get; set; }
    }
}
