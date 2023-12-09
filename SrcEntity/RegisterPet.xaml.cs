using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Globalization;

namespace myPetCare
{
    /// <summary>
    /// Interaction logic for RegisterPet.xaml
    /// </summary>
    public partial class RegisterPet : Window
    {
        string user;
        int userID = 0;
        public string petType { get; set; }
        public string photoPath { get; set; }

        PetCareEntities context = new PetCareEntities();

        public RegisterPet(string user)
        {
            InitializeComponent();
            this.user = user;
            userID = this.find_id();
        }

        private void CloseBtnPet_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void txtPetName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(petname.Text) && petname.Text.Length > 0)
                petnameplace.Visibility = Visibility.Collapsed;
            else
                petnameplace.Visibility = Visibility.Visible;
        }

        private void textPetName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            petname.Focus();
        }

        private void txtPetBreed_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(petname1.Text) && petname1.Text.Length > 0)
                petnameplace1.Visibility = Visibility.Collapsed;
            else
                petnameplace1.Visibility = Visibility.Visible;
        }

        private void textPetBreed_MouseDown(object sender, MouseButtonEventArgs e)
        {
            petname1.Focus();
        }

        private void txtPetBirth_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(petname2.Text) && petname2.Text.Length > 0)
                petnameplace2.Visibility = Visibility.Collapsed;
            else
                petnameplace2.Visibility = Visibility.Visible;
        }

        private void textPetBirth_MouseDown(object sender, MouseButtonEventArgs e)
        {
            petname2.Focus();
        }

        private void txtPetColor_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(petname3.Text) && petname3.Text.Length > 0)
                petnameplace3.Visibility = Visibility.Collapsed;
            else
                petnameplace3.Visibility = Visibility.Visible;
        }

        private void textPetColor_MouseDown(object sender, MouseButtonEventArgs e)
        {
            petname3.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HomePage home = new HomePage(this.user);
            home.Show();
            this.Close();
        }

        private void UploadPhotoButton_Clicked(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();

            bool? response = openFileDialog.ShowDialog();
            if (response == true)
            {
                string filepath = openFileDialog.FileName;
                UPLOAD.Text = filepath;
                UPLOAD.Text = "";

                BitmapImage MyImageSource = new BitmapImage(new Uri(filepath, UriKind.Relative));
                Image image = new Image();

                image.Source = MyImageSource;
                image.Width = 80;
                image.Height = 80;
                image.Stretch= Stretch.Uniform;
                image.Visibility = Visibility.Visible;
                InlineUIContainer container = new InlineUIContainer(image);

                UPLOAD.Inlines.Add(container);
                this.photoPath = filepath;
            }
            else
            {
                this.photoPath = null;
            }
        }

        private void PETRegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string pet_name = petname.Text;
            string breed = petname1.Text;
            string birth = petname2.Text;
            string color=petname3.Text;
            string sex;
            string pet_type=this.petType;

            if (pet_type == null)
            {
                MessageBox.Show("Please choose a pet type!");
                return;
            }

            if (Female.IsChecked == true) { sex = "Feminin"; }
            else if (Male.IsChecked == true) { sex = "Masculin"; }
            else
            {
                MessageBox.Show("Please select a gender!");
                return;
            }

            if (string.IsNullOrWhiteSpace(pet_name))
            {
                MessageBox.Show("Please enter a pet name!");
                return;
            }
            if (string.IsNullOrWhiteSpace(breed))
            {
                MessageBox.Show("Please enter a breed!");
                return;
            }
            if (string.IsNullOrWhiteSpace(birth))
            {
                MessageBox.Show("Please enter an date!");
                return;
            }
            if (string.IsNullOrWhiteSpace(color))
            {
                MessageBox.Show("Please enter an color!");
                return;
            }

            var new_pet = new Pet
            {
                C_petName = pet_name,
                C_sex = sex,
                C_color = color,
                C_breed = breed,
                C_birthdate = DateTime.ParseExact(birth, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                C_petType = pet_type,
                C_OwnerID = this.userID,
                C_photo = photoPath
            };

            context.Pets.Add(new_pet);
            context.SaveChanges();

            MessageBox.Show("Pet registration successful!");

            petname.Text = "";
            petname1.Text = "";
            petname2.Text = "";
            petname3.Text = "";
            Female.IsChecked = false;
            Male.IsChecked = false;
        }

        private void DogButton_Click(object sender, RoutedEventArgs e)
        {
            this.petType = "Dog";
        }

        private void CatButton_Click(object sender, RoutedEventArgs e)
        {
            this.petType = "Cat";
        }

        private void BirdButton_Click(object sender, RoutedEventArgs e)
        {
            this.petType = "Bird";
        }

        private void OtherButton_Click(object sender, RoutedEventArgs e)
        {
            this.petType = "Other";
        }

        private int find_id()
        {
            int us = (from u in context.Users where u.C_Username == user select new { u.IDUser }).SingleOrDefault().IDUser;
            return us;
        }
    }
}
