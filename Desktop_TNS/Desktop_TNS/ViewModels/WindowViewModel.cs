using Desktop_TNS.Views;
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
