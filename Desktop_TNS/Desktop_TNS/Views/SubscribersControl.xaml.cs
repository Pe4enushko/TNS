using Desktop_TNS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Desktop_TNS.Views
{
    /// <summary>
    /// Логика взаимодействия для SubscribersControl.xaml
    /// </summary>
    public partial class SubscribersControl : UserControl
    {
        public SubscribersControl()
        {
            InitializeComponent();
        }
        private void DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var context = this.DataContext as SubscribersViewModel;
            context.ChooseSubscriber.Execute(this);
            
        }
    }
}
