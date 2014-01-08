using ATFXamarin.Core.Common;
using ATFXamarin.Core.Services;
using Cirrious.MvvmCross.Plugins.PictureChooser;
using Cirrious.MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ATFXamarin.Core.ViewModels
{
	public class EvidenceViewModel : MvxViewModel
	{
		#region Init
		private readonly IMvxPictureChooserTask _pictureChooserTask;

		public void Init(string eng)
		{
			_Evidence.Engagement = Newtonsoft.Json.JsonConvert.DeserializeObject<Engagement>(eng); ;
			// _Engagement = Newtonsoft.Json.JsonConvert.DeserializeObject<Engagement>(eng); ;
		}


		public EvidenceViewModel(IMvxPictureChooserTask pictureChooserTask)
		{
			_Evidence = new Evidence();
			_pictureChooserTask = pictureChooserTask;
		}
		#endregion

		#region Properties

		private string _Notes = "Evidence Summary";
		public string Notes
		{
			get { return _Notes; }
			set { _Notes = value; RaisePropertyChanged(() => Notes);Update ();}
		}

		void Update()
		{
		}

		private Evidence _Evidence;
		public Evidence Evidence
		{
			get { return _Evidence; }
			set { _Evidence = value; RaisePropertyChanged(() => Evidence); }
		}

		private byte[] _bytes;
		public byte[] Bytes
		{
			get { return _bytes; }
			set { _bytes = value; RaisePropertyChanged(() => Bytes); }
		}
		#endregion

		#region Icommands
		private Cirrious.MvvmCross.ViewModels.MvxCommand _myCommand;
		public System.Windows.Input.ICommand CommandGoHome
		{
			get
			{
				_myCommand = _myCommand ?? new Cirrious.MvvmCross.ViewModels.MvxCommand(NavigateToEngagementView);
				return _myCommand;
			}
		}

		private Cirrious.MvvmCross.ViewModels.MvxCommand _SaveEvidenceCommand;
		public System.Windows.Input.ICommand SaveEvidenceCommand
		{
			get
			{
				_SaveEvidenceCommand = _SaveEvidenceCommand ?? new Cirrious.MvvmCross.ViewModels.MvxCommand(SaveEvidence);
				return _SaveEvidenceCommand;
			}
		}



		private Cirrious.MvvmCross.ViewModels.MvxCommand _takePictureCommand;
		public System.Windows.Input.ICommand TakePictureCommand
		{
			get
			{
				_takePictureCommand = _takePictureCommand ?? new Cirrious.MvvmCross.ViewModels.MvxCommand(DoTakePicture);
				return _takePictureCommand;
			}
		}



		private Cirrious.MvvmCross.ViewModels.MvxCommand _choosePictureCommand;
		public System.Windows.Input.ICommand ChoosePictureCommand
		{
			get
			{
				_choosePictureCommand = _choosePictureCommand ?? new Cirrious.MvvmCross.ViewModels.MvxCommand(DoChoosePicture);
				return _choosePictureCommand;
			}
		}
		#endregion

		#region Private Methods

		private void DoChoosePicture()
		{
			_pictureChooserTask.ChoosePictureFromLibrary(400, 95, OnPicture, () => { });
		}


		private void DoTakePicture()
		{
			_pictureChooserTask.TakePicture(400, 95, OnPicture, () => { });
		}

		private void OnPicture(Stream pictureStream)
		{
			var memoryStream = new MemoryStream();
			pictureStream.CopyTo(memoryStream);
			Bytes = memoryStream.ToArray();
		}

		public void SaveEvidence()
		{
			try
			{
				WebHeaderCollection headers = new WebHeaderCollection();
				// headers["filename"] = Evidence.FileName;
				headers["filename"] = String.Format("Evidence_{0}.jpg", Guid.NewGuid());
				headers["description"] = Evidence.Engagement.Description;
				headers["engagementid"] = Evidence.Engagement.Id.ToString();
				string url = Constants.URL + "Evidence";
				SimpleRestService restService = new SimpleRestService();
				//restService.FileToSend = Evidence.FileToSend;
				restService.FileToSend = Bytes;
				restService.PostRequest(url, headers, success=>{NavigateToEngagementView();}, error=>{});
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		public void SaveEvidence(Action<string> successAction, Action<Exception> errorAction)
		{
			try
			{
				WebHeaderCollection headers = new WebHeaderCollection();
				// headers["filename"] = Evidence.FileName;
				headers["filename"] = String.Format("Evidence_{0}.jpg", Guid.NewGuid());
				headers["description"] = Notes == null?Evidence.Engagement.Description:Notes;
				headers["engagementid"] = Evidence.Engagement.Id.ToString();
				string url = Constants.URL + "Evidence";
				SimpleRestService restService = new SimpleRestService();
				//restService.FileToSend = Evidence.FileToSend;
				restService.FileToSend = Bytes;
				restService.PostRequest(url, headers, successAction, errorAction);
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		public void NavigateToEngagementView()
		{
			ShowViewModel<EngagementViewModel>();
		}
		#endregion
	}



}