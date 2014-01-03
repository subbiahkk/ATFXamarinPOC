using ATFXamarin.Core.Common;
using ATFXamarin.Core.Services;
using Cirrious.MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ATFXamarin.Core.ViewModels
{
    public class EvidenceViewModel : MvxViewModel
    {


        public void Init(string eng)
        {
            _Evidence.Engagement = Newtonsoft.Json.JsonConvert.DeserializeObject<Engagement>(eng); ;
           // _Engagement = Newtonsoft.Json.JsonConvert.DeserializeObject<Engagement>(eng); ;
        }


        public EvidenceViewModel()
        {
            _Evidence = new Evidence();
        }

       
        

        private Evidence _Evidence;

        public Evidence Evidence
        {
            get { return _Evidence; }
            set { _Evidence = value; RaisePropertyChanged(() => Evidence); }
        }


        private Cirrious.MvvmCross.ViewModels.MvxCommand _myCommand;
        public System.Windows.Input.ICommand CommandGoHome
        {
            get
            {
                _myCommand = _myCommand ?? new Cirrious.MvvmCross.ViewModels.MvxCommand(DoMyCommand);
                return _myCommand;
            }
        }

        public void DoMyCommand()
        {
            ShowViewModel<EngagementViewModel>();
        }
        
        public void SaveEvidence(Action<string> successAction, Action<Exception> errorAction)
        {
            WebHeaderCollection headers = new WebHeaderCollection();
            headers["filename"] = Evidence.FileName;
            headers["description"] = Evidence.Engagement.Description;
            headers["engagementid"] = Evidence.Engagement.Id.ToString();           
            string url = Constants.URL + "Evidence";
            SimpleRestService restService = new SimpleRestService();
            restService.FileToSend = Evidence.FileToSend;
            restService.PostRequest(url,headers,successAction,errorAction);
        }
    }



}
