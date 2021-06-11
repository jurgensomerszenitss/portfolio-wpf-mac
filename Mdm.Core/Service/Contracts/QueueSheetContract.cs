using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Mdm.Core.Service.Contracts
{
    public class QueueSheetContract
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("title")]
        public string Name { get; set; }

        [JsonProperty("passcode")]
        public string Password { get; set; }

        [JsonProperty("is_locked")]
        public bool IsProtected { get; set; }
    } 
}
