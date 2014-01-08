using System;
using System.Drawing;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.ObjCRuntime;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;
using Cirrious.MvvmCross.Binding.Touch.Views;
using ATFXamarin.Core.ViewModels;

namespace ATFXamarin.Touch
{
	public partial class EvidenceView : MvxViewController
	{
		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public EvidenceView ()
			: base (UserInterfaceIdiomIsPhone ? "EvidenceView_iPhone" : "EvidenceView_iPad", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Title = "Evidence View";
			this.HeaderView.Layer.BorderColor = UIColor.LightGray.CGColor;
			this.HeaderView.Layer.BorderWidth = .5f;

			this.EvidenceBodyView.Layer.BorderColor = UIColor.LightGray.CGColor;
			this.EvidenceBodyView.Layer.BorderWidth = .5f;

			this.SaveEvidenceButton.Layer.CornerRadius = 5;

			CAGradientLayer cagLayer = new CAGradientLayer ();
			cagLayer.Bounds = SaveEvidenceButton.Bounds;


			CGColor gradColor1 = new CGColor (13.0f / 255.0f, 116.0f / 255.0f, 1.0f,1.0f);
			CGColor gradColor2 = new CGColor (0.0f, 53.0f / 255.0f, 126.0f / 255.0f,1.0f);

			cagLayer.Colors = new CGColor[2] { gradColor1, gradColor2 };
			cagLayer.CornerRadius = 12.0f;
			cagLayer.CornerRadius = SaveEvidenceButton.Layer.CornerRadius;
			SaveEvidenceButton.Layer.InsertSublayer (cagLayer, 0);
			SaveEvidenceButton.ClipsToBounds = true;

			var set = this.CreateBindingSet<EvidenceView,EvidenceViewModel >();
			set.Bind(EngagementDescLabel).To(vm => vm.Evidence.Engagement.Description);
			set.Bind(ClientLabel).To(vm => vm.Evidence.Engagement.ClientName);
			set.Bind (AddEvidenceButton).To (vm => vm.TakePictureCommand);
			set.Bind (GalleryButton).To (vm => vm.ChoosePictureCommand);
			set.Bind (SummaryText).To (vm => vm.Notes);
			//set.Bind (SaveEvidenceButton).To (vm => vm.SaveEvidenceCommand);
			//set.Bind (NotesLabel).To (vm => vm.Notes);
			set.Bind(EvidenceImage).To(vm => vm.Bytes).WithConversion("InMemoryImage");
			set.Apply ();

			this.SaveEvidenceButton.TouchUpInside += (sender, e) => {

				((EvidenceViewModel)this.ViewModel).SaveEvidence (success => {
					InvokeOnMainThread (delegate {  
					UILocalNotification notification = new UILocalNotification ();
					notification.FireDate = DateTime.Now.AddMinutes (1);
					notification.AlertAction = "View Alert";
					notification.AlertBody = "Your one minute alert has fired!";
					UIApplication.SharedApplication.ScheduleLocalNotification (notification);
					});
					((EvidenceViewModel)this.ViewModel).NavigateToEngagementView();
				}, error => {
				});



			};
			

		}
	}
}

