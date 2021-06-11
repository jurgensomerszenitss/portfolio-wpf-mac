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
	[Register ("TrackListItemController")]
	partial class TrackListItemController
	{
		[Outlet]
		AppKit.NSBox Background { get; set; }

		[Outlet]
		AppKit.NSImageCell Icon { get; set; }

		[Outlet]
		AppKit.NSImageView Image { get; set; }

		[Outlet]
		AppKit.NSTextField Label { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (Background != null) {
				Background.Dispose ();
				Background = null;
			}

			if (Label != null) {
				Label.Dispose ();
				Label = null;
			}

			if (Icon != null) {
				Icon.Dispose ();
				Icon = null;
			}

			if (Image != null) {
				Image.Dispose ();
				Image = null;
			}
		}
	}
}
