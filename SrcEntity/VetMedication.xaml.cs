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
    /// Interaction logic for VetMedication.xaml
    /// </summary>
    public partial class VetMedication : Window
    {
        string user;
        string petName;
        public VetMedication(string user,string petName)
        {
            InitializeComponent();
            this.user = user;

            petname.Text = petName;
            this.petName = petName;
            dataTextBox.Text = DateTime.Now.ToString("G");
        }

        private void HealthRecButton_Click(object sender, RoutedEventArgs e)
        {
            string diagnostic = diagnosticcc.Text;
            string healthRec = healthReccc.Text;
            if (petname.Text == "")
            {
                this.petName = petname.Text;
            }

            if (string.IsNullOrWhiteSpace(healthRec))
            {
                 MessageBox.Show("Please enter a health record!");
                 return;
            }
            if (string.IsNullOrWhiteSpace(diagnostic))
            {
                MessageBox.Show("Please enter a diagnose!");
                return;
            }

            var context = new PetCareEntities();

            var customer = (from c in context.Users

                            where c.C_Username == user
                            select c).FirstOrDefault();
            int idDoctor = 0;
            if (customer != null)
            {
                idDoctor = customer.IDUser;
            }
            else
            {
                // Poți gestiona aici situația în care nu s-a găsit niciun utilizator în baza de date.
                MessageBox.Show("User not found or null");
            }

            var idPet = (from c in context.Pets

                            where c.C_petName == this.petName
                            select c).FirstOrDefault();

            var petAppointment = (from c in context.Appointments
                                  where c.C_PetID == idPet.IDPet
                                  select c).FirstOrDefault();

            var healthRecord = new Health_Record
            {
                C_PetID = idPet.IDPet,
                C_diagnosis = diagnostic,
                C_treatment = healthRec,
                C_date = petAppointment.C_date,
                C_DoctorID = idDoctor
            };

            context.Health_Records.Add(healthRecord);
            context.SaveChanges();

            MessageBox.Show("Health record stored successfully!");

            diagnosticcc.Text = "";
            healthReccc.Text = "";
            petname.Text = "";

            petAppointment.C_status = "Finished";
            context.SaveChanges();


        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            HomePageVet home = new HomePageVet(user);
            home.Show();
            this.Close();
        }

        private void CloseBtnPetV_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void textNamePet_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(petname.Text) && petname.Text.Length > 0)
                petnameplace.Visibility = Visibility.Collapsed;
            else
                petnameplace.Visibility = Visibility.Visible;
        }

        private void textNamePet_MouseDown(object sender, MouseButtonEventArgs e)
        {
            petname.Focus();
        }

        private void txtDI_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(diagnosticcc.Text) && diagnosticcc.Text.Length > 0)
                diagnosticc.Visibility = Visibility.Collapsed;
            else
                diagnosticc.Visibility = Visibility.Visible;
        }

        private void textDI_MouseDown(object sender, MouseButtonEventArgs e)
        {
            diagnosticcc.Focus();
        }

        private void texthr_MouseDown(object sender, MouseButtonEventArgs e)
        {
            healthReccc.Focus();
        }

        private void txthr_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(healthReccc.Text) && healthReccc.Text.Length > 0)
                healthRecc.Visibility = Visibility.Collapsed;
            else
                healthRecc.Visibility = Visibility.Visible;
        }

        private void Medication_Click(object sender, RoutedEventArgs e)
        {
            VetMedicationWindow med = new VetMedicationWindow(this.user);
            med.Show();
            this.Close();
        }
    }
}
