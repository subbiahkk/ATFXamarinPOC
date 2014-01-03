using ATFXamarin.Core.Services;
using Cirrious.MvvmCross.ViewModels;
using System.Collections.Generic;


namespace ATFXamarin.Core.ViewModels
{
    public class EngagementViewModel 
		: MvxViewModel
    {

        public EngagementViewModel(IEngagementService engagementService)
        {
            engagementService.GetAllEngagements(result => Engagements = result,
                error => { });
        }

        

        private List<Engagement> _Engagements;

        public List<Engagement> Engagements
        {
            get { return _Engagements; }
            set { _Engagements = value; RaisePropertyChanged(() => Engagements); }
        }


        private Cirrious.MvvmCross.ViewModels.MvxCommand<Engagement> _ItemSelectedCommand;
        public System.Windows.Input.ICommand ItemSelectedCommand
        {
            get
            {
                _ItemSelectedCommand = _ItemSelectedCommand ?? new Cirrious.MvvmCross.ViewModels.MvxCommand<Engagement>(ShowEvidence);
                return _ItemSelectedCommand;
            }
        }

        private void ShowEvidence(Engagement engagement)
        {
            string strEngagement = Newtonsoft.Json.JsonConvert.SerializeObject(engagement);
            ShowViewModel<EvidenceViewModel>(new { eng = strEngagement });
           
           
        }

    }
}
