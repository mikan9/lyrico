﻿using Lyrico.Models;
using Prism.Events;

namespace Lyrico.Events
{
    public class CurrentlyPlayingUpdatedEvent : PubSubEvent<CurrentlyPlaying>
    {
    }
}
