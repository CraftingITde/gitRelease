using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Gitea.API.v1.Types
{
    public class Release
    {

        [DataMember]
        [JsonProperty("id")]
        public int Id { get; set; }
        [DataMember]
        [JsonProperty("body")]
        public string Body { get; set; }
        [DataMember]
        [JsonProperty("draft")]
        public bool Draft { get; set; }
        [DataMember]
        [JsonProperty("name")]
        public string Name { get; set; }
        [DataMember]
        [JsonProperty("prerelease")]
        public bool Prerelease { get; set; }
        [DataMember]
        [JsonProperty("tag_name")]
        public string Tag_name { get; set; }

    }
}
