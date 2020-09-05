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

            HttpResponseMessage response = await _restService.Post(Urls.TokenSwap, "code", code);
            if (response == null || response.StatusCode == System.Net.HttpStatusCode.NoContent) return null;

            string result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<SwapResponse>(result);
        }

        public async Task RefreshToken()
        {
            string refresh_token = await SecureStorage.GetAsync("refresh_token");
            HttpResponseMessage response = await _restService.Post(Urls.TokenRefresh, "refresh_token", refresh_token);
            if (response == null || response.StatusCode == System.Net.HttpStatusCode.NoContent) return;

            string result = await response.Content.ReadAsStringAsync();
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

            HttpResponseMessage response = await _restService.Get(Urls.CurrentlyPlaying, headers, null, null);
            string result = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized || string.IsNullOrEmpty(result))
                return null;

            
            return JsonConvert.DeserializeObject<CurrentlyPlaying>(result);
        }
    }
}
