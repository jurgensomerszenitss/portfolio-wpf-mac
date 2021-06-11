using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using Mdm.MacOS.TrackLists;
using Mdm.MacOS.Assets;

namespace Mdm.MacOS
{
    public partial class TrackListItemController : NSCollectionViewItem
    {
        #region Constructors

        // Called when created from unmanaged code
        public TrackListItemController(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        private TrackModel _track;

        // Called when created directly from a XIB file
        [Export("initWithCoder:")]
        public TrackListItemController(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        // Call to load from the XIB/NIB file
        public TrackListItemController() : base("TrackListItem", NSBundle.MainBundle)
        {
            Initialize();
        }

        // Shared initialization code
        void Initialize()
        {
        }

        public override void ViewDidLoad()
        {
            Background.BorderColor = Colors.ColoryTrackListBackground;
            base.ViewDidLoad();
            Image.WantsLayer = true;
            Image.Layer.CornerRadius = 36;
            Image.Layer.MasksToBounds = true; 
        }

        #endregion
 

        //strongly typed view accessor
        public new TrackListItem View
        {
            get
            {
                return (TrackListItem)base.View;
            }
        }

         
        [Export("Track")]
        public TrackModel Track
        {
            get => _track;
            set
            {
                WillChangeValue("Track");
                _track = value;

                if (_track != null && _track.IsFolder)
                {
                    Image.Image = null;
                    Image.Image = NSImage.ImageNamed("IconTrackFolder");
                    Image.NeedsDisplay = true;
                }
                DidChangeValue("Track");
            }
        }

        public NSColor BackgroundColor
        {
            get { return Background.FillColor; }
            set { Background.FillColor = value; }
        }

        public NSColor ForegroundColor
        {
            get { return Label.TextColor;  }
            set { Label.TextColor = value; }
        }

         

        public override bool Selected
        {
            get
            {
                return base.Selected;
            }
            set
            {
                base.Selected = value;

                // Set background color based on the selection state
                if (value)
                {
                    BackgroundColor = Colors.ColorListBackgroundSelected;
                    ForegroundColor = Colors.ColorListForegroundSelected;
                }
                else
                {
                    BackgroundColor = NSColor.FromDeviceRgba(0, 0, 0, 0);
                    ForegroundColor = Colors.ColorListForeground;
                }
            }
        }
    }
}
