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
using System.Runtime.Remoting.Contexts;

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
            string username = txtUsername.Text;
            string pass = txtPassword.Password;

            var context = new baza_PetCareDataContext();
            var useri = from user in context.Users select user;
            var flag = 0;
            foreach (var i in useri)
            {
                if (i._Username.Equals(username) && i._Password.Equals(pass))
                {
                    MessageBox.Show("Login successful!");
                    if (i._usertype == "Veterinary")
                    {
                        HomePageVet home = new HomePageVet(username);
                        home.Show();
                        this.Close();
                    }
                    else if (i._usertype == "Owner")
                    {
                        HomePage home = new HomePage(username);
                        home.Show();
                        this.Close();
                    }
                    txtUsername.Text = "";
                    txtPassword.Password = "";
                    flag = 1;
                }
            }
            if (flag.Equals(0))
            {
                MessageBox.Show("Username or password invalid. Enter carefully");
                txtUsername.Text = "";
                txtPassword.Password = "";
                return;
            }
        }

        private void CreateAccBtn_Click(object sender, RoutedEventArgs e)
        {
            string fullname=txtFullNameSignin.Text;
            string username = txtUsernameSignin.Text;
            string pass = txtPasswordSignin.Password;
            string mail = txtEmailSignin.Text;
            string usertype="";
            
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

                if(doctor.IsChecked == true)
                {
                    usertype = "Veterinary";
                }
                else if (pet_owner.IsChecked == true)
                {
                    usertype = "Owner";
                }
                else
                {
                    MessageBox.Show("Please select an user type!");
                    return;
                }

            var context = new baza_PetCareDataContext();
            var users = from user in context.Users select user._Username;
            foreach (var i in users)
            {
                if (i.Equals(username))
                {
                    MessageBox.Show("Username is already taken! Choose another one");
                    txtFullNameSignin.Text = "";
                    txtUsernameSignin.Text = "";
                    txtPasswordSignin.Password = "";
                    txtEmailSignin.Text = "";
                    doctor.IsChecked = false;
                    pet_owner.IsChecked = false;
                    return;
                }
            }
            /*var u = new Users
            {
                username = username,
                parola = pass
            };*/

            var new_user = new User
            {
                _FullName = fullname,
                _Username = username,
                _Password = pass,
                _Email = mail,
                _usertype = usertype
            };

            context.Users.InsertOnSubmit(new_user);
            context.SubmitChanges();

            //MessageBox.Show("Registration successful!");
            txtFullNameSignin.Text = "";
            txtUsernameSignin.Text = "";
            txtPasswordSignin.Password = "";
            txtEmailSignin.Text = "";
            doctor.IsChecked = false;
            pet_owner.IsChecked = false;

            Storyboard s = (Storyboard)TryFindResource("SignIN");
            s.Begin();
        }
    }
}
