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
using System.Windows.Shapes;

namespace myPetCare
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Window
    {
        string user;
        public HomePage(string user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            // Set tooltip visibility

            if (Tg_Btn.IsChecked == true)
            {
                tt_home.Visibility = Visibility.Collapsed;
                tt_contacts.Visibility = Visibility.Collapsed;
                tt_messages.Visibility = Visibility.Collapsed;
                tt_maps.Visibility = Visibility.Collapsed;
                tt_settings.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_home.Visibility = Visibility.Visible;
                tt_contacts.Visibility = Visibility.Visible;
                tt_messages.Visibility = Visibility.Visible;
                tt_maps.Visibility = Visibility.Visible;
                tt_settings.Visibility = Visibility.Visible;
            }
        }

        private void Tg_Btn_Unchecked(object sender, RoutedEventArgs e)
        {
            img_bg.Opacity = 1;
            registerPet.Opacity = 1;
        }

        private void Tg_Btn_Checked(object sender, RoutedEventArgs e)
        {
            img_bg.Opacity = 0.3;
            registerPet.Opacity = 0.3;
        }

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void registerPet_Click(object sender, RoutedEventArgs e)
        {
            RegisterPet pet = new RegisterPet(this.user);
            pet.Show();
            this.Close();
        }

        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            StartUp start=new StartUp();
            start.Show();
            this.Close();
        }

        private void AboutUs_Clicked(object sender, RoutedEventArgs e)
        {
            AboutUs info=new AboutUs(this.user);
            info.Show();
            this.Close();
        }

        private void ListView_MyPets_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MyPetsList mypets=new MyPetsList(this.user);
            mypets.Show();
            this.Close();
        }

        private void ListView_Chat_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Chat chat = new Chat(this.user);
            chat.Show();
            this.Close();
        }

        private void MakeAppointment_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Appointment app = new Appointment(this.user);
            app.Show();
            this.Close();
        }

        private void TrackMedication_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Medication med = new Medication(this.user);
            med.Show();
            this.Close();
        }
    }
}
