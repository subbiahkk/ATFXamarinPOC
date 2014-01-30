using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using ATFXamarin.Core.Services;

namespace ATFXamarin.Touch
{
	public partial class EngagementCell : MvxTableViewCell 
	{
		public static readonly UINib Nib = UINib.FromName ("EngagementCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("EngagementCell");


		UIScrollView scrollView;
		UIView scrollViewButtonView, scrollViewContentView;
		UIButton addButton, editButton;
		UILabel descLabel,clientLabel;
		float catchWidth = 700f;
		UIImage imageEng;
		UIImageView imageView;

		public delegate void SingleTouch(CommandTypeEnum commandType,Engagement engagement);
		public static event SingleTouch SingleTouchEvent;



		public EngagementCell (IntPtr handle) : base (handle)
		//public EngagementCell ( ) : base ()
		{
			this.DelayBind(() => {
				//this.ScrollView.ScrollEnabled = true;


				scrollView = new UIScrollView();
				scrollView.ShowsHorizontalScrollIndicator = false;
				scrollView.Delegate = new SlidingCellScrollDelegate(this);
				ContentView.AddSubview(scrollView);

				scrollViewButtonView = new UIView();
				scrollView.AddSubview(scrollViewButtonView);

				addButton = UIButton.FromType(UIButtonType.Custom);
				addButton.BackgroundColor = UIColor.FromRGB(153,153,255);//(0.78f, 0.78f, 0.8f, 1.0f);
				addButton.SetTitle("Add", UIControlState.Normal);
				addButton.SetTitleColor(UIColor.White, UIControlState.Normal);
				addButton.TouchUpInside += OnMoreButtonTouched;
				addButton.Layer.BorderWidth = .5f;
				addButton.Layer.CornerRadius = 5;
				scrollViewButtonView.AddSubview(addButton);

				editButton = UIButton.FromType(UIButtonType.Custom);
				editButton.BackgroundColor = UIColor.FromRGB(153,153,255);//FromRGBA(1.0f, 0.231f, 0.188f, 1.0f);
				editButton.SetTitle("Edit", UIControlState.Normal);
				editButton.SetTitleColor(UIColor.White, UIControlState.Normal);
				editButton.TouchUpInside += OnDeleteButtonTouched;
				editButton.Layer.BorderWidth = .5f;
				editButton.Layer.CornerRadius = 5;
				scrollViewButtonView.AddSubview(editButton);

				scrollViewContentView = new UIView();
				scrollViewContentView.BackgroundColor = UIColor.White;
				scrollView.AddSubview(scrollViewContentView);

				descLabel = new UILabel();
				clientLabel = new UILabel();
				scrollViewContentView.AddSubview(descLabel);
				scrollViewContentView.AddSubview(clientLabel);

				imageEng = new UIImage();
				imageEng = UIImage.FromFile("Engagement.png");

				imageView = new UIImageView();
				imageView.Image = imageEng;
				scrollViewContentView.AddSubview(imageView);


				NSNotificationCenter.DefaultCenter.AddObserver("SlidingCellHideMenuOptions", HideMenuOptions);

				var set = this.CreateBindingSet<EngagementCell, Engagement> ();
				//set.Bind(DescriptionLabel).To (item => item.Description);
				//set.Bind (ClientLabel).To (item => item.ClientName);

				set.Bind(descLabel).To (item => item.Description);
				set.Bind (clientLabel).To (item => item.ClientName);
				//set.Bind (_loader).To (item => item.volumeInfo.imageLinks.thumbnail); //smallThumbnail);
				set.Apply();


			});
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				addButton.TouchUpInside -= OnMoreButtonTouched;
				editButton.TouchUpInside -= OnDeleteButtonTouched;
			}

			base.Dispose(disposing);
		}

		public override UILabel TextLabel
		{
			get
			{
				return descLabel;
			}
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();

			var b = ContentView.Bounds;

			scrollView.Frame = new RectangleF(0, 0, b.Width, b.Height);
			scrollView.ContentSize = new SizeF(b.Width + catchWidth, b.Height);

			scrollViewButtonView.Frame = new RectangleF(b.Width - catchWidth, 0, catchWidth, b.Height);

			addButton.Frame = new RectangleF(0, 10, 280f / 2.0f, 60f);
			editButton.Frame = new RectangleF(300f / 2.0f, 10, 300f / 2.0f, 60f);

			scrollViewContentView.Frame = new RectangleF(0, 0, b.Width, b.Height);

			imageView.Frame = RectangleF.Inflate(new RectangleF(10, 20, 70, 70), -10, 0);
			//imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
			descLabel.Frame = RectangleF.Inflate(new RectangleF(80, 0, b.Width, b.Height), -10, 0);
			clientLabel.Frame = RectangleF.Inflate(new RectangleF(80, 25, b.Width, b.Height), -10, 0);
		}

		public override void PrepareForReuse()
		{
			base.PrepareForReuse();

			scrollView.SetContentOffset(PointF.Empty, false);
		}

		void OnDeleteButtonTouched (object sender, EventArgs e)
		{
			SingleTouchEvent (CommandTypeEnum.EditDocuments,(Engagement)this.DataContext);
			Console.WriteLine("Delete Touched");
		}

		void OnMoreButtonTouched (object sender, EventArgs e)
		{
			SingleTouchEvent (CommandTypeEnum.AddEvidence,(Engagement)this.DataContext);
			Console.WriteLine("More Touched");
		}

		void HideMenuOptions(NSNotification obj)
		{
			scrollView.SetContentOffset(PointF.Empty, true);
		}

		public class SlidingCellScrollDelegate : UIScrollViewDelegate
		{
			EngagementCell cell;
			public SlidingCellScrollDelegate(EngagementCell cell)
			{
				this.cell = cell;
			}

			public override void Scrolled(UIScrollView scrollView)
			{
				if (scrollView.ContentOffset.X < 0)
				{
					scrollView.ContentOffset = PointF.Empty;
				}        

				cell.scrollViewButtonView.Frame = new RectangleF(scrollView.ContentOffset.X + (cell.Bounds.Width - cell.catchWidth), 0f, cell.catchWidth, cell.Bounds.Height);                   
			}

			public override void WillEndDragging(UIScrollView scrollView, PointF velocity, ref PointF targetContentOffset)
			{
				if (scrollView.ContentOffset.X > 300f)//cell.catchWidth)
				{
					targetContentOffset.X = 300f; //cell.catchWidth;
				} else
				{
					targetContentOffset = PointF.Empty;

					InvokeOnMainThread(() =>
						{
							scrollView.SetContentOffset(PointF.Empty, true);
						});
				}
			}                     
		}
	}
}

