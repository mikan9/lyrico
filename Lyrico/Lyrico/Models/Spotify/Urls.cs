using System;
using System.Collections.Generic;
using System.Text;

namespace Lyrico.Models.Spotify
{
    public static class Urls
    {
        public static readonly Uri Authorize = new Uri("https://accounts.spotify.com/authorize");
        public static readonly Uri TokenSwap = new Uri("https://192.168.1.90:5001/swap");
        public static readonly Uri TokenRefresh = new Uri("https://192.168.1.90:5001/refresh");
        public static readonly Uri CurrentlyPlaying = new Uri("https://api.spotify.com/v1/me/player/currently-playing");
    }
}
