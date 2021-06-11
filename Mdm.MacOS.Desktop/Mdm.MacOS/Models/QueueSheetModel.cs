using System;
using Foundation;

namespace Mdm.MacOS.Models
{
    [Register("QueueSheetModel")]
    public partial class QueueSheetModel : NSObject
    {
        public QueueSheetModel()
        {
        }

        private string _id;
        private string _name;
        private string _password;
        private bool _isProtected;
        private bool _isUnlocked;

        [Export("Id")]
        public string Id
        {
            get => _id;
            set
            {
                WillChangeValue("Id");
                _id = value;
                DidChangeValue("Id");
            }
        }

        [Export("Name")]
        public string Name
        {
            get => _name;
            set
            {
                WillChangeValue("Name");
                _name = value;
                DidChangeValue("Name");
            }
        }

        [Export("Password")]
        public string Password
        {
            get => _password;
            set
            {
                WillChangeValue("Password");
                _password = value;
                DidChangeValue("Password");
            }
        }

        [Export("IsProtected")]
        public bool IsProtected
        {
            get => _isProtected;
            set
            {
                WillChangeValue("IsProtected");
                _isProtected = value;
                DidChangeValue("IsProtected");
            }
        }

        [Export("IsUnlocked")]
        public bool IsUnlocked
        {
            get => _isUnlocked;
            set
            {
                WillChangeValue("IsUnlocked");
                _isUnlocked = value;
                DidChangeValue("IsUnlocked");
            }
        } 
    }
}
