using Lyrico.Events;
using Lyrico.Models;
using Lyrico.Views;
using Prism.Events;

namespace Lyrico.ViewModels
{
    public class LyricsViewModel : BindableBase
    {
        private string _artist = "";
        private string _title = "";
        private string _lyricsContent = "";

        public LyricsViewModel(IEventAggregator ea)
        {
            ea.GetEvent<LyricsRetrievedEvent>().Subscribe(LyricsRetrieved);
            ea.GetEvent<ExceptionThrownEvent>().Subscribe(ExceptionThrown);
        }

        public string LyricsContent
        {
            get => _lyricsContent;
            set => SetProperty(ref _lyricsContent, value); 
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

        public void LyricsRetrieved(Lyrics lyrics)
        {
            Artist = lyrics.Artist;
            Title = lyrics.Title;
            LyricsContent = lyrics.Content;
        }

        public void ExceptionThrown(string message)
        {
            LyricsContent = message;
        }
    }
}
