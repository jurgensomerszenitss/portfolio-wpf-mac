using AppKit;
using Mdm.App;
using Mdm.Core;
using SimpleInjector;

namespace Mdm.MacOS
{
    static class MainClass
    {
        static void Main(string[] args)
        {
            Bootstrap();
            NSApplication.Init();
            NSApplication.Main(args);
      
        }

        internal static Container Container { get; private set; }

        private static void Bootstrap()
        {
            // Create the Container as usual.
            Container = new Container();

            // Register your types, for instance:
            Container.RegisterCore()
                     .ConfigureCore()
                     .RegisterApp();

            Core.Bootstrapper.VerifyCore();
        }
    }
}
