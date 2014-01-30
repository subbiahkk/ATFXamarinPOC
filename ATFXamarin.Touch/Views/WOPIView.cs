using System.Drawing;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.ObjCRuntime;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using Cirrious.MvvmCross.Binding.Touch.Views;
using System;
using ATFXamarin.Core.ViewModels;


namespace ATFXamarin.Touch
{
	public partial class WOPIView : MvxViewController
	{
		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public WOPIView ()
			: base (UserInterfaceIdiomIsPhone ? "WOPIView_iPhone" : "WOPIView_iPad", null)
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
			
			var set = this.CreateBindingSet<WOPIView,WOPIViewModel >();

			set.Bind(EngagementDescLabel).To(vm => vm.EngagementDesc);


			//string url = "http://eyowaserver.cloudapp.net:1234";//((WOPIViewModel) this.ViewModel).URL;
			//	WOPIWebView.LoadRequest(new NSUrlRequest(new NSUrl(url)));
			WOPIWebView.ScalesPageToFit = true;

			string url = "http://sslabsdev.cloudapp.net:1234";
			WOPIWebView.LoadRequest(new NSUrlRequest(new NSUrl(url)));
		}
	}
}

