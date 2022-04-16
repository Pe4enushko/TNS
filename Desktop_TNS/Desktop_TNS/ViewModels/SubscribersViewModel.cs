using Desktop_TNS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Desktop_TNS.ViewModels
{
    public class SubscribersViewModel : BaseVIewModel
    {
        bool active = false, unactive = false;
        //string currentSubscriberId;
        DataTable allSubscribers;
        DataRow currentSubscriber;
        string currentSubInfo;
        int _selectedInd;
        DataTable CurrentSub;
        public DataTable ShownData { get => allSubscribers; 
            set { allSubscribers = value; OnPropertyChanged(); } }

        public bool Active { get => active; set { active = value; OnPropertyChanged(); ChangeSubscribersCommand.Execute(this); } }
        public bool Unactive { get => unactive; set { unactive = value; OnPropertyChanged(); ChangeSubscribersCommand.Execute(this); } }
        public string CurrentSubscriberId { get => ShownData?.Rows[SelectedInd > 0 ? SelectedInd : 0]?.ItemArray[0].ToString() ?? null;}
        public string CurrentSubInfo { get => currentSubInfo; set { currentSubInfo = value; OnPropertyChanged(); } }

        public int SelectedInd { get => _selectedInd; set { _selectedInd = value; OnPropertyChanged(); } }

        public Command ChangeSubscribersCommand;
        public Command ChooseSubscriber;
        public SubscribersViewModel()
        {
            ChooseSubscriber = new Command(() => 
            {
                CurrentSub = DBWork.GetOneSubscriberData(CurrentSubscriberId,Unactive);
                CurrentSubInfo = GetInfo();
            });
            ChangeSubscribersCommand = new Command(() =>
            {
                if (Active)
                {
                    if (Unactive)
                    {
                        ShownData = DBWork.GetAllSubscribersShortData(2);
                    }
                    else
                    {
                        ShownData = DBWork.GetAllSubscribersShortData(1);
                    }
                }
                else
                {
                    if (Unactive)
                    {
                        ShownData = DBWork.GetAllSubscribersShortData(0);
                    }
                    else
                    {
                        ShownData.Clear();
                        CurrentSub.Clear();
                    }
                }
            });
        }
        string GetInfo()
        {
            string info = "";
            //[subscriber_Id], [fullname], [passport], [passport_date], [contract_Id]," +
            //    $" [contract_date], [contract_type]{(isTerminated ? ", [termination_date],[termination_cause]" : ""
            List<string> headers = new List<string>() { "ID", "ФИО","серия и номер паспорта",
                "дата получения паспорта","номер договора", "дата заключения договора", "тип договора" };
            if (CurrentSub != null)
            {
                if (CurrentSub.Rows[0].ItemArray.Length > 7)
                {
                    headers.AddRange( new string[] {"дата расторжения","причина расторжения" });
                }
                for (int param = 0; param < CurrentSub.Rows[0].ItemArray.Length; param++)
                {
                    info += "\t" + headers[param] + "\n";
                    info += CurrentSub.Rows[0].ItemArray[param].ToString() + "\n";
                    info += "---------------------------------------- \n";
                }
            }
            return info;
        }
    }
}
