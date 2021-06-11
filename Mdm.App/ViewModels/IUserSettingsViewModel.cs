using System;
using System.Windows.Input;

namespace Mdm.App.ViewModels
{
    public interface IUserSettingsViewModel : IBaseViewModel
    {
        string UserName { get; set; }  
        int SyncInterval { get; set; }
        string FileLocation { get; set; } 
        DateTime? LastLogin { get; }
        string BusinessName { get; }
        DateTime? ContractDate { get; }
        string Classification { get; }

        ICommand LogoutCommand { get; } 
        ICommand SaveCommand { get; }
        ICommand CancelCommand { get; }
        ICommand SelectFolderCommand { get; }

        event EventHandler OnCancel;
        event EventHandler OnSave;
        event EventHandler OnSelectFolder;
    }
}
