using Cirrious.MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATFXamarin.Core.ViewModels
{
	public class WOPIViewModel : MvxViewModel
	{
		public void Init(string eng)
		{
			//  _Evidence.Engagement = Newtonsoft.Json.JsonConvert.DeserializeObject<Engagement>(eng); ;
			// _Engagement = Newtonsoft.Json.JsonConvert.DeserializeObject<Engagement>(eng); ;
		}


		private string _URL = "http://eyowaserver.cloudapp.net:1234/";
		public string URL
		{
			get { return _URL; }
			set { _URL = value; RaisePropertyChanged(() => URL); }
		}

		private string _EngagementDesc = "Sample Engageement";
		public string EngagementDesc
		{
			get { return _EngagementDesc; }
			set { _EngagementDesc = value; RaisePropertyChanged(() => EngagementDesc); }
		}
	}
}