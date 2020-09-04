using System;
using System.Threading.Tasks;
using Lyrico.Common;
using Lyrico.iOS;
using Lyrico.Services.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(WebAuthService))]
namespace Lyrico.iOS
{
    public class WebAuthService : IWebAuthService
    {
        public async Task<WebAuthenticatorResult> LaunchUriAsync(string url)
        {
            var authResult = await WebAuthenticator.AuthenticateAsync(
                new Uri(url),
                new Uri(Constants.RedirectUri));

            return authResult;
        }
    }
}