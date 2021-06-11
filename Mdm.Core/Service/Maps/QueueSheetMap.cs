using Mapster;
using Mdm.Core.Models;
using Mdm.Core.Service.Contracts;

namespace Mdm.Core.Service.Maps
{
    public class QueueSheetMap : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<QueueSheetContract, QueueSheet>();
        }
    }
}
