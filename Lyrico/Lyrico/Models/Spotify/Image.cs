using Newtonsoft.Json;

namespace Lyrico.Models.Spotify
{
    public class Image
    {
        [JsonProperty("height")]
        public int Height { get; set; }
        [JsonProperty("width")]
        public int Width { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }

    }
}
