using Lyrico.Events;
using Lyrico.Models;
using Lyrico.Views;
using Xamarin.Essentials;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using System.Threading.Tasks;
using Xamarin.Forms;
using Prism.Common;
using System.Diagnostics;
using System;
using Lyrico.Services.Interfaces;
using Lyrico.Utils;
using Lyrico.Utils.Parsers;
using Lyrico.Models.Spotify;
using Lyrico.Extensions;

namespace Lyrico.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        const int POLLING_SPAN = 1;
        IEventAggregator _eventAggregator { get; }
        INavigationService _navigationService { get; }
        ISpotifyService _spotifyService { get; }
        IPollingService _pollingService { get; }

        bool isAddingLyrics = false;
        public MainPageViewModel(IEventAggregator ea, INavigationService ns, ISpotifyService ss, IPollingService ps)
        {
            _eventAggregator = ea;
            _navigationService = ns;
            _spotifyService = ss;
            _pollingService = ps;
            _pollingService.Span = TimeSpan.FromSeconds(POLLING_SPAN);
            _pollingService.Callback = GetSong;
           
            if (Preferences.Get("first_run", true) || !Preferences.Get("has_auth", false))
            {
                NavigateToSpotifyLoginPage();

                Preferences.Set("first_run", false);
            }
            else
            {
                _pollingService.Start();
            }
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if((string)parameters["auth"] == "success")
                _pollingService.Start();
            else if((string)parameters["auth"] == "fail")
                _eventAggregator.GetEvent<ExceptionThrownEvent>().Publish("Authentication to Spotify failed.");
        }

        private void NavigateToSpotifyLoginPage()
        {
            _navigationService.NavigateAsync("SpotifyLoginPage");
        }

        public async void GetSong()
        {
            CurrentlyPlaying currentlyPlaying = await _spotifyService.GetCurrentlyPlaying();
            if (currentlyPlaying == null)
            {
                _eventAggregator.GetEvent<ExceptionThrownEvent>().Publish("Communication with the server failed.");
                return;
            }

            Item item = currentlyPlaying.Item;

            _eventAggregator.GetEvent<CurrentlyPlayingUpdatedEvent>().Publish(currentlyPlaying);
            await GetLyrics(string.Join(", ", item.ArtistNames), item.Name);
        }

        async Task GetLyrics(string artist, string title)
         {
            bool success = true;
            var lyrics = await App.Database.GetLyricsAsync(artist.ToLower(), title.ToLower());
            if (lyrics == null)
            {
                success = await AddLyrics(artist, title);
                if (success)
                    lyrics = await App.Database.GetLyricsAsync(artist.ToLower(), title.ToLower());
            }
            if(success)
                _eventAggregator.GetEvent<LyricsRetrievedEvent>().Publish(lyrics);
            else
                _eventAggregator.GetEvent<ExceptionThrownEvent>().Publish("Lyrics could not be found.");
        }

        async Task<bool> AddLyrics(string artist, string title)
        {
            if (!isAddingLyrics)
            {
                isAddingLyrics = true;
                string content = await new MetrolyricsParser().ParseHtml(artist, title);

                if (string.IsNullOrWhiteSpace(content))
                {
                    isAddingLyrics = false;
                    return false;
                }

                await App.Database.SaveLyricsAsync(new Lyrics()
                {
                    Artist = artist.ToLower(),
                    Title = title.ToLower(),
                    Content = content
                });
                isAddingLyrics = false;

                return true;
            }
            return false;
        }
    }
}
