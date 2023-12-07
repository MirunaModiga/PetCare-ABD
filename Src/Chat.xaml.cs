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
        baza_PetCareDataContext context = new baza_PetCareDataContext();

        public Chat(string user)
        {
            InitializeComponent();
            this.user = user;

            var FullName = (from u in context.Users where u._Username == user select u._FullName).FirstOrDefault();
            Username.Content = FullName;

            int userID= (from u in context.Users where u._Username == user select u.IDUser).FirstOrDefault();

            DataContext = new MainViewModel(userID);

        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            HomePage home = new HomePage(this.user);
            home.Show();
            this.Close();
        }

        private void CloseBtnPet_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
