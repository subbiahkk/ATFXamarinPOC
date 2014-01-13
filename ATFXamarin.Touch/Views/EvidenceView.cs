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

			this.SummaryText.ShouldReturn = (tf) => // Use reference.
			{
				// This delegate will be executed at a later time, ensure its owner
				// object is rooted with a reference.
				tf.ResignFirstResponder();
				return true;
			} ;

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

		/// <summary>
		/// is called when the OS is going to rotate the application. It handles rotating the status bar
		/// if it's present, as well as it's controls like the navigation controller and tab bar, but you 
		/// must handle the rotation of your view and associated subviews. This call is wrapped in an 
		/// animation block in the underlying implementation, so it will automatically animate your control
		/// repositioning.
		/// </summary>
		public override void WillAnimateRotation (UIInterfaceOrientation toInterfaceOrientation, double duration)
		{

			base.WillAnimateRotation (toInterfaceOrientation, duration);
			if (this.HeaderView.Layer.Frame.Width != 1000f) {
				//float width = 1000f;
				this.HeaderView.Frame = new RectangleF(9, 103, 1000, 169);
				this.EvidenceBodyView.Frame = new RectangleF(9, 307, 1000, 616);
				//this.HeaderView.Layer.Frame.Width = width;
			} 
			else {
				this.HeaderView.Frame = new RectangleF(9, 103, 750, 169);
				this.EvidenceBodyView.Frame = new RectangleF(9, 307, 750, 616);
				//this.HeaderView.Layer.Frame.Width  = 750f;
			}
		}
	}
}

