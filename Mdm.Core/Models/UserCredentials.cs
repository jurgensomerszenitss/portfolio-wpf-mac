namespace Mdm.Core.Models
{
    public class UserCredentials
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public virtual bool IsValid => !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(UserPassword);
    }
}
