﻿using Newtonsoft.Json;
using Note.Core.Enums;

namespace Note.Core.Entities
{
    public class AppUser : Entity
    {
        [JsonProperty(PropertyName = "name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "email", Required = Required.Always)]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "password", Required = Required.Always)]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "salt", Required = Required.Always)]
        public string Salt { get; set; }

        [JsonProperty(PropertyName = "role", Required = Required.Always)]
        public AppUserRole Role { get; set; }
    }
}
