using Newtonsoft.Json;

namespace Mdm.Core.Service.Contracts
{
    public abstract class QueueSheetItemContract
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
