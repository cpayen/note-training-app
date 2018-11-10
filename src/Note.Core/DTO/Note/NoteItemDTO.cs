using Note.Core.Enums;
using System;
using System.Collections.Generic;

namespace Note.Core.DTO.Note
{
    public class NoteItemDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public NoteItemStatus Status { get; set; }
        public List<NoteCategoryDTO> Categories { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}
