using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace myPetCare
{
    /// <summary>
    /// Interaction logic for Medication.xaml
    /// </summary>
    public partial class Medication : Window
    {
        string user;
        int userID = 0;

        baza_PetCareDataContext context = new baza_PetCareDataContext();

        public Medication(string user)
        {
            InitializeComponent();
            PetList.Items.Clear();
            this.user = user;
            userID = (from u in context.Users where u._Username == user select new { u.IDUser }).SingleOrDefault().IDUser;

            var petNames = (from pet_obj in context.Pets
                            where pet_obj._OwnerID == userID
                            select pet_obj._petName).ToList();

            string[] petNamesArray = petNames.ToArray();

            PetList.ItemsSource = petNamesArray;
        }

        private void CloseBtnPet_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            HomePage home = new HomePage(this.user);
            home.Show();
            this.Close();
        }

        private void TrackMedButton_Click(object sender, RoutedEventArgs e)
        {
            TrackMed track = new TrackMed(this.user,PetList.SelectedItem as string);
            track.Show();
            this.Close();
        }
    }
}
