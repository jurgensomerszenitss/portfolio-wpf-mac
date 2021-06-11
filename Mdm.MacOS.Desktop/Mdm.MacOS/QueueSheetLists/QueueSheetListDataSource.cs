using System;
using System.Collections.Generic;
using AppKit;
using Foundation;

namespace Mdm.MacOS.QueueSheetLists
{
    public class QueueSheetListDataSource : NSCollectionViewDataSource
    {
        public QueueSheetListDataSource()
        {
        }

        public QueueSheetListDataSource(NSCollectionView parent)
        {
            // Initialize
            ParentCollectionView = parent;

            // Attach to collection view
            parent.DataSource = this;

        }

        public NSCollectionView ParentCollectionView { get; set; } 

        public List<QueueSheetModel> Data { get; set; } = new List<QueueSheetModel>();

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
            var flimch = collectionView.MakeItem("QueueSheetCell", indexPath);
            var item = collectionView.MakeItem("QueueSheetCell", indexPath) as QueueSheetListItemController;
            item.QueueSheet = Data[(int)indexPath.Item];

            return item;
        }
    }
}
