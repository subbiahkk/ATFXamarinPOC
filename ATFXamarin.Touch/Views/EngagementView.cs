using System.Drawing;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.ObjCRuntime;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using Cirrious.MvvmCross.Binding.Touch.Views;
using System;
using ATFXamarin.Core.Services;

namespace ATFXamarin.Touch.Views
{
    [Register("EngagementView")]
	partial class EngagementView : MvxViewController
    {


		///*
		public override void ViewDidLoad()
		{
			View = new UIView(){ 
				//	BackgroundColor = UIColor.Black
			};
			base.ViewDidLoad();

			this.Title = "Engagements";

			var activity = new UIActivityIndicatorView(new RectangleF(130, 130, 60, 60));
			activity.Color = UIColor.Orange;

			var tableView = new UITableView(new RectangleF(0, 0, 1200, 1208), UITableViewStyle.Plain);
			//var tableView = new UITableView(View., UITableViewStyle.Plain);
			Add(tableView);

			tableView.RowHeight = 89;
			tableView.BackgroundColor = UIColor.White;

			var source = new MvxSimpleTableViewSource(tableView, EngagementCell.Key, EngagementCell.Key);
			tableView.Source = source;

			tableView.UserInteractionEnabled = true;


			var set = this.CreateBindingSet<EngagementView, ATFXamarin.Core.ViewModels.EngagementViewModel>();
			set.Bind(source).To(vm => vm.Engagements);
			//set.Bind (source).For(s => s.SelectionChangedCommand).To(vm => vm.ItemSelectedCommand);
			//set.Bind (source).For(s => s.AccessoryTappedCommand).To(vm => vm.ItemSelectedCommand);
			set.Apply();

			tableView.ReloadData();

			//EngagementCell ec = new EngagementCell ();

			EngagementCell.SingleTouchEvent	+=	new EngagementCell.SingleTouch (SingleTouchEvent);
		}

		public void SingleTouchEvent(CommandTypeEnum commandType,Engagement engagement)
		{
			switch(commandType)
			{
			case CommandTypeEnum.AddEvidence:

				((ATFXamarin.Core.ViewModels.EngagementViewModel)this.ViewModel).ShowEvidence (engagement);
				break;
			case CommandTypeEnum.EditDocuments:
				((ATFXamarin.Core.ViewModels.EngagementViewModel)this.ViewModel).EditEvidenceDocument (engagement);
				Console.WriteLine (commandType.ToString ());
				break;
			}
		}

		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			base.TouchesBegan (touches, evt);
			UITouch touch = touches.AnyObject as UITouch;
			if (touch != null)
			{
				if (touch.TapCount == 2) 
				{
					// do something with the double touch.
				}
			}
		}

		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// we're passed to orientation that it will rotate to. in our case, we could
			// just return true, but this switch illustrates how you can test for the 
			// different cases
			switch (toInterfaceOrientation) {
			case UIInterfaceOrientation.LandscapeLeft:
			case UIInterfaceOrientation.LandscapeRight:
			case UIInterfaceOrientation.Portrait:
			case UIInterfaceOrientation.PortraitUpsideDown:
			default:
				return true;
			}
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
			this.Title = DateTime.Now.ToString ();
			// call our helper method to position the controls
			//PositionControls (toInterfaceOrientation);
		}
	}
}