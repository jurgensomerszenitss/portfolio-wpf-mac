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
	[Register ("UnlockViewController")]
	partial class UnlockViewController
	{
		[Outlet]
		AppKit.NSButton _btnCancel { get; set; }

		[Outlet]
		AppKit.NSButton _btnSave { get; set; }

		[Outlet]
		AppKit.NSSecureTextField _fldDigit1 { get; set; }

		[Outlet]
		AppKit.NSSecureTextField _fldDigit2 { get; set; }

		[Outlet]
		AppKit.NSSecureTextField _fldDigit3 { get; set; }

		[Outlet]
		AppKit.NSSecureTextField _fldDigit4 { get; set; }

		[Action ("CancelClicked:")]
		partial void CancelClicked (Foundation.NSObject sender);

		[Action ("DigitEntered:")]
		partial void DigitEntered (Foundation.NSObject sender);

		[Action ("OkClicked:")]
		partial void OkClicked (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (_fldDigit1 != null) {
				_fldDigit1.Dispose ();
				_fldDigit1 = null;
			}

			if (_fldDigit2 != null) {
				_fldDigit2.Dispose ();
				_fldDigit2 = null;
			}

			if (_fldDigit3 != null) {
				_fldDigit3.Dispose ();
				_fldDigit3 = null;
			}

			if (_fldDigit4 != null) {
				_fldDigit4.Dispose ();
				_fldDigit4 = null;
			}

			if (_btnSave != null) {
				_btnSave.Dispose ();
				_btnSave = null;
			}

			if (_btnCancel != null) {
				_btnCancel.Dispose ();
				_btnCancel = null;
			}
		}
	}
}
