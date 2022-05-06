using Desktop_TNS.Models;
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
    /// Логика взаимодействия для HardwareManagementPage.xaml
    /// </summary>
    public partial class HardwareManagementPage : Page
    {
        public HardwareManagementPage()
        {
            InitializeComponent();
        }

        private void Dg_hardware_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dc = this.DataContext as HardwareManagementViewModel;
            dc.CheckCommand.Execute(this);
        }
    }
}
