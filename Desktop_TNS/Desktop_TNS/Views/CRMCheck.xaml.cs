using Desktop_TNS.Models;
using Desktop_TNS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для CRMCheck.xaml
    /// </summary>
    public partial class CRMCheck : Window
    {
        public CRMCheck()
        {
            InitializeComponent();
        }
        /// <summary>
        /// exit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (DBWork.CheckSubscriber(Txt_Number.Text,Txt_Name.Text)) //TODO: проверка на существование такого имени и номера договора абонента
            {
                CRMNewRequest newReq = new CRMNewRequest();
                var dt = newReq.DataContext as NewRequestViewModel;
                dt.Bill = DBWork.GetBill(Txt_Number.Text);
                this.Close();
                newReq.Show();
            }
            else
                MessageBox.Show("Такого абонента нет.","Увы и ах");
        }

        private void Txt_Number_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
    }
}
