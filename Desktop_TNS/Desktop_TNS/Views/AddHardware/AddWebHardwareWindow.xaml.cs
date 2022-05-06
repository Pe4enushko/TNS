using Desktop_TNS.Models;
using System.Windows;
using System.Windows.Input;

namespace Desktop_TNS.Views.AddHardware
{
    /// <summary>
    /// Логика взаимодействия для AddWebHardwareWindow.xaml
    /// </summary>
    public partial class AddWebHardwareWindow : Window
    {
        public AddWebHardwareWindow()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = char.IsDigit((char)e.Key);
        }

        private void Btn_Ok_Click(object sender, RoutedEventArgs e)
        {
            if(DBWork.AddWebHardware(Txt_Series.Text,Txt_Title.Text,Txt_Ports.Text,
                Txt_TransferStandard.Text,double.Parse(Txt_Frequency.Text),Txt_Interfaces.Text,Txt_Speed.Text,Txt_Address.Text))
            {
                MessageBox.Show("Готово");
            }
            else
            {
                MessageBox.Show("Всё хреново");
            }
        }
    }
}
