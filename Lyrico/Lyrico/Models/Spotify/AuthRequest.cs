#nullable enable
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web;

namespace Lyrico.Models.Spotify
{
    public enum ResponseType
    {
        Code,
        Token
    }
    public class AuthRequest
    {
        public string RedirectUri { get; }
        public string ClientId { get; }
        public string? CodeChallengeMethod { get; set; }
        public string? CodeChallenge { get; set; }
        public string? State { get; set; }
        public ICollection<string>? Scope { get; set; }
        public ResponseType Type { get; }
        public AuthRequest(string redirectUri, string clientId, ResponseType responseType)
        {
            RedirectUri = redirectUri;
            ClientId = clientId;
            Type = responseType;
        }

        public Uri ToUri()
        {
            StringBuilder stringBuilder = new StringBuilder(Urls.Authorize.OriginalString);
            stringBuilder.Append($"?response_type={Type.ToString().ToLower(CultureInfo.InvariantCulture)}");
            stringBuilder.Append($"&client_id={ClientId}");
            stringBuilder.Append($"&redirect_uri={RedirectUri}");

            if(Scope != null) 
                stringBuilder.Append($"&scope={HttpUtility.UrlEncode(string.Join(" ", Scope))}");
            if (State != null)
                stringBuilder.Append($"&state={HttpUtility.UrlEncode(State)}");
            if (CodeChallenge != null)
                stringBuilder.Append($"&code_challenge={CodeChallenge}");
            if (CodeChallengeMethod != null)
                stringBuilder.Append($"&code_challenge_method={CodeChallengeMethod}");

            return new Uri(stringBuilder.ToString());
        }

    }
}
