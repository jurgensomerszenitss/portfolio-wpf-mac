using System.Threading.Tasks;

namespace Mdm.Core.Operations
{
    public interface IOperations
    {
        ILocalOperations Local { get; }
        IRemoteOperations Remote { get; }

        Task<bool> Authenticate(); 
    } 
}
