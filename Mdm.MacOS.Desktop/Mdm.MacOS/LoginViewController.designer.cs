// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Mdm.MacOS
{
	[Register ("LoginViewController")]
	partial class LoginViewController
	{
		[Outlet]
		AppKit.NSButton _btnForgot { get; set; }

		[Outlet]
		AppKit.NSButton _btnLogin { get; set; }

		[Outlet]
		AppKit.NSBox _divider1 { get; set; }

		[Outlet]
		AppKit.NSBox _divider2 { get; set; }

		[Outlet]
		AppKit.NSSecureTextField _fldPassword { get; set; }

		[Outlet]
		AppKit.NSButton _fldRemember { get; set; }

		[Outlet]
		AppKit.NSTextField _fldUserName { get; set; }

		[Outlet]
		AppKit.NSTextField _lblLoginResult { get; set; }

		[Action ("ForgotClicked:")]
		partial void ForgotClicked (Foundation.NSObject sender);

		[Action ("LoginClicked:")]
		partial void LoginClicked (Foundation.NSObject sender);

		[Action ("PasswordChanged:")]
		partial void PasswordChanged (Foundation.NSObject sender);

		[Action ("UserNameChanged:")]
		partial void UserNameChanged (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (_btnLogin != null) {
				_btnLogin.Dispose ();
				_btnLogin = null;
			}

			if (_divider1 != null) {
				_divider1.Dispose ();
				_divider1 = null;
			}

			if (_divider2 != null) {
				_divider2.Dispose ();
				_divider2 = null;
			}

			if (_fldPassword != null) {
				_fldPassword.Dispose ();
				_fldPassword = null;
			}

			if (_fldRemember != null) {
				_fldRemember.Dispose ();
				_fldRemember = null;
			}

			if (_fldUserName != null) {
				_fldUserName.Dispose ();
				_fldUserName = null;
			}

			if (_btnForgot != null) {
				_btnForgot.Dispose ();
				_btnForgot = null;
			}

			if (_lblLoginResult != null) {
				_lblLoginResult.Dispose ();
				_lblLoginResult = null;
			}
		}
	}
}
