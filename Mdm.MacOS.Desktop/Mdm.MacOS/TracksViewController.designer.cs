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
	[Register ("TracksViewController")]
	partial class TracksViewController
	{
		[Outlet]
		AppKit.NSButton _btnActivate { get; set; }

		[Outlet]
		AppKit.NSButton _btnSearch { get; set; }

		[Outlet]
		AppKit.NSBox _divider1 { get; set; }

		[Outlet]
		AppKit.NSTextFieldCell _fldCount { get; set; }

		[Outlet]
		AppKit.NSTextField _fldPath { get; set; }

		[Outlet]
		AppKit.NSTextField _fldSearch { get; set; }

		[Outlet]
		AppKit.NSButton btnIconView { get; set; }

		[Outlet]
		AppKit.NSButton btnSort { get; set; }

		[Outlet]
		AppKit.NSButton btnTableView { get; set; }

		[Outlet]
		AppKit.NSTableColumn ColumnIcon { get; set; }

		[Outlet]
		AppKit.NSTableColumn ColumnTitle { get; set; }

		[Outlet]
		AppKit.NSScrollView IconContainer { get; set; }

		[Outlet]
		AppKit.NSScrollView TableContainer { get; set; }

		[Outlet]
		AppKit.NSCollectionView TrackCollection { get; set; }

		[Outlet]
		AppKit.NSTableView TrackTable { get; set; }

		[Action ("ActivateClicked:")]
		partial void ActivateClicked (Foundation.NSObject sender);

		[Action ("IconViewClicked:")]
		partial void IconViewClicked (Foundation.NSObject sender);

		[Action ("SearchClicked:")]
		partial void SearchClicked (Foundation.NSObject sender);

		[Action ("SearchTextChanged:")]
		partial void SearchTextChanged (Foundation.NSObject sender);

		[Action ("SortClicked:")]
		partial void SortClicked (Foundation.NSObject sender);

		[Action ("TableViewClicked:")]
		partial void TableViewClicked (Foundation.NSObject sender);
		
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

			if (_btnActivate != null) {
				_btnActivate.Dispose ();
				_btnActivate = null;
			}

			if (_divider1 != null) {
				_divider1.Dispose ();
				_divider1 = null;
			}

			if (_fldCount != null) {
				_fldCount.Dispose ();
				_fldCount = null;
			}

			if (_fldPath != null) {
				_fldPath.Dispose ();
				_fldPath = null;
			}

			if (btnIconView != null) {
				btnIconView.Dispose ();
				btnIconView = null;
			}

			if (btnSort != null) {
				btnSort.Dispose ();
				btnSort = null;
			}

			if (btnTableView != null) {
				btnTableView.Dispose ();
				btnTableView = null;
			}

			if (ColumnIcon != null) {
				ColumnIcon.Dispose ();
				ColumnIcon = null;
			}

			if (ColumnTitle != null) {
				ColumnTitle.Dispose ();
				ColumnTitle = null;
			}

			if (IconContainer != null) {
				IconContainer.Dispose ();
				IconContainer = null;
			}

			if (TableContainer != null) {
				TableContainer.Dispose ();
				TableContainer = null;
			}

			if (TrackCollection != null) {
				TrackCollection.Dispose ();
				TrackCollection = null;
			}

			if (TrackTable != null) {
				TrackTable.Dispose ();
				TrackTable = null;
			}
		}
	}
}
