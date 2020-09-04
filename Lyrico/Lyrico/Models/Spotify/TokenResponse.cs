using Lyrico.Models.Spotify.Interfaces;
using Newtonsoft.Json;
using System;

namespace Lyrico.Models.Spotify
{
    public class TokenResponse : IToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("scope")]
        public string Scope { get; set; }
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
