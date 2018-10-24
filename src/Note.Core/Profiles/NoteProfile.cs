using AutoMapper;
using Note.Core.Entities;
using Note.Core.Entities.DTO.AppUser;
using Note.Core.Entities.DTO.Note;

namespace Note.Core.Profiles
{
    public class AppUserProfile : Profile
    {
        public AppUserProfile()
        {
            CreateMap<AppUser, AppUserDTO>();
            CreateMap<CreateAppUserDTO, AppUser>();
            CreateMap<UpdateAppUserDTO, AppUser>();
        }
    }

    public class NoteListProfile : Profile
    {
        public NoteListProfile()
        {
            CreateMap<NoteList, NoteListDTO>();
            CreateMap<CreateNoteListDTO, NoteList>();
            CreateMap<UpdateNoteListDTO, NoteList>();
        }
    }

    public class NoteItemProfile : Profile
    {
        public NoteItemProfile()
        {
            CreateMap<NoteItem, NoteItemDTO>();
            CreateMap<CreateNoteItemDTO, NoteItem>();
            CreateMap<UpdateNoteItemDTO, NoteItem>();
        }
    }

    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<NoteCategory, NoteCategoryDTO>();
            CreateMap<NoteCategoryDTO, NoteCategory>();
            CreateMap<CreateNoteCategoryDTO, NoteCategory>();
            CreateMap<UpdateNoteCategoryDTO, NoteCategory>();
        }
    }
}
