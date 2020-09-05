using Lyrico.Models;
using Prism.Events;

namespace Lyrico.Events
{
    public class LyricsRetrievedEvent : PubSubEvent<Lyrics>
    {
    }
}
