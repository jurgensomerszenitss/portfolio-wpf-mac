using Newtonsoft.Json;
using System;

namespace Mdm.Core.Service.Contracts
{
    public class UserContract
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("username")]
        public string UserName { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("company_name")]
        public string CompanyName { get; set; }

        [JsonProperty("member_since")]
        public DateTime? ContractDate { get; set; }

        [JsonProperty("lastLogin")]
        public DateTime? LastLogin { get; set; }

        [JsonProperty("type")]
        public string Classification { get; set; }
    }
}
