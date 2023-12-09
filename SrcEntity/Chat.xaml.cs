using myPetCare.MVVM.ViewModel;
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
    /// Interaction logic for Chat.xaml
    /// </summary>
    public partial class Chat : Window
    {
        string user;
        PetCareEntities context = new PetCareEntities();

        public Chat(string user)
        {
            InitializeComponent();
            this.user = user;

            var FullName = (from u in context.Users where u.C_Username == user select u.C_FullName).FirstOrDefault();
            Username.Content = FullName;

            int userID= (from u in context.Users where u.C_Username == user select u.IDUser).FirstOrDefault();

            DataContext = new MainViewModel(userID);

        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            string userType = (from user in context.Users
                               where user.C_Username == this.user
                               select user.C_usertype).FirstOrDefault();

            if (userType == "Owner")
            {
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

        private void CloseBtnPet_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
