using Lyrico.Common;
using Lyrico.Helpers;
using Lyrico.Models;
using Lyrico.Models.Spotify;
using Lyrico.Services.Interfaces;
using Lyrico.Utils;
using Lyrico.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Security;
using System.Threading.Tasks;

namespace Lyrico.ViewModels
{
    public class SpotifyLoginPageViewModel : BindableBase, INavigatedAware, IDisposable
    {
        private INavigationService _navigationService { get; }
        private IWebAuthService _webAuthService { get; }
        private ISpotifyService _spotifyService { get; }
        public SpotifyLoginPageViewModel(INavigationService ns, IWebAuthService was, ISpotifyService ss)
        {
            _navigationService = ns;
            _webAuthService = was;
            _spotifyService = ss;

            OpenAuthenticationPageCommand = new DelegateCommand(async () => await OpenAuthenticationPage());
        }
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        public void Dispose()
        {
        }

        public DelegateCommand OpenAuthenticationPageCommand { get; }

        public async Task OpenAuthenticationPage()
        {
            AuthRequest request = new AuthRequest(Constants.RedirectUri, Secrets.SpotifyClientId, ResponseType.Code)
            {
                Scope = new List<string> { Scopes.UserReadCurrentlyPlaying, Scopes.UserReadPlaybackState },
                State = Guid.NewGuid().ToString("N"),
            };

            Uri uri = request.ToUri();

            var result = await _webAuthService.LaunchUriAsync(uri.OriginalString);

            Console.WriteLine("Request State: " + request.State + " | Result State: " + result.Properties["state"]);
            if (request.State != result.Properties["state"])
                throw new SecurityException("Antiforgery token validation failed");

            var swapResult = await _spotifyService.RequestToken(result.Properties["code"]);
            Console.WriteLine(swapResult.AccessToken);

            TokenUtil.SetAccessToken(swapResult);

            await _navigationService.NavigateAsync("MainPage?auth=success");
        }

    }
}
