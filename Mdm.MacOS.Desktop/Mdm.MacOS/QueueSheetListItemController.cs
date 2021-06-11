using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using Mdm.MacOS.QueueSheetLists;
using Mdm.MacOS.Assets;
using Mdm.App.ViewModels;

namespace Mdm.MacOS
{
    public partial class QueueSheetListItemController : NSCollectionViewItem
    {
        private QueueSheetModel _queueSheet;
        private IQueueSheetViewModel _queueSheetViewModel;
        #region Constructors

        // Called when created from unmanaged code
        public QueueSheetListItemController(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        // Called when created directly from a XIB file
        [Export("initWithCoder:")]
        public QueueSheetListItemController(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        // Call to load from the XIB/NIB file
        public QueueSheetListItemController() : base("QueueSheetListItem", NSBundle.MainBundle)
        {
            Initialize();
        }

        // Shared initialization code
        void Initialize()
        {
            _queueSheetViewModel = MainClass.Container.GetInstance<IQueueSheetViewModel>();

        }

        #endregion

        //strongly typed view accessor
        public new QueueSheetListItem View
        {
            get
            {
                return (QueueSheetListItem)base.View;
            }
        }

        [Export("QueueSheet")]
        public QueueSheetModel QueueSheet
        {
            get => _queueSheet;
            set
            {
                WillChangeValue("QueueSheet");
                _queueSheet = value;
                DidChangeValue("QueueSheet");
            }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            _divider.BorderColor = Colors.ColorQueueSheetBorder;
            _divider.BoxType = NSBoxType.NSBoxCustom;
            _divider.BorderType = NSBorderType.LineBorder;
            _divider.SetFrameSize(new CoreGraphics.CGSize(320, 1));
        }

        public NSColor BackgroundColor
        {
            get { return Background.FillColor; }
            set { Background.FillColor = value; }
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
                    BackgroundColor = Colors.ColorQueueSheetBackgroundSelected;
                    Foreground.TextColor = Colors.ColorQueueSheetForegroundSelected;
                    Icon.Image = NSImage.ImageNamed("IconFolderActive");               
                }
                else
                {
                    BackgroundColor = Colors.ColorMenuBackground;
                    Foreground.TextColor = Colors.ColorQueueSheetForeground;
                    Icon.Image = NSImage.ImageNamed("IconFolder");
                }
            }
        }

        partial void UnlockClicked(NSObject sender)
        {
            _queueSheetViewModel.UnlockCommand.Execute(_queueSheet.Origin);
        }
    }
}
