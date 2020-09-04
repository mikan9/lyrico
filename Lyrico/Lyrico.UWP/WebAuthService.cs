using System;
using Lyrico.Common;
using Lyrico.Services.Interfaces;
using Xamarin.Essentials;
using System.Threading.Tasks;
using Xamarin.Forms;
using Lyrico.UWP;

[assembly: Dependency(typeof(WebAuthService))]
namespace Lyrico.UWP
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
