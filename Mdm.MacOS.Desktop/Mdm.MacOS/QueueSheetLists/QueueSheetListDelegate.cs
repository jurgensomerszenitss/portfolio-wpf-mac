using System;
using Foundation;
using AppKit;

namespace Mdm.MacOS.QueueSheetLists
{
    public class QueueSheetListDelegate : NSCollectionViewDelegateFlowLayout
    {
        public ViewController ParentViewController { get; set; }

        public QueueSheetListDelegate(ViewController parentViewController)
        {
            // Initialize
            ParentViewController = parentViewController;
        }

        public override void ItemsSelected(NSCollectionView collectionView, NSSet indexPaths)
        {
            // Dereference path
            var paths = indexPaths.ToArray<NSIndexPath>();
            var index = (int)paths[0].Item;

            // Save the selected item
            ParentViewController.QueueSheetSelected = ParentViewController.QueueSheetListDatasource.Data[index];

        }

        /// <summary>
        /// Handles one or more items being deselected.
        /// </summary>
        /// <param name="collectionView">The parent Collection view.</param>
        /// <param name="indexPaths">The Index paths of the items being deselected.</param>
        public override void ItemsDeselected(NSCollectionView collectionView, NSSet indexPaths)
        {
            // Dereference path
            var paths = indexPaths.ToArray<NSIndexPath>();
            var index = paths[0].Item;

            // Clear selection
            ParentViewController.QueueSheetSelected = null;
        }
    }
}
