using System.Collections.ObjectModel;
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
    /// Interaction logic for VetAppointment.xaml
    /// </summary>
    public partial class VetAppointment : Window
    {
        string user;
        PetCareEntities context = new PetCareEntities();

        public VetAppointment(string user)
        {
            InitializeComponent();
            this.user = user;

            var customer = (from c in context.Users
                            where c.C_Username == user
                            select c).FirstOrDefault();
            int idDoctor = 0;
            if (customer != null)
            {
                DoctorName.Text = customer.C_FullName;
                idDoctor = customer.IDUser;
            }
            else
            {
                // Poți gestiona aici situația în care nu s-a găsit niciun utilizator în baza de date.
                MessageBox.Show("User not found or null");
            }

            int nrAppointments = (from c in context.Appointments
                                  where c.C_DoctorID == idDoctor
                                  select c).Count();

            var IDDoc = (from c in context.Users
                         where c.IDUser == idDoctor
                         select c).FirstOrDefault();

            var converter = new BrushConverter();
            ObservableCollection<Pets> pets = new ObservableCollection<Pets>();

            var appointments = (from c in context.Appointments
                                where c.C_DoctorID == idDoctor
                                select c).ToList();

            System.DateTime today = System.DateTime.Today;

            foreach (var appointment in appointments)
            {
                var pet = (from c in context.Pets
                           where c.IDPet == appointment.C_PetID
                           select c).FirstOrDefault();

                if (pet != null || today != appointment.C_date)
                {
                    pets.Add(new Pets { numar = appointment.IDAppointment, name = pet.C_petName, hour = appointment.C_date.ToString(), purpos = appointment.C_purpose, status = appointment.C_status });
                }
            }
            DataGridApp.ItemsSource = pets;
            DataGridApp.CanUserAddRows = false;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void VetApHome_Click(object sender, MouseButtonEventArgs e)
        {
            HomePageVet Home = new HomePageVet(this.user);
            Home.Show();
            this.Close();
        }

        private void VetApHR_Click(object sender, MouseButtonEventArgs e)
        {
            VetMedication HealthRecords = new VetMedication(this.user,"");
            HealthRecords.Show();
            this.Close();
        }

        private void VetApAp_Click(object sender, MouseButtonEventArgs e)
        {
            VetAppointment Appointment = new VetAppointment(this.user);
            Appointment.Show();
            this.Close();
        }

        private void VetApAboutus_Click(object sender, MouseButtonEventArgs e)
        {
            AboutUs info = new AboutUs(this.user);
            info.Show();
            this.Close();
        }

        private void VetApLogOut_Click(object sender, MouseButtonEventArgs e)
        {
            StartUp start = new StartUp();
            start.Show();
            this.Close();
        }

        private void VetApCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private bool IsMaximize = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximize)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    IsMaximize = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximize = true;
                }
            }
        }

        private void vetDeleteAp_Click(object sender, RoutedEventArgs e)
        {

            var pet = DataGridApp.SelectedItem as Pets;

            var appToDelete = (from c in context.Appointments
                         where c.C_PetID == pet.numar
                         select c).FirstOrDefault();

            context.Appointments.Remove(appToDelete);
            context.SaveChanges();

            MessageBox.Show("Appointment deleted successfully!");
        }

        private void vetFinishAp_Click(object sender, RoutedEventArgs e)
        {
            var pet = DataGridApp.SelectedItem as Pets;
            VetMedication healthRec = new VetMedication(this.user,pet.name);
            healthRec.Show();
            this.Close();
        }
    }

    public class Pets
    {
        public int numar { get; set; }
        public string name { get; set; }
        public string hour { get; set; }
        public string purpos { get; set; }
        public string status { get; set; }
    }
}
