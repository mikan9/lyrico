using Lyrico.Models;
using Lyrico.Models.Spotify;
using System.Threading.Tasks;

namespace Lyrico.Services.Interfaces
{
    public interface ISpotifyService
    {
        Task<SwapResponse> RequestToken(string code);
        Task<(CurrentlyPlaying, SpotifyService.StatusCode)> GetCurrentlyPlaying();

    }
}
