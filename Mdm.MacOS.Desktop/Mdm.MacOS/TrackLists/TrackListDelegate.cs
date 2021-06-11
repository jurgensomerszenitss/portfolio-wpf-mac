using System;
using AppKit;
using Foundation;

namespace Mdm.MacOS.TrackLists
{
    public class TrackListDelegate : NSCollectionViewDelegateFlowLayout
    {
        public TracksViewController ParentViewController { get; set; }

        public TrackListDelegate(TracksViewController parentViewController)
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
            ParentViewController.SelectTrack(ParentViewController.TrackListDataSource.Data[index]);

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
            foreach(var path in paths)
            {
                var index = (int) path.Item;

                // Clear selection
                ParentViewController.UnselectTrack(ParentViewController.TrackListDataSource.Data[index]);
            }
         
        }
    }
}
