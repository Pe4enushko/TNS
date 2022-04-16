using Desktop_TNS.Models;
using Desktop_TNS.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Desktop_TNS.ViewModels
{
    public class MainViewModel : BaseVIewModel
    {
        Roles role;
        GridLength smallWidth = new GridLength(30, GridUnitType.Pixel);
        GridLength bigWidth = new GridLength(1, GridUnitType.Star);
        GridLength _menuWidth = new GridLength(1, GridUnitType.Star);
        bool _isCollapsed = false;
        string _pageTag;
        Page _functionalPage;
        ObservableCollection<string> _events;
        public Page FunctionalPage { get => _functionalPage; set {_functionalPage = value; OnPropertyChanged(); PageTag = value.Tag?.ToString() ?? "NoTag"; } }
        public bool IsCollapsed { get => _isCollapsed; set { _isCollapsed = value; OnPropertyChanged(); } }
        public Command CollapseCommand { get; set; }
        public GridLength MenuWidth { get => _menuWidth; set { _menuWidth = value; OnPropertyChanged(); } }
        public ObservableCollection<string> Events { get => _events; set { _events = value; OnPropertyChanged(); } }
        public string PageTag { get => _pageTag; set { _pageTag = value; OnPropertyChanged(); } }

        public MainViewModel()
        {
            Properties.Settings.Default.RoleNumber = 1.ToString();
            Properties.Settings.Default.Save();
            role = (Roles)int.Parse(Properties.Settings.Default.RoleNumber);
            Events = DBWork.ConvertListToObsCol(DBWork.GetEvents(role));
            CollapseCommand = new Command(() =>
            {
                IsCollapsed = !IsCollapsed;
                MenuWidth = IsCollapsed ? smallWidth : bigWidth;
            });
            FunctionalPage = new Views.SubscribersPage();
        }
        public void DoChangePage(string buttonTag)
        {
            switch (buttonTag)
            {
                case "Subscribers": FunctionalPage = new SubscribersPage(); break;
                case "Actives": FunctionalPage = new ActivesPage();  break;
                case "Hardware": FunctionalPage = new Views.HardwareManagementPage(); break;
                case "Billing": FunctionalPage = new BillingPage(); break;
                case "Support": FunctionalPage = new SupportPage(); break;
                case "CRM": FunctionalPage = new CRMPage(); break;
                default: MessageBox.Show("Ошибка! Страница не указана!");
                    break;
            }
        }
    }
}
