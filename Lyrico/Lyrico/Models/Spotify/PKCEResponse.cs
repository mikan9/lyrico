using System;
using System.Collections.Generic;
using System.Text;

namespace Lyrico.Models.Spotify
{
    public class PKCEResponse
    {
        public string Response { get; set; }
        public BrowserResultType ResultType { get; set; }
        public string Error { get; set; }
    }
}
