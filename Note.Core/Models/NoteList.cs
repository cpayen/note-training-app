using Newtonsoft.Json;
using System.Collections.Generic;

namespace Note.Core.Models
{
    public enum NoteListStatus
    {
        Enabled = 1,
        Archived = 2
    }

    // TODO: Add public notes.
    //public enum NoteListVisibility
    //{
    //    Public = 1,
    //    Private = 2
    //}

    public class NoteList : Entity
    {
        [JsonProperty(PropertyName = "userId", Required = Required.Always)]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "order", Required = Required.Always)]
        public int Order { get; set; }

        [JsonProperty(PropertyName = "status", Required = Required.Always)]
        public NoteListStatus Status { get; set; }

        [JsonProperty(PropertyName = "items", NullValueHandling = NullValueHandling.Ignore)]
        public List<NoteItem> Items { get; set; }
    }
}
