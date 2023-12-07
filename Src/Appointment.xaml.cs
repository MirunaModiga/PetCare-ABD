using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using MaterialDesignThemes.Wpf;

namespace myPetCare
{
    /// <summary>
    /// Interaction logic for Appointment.xaml
    /// </summary>
    public partial class Appointment : Window
    {
        string user;
        int userID = 0;
        int selectedPetID = 0;
        string selectedDate = "";
        int selectedDoctorID = 0;
        string purpose = "";
        DateTime date;

        baza_PetCareDataContext context = new baza_PetCareDataContext();

        public Appointment(string user)
        {
            InitializeComponent();

            DatePicker.SelectedDate=DateTime.Now;
            this.user = user;
            userID = (from u in context.Users where u._Username == user select new { u.IDUser }).SingleOrDefault().IDUser;

            var petNames = (from pet_obj in context.Pets
                            where pet_obj._OwnerID == userID
                            select pet_obj._petName).ToList();

            string[] petNamesArray = petNames.ToArray();

            PetListCombo.ItemsSource = petNamesArray;

        }

        private void CloseBtnPet_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            HomePage home = new HomePage(this.user);
            home.Show();
            this.Close();
        }

        private void PetListCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedPetName = PetListCombo.SelectedItem as string;

            this.selectedPetID = (from pet_obj in context.Pets
                                 where pet_obj._OwnerID == userID && pet_obj._petName == selectedPetName
                                 select pet_obj.IDPet).FirstOrDefault();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.selectedDate = DatePicker.SelectedDate.GetValueOrDefault().Date.ToShortDateString();
            string day = DatePicker.SelectedDate.GetValueOrDefault().DayOfWeek.ToString();

            var availableDoctors = (from doc in context.Users
                                    where doc._workDate == DatePicker.SelectedDate.GetValueOrDefault().DayOfWeek.ToString()
                                    select doc._FullName).ToList();

            string[] doctorsArray = availableDoctors.ToArray();

            DoctorsListCombo.ItemsSource = doctorsArray;
        }

        private void DoctorsListCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.selectedDate == "")
            {
                MessageBox.Show("Please select a date first!");
                return;
            }

            string selectedDoctorName = DoctorsListCombo.SelectedItem as string;

            this.selectedDoctorID = (from doc in context.Users
                                  where doc._FullName == selectedDoctorName
                                  select doc.IDUser).FirstOrDefault();
        }

        private void txtService_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(purposeText.Text) && purposeText.Text.Length > 0)
                serviceplaceholder.Visibility = Visibility.Collapsed;
            else
                serviceplaceholder.Visibility = Visibility.Visible;
        }

        private void textService_MouseDown(object sender, MouseButtonEventArgs e)
        {
            purposeText.Focus();
        }

        private void TimeApointmentChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            DateTime selectedTime = TimePicker.SelectedTime.GetValueOrDefault();

            DateTime selectedDateTime = DatePicker.SelectedDate.GetValueOrDefault().Date;
            DateTime startInterval = selectedDateTime.Add(selectedTime.TimeOfDay);

            DateTime data = new DateTime(selectedDateTime.Year, selectedDateTime.Month, selectedDateTime.Day)+new TimeSpan(selectedTime.Hour,selectedTime.Minute, selectedTime.Second);

            DateTime endInterval = startInterval.AddHours(1);

            var existingAppointments = from app in context.Appointments
                                       where app._DoctorID == selectedDoctorID
                                          && app._date >= startInterval
                                          && app._date < endInterval
                                       select app;

            if (existingAppointments.Any())
            {
                MessageBox.Show("The selected doctor already has an appointment within the chosen hour interval. Please choose another time.");
                return;
            }
            else
            {
                this.date = data;
            }
        }

        private void BookAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            this.purpose = purposeText.Text;
            if (this.selectedPetID == 0)
            {
                MessageBox.Show("Please choose a pet!");
                return;
            }
            if (string.IsNullOrWhiteSpace(this.purpose))
            {
                MessageBox.Show("Please enter what service do you need!");
                return;
            }
            if (this.selectedDoctorID == 0)
            {
                MessageBox.Show("Please select a doctor!");
                return;
            };

           // string formattedDate = this.date.ToString("MM-dd-yyyy HH:mm", CultureInfo.InvariantCulture);
          //  DateTime data = Convert.ToDateTime(formattedDate);

            /////////////////////////////////////////////////////////// ora 

            DateTime dt = DatePicker.SelectedDate.Value + new TimeSpan(TimePicker.SelectedTime.Value.Hour,TimePicker.SelectedTime.Value.Minute,TimePicker.SelectedTime.Value.Second);

            var new_appointment = new Appointment
            {
                _PetID = this.selectedPetID,
                _DoctorID = this.selectedDoctorID,
                _date = dt,
                _purpose = this.purpose,
                _status = "Confirmed"
            };

            context.Appointments.InsertOnSubmit(new_appointment);
            context.SubmitChanges();

            MessageBox.Show("You have booked your appointment on " + this.date);

            PetListCombo.SelectedIndex = -1;
            DatePicker.SelectedDate = null; 
            DoctorsListCombo.SelectedIndex = -1;
            TimePicker.SelectedTime = null;
            purposeText.Text = "";

        }
    }
}
