using System;

namespace Mdm.Core.Models
{
    public class User
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public DateTime? ContractDate { get; set; } 
        public DateTime? LastLogin { get; set; } 
        public string Classification { get; set; }

    }
}
