using Desktop_TNS.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Desktop_TNS.ViewModels
{
    class WindowViewModel : BaseVIewModel
    {
        Page _activePage;

        public Page ActivePage { get => _activePage; set { _activePage = value; OnPropertyChanged(); } }
        public WindowViewModel()
        {
            ActivePage = new MainPage();
        }
    }
}
