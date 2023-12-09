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
    public partial class Medications : Window
    {
        string user;
        int userID = 0;

        PetCareEntities context = new PetCareEntities();

        public Medications(string user)
        {
            InitializeComponent();
            PetList.Items.Clear();
            this.user = user;
            userID = (from u in context.Users where u.C_Username == user select new { u.IDUser }).SingleOrDefault().IDUser;

            var petNames = (from pet_obj in context.Pets
                            where pet_obj.C_OwnerID == userID
                            select pet_obj.C_petName).ToList();

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
