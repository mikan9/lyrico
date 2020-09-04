using Lyrico.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Lyrico.Services
{
    public class PollingService : IPollingService
    {
        public TimeSpan Span { get; set; }
        public Action Callback { get; set; }
        private CancellationTokenSource cancellation;
        public PollingService()
        {
            cancellation = new CancellationTokenSource();
        }
        public void Start()
        {
            CancellationTokenSource cts = cancellation;
            Device.StartTimer(Span, () =>
            {
                if (cts.IsCancellationRequested) return false;
                Callback.Invoke();
                return true;
            });
        }
        public void Stop()
        {
            Interlocked.Exchange(ref cancellation, new CancellationTokenSource()).Cancel();
        }
    }
}
