using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Ioc;
using Lyrico.Navigation;
using Lyrico.Repository;
using Lyrico.ViewModels;
using Lyrico.Views;
using Lyrico.Services;
using Lyrico.Services.Interfaces;
using Prism;

namespace Lyrico
{
    public partial class App
    {
        static LyricsDatabase database;
        public App(IPlatformInitializer platformInitializer)
            :base(platformInitializer)
        {
            InitializeComponent();
        }

        public static LyricsDatabase Database
        {
            get
            {
                if(database == null)
                {
                    database = new LyricsDatabase();
                }
                return database;
            }
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            var result = await NavigationService.NavigateAsync(Navigate.Start);

            if (!result.Success)
            {
                Console.WriteLine(result.Exception.ToString());
                System.Diagnostics.Debugger.Break();
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<SpotifyLoginPage>();
            containerRegistry.Register<IRestService, RestService>();
            containerRegistry.Register<ISpotifyService, SpotifyService>();
            containerRegistry.Register<IPollingService, PollingService>();
        }
    }
}
