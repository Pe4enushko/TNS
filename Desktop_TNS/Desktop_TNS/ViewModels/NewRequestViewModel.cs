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

        public ObservableCollection<string> Hardware { get; set; } = new ObservableCollection<string>();
        public DateTime Created { get => created; set { created = value; OnPropertyChanged(); SetId(); } }
        public DateTime Closed { get => closed; set { closed = value;OnPropertyChanged();} }
        public string Bill { get => bill; set { bill = value;OnPropertyChanged(); SetId(); } }
        public string Problem { get => problem; set { problem = value;OnPropertyChanged();} }
        public string Request_id { get => request_id; set { request_id = value;OnPropertyChanged();} }
        public NewRequestViewModel()
        {
            Created = DateTime.Now;
            Closed = DateTime.Now;
            Hardware = DBWork.GetHardwareSeries();
        }
        void SetId()
        {
            Request_id = $"{bill}/{Created:dd/MM/yyyy}";
        }
    }
}
