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
	[Register ("EngCell")]
	partial class EngCell
	{
		[Outlet]
		MonoTouch.UIKit.UILabel ClientLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel DescriptionLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ClientLabel != null) {
				ClientLabel.Dispose ();
				ClientLabel = null;
			}

			if (DescriptionLabel != null) {
				DescriptionLabel.Dispose ();
				DescriptionLabel = null;
			}
		}
	}
}
