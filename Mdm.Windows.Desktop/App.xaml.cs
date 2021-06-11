using Mdm.App;
using Mdm.App.ViewModels;
using Mdm.Core;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Mdm.Windows.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Bootstrap();
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

            // Register your windows and view models:
            Container.RegisterSingleton<MainWindow>();

            //Container.Verify();
            Core.Bootstrapper.VerifyCore();

            var mainWindow = Container.GetInstance<MainWindow>();
            mainWindow.Show();
        }

    }
}
