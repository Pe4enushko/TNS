using Desktop_TNS.Models;
using Desktop_TNS.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Desktop_TNS.ViewModels
{
    public class HardwareManagementViewModel : BaseVIewModel
    {
        DataTable shownData;
        DataTable h_highway, h_web, h_subs;
        int selectedTable, selectedRow;
        bool allToCheck;
        string info;

        DataTable H_highway { 
            get 
            {
                if(h_highway == null)
                {
                    h_highway = DBWork.GetHardwareInfo(2);
                    h_highway.Columns.Add("isBroken");
                }
                return h_highway;
            }
            set { h_highway = value;}
        }
        DataTable H_web { 
            get 
            { 
                if (h_web == null)
                {
                    h_web = DBWork.GetHardwareInfo(0);
                    h_web.Columns.Add("isBroken");
                }
                return h_web;
            }
            set { h_web = value;}
        }
        DataTable H_subs { 
            get 
            { 
                if(h_subs == null)
                {
                    h_subs = DBWork.GetHardwareInfo(1);
                    h_subs.Columns.Add("isBroken");
                }
                return h_subs;
            }
            set { h_subs = value; } 
        }

        public int SelectedTable { get => selectedTable; 
            set 
            { 
                selectedTable = value;
                OnPropertyChanged();
                switch (value)
                {
                    case 0: ShownData = H_web; break;
                    case 1: ShownData = H_subs; break;
                    case 2: ShownData = H_highway; break;
                    default: ShownData = null;
                        break;
                }
            } }
        public DataTable ShownData { get => shownData; set { shownData = value; OnPropertyChanged(); } }
        public bool AllToCheck { get => allToCheck; set { allToCheck = value; OnPropertyChanged(); } }
        public int SelectedRow { get => selectedRow; set { selectedRow = value; OnPropertyChanged(); } }
        public string Info { get => info; set { info = value; OnPropertyChanged(); } }
        
        public Command CheckCommand { get; set; }
        public Command GetInfoCommand { get; set; }

        public HardwareManagementViewModel()
        {
            SelectedTable = 0;
            GetInfoCommand = new Command(() =>
            {
                string temp = "";
                foreach (var item in ShownData.Rows[SelectedRow].ItemArray)
                {
                    temp += item.ToString() + "\n";
                }
                Info = temp;
            });
            CheckCommand = new Command(() =>
            {
                Check();
            });
        }
        public async void Check()
        {
            if (!ShownData.Columns.Contains("isBroken"))
            {
                ShownData.Columns.Add("isBroken");
            }
            if (allToCheck)
            {
                foreach (DataRow item in ShownData.Rows)
                {
                    if (await ApiWork.GetStatus(item.ItemArray[0].ToString()))
                    {
                        item["isBroken"] = "Неисправно";
                        CRMNewRequest newReq = new CRMNewRequest(true,item[0].ToString());
                        var dt = newReq.DataContext as NewRequestViewModel;
                        dt.Bill = item[0].ToString();
                        newReq.Show();
                    }
                    else
                    {
                        item["isBroken"] = "Исправно";
                    }
                }
                MessageBox.Show("Всё проверено!");
            }
            else
            {
                bool isBroken = await ApiWork.GetStatus(ShownData.Rows[SelectedRow].ItemArray[0].ToString());
                ShownData.Rows[SelectedRow]["isBroken"] = 
                    isBroken ? "Неисправно" : "Исправно";
                if (isBroken)
                {
                    CRMNewRequest newReq = new CRMNewRequest(true,ShownData.Rows[selectedRow][0].ToString());
                    var dt = newReq.DataContext as NewRequestViewModel;
                    dt.Bill = ShownData.Rows[SelectedRow][0].ToString();
                    newReq.Show();
                }
                GetInfoCommand.Execute(this);
                MessageBox.Show("Проверено.\n " + Info);
            }
        }
    }
}
