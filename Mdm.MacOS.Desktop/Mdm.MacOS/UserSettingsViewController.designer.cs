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
	[Register ("UserSettingsViewController")]
	partial class UserSettingsViewController
	{
		[Outlet]
		AppKit.NSBox _divider1 { get; set; }

		[Outlet]
		AppKit.NSBox _divider2 { get; set; }

		[Outlet]
		AppKit.NSBox _divider3 { get; set; }

		[Outlet]
		AppKit.NSTextField _fldBusinessName { get; set; }

		[Outlet]
		AppKit.NSTextField _fldContractDate { get; set; }

		[Outlet]
		AppKit.NSTextField _fldCustomerClassification { get; set; }

		[Outlet]
		AppKit.NSTextField _fldFileLocation { get; set; }

		[Outlet]
		AppKit.NSTextField _fldLastLoginDate { get; set; }

		[Outlet]
		AppKit.NSButton _fldSync30 { get; set; }

		[Outlet]
		AppKit.NSButton _fldSync5 { get; set; }

		[Outlet]
		AppKit.NSButton _fldSync60 { get; set; }

		[Outlet]
		AppKit.NSButton _fldSyncOff { get; set; }

		[Outlet]
		AppKit.NSTextField _fldUserName { get; set; }

		[Outlet]
		AppKit.NSButton btnBrowse { get; set; }

		[Outlet]
		AppKit.NSButton btnCancel { get; set; }

		[Outlet]
		AppKit.NSButton btnLogout { get; set; }

		[Outlet]
		AppKit.NSButton btnSave { get; set; }

		[Outlet]
		AppKit.NSBox LocationBorder { get; set; }

		[Action ("CancelClicked:")]
		partial void CancelClicked (Foundation.NSObject sender);

		[Action ("LogoutClicked:")]
		partial void LogoutClicked (Foundation.NSObject sender);

		[Action ("SaveClicked:")]
		partial void SaveClicked (Foundation.NSObject sender);

		[Action ("SelectFolderClicked:")]
		partial void SelectFolderClicked (Foundation.NSObject sender);

		[Action ("SyncValueChanged:")]
		partial void SyncValueChanged (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnLogout != null) {
				btnLogout.Dispose ();
				btnLogout = null;
			}

			if (_fldBusinessName != null) {
				_fldBusinessName.Dispose ();
				_fldBusinessName = null;
			}

			if (btnCancel != null) {
				btnCancel.Dispose ();
				btnCancel = null;
			}

			if (btnSave != null) {
				btnSave.Dispose ();
				btnSave = null;
			}

			if (btnBrowse != null) {
				btnBrowse.Dispose ();
				btnBrowse = null;
			}

			if (_fldContractDate != null) {
				_fldContractDate.Dispose ();
				_fldContractDate = null;
			}

			if (_fldCustomerClassification != null) {
				_fldCustomerClassification.Dispose ();
				_fldCustomerClassification = null;
			}

			if (_fldFileLocation != null) {
				_fldFileLocation.Dispose ();
				_fldFileLocation = null;
			}

			if (_fldLastLoginDate != null) {
				_fldLastLoginDate.Dispose ();
				_fldLastLoginDate = null;
			}

			if (_fldSync30 != null) {
				_fldSync30.Dispose ();
				_fldSync30 = null;
			}

			if (_fldSync5 != null) {
				_fldSync5.Dispose ();
				_fldSync5 = null;
			}

			if (_fldSync60 != null) {
				_fldSync60.Dispose ();
				_fldSync60 = null;
			}

			if (_fldSyncOff != null) {
				_fldSyncOff.Dispose ();
				_fldSyncOff = null;
			}

			if (_fldUserName != null) {
				_fldUserName.Dispose ();
				_fldUserName = null;
			}

			if (LocationBorder != null) {
				LocationBorder.Dispose ();
				LocationBorder = null;
			}

			if (_divider1 != null) {
				_divider1.Dispose ();
				_divider1 = null;
			}

			if (_divider2 != null) {
				_divider2.Dispose ();
				_divider2 = null;
			}

			if (_divider3 != null) {
				_divider3.Dispose ();
				_divider3 = null;
			}
		}
	}
}
