using System.Threading.Tasks;

namespace Mdm.Core.Operations
{
    public class Operations : IOperations
    {
        public Operations (ILocalOperations localOperations, IRemoteOperations remoteOperations)
        {
            Local = localOperations;
            Remote = remoteOperations;
        }

        /// <summary>
        /// gets the current operations
        /// </summary>
        public static IOperations Current => Bootstrapper.Container.GetInstance<IOperations>();

        public ILocalOperations Local { get; }

        public IRemoteOperations Remote { get; }

        public async Task<bool> Authenticate()
        {
            var userCredentials  = await Local.GetUserCredentialsAsync();
            if (userCredentials == null) return false;
            return await Remote.Authenticate(userCredentials);
        }
    }
}
