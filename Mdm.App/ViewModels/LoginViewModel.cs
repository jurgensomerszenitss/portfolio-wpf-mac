using Mdm.App.Infrastructure;
using Mdm.Core.Models;
using Mdm.Core.Operations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mdm.App.ViewModels
{
    public class LoginViewModel : BaseViewModel, ILoginViewModel
    {
        public LoginViewModel(IOperations operations, IStateManager stateManager)
        {
            _operations = operations;
            _stateManager = stateManager; 
              
            LoginCommand = new RelayCommand(Login, OnCanLogin);
            ForgotCommand = new RelayCommand(Forgot);
        }

        private readonly IOperations _operations;
        private readonly IStateManager _stateManager;

        private string _userName;
        private string _password;
        private bool _remember; 
        private string _loginResult;
        private bool _isLoginOk; 

        public ICommand LoginCommand { get; private set; }  
        public ICommand ForgotCommand { get; private set; }

        public event EventHandler<bool> OnLogin; 

        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public bool Remember
        {
            get => _remember;
            set => SetProperty(ref _remember, value);
        }

        public string LoginResult
        {
            get => _loginResult;
            set => SetProperty(ref _loginResult, value);
        }

        public bool IsLoginOk
        {
            get => _isLoginOk;
            set => SetProperty(ref _isLoginOk, value);
        }


        public override async Task Initialize()
        {
            await base.Initialize();
            UserName = string.Empty;
            Password = string.Empty;
            IsLoginOk = false;
            LoginResult = string.Empty;
        }

        private async Task Login()
        {
            var userCredentials = new UserCredentials { UserName = UserName, UserPassword = Password };
            if (await _operations.Remote.Authenticate(userCredentials))
            {
                IsLoginOk = true;
                LoginResult = "Login ok";
                _stateManager.IsAuthenticated = true;
                _stateManager.UserSettings = await _operations.Local.GetUserSettingsAsync(UserName);
                _stateManager.UserCredentials = userCredentials;
                if(_remember)
                    await _operations.Local.SetUserCredentialsAsync(userCredentials);

                this.OnLogin?.Invoke(this, true);
            }
            else
            {
                LoginResult = "Invalid username or password";
                IsLoginOk = false;
                //this.OnLogin?.Invoke(this, false);
            } 
        }

        private bool OnCanLogin()
        {
            return !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password);
        }

        private async Task Forgot()
        {
            var url = await _operations.Local.GetForgotPasswordUrlAsync();
            var process = new Process();
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.FileName = url;
            process.Start();
            await Task.Yield();
        }
         
    }
}
