using Lyrico.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lyrico.Events
{
    public class LyricsRetrievedEvent : PubSubEvent<Lyrics>
    {
    }
}
