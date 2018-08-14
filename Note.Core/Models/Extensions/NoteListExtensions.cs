using Note.Core.Models.DTO.Note;
using System.Collections.Generic;

namespace Note.Core.Models.Extensions
{
    public static class NoteListExtensions
    {
        public static NoteListDTO ToDTO(this NoteList noteList)
        {
            var dto = new NoteListDTO
            {
                Id = noteList.Id,
                Name = noteList.Name,
                Description = noteList.Description,
                Status = noteList.Status,
                CreatedAt = noteList.CreatedAt,
                CreatedBy = noteList.CreatedBy,
                UpdatedAt = noteList.UpdatedAt,
                UpdatedBy = noteList.UpdatedBy
            };

            if(noteList.Items != null)
            {
                dto.Items = new List<NoteItemDTO>();
                foreach (var noteItem in noteList.Items)
                {
                    dto.Items.Add(new NoteItemDTO
                    {
                        Id = noteItem.Id,
                        Name = noteItem.Name,
                        Description = noteItem.Description,
                        Status = noteItem.Status,
                        CreatedAt = noteItem.CreatedAt,
                        CreatedBy = noteItem.CreatedBy,
                        UpdatedAt = noteItem.UpdatedAt,
                        UpdatedBy = noteItem.UpdatedBy
                    });
                }
            }
            
            return dto;
        }

        public static List<NoteListDTO> ToDTOList(this IEnumerable<NoteList> list)
        {
            var dtoList = new List<NoteListDTO>();
            foreach (var item in list)
            {
                dtoList.Add(item.ToDTO());
            }
            return dtoList;
        }
    }
}
