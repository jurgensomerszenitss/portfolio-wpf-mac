using Mapster;
using Mdm.Core.Models;
using Mdm.Core.Service.Contracts;

namespace Mdm.Core.Service.Maps
{
    public class TrackMap : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<TrackContract, Track>()
                .Map(d => d.QueueItemId, s => s.Id)
                .Ignore(d => d.Id);
        }
    }
}
