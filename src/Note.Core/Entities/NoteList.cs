using Newtonsoft.Json;
using Note.Core.Enums;
using System.Collections.Generic;

namespace Note.Core.Entities
{
    public class NoteList : Entity
    {
        [JsonProperty(PropertyName = "dashboardId", Required = Required.Always)]
        public string DashboardId { get; set; }

        [JsonProperty(PropertyName = "name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "status", Required = Required.Always)]
        public NoteListStatus Status { get; set; }

        [JsonProperty(PropertyName = "items", NullValueHandling = NullValueHandling.Ignore)]
        public List<NoteItem> Items { get; set; }
    }
}
