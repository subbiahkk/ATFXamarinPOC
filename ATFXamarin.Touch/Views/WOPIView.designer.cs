// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace ATFXamarin.Touch
{
	[Register ("WOPIView")]
	partial class WOPIView
	{
		[Outlet]
		MonoTouch.UIKit.UILabel EngagementDescLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIWebView WOPIWebView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (WOPIWebView != null) {
				WOPIWebView.Dispose ();
				WOPIWebView = null;
			}

			if (EngagementDescLabel != null) {
				EngagementDescLabel.Dispose ();
				EngagementDescLabel = null;
			}
		}
	}
}
