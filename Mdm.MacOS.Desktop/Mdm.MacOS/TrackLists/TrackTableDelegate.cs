using System;
using AppKit;
using CoreGraphics;
using Foundation;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mdm.MacOS.TrackLists
{
    public class TrackTableDelegate : NSTableViewDelegate
	{
		#region Constants 
		private const string CellIdentifier = "TitleCell";
		#endregion

		#region Private Variables
		private TrackTableDataSource DataSource;
		private NSTableView _tableView;
		public TracksViewController ParentViewController { get; set; }
		#endregion

		#region Constructors
		public TrackTableDelegate(TrackTableDataSource datasource, TracksViewController parentViewController)
		{
			this.DataSource = datasource;
			this.ParentViewController = parentViewController;
		}
		#endregion

		#region Override Methods
		public override NSView GetViewForItem(NSTableView tableView, NSTableColumn tableColumn, nint row)
		{

			NSTableCellView view = (NSTableCellView)tableView.MakeView(tableColumn.Title, this);
			if (view == null)
			{
				view = new NSTableCellView();
				if (tableColumn.Title == "Title")
				{
					view.ImageView = new NSImageView(new CGRect(0, 0, 24, 24));
					view.AddSubview(view.ImageView);
					view.TextField = new NSTextField(new CGRect(20, 0, 400, 24));
				}
				else
				{
					view.TextField = new NSTextField(new CGRect(0, 0, 400, 24));
				}
				view.TextField.AutoresizingMask = NSViewResizingMask.WidthSizable;
				view.AddSubview(view.TextField);
				view.Identifier = tableColumn.Title;
				view.TextField.BackgroundColor = NSColor.Clear;
				view.TextField.Bordered = false;
				view.TextField.Selectable = true;
				view.TextField.Editable = false;
				view.TextField.Font = NSFont.FromFontName("FilsonSoftW03-Regular", 12);
				view.TextField.TextColor = NSColor.White;
			}

			// Tag view
			view.TextField.Tag = row;

			

			// Setup view based on the column selected
			switch (tableColumn.Title)
			{
				case "Title":
					view.ImageView.Image = DataSource.Tracks[(int)row].IsFolder ? NSImage.ImageNamed("IconTrackItemFolder")  : NSImage.ImageNamed("IconTrackItemSong");
					view.TextField.StringValue = DataSource.Tracks[(int)row].Title;
					break; 
			}

			return view; 
		}

		
		public override bool ShouldSelectRow(NSTableView tableView, nint row)
		{
			_tableView = tableView; 
			return true;
		}

        [Export("tableViewSelectionDidChange:")]
        public override void SelectionDidChange(NSNotification notification)
        {
			var rows = _tableView.SelectedRows;
			ParentViewController.UnselectAllTracks();
			foreach (var row in rows)
            {
				ParentViewController.SelectTrack(DataSource.Tracks[(int)row]);
			}
        }

        #endregion


    }

}
