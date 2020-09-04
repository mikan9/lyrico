using Lyrico.Models.Spotify;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Lyrico.Utils
{
    public static class TokenUtil
    {
        public async static Task<string> GetAccessToken()
        {
            return await SecureStorage.GetAsync("access_token");
        }
        public async static void SetAccessToken(SwapResponse token)
        {
            await SecureStorage.SetAsync("access_token", token.AccessToken);
            Preferences.Set("token_type", token.TokenType);
            Preferences.Set("token_scope", token.Scope);
            Preferences.Set("token_expires_in", token.ExpiresIn);
            Preferences.Set("token_created", DateTime.UtcNow);
            Preferences.Set("has_auth", true);

            SetRefreshToken(token.RefreshToken);
        }

        public async static void SetAccessToken(TokenResponse token)
        {
            await SecureStorage.SetAsync("access_token", token.AccessToken);
            Preferences.Set("token_type", token.TokenType);
            Preferences.Set("token_scope", token.Scope);
            Preferences.Set("token_expires_in", token.ExpiresIn);
            Preferences.Set("token_created", DateTime.UtcNow);
            Preferences.Set("has_auth", true);
        }

        public async static Task<string> GetRefreshToken()
        {
            return await SecureStorage.GetAsync("refresh_token");
        }
        public static async void SetRefreshToken(string token)
        {
            await SecureStorage.SetAsync("refresh_token", token);
        }

        public static bool IsExpired {
            get {
                DateTime created = Preferences.Get("token_created", DateTime.UtcNow);
                int expiresIn = Preferences.Get("token_expires_in", 0);
                return created.AddSeconds(expiresIn) <= DateTime.UtcNow;
            }
        }
    }
}
