using Newtonsoft.Json;
using Note.Core.Enums;

namespace Note.Core.Models
{
    public class NoteItem : Entity
    {
        [JsonProperty(PropertyName = "name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "order", Required = Required.Always)]
        public int Order { get; set; }

        [JsonProperty(PropertyName = "status", Required = Required.Always)]
        public NoteItemStatus Status { get; set; }
    }
}
