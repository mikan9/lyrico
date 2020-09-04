using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.Content;
using Lyrico.Common;
using Lyrico.Services.Interfaces;
using Xamarin.Essentials;
using Lyrico.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(WebAuthService))]
namespace Lyrico.Droid
{

    [Activity(NoHistory = true, LaunchMode = LaunchMode.SingleTop)]
    [IntentFilter(new []
        { Intent.ActionView },
        Categories = new [] { Intent.CategoryDefault, Intent.CategoryBrowsable },
        DataScheme = Constants.RedirectScheme
    )]
    public class WebAuthService : WebAuthenticatorCallbackActivity, IWebAuthService
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