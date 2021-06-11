using Newtonsoft.Json;
using System;

namespace Mdm.Core.Service.Contracts
{
    public class TrackContract : QueueSheetItemContract
    {
       

        [JsonProperty("url")] 
        public string Source { get; set; }
        public string Path { get; set; }
        public string Artist { get; set; }
       
        public DateTime DownloadedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public byte[] Content { get; set; }
        public string QueueSheetId { get; set; }
        [JsonProperty("cover_url")]
        public string CoverUrl { get; set; }
    }
}
