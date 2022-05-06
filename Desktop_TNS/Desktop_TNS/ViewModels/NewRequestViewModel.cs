using Desktop_TNS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_TNS.ViewModels
{
    public class NewRequestViewModel : BaseVIewModel
    {
        DateTime created, closed;
        string bill, problem, request_id;
        string service, serviceType, serviceKind, problemKind, selectedHardware,status;
        bool isSub;

        public ObservableCollection<string> Hardware { get; set; } = new ObservableCollection<string>();
        public DateTime Created { get => created; set { created = value; OnPropertyChanged(); SetId(); } }
        public DateTime Closed { get => closed; set { closed = value;OnPropertyChanged();} }
        public string Bill { get => bill; set { bill = value;OnPropertyChanged(); SetId(); } }
        public string Problem { get => problem; set { problem = value;OnPropertyChanged();} }
        public string Request_id { get => request_id; set { request_id = value;OnPropertyChanged();} }
        public string Service { get => service; set { service = value; OnPropertyChanged(); } }
        public string ServiceType { get => serviceType; set { serviceType = value; OnPropertyChanged();} }
        public string ServiceKind { get => serviceKind; set { serviceKind = value; OnPropertyChanged(); } }
        public string ProblemKind { get => problemKind; set { problemKind = value; OnPropertyChanged();} }
        public string SelectedHardware { get => selectedHardware; set { selectedHardware = value; OnPropertyChanged(); } }
        public string Status { get => status; set { status = value; OnPropertyChanged(); } }
        public bool IsSub { get => isSub; set { isSub = value; OnPropertyChanged(); } }
        public Command SendCommand { get; set; }

        public NewRequestViewModel()
        {
            Created = DateTime.Now;
            Closed = DateTime.Now;
            Hardware = DBWork.GetHardwareSeries();
            SendCommand = new Command(() => 
            {
                DBWork.InsertCRMRequest(Request_id,Created,Closed,Bill,Service,ServiceKind,ServiceType,Status,"NULL",Problem,ProblemKind);
            });
        }
        /// <summary>
        /// 
        /// </summary>
        void SetId()
        {
            Request_id = $"{bill}/{Created:dd/MM/yyyy}";
        }
    }
}
