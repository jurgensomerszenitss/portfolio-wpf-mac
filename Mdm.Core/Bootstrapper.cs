using Mapster;
using Mdm.Core.Config;
using Mdm.Core.Operations;
using Mdm.Core.Repository;
using Mdm.Core.Service;
using Mdm.Core.Service.Auth;
using Newtonsoft.Json;
using SimpleInjector;
using System.IO;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Mdm.Core.Tests")]

namespace Mdm.Core
{
    public static class Bootstrapper
    {
      
        static Bootstrapper()
        {
        }

        public static Container Container { get; private set; }
        private static bool _isConfigured;

        public static Container RegisterCore(this Container container)
        {
            container.RegisterSingleton<IOperations, Operations.Operations>();
            container.RegisterSingleton<IServiceAgent, ServiceAgent>();
            container.RegisterSingleton<IRepository, Repository.Repository>();
            container.RegisterSingleton<IFileRepository, FileRepository>();
            container.RegisterSingleton<ILocalOperations, LocalOperations>();
            container.RegisterSingleton<IRemoteOperations, RemoteOperations>();
            container.RegisterSingleton<IAuthenticator, Authenticator>();

            TypeAdapterConfig.GlobalSettings.Scan(typeof(Bootstrapper).Assembly); 

            if (Container == null) Container = container;
            return container;
        }
         

        public static Container ConfigureCore(this Container container)
        {
            if (!_isConfigured)
            {
                var assembly = typeof(Bootstrapper).Assembly;
                var resourceName = $"{assembly.GetName().Name}.settings.json";
                using (var stream = assembly.GetManifestResourceStream(resourceName))
                using (var reader = new StreamReader(stream))
                {
                    string jsonFile = reader.ReadToEnd();
                    var settings = JsonConvert.DeserializeObject<AppSettings>(jsonFile);
                    container.RegisterInstance(settings);
                }
                _isConfigured = true;
            }

            if (Container == null) Container = container;
            return container;
        }

        public static void VerifyCore()
        {
            var settings = Container.GetInstance<AppSettings>();
            if (!Directory.Exists(settings.LocalFolder))
            {
                Directory.CreateDirectory(settings.LocalFolder);
            }
        }
    }
}
