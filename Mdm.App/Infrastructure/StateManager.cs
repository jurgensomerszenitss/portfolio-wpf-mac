using Mdm.Core.Models;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Mdm.App.Infrastructure
{
   
    
    public class StateManager : IStateManager
    {
        private QueueSheet _queueSheet;
        public QueueSheet QueueSheet
        {
            get => _queueSheet;
            set
            {
                var isChanged = value != _queueSheet;
                _queueSheet = value;
                QueueSheetChanged?.Invoke(this, value);
            }
        }
        public QueueSheet UnlockParam { get; set; }

        private UserSettings _userSettings;
        public UserSettings UserSettings
        {
            get => _userSettings;
            set
            {
                var isChanged = value != _userSettings;
                _userSettings = value;
                UserSettingsChanged?.Invoke(this, value);
            }
        }
        public UserCredentials UserCredentials { get; set; }

        private bool _isAuthenticated; 
        public bool IsAuthenticated
        {

            get => _isAuthenticated;
            set
            {
                var isChanged = value != _isAuthenticated;
                _isAuthenticated = value;
                if(isChanged) AuthenticatedChanged?.Invoke(this, value);
            }
        }

        public event EventHandler<QueueSheet> QueueSheetChanged;
        public event EventHandler<bool> AuthenticatedChanged;
        public event EventHandler<UserSettings> UserSettingsChanged;
       

       
        
    }
}
