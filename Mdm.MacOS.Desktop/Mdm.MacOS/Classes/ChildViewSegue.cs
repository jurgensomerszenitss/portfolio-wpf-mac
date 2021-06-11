using System;
using AppKit;
using CoreGraphics;
using Foundation;


namespace Mdm.MacOS.Classes
{
	[Register("ChildViewSegue")]
	public class ChildViewSegue : NSStoryboardSegue
	{
		#region Constructors
		public ChildViewSegue()
		{

		}

		public ChildViewSegue(string identifier, NSObject sourceController, NSObject destinationController) : base(identifier, sourceController, destinationController)
		{

		}

		public ChildViewSegue(IntPtr handle) : base(handle)
		{
		}

		public ChildViewSegue(NSObjectFlag x) : base(x)
		{
		}
		#endregion

		#region Override Methods
		public override void Perform()
		{
			// Cast the source and destination controllers
			var source = SourceController as ViewController;
			var destination = DestinationController as NSViewController;

			// Remove any existing view
			if (source.Content.Subviews.Length > 0)
			{
				source.Content.Subviews[0].RemoveFromSuperview();
			}

			// Adjust sizing and add new view
			destination.View.Frame = new CGRect(0, 0, source.Content.Frame.Width, source.Content.Frame.Height);
			destination.View.AutoresizingMask = NSViewResizingMask.HeightSizable | NSViewResizingMask.WidthSizable;
			destination.View.WantsLayer = true;
			source.Content.AddSubview(destination.View);
			//destination.View.Layer.BackgroundColor = NSColor.SystemPinkColor.CGColor;
			

		}
		#endregion

	}
}


