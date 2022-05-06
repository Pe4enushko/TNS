using Desktop_TNS.Models;
using System.Windows;
using System.Windows.Input;

namespace Desktop_TNS.Views.AddHardware
{
    /// <summary>
    /// Логика взаимодействия для AddHighwayHardwareWindow.xaml
    /// </summary>
    public partial class AddHighwayHardwareWindow : Window
    {
        public AddHighwayHardwareWindow()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = char.IsDigit((char)e.Key);
        }

        private void Btn_Ok_Click(object sender, RoutedEventArgs e)
        {
            if(DBWork.AddHighWayHardware(Txt_Series.Text,Txt_Title.Text,double.Parse(Txt_Frequency.Text),Txt_FadeKoef.Text,Txt_Transfer.Text,Txt_Address.Text))
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
