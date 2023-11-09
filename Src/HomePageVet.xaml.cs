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
        public HomePageVet()
        {
            InitializeComponent();
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            // Set tooltip visibility

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
            profile.Opacity = 1;
        }

        private void Tg_BtnVet_Checked(object sender, RoutedEventArgs e)
        {
            img_bgVet.Opacity = 0.3;
            profile.Opacity = 0.3;
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
    }
}
