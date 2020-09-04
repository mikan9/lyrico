using System;
using System.Collections.Generic;
using System.Text;

namespace Lyrico.Services.Interfaces
{
    public interface IPollingService
    {
        TimeSpan Span { get; set; }
        Action Callback { get; set; }
        void Start();
        void Stop();
    }
}
