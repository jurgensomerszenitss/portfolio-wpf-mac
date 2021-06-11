using Mdm.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Mdm.App.ViewModels
{
    public interface IQueueSheetViewModel : IBaseViewModel
    {
        ObservableCollection<QueueSheet> List { get; }
        QueueSheet SelectedItem { get; }
        ICommand SelectCommand { get; }
        ICommand ShowSettingsCommand { get; } 
        ICommand RefreshCommand { get; }
        ICommand SearchCommand { get; }
        ICommand UnlockCommand { get; }

        event EventHandler OnShowSettings;
        event EventHandler OnShowLogin;
        event EventHandler<QueueSheet> OnUnlock;

        string SearchText { get; set; }
        string UserName { get; set; }
    }
}
