using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;

namespace Mdm.MacOS
{
    public partial class QueueSheetListItem : AppKit.NSView
    {
        #region Constructors

        // Called when created from unmanaged code
        public QueueSheetListItem(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        // Called when created directly from a XIB file
        [Export("initWithCoder:")]
        public QueueSheetListItem(NSCoder coder) : base(coder)
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
