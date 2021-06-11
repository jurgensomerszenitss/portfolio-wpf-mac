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
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSButton _btnSearch { get; set; }

		[Outlet]
		AppKit.NSBox _divider1 { get; set; }

		[Outlet]
		AppKit.NSTextField _fldSearch { get; set; }

		[Outlet]
		AppKit.NSView ContentView { get; set; }

		[Outlet]
		AppKit.NSCollectionView QueueSheetCollection { get; set; }

		[Outlet]
		AppKit.NSView QueueView { get; set; }

		[Outlet]
		AppKit.NSTextField UserName { get; set; }

		[Action ("RefreshClicked:")]
		partial void RefreshClicked (Foundation.NSObject sender);

		[Action ("SearchClicked:")]
		partial void SearchClicked (Foundation.NSObject sender);

		[Action ("UserNameClicked:")]
		partial void UserNameClicked (Foundation.NSObject sender);

		[Action ("UserSettingsClicked:")]
		partial void UserSettingsClicked (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (_btnSearch != null) {
				_btnSearch.Dispose ();
				_btnSearch = null;
			}

			if (_fldSearch != null) {
				_fldSearch.Dispose ();
				_fldSearch = null;
			}

			if (ContentView != null) {
				ContentView.Dispose ();
				ContentView = null;
			}

			if (QueueSheetCollection != null) {
				QueueSheetCollection.Dispose ();
				QueueSheetCollection = null;
			}

			if (QueueView != null) {
				QueueView.Dispose ();
				QueueView = null;
			}

			if (UserName != null) {
				UserName.Dispose ();
				UserName = null;
			}

			if (_divider1 != null) {
				_divider1.Dispose ();
				_divider1 = null;
			}
		}
	}
}
