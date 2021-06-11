using Mdm.Core.Models;
using System;
using System.Threading.Tasks;

namespace Mdm.App.Infrastructure
{
    public interface IStateManager
    {
        QueueSheet QueueSheet { get; set; }
        QueueSheet UnlockParam { get; set; }
        bool IsAuthenticated { get; set; }
        UserSettings UserSettings { get; set; }
        UserCredentials UserCredentials { get; set; }

        event EventHandler<QueueSheet> QueueSheetChanged;
        event EventHandler<bool> AuthenticatedChanged;
        event EventHandler<UserSettings> UserSettingsChanged;

    }
      

}
