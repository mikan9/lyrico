using Lyrico.Models.Spotify;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Lyrico.Services.Interfaces
{
    public interface IWebAuthService
    {
        Task<WebAuthenticatorResult> LaunchUriAsync(string url);
    }
}
