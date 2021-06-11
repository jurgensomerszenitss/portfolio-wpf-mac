using System;
using System.Collections.Generic;
using AppKit;
using Foundation;

namespace Mdm.MacOS.TrackLists
{
    public class TrackListDataSource : NSCollectionViewDataSource
    {
        public TrackListDataSource()
        {
        }

        public TrackListDataSource(NSCollectionView parent)
        {
            // Initialize
            ParentCollectionView = parent;

            // Attach to collection view
            parent.DataSource = this;

        }

        public NSCollectionView ParentCollectionView { get; set; }

        public List<TrackModel> Data { get; set; } = new List<TrackModel>();

        public override nint GetNumberOfSections(NSCollectionView collectionView)
        {
            // There is only one section in this view
            return 1;
        }

        public override nint GetNumberofItems(NSCollectionView collectionView, nint section)
        {
            // Return the number of items
            return Data.Count;
        }

        public override NSCollectionViewItem GetItem(NSCollectionView collectionView, NSIndexPath indexPath)
        { 
            var item = collectionView.MakeItem("TrackCell", indexPath) as TrackListItemController;
            item.Track = Data[(int)indexPath.Item];

            return item;
        }
    }
}
