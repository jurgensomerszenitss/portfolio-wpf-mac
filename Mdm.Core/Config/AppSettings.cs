using System;

namespace Mdm.Core.Config
{
    internal class AppSettings
    {
        public string ApiRoot { get; set; }
        public string Database { get; set; }
        public int Expires { get; set; }
        public string LocalFolder => $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/mdm/";
        public string ForgotPasswordUrl { get; set; }
    }
}
