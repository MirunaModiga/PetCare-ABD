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

namespace myPetCare
{
    /// <summary>
    /// Interaction logic for AboutUs.xaml
    /// </summary>
    public partial class AboutUs : Window
    {
        string user;

        baza_PetCareDataContext context = new baza_PetCareDataContext();

        public AboutUs(string user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void CloseBtnPet_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string userType = (from user in context.Users
                              where user._Username == this.user
                              select user._usertype).FirstOrDefault();

            if(userType == "Owner") {
                HomePage home = new HomePage(this.user);
                home.Show();
                this.Close();
            }
            else
            {
                HomePageVet home = new HomePageVet(this.user);
                home.Show();
                this.Close();
            }
        }
    }
}
