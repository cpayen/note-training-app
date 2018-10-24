using Newtonsoft.Json;
using Note.Core.Enums;
using System.Collections.Generic;

namespace Note.Core.Entities
{
    public class NoteItem : Entity
    {
        [JsonProperty(PropertyName = "name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "status", Required = Required.Always)]
        public NoteItemStatus Status { get; set; }

        [JsonProperty(PropertyName = "categories", NullValueHandling = NullValueHandling.Ignore)]
        public List<NoteCategory> Categories { get; set; }
    }
}
