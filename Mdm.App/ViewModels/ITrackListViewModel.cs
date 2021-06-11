using Mdm.App.Infrastructure;
using Mdm.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mdm.App.ViewModels
{
    public interface ITrackListViewModel : IBaseViewModel
    {
        ObservableCollection<QueueSheetItem> Children { get; }
        ObservableCollection<Track> SelectedTracks { get; }

        string QueueSheetName { get; }
        ObservableCollection<string> Path { get; }
        string FullPath { get;  }
        bool IsLoading { get; }
        string SearchText { get; set; }
        ListDisplayMode DisplayMode { get; }
        ICommand ToggleIconViewCommand { get; }
        ICommand ToggleListViewCommand { get; } 
        ICommand DownloadCommand { get; }
        ICommand OrderCommand { get; }
        ICommand GoToFolderCommand { get; }
        ICommand SearchCommand { get; }

        event EventHandler<string> OnDownloadSuccess;
        event EventHandler<string> OnDownloadFailed; 

        Task Load();
    }
}
