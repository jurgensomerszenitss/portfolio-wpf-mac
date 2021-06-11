using Mdm.App.Infrastructure;
using Mdm.Core.Models;
using Mdm.Core.Operations;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mdm.App.ViewModels
{
    public class UserSettingsViewModel : BaseViewModel, IUserSettingsViewModel
    {
        public UserSettingsViewModel(IOperations operations, IStateManager stateManager)
        {
            _operations = operations;
            _stateManager = stateManager;
            _userSettings = new UserSettings();

            SaveCommand = new RelayCommand(Save, CanSave);
            CancelCommand = new RelayCommand(Cancel);
            SelectFolderCommand = new RelayCommand(SelectFolder);
            LogoutCommand = new RelayCommand(OnLogout);
            SetAutoSyncCommand = new RelayCommand<string>(OnSetAutoSync);
        }

        private readonly IOperations _operations;
        private readonly IStateManager _stateManager;

        private string _userName;  
        private int _syncInterval;
        private string _fileLocation;
        private DateTime? _lastLogin;
        private string _businessName;
        private DateTime? _contractDate;
        private string _classification;
        private UserSettings _userSettings;

        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        } 
        

        public string FileLocation
        {
            get => _fileLocation;
            set => SetProperty(ref _fileLocation, value);
        }

        public int SyncInterval
        {
            get => _syncInterval;
            set =>  SetProperty(ref _syncInterval, value);
        }
       

        public DateTime? LastLogin
        {
            get => _lastLogin;
            private set => SetProperty(ref _lastLogin, value);
        }

        public string BusinessName
        {
            get => _businessName;
            private set => SetProperty(ref _businessName, value);
        }
        public DateTime? ContractDate
        {
            get => _contractDate;
            private set => SetProperty(ref _contractDate, value);
        }
        public  string Classification
        {
            get => _classification;
            private set => SetProperty(ref _classification, value);
        }


        public override bool IsValid
        {
            get
            {
                return !string.IsNullOrWhiteSpace(FileLocation);
            }
        }

        public ICommand LogoutCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public ICommand SelectFolderCommand { get; private set; }
        public ICommand SetAutoSyncCommand { get; private set; }

        public event EventHandler OnCancel;
        public event EventHandler OnSave;
        public event EventHandler OnSelectFolder;


        public override async Task Initialize()
        {
            var user = await _operations.Remote.GetUserAsync();

            _userSettings = _stateManager.UserSettings;
            _userSettings.UserName = user.UserName;
            UserName = user.Name;
            FileLocation = _userSettings.FileLocation;
            SyncInterval = _userSettings.SyncInterval;
            LastLogin = user.LastLogin;
            BusinessName = user.CompanyName;
            ContractDate = user.ContractDate;
            Classification = user.Classification;
        }

        private async Task OnLogout()
        {
            await _operations.Local.SetUserCredentialsAsync(null);
            await Cancel();
            _stateManager.IsAuthenticated = false;            
        }

        private async Task Save()
        {
            _userSettings.Name = _userName; 
            _userSettings.FileLocation = _fileLocation;
            _userSettings.AutoSync = _syncInterval > 0;
            _userSettings.SyncInterval = _syncInterval;
            await _operations.Local.UpdateUserSettings(_userSettings);
            _stateManager.UserSettings = _userSettings;

            if (this.OnSave != null)
                OnSave(this, EventArgs.Empty);
        }

        private bool CanSave()
        {
            return IsValid;
        }

        private async Task Cancel()
        {
            OnCancel?.Invoke(this, EventArgs.Empty);
            await Task.Yield();
        }

        private async Task SelectFolder()
        {
            OnSelectFolder?.Invoke(this, EventArgs.Empty);
            await Task.Yield();
        }

        private async Task  OnSetAutoSync(string interval)
        {
            SyncInterval = int.Parse(interval);
            await Task.Yield();
        }

        protected override string GetPropertyError(string propertyName)
        {
            if (propertyName == nameof(FileLocation) && string.IsNullOrWhiteSpace(FileLocation)) return $"File location is mandatory";
            return base.GetPropertyError(propertyName);
        }

     
    }
}
