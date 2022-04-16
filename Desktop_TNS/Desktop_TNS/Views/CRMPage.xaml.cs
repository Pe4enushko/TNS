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
    /// Логика взаимодействия для CRMPage.xaml
    /// </summary>
    public partial class CRMPage : Page
    {
        public CRMPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var check = new CRMCheck();
            check.Show();
        }
    }
}
