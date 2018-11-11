using Newtonsoft.Json;
using Note.Core.Enums;
using System.Collections.Generic;

namespace Note.Core.Entities
{
    public class Dashboard : Entity
    {
        [JsonProperty(PropertyName = "userId", Required = Required.Always)]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "visibility", Required = Required.Always)]
        public DashboardVisibility Visibility { get; set; }

        [JsonProperty(PropertyName = "listIds", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> ListIds { get; set; }
    }
}
