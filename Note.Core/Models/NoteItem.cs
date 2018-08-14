using Newtonsoft.Json;

namespace Note.Core.Models
{
    public enum NoteItemStatus
    {
        Pending = 0,
        Done = 1
    }

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
