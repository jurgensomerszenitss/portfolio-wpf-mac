using Mdm.App.Infrastructure;
using Mdm.Core.Models;
using Mdm.Core.Operations;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;

namespace Mdm.App.ViewModels
{
    public class QueueSheetViewModel : BaseViewModel, IQueueSheetViewModel
    {
        public QueueSheetViewModel(IOperations operations, IStateManager stateManager)
        {
            _operations = operations;
            _stateManager = stateManager;
            SelectCommand = new RelayCommand<QueueSheet>(Select);
            ShowSettingsCommand = new RelayCommand(ShowSettings);
            UnlockCommand = new RelayCommand<QueueSheet>(Unlock);
            RefreshCommand = new RelayCommand(Load);
            SearchCommand = new RelayCommand(Search, CanSearch);

            _cleanupTimer = new Timer(1000*60*60);
            _cleanupTimer.Elapsed += OnCleanUpElapsed;
            _cleanupTimer.Start();
        }

      
        private readonly IOperations _operations;
        private readonly IStateManager _stateManager;
        private ObservableCollection<QueueSheet> _list;
        private QueueSheet _selectedItem;
        private string _searchText;
        private string _userName;
        private Timer _syncTimer;
        private readonly Timer _cleanupTimer;

        public ObservableCollection<QueueSheet> List
        {
            get => _list;
            set => SetProperty(ref _list, value);
        }

        public QueueSheet SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }
        public ICommand SelectCommand { get; }
        public ICommand ShowSettingsCommand { get; }
        public ICommand UnlockCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand SearchCommand { get; }

        public event EventHandler OnShowSettings;
        public event EventHandler OnShowLogin;
        public event EventHandler<QueueSheet> OnUnlock;

        public override async Task Initialize()
        {
            _stateManager.AuthenticatedChanged += OnAuthenticatedChanged;
            _stateManager.UserSettingsChanged += OnUserSettingsChanged;
            if (await _operations.Authenticate())
            {
                _stateManager.IsAuthenticated = true;
                _stateManager.UserCredentials = await _operations.Local.GetUserCredentialsAsync();
                _stateManager.UserSettings = await _operations.Local.GetUserSettingsAsync(_stateManager.UserCredentials.UserName);

                await _operations.Local.CleanupAsync();
            }
            else
            {
                OnShowLogin?.Invoke(this, EventArgs.Empty);
            }
            await base.Initialize();
        }

      
        private async Task Load()
        {
            if (_stateManager.IsAuthenticated)
            { 
                await Select(null);
                UserName = (await _operations.Remote.GetUserAsync()).Name;
                var selectedItem = _selectedItem;
                var list = await _operations.Remote.SearchQueueSheetsAsync(_searchText);
                List = new ObservableCollection<QueueSheet>(list);
                await Select(selectedItem);
            }  
        }

        private async Task Select(QueueSheet queueSheet)
        {
            this.SelectedItem = queueSheet;
            _stateManager.QueueSheet = queueSheet;
            await Task.Yield();
        }

        private async Task ShowSettings()
        {
            if (OnShowSettings != null)
                OnShowSettings.Invoke(this, EventArgs.Empty);
            await Task.Yield();
        }

        private async Task Unlock(QueueSheet queueSheet)
        {
            _stateManager.UnlockParam = queueSheet;
            if (OnUnlock != null)
                OnUnlock.Invoke(this, queueSheet);

            if (queueSheet.IsUnlocked)
            {
                await Select(queueSheet);
            } 
        }

        private async Task Search()
        {
            await Load();
        }

        private bool CanSearch()
        {
            //return !string.IsNullOrEmpty(SearchText);
            return true;
        }

        private async Task InitializeAutoSync()
        {
            await Task.Yield();
            var userSettings = _stateManager.UserSettings;
            if(_syncTimer != null)
            {
                _syncTimer.Stop();
                _syncTimer.Dispose();
            }

            if (userSettings.AutoSync && userSettings.SyncInterval > 0)
            {
                _syncTimer = new Timer(userSettings.SyncInterval * 60000);
                _syncTimer.Elapsed += Timer_Elapsed;
                _syncTimer.Start();
            }
        }

        private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            await Load();
        }

        private async void OnAuthenticatedChanged(object sender, bool e)
        {
            if (_stateManager.IsAuthenticated)
            {
                await Load();
            }
            else
            {
                //this.List = new ObservableCollection<QueueSheet>();
                //SelectedItem = null;
                OnShowLogin?.Invoke(this, EventArgs.Empty);
            }
        }

        private async void OnUserSettingsChanged(object sender, UserSettings e)
        {
            await InitializeAutoSync();
        }

        private async void OnCleanUpElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                await _operations.Local.CleanupAsync();
            }
            catch
            {
                
            }
        }


    }
}
 