using ATFXamarin.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATFXamarin.Core.Services
{
    
    public class Engagement
    {
        public Engagement()
        {
            ClientName = "Coca Cola";
            ImageURL = string.Format("http://placekitten.com/300/300");
        }

        public int Id { get; set; }
        
        public string Description { get; set; }

        public List<Evidence> Evidences { get; set; }

        public string ClientName { get; set; }


        public string ImageURL { get; set; }
    }

    //public class EngagementList : IEnumerable<Engagement>
    //{
    //    public List<Engagement> Items { get; set; }
    //}

    public class Task
    {
        
        public int Id { get; set; }
        
        public string Description { get; set; }
        
        public int EngagementId { get; set; }

        public List<Evidence> Evidences { get; set; }

        
    }

    
    public class Evidence
    {
        
        public int Id { get; set; }
        
        public string Description { get; set; }

        //public int EngagementId { get; set; }
        
        public string FileName { get; set; }

        public string FileLocalPath { get; set; }
        
        public string FilePath { get; set; }

        public byte[] FileToSend { get; set; }

        public Engagement Engagement{ get; set; }
        
    }

    public interface IEngagementService
    {
        void GetAllEngagements(Action<List<Engagement>> Success, Action<Exception> Error);
    }

    public interface ITaskService
    {
        void GetAllTasks(string engagementId,Action<List<Task>> Success, Action<Exception> Error);
    }

    public interface IEvidenceService
    {
        void GetAllEvidence(string taskId, Action<List<Task>> Success, Action<Exception> Error);

        void SaveEvidence(Evidence evidence, Action<List<Task>> Success, Action<Exception> Error);
    }

    public class EngagementService : IEngagementService
    {
        private readonly ISimpleRestService _SimpleRestService;        

        public EngagementService(ISimpleRestService simpleRestService)
        {
            _SimpleRestService = simpleRestService;
        }

        public void GetAllEngagements(Action<List<Engagement>> success, Action<Exception> error)
        {
        _SimpleRestService.MakeRequest<List<Engagement>>(
                    Constants.URL + "Engagements", "Get", success, error);              
        }
    }

    public class TaskService : ITaskService
    {
        private readonly ISimpleRestService _SimpleRestService;
       

        public TaskService(ISimpleRestService simpleRestService)
        {
            _SimpleRestService = simpleRestService;
        }
              

        public void GetAllTasks(string engagementId, Action<List<Task>> Success, Action<Exception> Error)
        {

            _SimpleRestService.MakeRequest<List<Task>>(
                    //"http://xamarincloudrestservice.cloudapp.net/XamarinATFService.svc/task", "Get", Success, Error);
              "http://localhost/XamarinATFService.svc/Engagements", "Get", Success, Error);
           
           
        }
    }


    public class EvidenceService : IEvidenceService
    {
        private readonly ISimpleRestService _SimpleRestService;       


        public EvidenceService(ISimpleRestService simpleRestService)
        {
            _SimpleRestService = simpleRestService;
        }

        public void GetAllEvidence(string taskId, Action<List<Task>> Success, Action<Exception> Error)
        {
            throw new NotImplementedException();
        }

        public void SaveEvidence(Evidence evidence, Action<List<Task>> Success, Action<Exception> Error)
        {
            throw new NotImplementedException();
        }
    }
}
