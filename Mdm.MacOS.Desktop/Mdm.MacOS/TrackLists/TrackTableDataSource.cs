using System;
using System.Collections.Generic;
using AppKit;

namespace Mdm.MacOS.TrackLists
{
    public class TrackTableDataSource : NSTableViewDataSource
	{
		#region Public Variables
		public List<TrackModel> Tracks = new List<TrackModel>();
		#endregion

		#region Constructors
		public TrackTableDataSource()
		{
		}
        #endregion

        #region Public Methods
   
        #endregion

        #region Override Methods
        public override nint GetRowCount(NSTableView tableView)
        {
            return Tracks.Count;
        }
        #endregion
    }
}
    