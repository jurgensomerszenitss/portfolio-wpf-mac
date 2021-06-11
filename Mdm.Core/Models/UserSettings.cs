namespace Mdm.Core.Models
{
    public class UserSettings : UserCredentials
    {
        public string Id { get; set; }
       
        public string FileLocation { get; set; }
        public bool AutoSync { get; set; }
        public int SyncInterval { get; set; }

        public override bool IsValid => !string.IsNullOrEmpty(FileLocation);
    }
}
