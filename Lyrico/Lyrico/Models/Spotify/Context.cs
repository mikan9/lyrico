using Newtonsoft.Json;
using System.Collections.Generic;

namespace Lyrico.Models.Spotify
{
    public class Context
    {
        [JsonProperty("external_urls")]
        public Dictionary<string, string> ExternalUrls { get; set; }
        [JsonProperty("href")]
        public string Href { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}
