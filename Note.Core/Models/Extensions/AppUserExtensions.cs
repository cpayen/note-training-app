using Note.Core.Models.DTO.AppUser;
using System.Collections.Generic;

namespace Note.Core.Models.Extensions
{
    public static class AppUserExtensions
    {
        public static AppUserDTO ToDTO(this AppUser user)
        {
            return new AppUserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
                CreatedBy = user.CreatedBy,
                UpdatedAt = user.UpdatedAt,
                UpdatedBy = user.UpdatedBy
            };
        }

        public static List<AppUserDTO> ToDTOList(this IEnumerable<AppUser> list)
        {
            var dtoList = new List<AppUserDTO>();
            foreach (var item in list)
            {
                dtoList.Add(item.ToDTO());
            }
            return dtoList;
        }
    }
}
