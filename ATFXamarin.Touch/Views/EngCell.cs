using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using ATFXamarin.Core.Services;

namespace ATFXamarin.Touch
{
	public partial class EngCell : MvxTableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("EngCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("EngCell");

		public EngCell (IntPtr handle) : base (handle)
		{
			this.DelayBind(() => {
				var set = this.CreateBindingSet<EngCell, Engagement> ();
				set.Bind(DescriptionLabel).To (item => item.Description);
				set.Bind (ClientLabel).To (item => item.ClientName);
				//set.Bind (_loader).To (item => item.volumeInfo.imageLinks.thumbnail); //smallThumbnail);
				set.Apply();
			});
		}

		public static EngCell Create ()
		{
			return (EngCell)Nib.Instantiate (null, null) [0];
		}
	}
}

