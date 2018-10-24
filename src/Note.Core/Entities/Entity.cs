using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Note.Core.Entities
{
    public class Entity
    {
        [Required]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [Required]
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [Required]
        [JsonProperty(PropertyName = "createdAt")]
        public DateTime CreatedAt { get; set; }

        [Required]
        [JsonProperty(PropertyName = "createdBy")]
        public string CreatedBy { get; set; }
        
        [JsonProperty(PropertyName = "updatedAt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? UpdatedAt { get; set; }
        
        [JsonProperty(PropertyName = "updatedBy", NullValueHandling = NullValueHandling.Ignore)]
        public string UpdatedBy { get; set; }
    }
}
