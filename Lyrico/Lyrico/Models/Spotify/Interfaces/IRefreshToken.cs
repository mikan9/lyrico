using System;
using System.Collections.Generic;
using System.Text;

namespace Lyrico.Models.Spotify.Interfaces
{
    public interface IRefreshToken : IToken
    {
        public string RefreshToken { get; set; }

    }
}
