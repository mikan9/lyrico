using System;

namespace Lyrico.Models.Spotify.Interfaces
{
    public interface IToken
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public string Scope { get; set; }
        public int ExpiresIn { get; set; }
    }
}
