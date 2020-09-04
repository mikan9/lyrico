using Lyrico.Events;
using Lyrico.Models;
using Lyrico.Views;
using Prism.Commands;
using Prism.Events;
using System;
using System.Diagnostics;

namespace Lyrico.ViewModels
{
    public class LyricsViewModel : BindableBase
    {
        private Lyrics _lyricsItem;
        private string _artist = "";
        private string _title = "";
        private string _lyricsContent = "";

        public LyricsViewModel(IEventAggregator ea)
        {
            ea.GetEvent<LyricsRetrievedEvent>().Subscribe(LyricsRetrieved);
            _lyricsItem = Lyrics.Empty();

        }

        public Lyrics LyricsItem
        {
            get => _lyricsItem;
            set
            {
                Lyrics lyrics = Lyrics.Empty(); ;
                if (value != null) lyrics = value;

                SetProperty(ref _lyricsItem, lyrics);
                Artist = lyrics.Artist;
                Title = lyrics.Title;
                LyricsContent = lyrics.Content;
            }
        }

        public string LyricsContent
        {
            get => LyricsItem.Content;
            set => SetProperty(ref _lyricsContent, value);
        }

        public string Artist
        {
            get => LyricsItem.Artist;
            set => SetProperty(ref _artist, value);
        }

        public string Title
        {
            get => LyricsItem.Title;
            set => SetProperty(ref _title, value);
        }

        public void LyricsRetrieved(Lyrics lyrics)
        {
            LyricsItem = lyrics;
        }
    }
}
