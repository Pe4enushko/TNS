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
        string otherBill;
        public CRMNewRequest(bool isSubscriberHardware = false, string otherBill = "")
        {
            InitializeComponent();
            Chb_Sub.IsChecked = isSubscriberHardware;
            this.otherBill = otherBill;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if ((bool)Chb_Sub.IsChecked)
            {
                var dc = this.DataContext as ViewModels.NewRequestViewModel;
                dc.Hardware.Add(otherBill);
                Cmb_Hardware.SelectedIndex = Cmb_Hardware.Items.Count-1;
            }
        }

        private async void btn_Check_Click(object sender, RoutedEventArgs e)
        {
            bool flag = false;
            try
            {
                ProgBar.Value = 50;            
                flag = await ApiWork.GetStatus(Cmb_Hardware.SelectedItem?.ToString());
                if (flag)
                {
                    ProgBar.Value = 100;
                    MessageBox.Show("Оборудование исправно");
                    Cmb_Status.SelectedIndex = 0;
                }
                else
                {
                    ProgBar.Value = 100;
                    MessageBox.Show("Оборудование неисправно");
                    Cmb_Status.SelectedIndex = 1;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            await Task.Delay(300);
            ProgBar.Value = 0;
        }

    }
}
