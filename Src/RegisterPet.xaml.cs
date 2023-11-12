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

namespace testnou
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

        public RegisterPet(string user)
        {
            InitializeComponent();
            this.user = user;
            userID = this.find_id();
        }

        private void CloseBtnPet_Click(object sender, RoutedEventArgs e)
        {
            Close();
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

            var context = new baza_PetCareDataContext();

            var new_pet = new Pet
            {
                _petName = pet_name,
                _sex = sex,
                _color = color,
                _breed = breed,
                _birthdate = DateTime.ParseExact(birth, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                _petType = pet_type,
                _OwnerID = this.userID,
                _photo = photoPath
            };

            context.Pets.InsertOnSubmit(new_pet);
            context.SubmitChanges();

            MessageBox.Show("Pet registration successful!");

            petname.Text = "";
            petname1.Text = "";
            petname2.Text = "";
            petname3.Text = "";
            Female.IsChecked = false;
            Male.IsChecked = false;

            /*SqlConnection sqlCon = new SqlConnection(@"Server=DESKTOP-KS3LE58;Database=PetCare;Integrated Security=True");
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }


              /*  string query_id = String.Format("SELECT IDUser FROM Users WHERE _Username='{0}' AND _Password='{1}'", user , password);
                SqlCommand cmd = new SqlCommand(query_id, sqlCon);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    userID =reader["IDUser"];
                    reader.Close();
                }
              
                string query = "INSERT INTO Users VALUES(@_petName,@_sex,@_breed,@_color,@_birthdate,@_petType,@_photo)";

                SqlCommand cmd = new SqlCommand(query, sqlCon);

                cmd.Parameters.AddWithValue("@_petName", pet_name);
                cmd.Parameters.AddWithValue("@_sex", sex);
                cmd.Parameters.AddWithValue("@_breed", breed);
                cmd.Parameters.AddWithValue("@_color", color);
                cmd.Parameters.AddWithValue("@_birthdate", birth);
                cmd.Parameters.AddWithValue("@_petType", pet_type);

                if(this.photoPath != null)
                {
                    cmd.Parameters.AddWithValue("@_photo",this.photoPath);
                }

                cmd.ExecuteNonQuery();
                //sqlCon.Close();

                petname.Text = "";
                petname1.Text = "";
                petname2.Text = "";
                petname3.Text = "";
                Female.IsChecked = false;
                Male.IsChecked = false;

                //MainWindow mainWindow = new MainWindow();
                //this.Close();
                //mainWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }*/
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
            var context = new baza_PetCareDataContext();
            int us = (from u in context.Users where u._Username == user select new { u.IDUser }).SingleOrDefault().IDUser;
            return us;
        }
    }
}
