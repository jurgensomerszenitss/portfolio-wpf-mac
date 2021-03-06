// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using AppKit;
using Mdm.MacOS.Assets;
using Mdm.App.ViewModels;

namespace Mdm.MacOS
{
	public partial class LoginViewController : NSViewController
	{
		private NSViewController _presentor;
		private ILoginViewModel _loginViewModel;

		public NSViewController Presentor
		{
			get { return _presentor; }
			set { _presentor = value; }
		}

		public LoginViewController (IntPtr handle) : base (handle)
		{
		}

		public EventHandler OnLogin;

		internal void RaiseOnLogin()
		{
			if (this.OnLogin != null)
				this.OnLogin(this, EventArgs.Empty);
		}

        public override void ViewDidLoad()
        {
			_loginViewModel = MainClass.Container.GetInstance<ILoginViewModel>();
            _loginViewModel.OnLogin += _loginViewModel_OnLogin;
            _loginViewModel.PropertyChanged += _loginViewModel_PropertyChanged;
            _loginViewModel.LoginCommand.CanExecuteChanged += LoginCommand_CanExecuteChanged;

			_divider1.BorderColor = Colors.ColorQueueSheetBorder;
			_divider1.BoxType = NSBoxType.NSBoxCustom;
			_divider1.BorderType = NSBorderType.LineBorder;
			_divider1.SetFrameSize(new CoreGraphics.CGSize(240, 1));

			_divider2.BorderColor = Colors.ColorQueueSheetBorder;
			_divider2.BoxType = NSBoxType.NSBoxCustom;
			_divider2.BorderType = NSBorderType.LineBorder;
			_divider2.SetFrameSize(new CoreGraphics.CGSize(240, 1));

			_btnLogin.WantsLayer = true;
			_btnLogin.Layer.BackgroundColor = Colors.ColorLoginButtonBackgroundDisabled.CGColor;
			_btnLogin.ContentTintColor = Colors.ColorLoginButtonForegroundDisabled;
			_btnLogin.Layer.CornerRadius = 24;
			_btnLogin.Enabled = false;

			_btnForgot.WantsLayer = true;
			_btnForgot.ContentTintColor = Colors.ColorSettingsButtonBorder;
			_btnForgot.Layer.CornerRadius = 12;
			_btnForgot.Layer.BorderWidth = 1;
			_btnForgot.Layer.BorderColor = Colors.ColorSettingsButtonBorder.CGColor;

			this.View.WantsLayer = true;
			this.View.Layer.BackgroundColor = Colors.ColorMenuBackground.CGColor;
			base.ViewDidLoad();
        }

        private void LoginCommand_CanExecuteChanged(object sender, EventArgs e)
        {
			_btnLogin.Enabled = _loginViewModel.LoginCommand.CanExecute(null);

			if (_btnLogin.Enabled)
            {
				_btnLogin.Layer.BackgroundColor = Colors.ColorLoginButtonBackgroundEnabled.CGColor;
				_btnLogin.ContentTintColor = Colors.ColorLoginButtonForegroundEnabled;
			}				
			else
            {
				_btnLogin.Layer.BackgroundColor = Colors.ColorLoginButtonBackgroundDisabled.CGColor;
				_btnLogin.ContentTintColor = Colors.ColorLoginButtonForegroundDisabled;
			}
				
		}

        private void _loginViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

			if (e.PropertyName == nameof(ILoginViewModel.LoginResult))
			{
				BeginInvokeOnMainThread(() => {
					_lblLoginResult.StringValue = _loginViewModel.LoginResult;
					_lblLoginResult.Hidden = false;
				});
			}
				
			if (e.PropertyName == nameof(ILoginViewModel.IsLoginOk))
				BeginInvokeOnMainThread(() => _lblLoginResult.Hidden = _loginViewModel.IsLoginOk);
		}

        partial void LoginClicked(NSObject sender)
        {
			_loginViewModel.UserName = _fldUserName.StringValue;
			_loginViewModel.Password = _fldPassword.StringValue;
			_loginViewModel.Remember = _fldRemember.State == NSCellStateValue.On;

			_loginViewModel.LoginCommand.Execute(null);
        }

        partial void ForgotClicked(NSObject sender)
        {
			_loginViewModel.ForgotCommand.Execute(null);
        }

        partial void UserNameChanged(NSObject sender)
        {
			_loginViewModel.UserName = _fldUserName.StringValue;
        }

        partial void PasswordChanged(NSObject sender)
        {
			_loginViewModel.Password = _fldPassword.StringValue;
        }

        private void _loginViewModel_OnLogin(object sender, bool e)
		{
			if(e) CloseDialog();
		}

		private void CloseDialog()
		{
			BeginInvokeOnMainThread(() => Presentor.DismissViewController(this));
		}
	}
}

