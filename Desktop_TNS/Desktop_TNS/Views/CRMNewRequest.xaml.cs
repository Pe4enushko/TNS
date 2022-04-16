using Desktop_TNS.Models;
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
using System.Windows.Shapes;

namespace Desktop_TNS.Views
{
    /// <summary>
    /// Логика взаимодействия для CRMNewRequest.xaml
    /// </summary>
    public partial class CRMNewRequest : Window
    {
        public CRMNewRequest()
        {
            InitializeComponent();
        }

        private async void btn_Check_Click(object sender, RoutedEventArgs e)
        {
            bool flag = false;
            ProgBar.Value = 1;
            try
            {
                flag = await ApiWork.GetStatus(Cmb_Hardware.SelectedItem?.ToString());
                btn_Send.IsEnabled = true;
                if (flag)
                {
                    MessageBox.Show("Оборудование исправно");
                }
                else
                {
                    MessageBox.Show("Оборудование неисправно");
                    Cmb_Status.SelectedIndex = 1;
                }    
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
