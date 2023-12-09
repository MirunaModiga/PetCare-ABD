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
using System.Globalization;

namespace myPetCare
{
    /// <summary>    /// Interaction logic for VetMedicationWindow.xaml
    /// </summary>
    public partial class VetMedicationWindow : Window
    {
        string user;
        public VetMedicationWindow(string user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void CloseBtnPetM_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MedicationButton_Click(object sender, RoutedEventArgs e)
        {
            string medication = medicationnn.Text;
            string dosage = dosageee.Text;
            string frequency = frecvv.Text;
            string firstDay = starttt.Text;
            string lastDay = stoppp.Text;
            string petName = petname.Text;

            DateTime startDate;
            if (!DateTime.TryParseExact(firstDay, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate))
            {
                // Afisează un mesaj de eroare sau gestionează situația în care conversia a eșuat
                MessageBox.Show("Invalid start date. Please enter a valid date in format dd-MM-yyyy.");
                return;
            }

            DateTime endDate;
            if (!DateTime.TryParseExact(lastDay, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
            {
                // Afisează un mesaj de eroare sau gestionează situația în care conversia a eșuat
                MessageBox.Show("Invalid end date. Please enter a valid date in format dd-MM-yyyy.");
                return;
            }

            int dosageValue;
            if (!int.TryParse(dosage, out dosageValue))
            {
                // Afisează o eroare sau gestionează situația în care conversia a eșuat
                MessageBox.Show("Invalid dosage. Please enter a valid number.");
                return;
            }

            int frequencyValue;
            if (!int.TryParse(frequency, out frequencyValue))
            {
                // Afisează o eroare sau gestionează situația în care conversia a eșuat
                MessageBox.Show("Invalid frequency. Please enter a valid number.");
                return;
            }

            if (string.IsNullOrWhiteSpace(petName))
            {
                MessageBox.Show("Please enter a pet name!");
                return;
            }
            if (string.IsNullOrWhiteSpace(medication))
            {
                MessageBox.Show("Please enter a medication!");
                return;
            }
            if (string.IsNullOrWhiteSpace(frequency))
            {
                MessageBox.Show("Please enter a frequency!");
                return;
            }
            if (string.IsNullOrWhiteSpace(dosage))
            {
                MessageBox.Show("Please enter a dosage!");
                return;
            }
            if (string.IsNullOrWhiteSpace(firstDay))
            {
                MessageBox.Show("Please enter a day to start the treatment!");
                return;
            }
            if (string.IsNullOrWhiteSpace(lastDay))
            {
                MessageBox.Show("Please enter day to stop the treatment!");
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

                         where c.C_petName == petName
                         select c).FirstOrDefault();

            var petHealthRec = (from c in context.Health_Records
                                  where c.C_PetID == idPet.IDPet
                                  select c).FirstOrDefault();

            var petMedication = new Medication
            {
                C_PetID = idPet.IDPet,
                C_medicationName = medication,
                C_dosage = dosageValue,
                C_frequency = frequencyValue,
                C_start = startDate,
                C_end = endDate,
                C_notes = petHealthRec.C_treatment
            };

            context.Medications.Add(petMedication);
            context.SaveChanges();

            MessageBox.Show("Health record stored successfully!");

            petname.Text = "";
            medicationnn.Text = "";
            dosageee.Text = "";
            frecvv.Text = "";
            starttt.Text = "";
            stoppp.Text = "";
            petname.Text = "";
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            HomePageVet home = new HomePageVet(this.user);
            home.Show();
            this.Close();
        }

        private void txtPetName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(petname.Text) && petname.Text.Length > 0)
                petnameplace.Visibility = Visibility.Collapsed;
            else
                petnameplace.Visibility = Visibility.Visible;
        }

        private void txtDosage_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(dosageee.Text) && dosageee.Text.Length > 0)
                dosagee.Visibility = Visibility.Collapsed;
            else
                dosagee.Visibility = Visibility.Visible;
        }

        private void txtMedication_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(medicationnn.Text) && medicationnn.Text.Length > 0)
                medicationn.Visibility = Visibility.Collapsed;
            else
                medicationn.Visibility = Visibility.Visible;
        }

        private void txtFrequency_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(frecvv.Text) && frecvv.Text.Length > 0)
                frecv.Visibility = Visibility.Collapsed;
            else
                frecv.Visibility = Visibility.Visible;
        }

        private void txtFirst_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(starttt.Text) && starttt.Text.Length > 0)
                startt.Visibility = Visibility.Collapsed;
            else
                startt.Visibility = Visibility.Visible;
        }

        private void txtLast_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(stoppp.Text) && stoppp.Text.Length > 0)
                stopp.Visibility = Visibility.Collapsed;
            else
                stopp.Visibility = Visibility.Visible;
        }

        private void textPetName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            petname.Focus();
        }

        private void textMediccation_MouseDown(object sender, MouseButtonEventArgs e)
        {
            medicationnn.Focus();
        }

        private void textDosage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dosageee.Focus();
        }

        private void textFrequency_MouseDown(object sender, MouseButtonEventArgs e)
        {
            frecvv.Focus();
        }

        private void textFirst_MouseDown(object sender, MouseButtonEventArgs e)
        {
            starttt.Focus();
        }

        private void textLast_MouseDown(object sender, MouseButtonEventArgs e)
        {
            stoppp.Focus();
        }
    }
}
