using Note.Core.Models.DTO.Note;
using System;
using System.Collections.Generic;
using System.Text;

namespace Note.Core.Models.Extensions
{
    public static class NoteCategoryExtensions
    {
        public static NoteCategoryDTO ToDTO(this NoteCategory category)
        {
            return new NoteCategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Color = category.Color,
                CreatedAt = category.CreatedAt,
                CreatedBy = category.CreatedBy,
                UpdatedAt = category.UpdatedAt,
                UpdatedBy = category.UpdatedBy
            };
        }

        public static List<NoteCategoryDTO> ToDTOList(this IEnumerable<NoteCategory> list)
        {
            var dtoList = new List<NoteCategoryDTO>();
            foreach (var item in list)
            {
                dtoList.Add(item.ToDTO());
            }
            return dtoList;
        }
    }
}
