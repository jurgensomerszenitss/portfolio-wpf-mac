using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;

namespace Mdm.MacOS
{
    public partial class TrackListItem : AppKit.NSView
    {
        #region Constructors

        // Called when created from unmanaged code
        public TrackListItem(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        // Called when created directly from a XIB file
        [Export("initWithCoder:")]
        public TrackListItem(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        // Shared initialization code
        void Initialize()
        {
        }

        #endregion
    }
}
