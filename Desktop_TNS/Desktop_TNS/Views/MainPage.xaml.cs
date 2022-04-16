using Desktop_TNS.Models;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Configuration;
using System.IO;
using Desktop_TNS.ViewModels;

namespace Desktop_TNS.Views
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        DataTable NamesAndIDs;
        public MainPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Roles role = (Roles)int.Parse(Properties.Settings.Default.RoleNumber);
            // Блокирование кнопок
            switch (role)
            {
                case Roles.clientLead:
                    Btn_Subs.IsEnabled = Btn_CRM.IsEnabled = Btn_Billing.IsEnabled = true;
                    break;
                case Roles.clientManager:
                    Btn_Subs.IsEnabled = Btn_CRM.IsEnabled = true;
                    break;
                case Roles.techLead:
                    Btn_Subs.IsEnabled = Btn_Support.IsEnabled = Btn_CRM.IsEnabled = Btn_Support.IsEnabled = true;
                    break;
                case Roles.techSupport:
                    Btn_Subs.IsEnabled = Btn_Support.IsEnabled = Btn_CRM.IsEnabled = Btn_Hardware.IsEnabled = true;
                    break;
                case Roles.accounter:
                    Btn_Subs.IsEnabled = Btn_Billing.IsEnabled = Btn_Actives.IsEnabled = true;
                    break;
                case Roles.growDirector:
                    Btn_Subs.IsEnabled = Btn_Support.IsEnabled = Btn_CRM.IsEnabled = Btn_Support.IsEnabled = Btn_Billing.IsEnabled = Btn_Actives.IsEnabled = true;
                    break;
                case Roles.techClerk:
                    Btn_Subs.IsEnabled = Btn_Actives.IsEnabled = Btn_CRM.IsEnabled = Btn_Hardware.IsEnabled = true;
                    break;
                default:
                    break;
            }
            NamesAndIDs = DBWork.GetEmployees();
            List<string> names = new List<string>();
            foreach (DataRow row in NamesAndIDs.Rows)
            {
                names.Add(row.ItemArray[0].ToString());
            }
            Cmb_Role.ItemsSource = names;
            Cmb_Role.SelectedIndex = 0;
            ChangeProfilePicture();
        }
        private void ChangeProfilePicture()
        {
            string name = Cmb_Role.SelectedItem.ToString();
            BitmapImage bmpSrc = new BitmapImage();
            Bitmap bm;
            foreach (DataRow row in NamesAndIDs.Rows)
            {
                //TODO: Протестить эту парашу, я тут по ролям входы делал и аву пытаюсь менять челам, а ещё приложуха запоминает кто вошёл
                if (row.ItemArray[0].ToString() == name)
                {
                    bm = (Bitmap)Properties.Resources.ResourceManager.GetObject("ID" + row.ItemArray[1].ToString());
                    bmpSrc = GetProfilePic(bm);
                    break;
                }
                else
                {
                    bm = (Bitmap)Properties.Resources.ResourceManager.GetObject("NoName");
                    bmpSrc = GetProfilePic(bm);
                }
            }
            Img_Profile.Fill = new ImageBrush(bmpSrc);
        }
        BitmapImage GetProfilePic(Bitmap bmp)
        {
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            using(MemoryStream memory = new MemoryStream())
            {
                bmp.Save(memory, System.Drawing.Imaging.ImageFormat.Jpeg);
                memory.Position = 0;
                img.StreamSource = memory;
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.EndInit();
                img.Freeze();
            }
            return img;
        }

        private void Cmb_Role_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string name = Cmb_Role.SelectedItem.ToString();
            Properties.Settings.Default.EmployeeName = name;
            Properties.Settings.Default.RoleNumber = ((int)DBWork.GetEmployeeRole(name)).ToString();
            Properties.Settings.Default.Save();
            
            ChangeProfilePicture();
        }

        private void ChangePageByButton(object sender, System.Windows.RoutedEventArgs e)
        {
            var dc = this.DataContext as MainViewModel;
            var btn = sender as Button;
            dc.DoChangePage(btn.Tag.ToString());
        }
    }
}
