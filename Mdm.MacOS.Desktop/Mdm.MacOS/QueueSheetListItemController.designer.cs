// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Mdm.MacOS
{
	[Register ("QueueSheetListItemController")]
	partial class QueueSheetListItemController
	{
		[Outlet]
		AppKit.NSBox _divider { get; set; }

		[Outlet]
		AppKit.NSBox Background { get; set; }

		[Outlet]
		AppKit.NSButton btnUnlock { get; set; }

		[Outlet]
		AppKit.NSTextField Foreground { get; set; }

		[Outlet]
		AppKit.NSImageView Icon { get; set; }

		[Action ("UnlockClicked:")]
		partial void UnlockClicked (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (_divider != null) {
				_divider.Dispose ();
				_divider = null;
			}

			if (Background != null) {
				Background.Dispose ();
				Background = null;
			}

			if (Foreground != null) {
				Foreground.Dispose ();
				Foreground = null;
			}

			if (Icon != null) {
				Icon.Dispose ();
				Icon = null;
			}

			if (btnUnlock != null) {
				btnUnlock.Dispose ();
				btnUnlock = null;
			}
		}
	}
}
