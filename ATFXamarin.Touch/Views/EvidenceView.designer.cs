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
	[Register ("EvidenceView")]
	partial class EvidenceView
	{
		[Outlet]
		MonoTouch.UIKit.UIButton AddEvidenceButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel ClientLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel EngagementDescLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIView EvidenceBodyView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView EvidenceImage { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton GalleryButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIView HeaderView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton SaveEvidenceButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField SummaryText { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (EvidenceBodyView != null) {
				EvidenceBodyView.Dispose ();
				EvidenceBodyView = null;
			}

			if (AddEvidenceButton != null) {
				AddEvidenceButton.Dispose ();
				AddEvidenceButton = null;
			}

			if (ClientLabel != null) {
				ClientLabel.Dispose ();
				ClientLabel = null;
			}

			if (EngagementDescLabel != null) {
				EngagementDescLabel.Dispose ();
				EngagementDescLabel = null;
			}

			if (EvidenceImage != null) {
				EvidenceImage.Dispose ();
				EvidenceImage = null;
			}

			if (GalleryButton != null) {
				GalleryButton.Dispose ();
				GalleryButton = null;
			}

			if (HeaderView != null) {
				HeaderView.Dispose ();
				HeaderView = null;
			}

			if (SaveEvidenceButton != null) {
				SaveEvidenceButton.Dispose ();
				SaveEvidenceButton = null;
			}

			if (SummaryText != null) {
				SummaryText.Dispose ();
				SummaryText = null;
			}
		}
	}
}
