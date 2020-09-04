using Lyrico.Models.Spotify;
using Newtonsoft.Json;

namespace Lyrico.Models
{
    public class CurrentlyPlaying
    {
        [JsonProperty("context")]
        public Context Context { get; set; }
        [JsonProperty("currently_playing_type")]
        public string CurrentlyPlayingType { get; set; }
        [JsonProperty("is_playing")]
        public bool IsPlaying { get; set; }
        [JsonProperty("item")]
        public Item Item { get; set; }
        [JsonProperty("progress_ms")]
        public int? ProgressMs { get; set; }
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }
    }
}
