using Lyrico.Events;
using Lyrico.Models;
using Lyrico.Models.Spotify;
using Lyrico.Views;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Lyrico.ViewModels
{
    public class SongViewModel : BindableBase
    {
        private string _artist = "";
        private string _title = "";
        private Uri _albumCover;

        IEventAggregator _eventAggregator { get; }
        public SongViewModel(IEventAggregator ea)
        {
            _eventAggregator = ea;
            _eventAggregator.GetEvent<CurrentlyPlayingUpdatedEvent>().Subscribe(CurrentlyPlayingUpdated);
        }

        public string Artist
        {
            get => _artist;
            set => SetProperty(ref _artist, value);
        }
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public Uri AlbumCover
        {
            get => _albumCover;
            set => SetProperty(ref _albumCover, value);
        }

        private void CurrentlyPlayingUpdated(CurrentlyPlaying currentlyPlaying)
        {
            Artist = string.Join(", ", string.Join(", ", currentlyPlaying.Item.ArtistNames));
            Title = currentlyPlaying.Item.Name;
            AlbumCover = new Uri(currentlyPlaying.Item.Album.Images[1].Url);
        }
    }
}
