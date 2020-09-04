using Lyrico.Services.Interfaces;
using Prism;
using Prism.Ioc;

namespace Lyrico.iOS
{
    public class PlatformInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IWebAuthService, WebAuthService>();
        }
    }
}