using System;

namespace Lyrico.Services.Interfaces
{
    public interface ITokenPublisherService
    {
        event EventHandler<string> TokenReceived;
        void ReceiveToken(Uri uri);
    }
}
