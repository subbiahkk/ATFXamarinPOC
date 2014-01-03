using System.Drawing;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.ObjCRuntime;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using Cirrious.MvvmCross.Binding.Touch.Views;

namespace ATFXamarin.Touch.Views
{
    [Register("EngagementView")]
	partial class EngagementView : MvxViewController
    {
        

		public override void ViewDidLoad()
		{
			View = new UIView(){ BackgroundColor = UIColor.White};
			base.ViewDidLoad();

			var textField = new UITextField(new RectangleF(10, 10, 300, 40));
			Add(textField);

			var activity = new UIActivityIndicatorView(new RectangleF(130, 130, 60, 60));
			activity.Color = UIColor.Orange;

			//Add(activity);

			var tableView = new UITableView(new RectangleF(0, 50, 320, 500), UITableViewStyle.Plain);
			Add(tableView);

			// choice here:
			//
			//   for original demo use:
			//     var source = new MvxStandardTableViewSource(tableView, "TitleText");
			//
			//   or for prettier cells from XIB file use:
			//     tableView.RowHeight = 88;
			//     var source = new MvxSimpleTableViewSource(tableView, BookCell.Key, BookCell.Key);

			tableView.RowHeight = 88;
			var source = new MvxSimpleTableViewSource(tableView, EngCell.Key, EngCell.Key);
			tableView.Source = source;

			var set = this.CreateBindingSet<EngagementView, ATFXamarin.Core.ViewModels.EngagementViewModel>();
			//set.Bind(textField).To(vm => vm.SearchTerm);
			//set.Bind(textField).For(t => t.Enabled).To(vm => vm.IsLoading).WithConversion("InverseBool");
			set.Bind(source).To(vm => vm.Engagements);
			//set.Bind(activity).For("Visibility").To(vm => vm.IsLoading).WithConversion("Visibility");
			//set.Bind(tableView).For("Visibility").To(vm => vm.IsLoading).WithConversion("InvertedVisibility");
			set.Apply();

			tableView.ReloadData();
		}

        //public override void ViewDidLoad()
        //{
        //    base.ViewDidLoad();

        //    var source = new MvxStandardTableViewSource(TableView, "TitleText ClientName;ImageUrl ImageURL");
        //    TableView.Source = source;

        //    var set = this.CreateBindingSet<EngagementView, ATFXamarin.Core.ViewModels.EngagementViewModel>();
        //    set.Bind(source).To(vm => vm.Engagements);
        //    set.Apply();

        //    TableView.ReloadData();
        //}   
	}
}