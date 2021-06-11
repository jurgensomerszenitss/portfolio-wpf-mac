using Mapster;
using Mdm.App.Infrastructure;
using Mdm.Core.Extensions;
using Mdm.Core.Models;
using Mdm.Core.Operations;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mdm.App.ViewModels
{
    public class TrackListViewModel : BaseViewModel, ITrackListViewModel
    {
        public TrackListViewModel(IOperations operations, IStateManager stateManager)
        {
            _operations = operations;
            _stateManager = stateManager;

            ToggleIconViewCommand = new RelayCommand(OnToggleIconView);
            ToggleListViewCommand = new RelayCommand(OnToggleListView); 
            DownloadCommand = new RelayCommand(Download, CanDownload);
            GoToFolderCommand = new RelayCommand<string>(GoToFolder);
            OrderCommand = new RelayCommand(ToggleOrder);
            SearchCommand = new RelayCommand(Search);

            _selectedTracks = new ObservableCollection<Track>();
        }

        private readonly IOperations _operations;
        private readonly IStateManager _stateManager;
        private ObservableCollection<QueueSheetItem> _children;
        private ListDisplayMode _listDisplayMode;
        private ObservableCollection<Track> _selectedTracks;
        private QueueSheet _selectedQueueSheet;
        private bool _isLoading;
        private string _queuesheetName;
        private ObservableCollection<string> _path;
        private SortDirection _sortDirection;
        private string _searchText;

        public ObservableCollection<QueueSheetItem> Children
        {
            get => _children;
            set => SetProperty(ref _children, value);
        }

        public ObservableCollection<Track> SelectedTracks
        {
            get => _selectedTracks;
            set => SetProperty(ref _selectedTracks, value);
        }

        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        public ListDisplayMode DisplayMode
        {
            get => _listDisplayMode;
            private set => SetProperty(ref _listDisplayMode, value);
        }

        public bool IsLoading
        {
            get => _isLoading;
            private set => SetProperty(ref _isLoading, value);
        }

        public string QueueSheetName
        {
            get => _queuesheetName;
            private set => SetProperty(ref _queuesheetName, value);
        }

        public ObservableCollection<string> Path
        {
            get => _path;
            private set => SetProperty(ref _path, value);
        }

        public string FullPath
        {
            get => string.IsNullOrWhiteSpace(QueueSheetName) ? "" : QueueSheetName + (_path.Any() ? " / " + String.Join(" / ",_path) : "");
        }
      
        public ICommand ToggleIconViewCommand { get; }
        public ICommand ToggleListViewCommand { get; }
        public ICommand SelectAllCommand { get; }
        public ICommand DownloadCommand { get; }
        public ICommand OrderCommand { get; }
        public ICommand GoToFolderCommand { get; }
        public ICommand SearchCommand { get; }

        public event EventHandler<string> OnDownloadSuccess;
        public event EventHandler<string> OnDownloadFailed;

        public override async Task Initialize()
        { 
            _stateManager.QueueSheetChanged += OnQueueSheetChanged; 
            SelectedTracks = new ObservableCollection<Track>();
            Path = new ObservableCollection<string>();
            await Task.Yield();
        }
         

        private async void OnQueueSheetChanged(object sender, QueueSheet e)
        {
            _selectedQueueSheet = e;
            QueueSheetName = e?.Name;
            Path = new ObservableCollection<string>();
            RaisePropertyChanged(nameof(FullPath));
            await Load();
        }

        private async Task OnToggleIconView()
        {
            DisplayMode = ListDisplayMode.Icons;
            await Task.Yield();
        }

        private async Task OnToggleListView()
        {
            DisplayMode = ListDisplayMode.List;
            await Task.Yield();
        }

        public async Task Load()
        {
            if(SelectedTracks != null) SelectedTracks.CollectionChanged -= (s, eArgs) => CommandManager.RefreshCommandStates();
            SelectedTracks = new ObservableCollection<Track>();
            _selectedTracks.CollectionChanged += (s, eArgs) => CommandManager.RefreshCommandStates();

            var isNotLocked = _selectedQueueSheet != null && ((_selectedQueueSheet.IsProtected && _selectedQueueSheet.IsUnlocked) || !_selectedQueueSheet.IsProtected);
            if (_selectedQueueSheet != null && isNotLocked)
            {
                await LoadData();
            }
            else
            {
                Children = new ObservableCollection<QueueSheetItem>();
            }
        }
        
        private async Task LoadData()
        {
            try
            {
                RaisePropertyChanged(nameof(FullPath));
                RaisePropertyChanged(nameof(Path));

                IsLoading = true;
                SelectedTracks.Clear();
                var children = await _operations.Remote.SearchChildrenAsync(_selectedQueueSheet.Id, _sortDirection, _searchText, _path?.ToArray());
                Children = new ObservableCollection<QueueSheetItem>(children);
                
            }
            catch
            {

            }
            finally
            {
                IsLoading = false;
            }
        }
       
         
        private async Task Download()
        {
            try
            {
                var succeededDownloads = 0;
                foreach (var file in _selectedTracks)
                {
                    var result = await _operations.Remote.DownloadAsync(file, _stateManager.UserSettings);
                    if (result != null) succeededDownloads++;
                }
                OnDownloadSuccess?.Invoke(this, $"{succeededDownloads} files are downloaded");
            }
            catch(Exception)
            {
                OnDownloadFailed?.Invoke(this, "Download of files failed");
            }
        }

        private bool CanDownload()
        {
            return SelectedTracks != null && SelectedTracks.Any();
        }

      
        private async Task GoToFolder(string path)
        {
            if (_path == null)
                Path = new ObservableCollection<string>();

            if (path == "..." && _path.Count > 0)
            {
                _path.Remove(_path.Last());

            }
            else
            {
                _path.Add(path); 
            }
 
            await LoadData();
        }

        private async Task ToggleOrder()
        {
            _sortDirection = _sortDirection == SortDirection.Asc ? SortDirection.Desc : SortDirection.Asc;
            await LoadData();
        }

        private async Task Search()
        {
            await LoadData();
        }
    }
}
