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

namespace testnou
{
    /// <summary>
    /// Interaction logic for HomePageVet.xaml
    /// </summary>
    public partial class HomePageVet : Window
    {
        public object SelectedWorkDay { get; set; }
        string user;
        public HomePageVet(string user)
        {
            InitializeComponent();
            this.user = user;
            string[] daysOfWork = new[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            DayOfWork.ItemsSource = daysOfWork;
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_BtnVet.IsChecked == true)
            {
                tt_homevet.Visibility = Visibility.Collapsed;
                tt_health .Visibility = Visibility.Collapsed;
                tt_app.Visibility = Visibility.Collapsed;
                tt_info.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_homevet.Visibility = Visibility.Visible;
                tt_health.Visibility = Visibility.Visible;
                tt_app.Visibility = Visibility.Visible;
                tt_info.Visibility = Visibility.Visible;
            }
        }

        private void Tg_BtnVet_Unchecked(object sender, RoutedEventArgs e)
        {
            img_bgVet.Opacity = 1;
        }

        private void Tg_BtnVet_Checked(object sender, RoutedEventArgs e)
        {
            img_bgVet.Opacity = 0.3;
        }

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_BtnVet.IsChecked = false;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            StartUp start = new StartUp();
            start.Show();
            this.Close();
        }
        
        private void VetAboutUs_Click(object sender, RoutedEventArgs e)
        {
            AboutUs info = new AboutUs(this.user);
            info.Show();
            this.Close();
        }

        private void DayOfWork_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.SelectedWorkDay = DayOfWork.SelectedItem;
            string workDay = (string)this.SelectedWorkDay;

            if (workDay == null)
            {
                MessageBox.Show("Please choose a day when you want to work!");
                return;
            }

            var context = new baza_PetCareDataContext();

            var customer = (from c in context.Users

                            where c._Username == user
                            select c).FirstOrDefault();

            if (customer != null)
            {
                customer._workDate = workDay;
                context.SubmitChanges();
                MessageBox.Show("Working date registration successful!");
            }
            else
            {
                MessageBox.Show("User not found or null");
            }
        }
    }
}
