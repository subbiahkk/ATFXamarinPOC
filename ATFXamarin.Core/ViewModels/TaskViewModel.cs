using ATFXamarin.Core.Services;
using Cirrious.MvvmCross.ViewModels;
using System.Collections.Generic;


namespace ATFXamarin.Core.ViewModels
{
    public class TaskViewModel 
		: MvxViewModel
    {

        public TaskViewModel(ITaskService taskService)
        {
            taskService.GetAllTasks("1", result => Tasks = result,
                error => { });

        }
        

        private List<Task> _Tasks;

        public List<Task> Tasks
        {
            get { return _Tasks; }
            set { _Tasks = value; RaisePropertyChanged(() => Tasks); }
        }


        //private Cirrious.MvvmCross.ViewModels.MvxCommand<Engagement> _myCommand;
        //public System.Windows.Input.ICommand MyCommand
        //{
        //    get
        //    {
        //        _myCommand = _myCommand ?? new Cirrious.MvvmCross.ViewModels.MvxCommand<Engagement>(DoMyCommand);
        //        return _myCommand;
        //    }
        //}

        //private void DoMyCommand(Engagement eng)
        //{
        //    Hello = Hello + " World";
        //}

    }
}
