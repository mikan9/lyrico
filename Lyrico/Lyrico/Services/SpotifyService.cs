using Lyrico.Models;
using Lyrico.Models.Spotify;
using Lyrico.Services.Interfaces;
using Lyrico.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Lyrico.Services
{
    public class SpotifyService : ISpotifyService
    {
        private IRestService _restService { get; }

        public SpotifyService(IRestService rs)
        {
            _restService = rs;
        }

        public async Task<SwapResponse> RequestToken(string code)
        {
            string result = await _restService.Post(Urls.TokenSwap, "code", code);

            return JsonConvert.DeserializeObject<SwapResponse>(result);
        }

        public async Task RefreshToken()
        {
            string refresh_token = await SecureStorage.GetAsync("refresh_token");
            string result = await _restService.Post(Urls.TokenRefresh, "refresh_token", refresh_token);
            TokenResponse res = JsonConvert.DeserializeObject<TokenResponse>(result);
            TokenUtil.SetAccessToken(res);
        }

        public async Task<CurrentlyPlaying> GetCurrentlyPlaying()
        {
            if (TokenUtil.IsExpired)
                await RefreshToken();

            string accessToken = await TokenUtil.GetAccessToken();
                
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Accept", "application/json" },
                { "Authorization",  $"Bearer {accessToken}" }
            };

            string result = await _restService.Get(Urls.CurrentlyPlaying, headers, null, null);

            if (string.IsNullOrEmpty(result))
                return null;
            
            return JsonConvert.DeserializeObject<CurrentlyPlaying>(result);
        }
    }
}
