using System.ComponentModel.DataAnnotations;

namespace Note.Core.Enums
{
    public enum AppUserRole
    {
        [Display(Name = "User")]
        User = 0,
        [Display(Name = "Administrator")]
        Admin = 1
    }

    //public enum DashboardVisibility
    //{
    //    Public = 0,
    //    Private = 1
    //}

    public enum NoteListStatus
    {
        [Display(Name = "Enabled")]
        Enabled = 0,
        [Display(Name = "Archived")]
        Archived = 1
    }

    public enum NoteItemStatus
    {
        [Display(Name = "Pending")]
        Pending = 0,
        [Display(Name = "Done")]
        Done = 1
    }
}
