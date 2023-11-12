﻿using System;
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
                        HomePageVet home = new HomePageVet();
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

            /*SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-KS3LE58; Initial Catalog=PetCare; Integrated Security=True;");
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

                string userType = "";
                if (count == 1)
                {
                    query = String.Format("SELECT _usertype FROM Users WHERE _Username='{0}' AND _Password='{1}'", txtUsername.Text, txtPassword.Password);
                    cmd = new SqlCommand(query, sqlCon);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        userType = reader["_usertype"].ToString();
                        reader.Close();
                    }

                    Users user = new Users
                    {
                        username = username,
                        parola = pass
                    };

                    txtUsername.Text = "";
                    txtPassword.Password = "";

                    if (userType == "Veterinary")
                    {
                        HomePageVet home = new HomePageVet();
                        home.Show();
                        this.Close();
                    }
                    else if (userType == "Owner")
                    {
                        HomePage home = new HomePage();
                        home.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Usertype invalid from database.");
                    }
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
            }*/
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

            /* SqlConnection sqlCon = new SqlConnection(@"Server=DESKTOP-KS3LE58;Database=PetCare;Integrated Security=True");
             try
             {
                 if (sqlCon.State == ConnectionState.Closed)
                 {
                     sqlCon.Open();
                 }

                 string query = "INSERT INTO Users VALUES(@_FullName,@_Username,@_Password,@_Email,@_usertype)";

                 SqlCommand cmd = new SqlCommand(query, sqlCon);

                 cmd.Parameters.AddWithValue("@_FullName",txtFullNameSignin.Text);
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

                 /*Users user = new Users
                 {
                     username = username,
                     parola = pass
                 };

                 txtFullNameSignin.Text = "";
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
             }*/
        }
    }
}
