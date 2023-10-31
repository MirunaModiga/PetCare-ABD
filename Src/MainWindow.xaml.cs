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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Media.Animation;

namespace testnou
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }        

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-KS3LE58; Initial Catalog=PetCare; Integrated Security=True;");
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }

                String query = "SELECT COUNT(1) FROM Users WHERE _Username=@_Username AND _Password=@_Password";
                SqlCommand cmd = new SqlCommand(query, sqlCon);

                cmd.Parameters.AddWithValue("@_Username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@_Password", txtPassword.Password);

                cmd.ExecuteNonQuery();
                int count = Convert.ToInt32(cmd.ExecuteScalar());

                txtUsername.Text = "";
                txtPassword.Password = "";
                if (count == 1)
                {
                    HomePage home = new HomePage();
                    home.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Username or password is incorrect.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }

        private void CreateAccBtn_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsernameSignin.Text;
            string pass = txtPasswordSignin.Password;
            string mail = txtEmailSignin.Text;
            
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Please enter a username!");
                return;
            }

                if (string.IsNullOrWhiteSpace(pass))
                {
                    MessageBox.Show("Please enter a password!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(mail))
                {
                    MessageBox.Show("Please enter an email!");
                    return;
                }

                if(doctor.IsChecked == false && pet_owner.IsChecked==false) 
                {
                    MessageBox.Show("Please select an user type!");
                    return;
                }


            SqlConnection sqlCon = new SqlConnection(@"Server=DESKTOP-KS3LE58;Database=PetCare;Integrated Security=True");
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }

                string query = "INSERT INTO Users VALUES(@_Username,@_Password,@_Email,@_usertype)";

                SqlCommand cmd = new SqlCommand(query, sqlCon);

                cmd.Parameters.AddWithValue("@_Username", txtUsernameSignin.Text);
                cmd.Parameters.AddWithValue("@_Password", txtPasswordSignin.Password);
                cmd.Parameters.AddWithValue("@_Email", txtEmailSignin.Text);

                if (doctor.IsChecked == true)
                {
                    cmd.Parameters.AddWithValue("@_usertype", "Veterinary");
                }
                else if (pet_owner.IsChecked == true)
                {
                    cmd.Parameters.AddWithValue("@_usertype", "Owner");
                }

                cmd.ExecuteNonQuery();
                //sqlCon.Close();

                txtUsernameSignin.Text = "";
                txtPasswordSignin.Password = "";
                txtEmailSignin.Text = "";
                doctor.IsChecked = false;
                pet_owner.IsChecked = false;

                //MainWindow mainWindow = new MainWindow();
                //this.Close();
                //mainWindow.Show();
                Storyboard s = (Storyboard)TryFindResource("SignIN");
                s.Begin();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }
    }
}
