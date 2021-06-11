// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Mdm.MacOS.Models
{
	partial class QueueSheetModel
	{
		[Outlet]
		AppKit.NSSearchField searchBox { get; set; }

		[Action ("OnSearch:")]
		partial void OnSearch (AppKit.NSSearchField sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (searchBox != null) {
				searchBox.Dispose ();
				searchBox = null;
			}
		}
	}
}
