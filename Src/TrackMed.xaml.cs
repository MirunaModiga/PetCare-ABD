using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
using static System.Net.Mime.MediaTypeNames;

namespace myPetCare
{
    /// <summary>
    /// Interaction logic for TrackMed.xaml
    /// </summary>
    public partial class TrackMed : Window
    {
        string user;
        int userID = 0;
        string petName;

        baza_PetCareDataContext context =new baza_PetCareDataContext();
        
        public TrackMed(string user,string petName)
        {
            InitializeComponent();

            this.user = user;
            userID = (from u in context.Users where u._Username == user select new { u.IDUser }).SingleOrDefault().IDUser;
            this.petName = petName;
            PetTitle.Text = "\n" + petName + "'s Today Plan"; 

            LoadMedications();
        }

        private void CloseBtnPet_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            HomePage home = new HomePage(this.user);
            home.Show();
            this.Close();
        }

        private void LoadMedications()
        {
            int petID = (from pet_obj in context.Pets
                            where pet_obj._petName == this.petName
                            select pet_obj.IDPet).FirstOrDefault();

            List<Medication> medications= (from med in context.Medications
                              where med._PetID == petID
                              select med).ToList();

            StackPanel medicationStackPanel = new StackPanel
            {
                Width = 600
            };

            foreach (Medication med in medications)
            {
                Border border = new Border
                {
                    Width = medicationStackPanel.Width,
                    Height = 170,
                    Background= Brushes.DarkSlateGray,
                    CornerRadius = new CornerRadius(10,10,10,10),
                    Margin = new Thickness(0, 0, 0, 10)
                };

                StackPanel treatmentStackPanel = new StackPanel
                {
                    Width = medicationStackPanel.Width,
                    Height = 170
                };
                treatmentStackPanel.Orientation = Orientation.Vertical;

                StackPanel borderStackPanel = new StackPanel();
                borderStackPanel.Orientation = Orientation.Horizontal;

                StackPanel MedStackPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal
                };

                Rectangle rect = new Rectangle
                {
                    Width = 50,
                    Height = 50,
                    Margin = new Thickness(10, 10, 0, 0),
                    VerticalAlignment = VerticalAlignment.Top,
                    Fill = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/images/icons8-pills-colormed.png", UriKind.Absolute))
                    }
                };

                TextBlock medicationTextBlock = new TextBlock
                {
                    Text = med._medicationName,
                    Foreground = Brushes.White,
                    Height = 100,
                    Margin = new Thickness(10, 10, 0, 0),
                    VerticalAlignment = VerticalAlignment.Top,
                    Background = Brushes.Transparent,
                    FontFamily = new FontFamily("Stika Text Semibold"),
                    FontSize = 30,
                };

                MedStackPanel.Children.Add(rect);
                MedStackPanel.Children.Add(medicationTextBlock);

                StackPanel topInfoStackPanel = new StackPanel
                {
                    Orientation = Orientation.Vertical,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Margin =new Thickness(150,0,0,0)
                };

                topInfoStackPanel.Children.Add(new TextBlock
                {
                    Text = $"Dosage: {med._dosage} mg",
                    Foreground = Brushes.LightCyan,
                    FontFamily = new FontFamily("Bahnschrift Condensed"),
                    FontSize = 20,
                    Margin = new Thickness(10, 10, 0, 0),
                });

                topInfoStackPanel.Children.Add(new TextBlock
                {
                    Text = $"Frequency: {med._frequency} times per day",
                    Foreground = Brushes.LightCyan,
                    FontFamily = new FontFamily("Bahnschrift Condensed"),
                    FontSize = 20,
                    Margin = new Thickness(10, 5, 0, 0),
                });

                borderStackPanel.Children.Add(MedStackPanel);
                borderStackPanel.Children.Add(topInfoStackPanel);

                StackPanel dateStackPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal
                };

                dateStackPanel.Children.Add(new TextBlock
                {
                    Text = $"Start Date: {med._start.ToString("dd/MM/yyyy")}",
                    Foreground = Brushes.GhostWhite,
                    FontFamily = new FontFamily("Bahnschrift Condensed"),
                    FontSize = 20,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    TextAlignment = TextAlignment.Left,
                    Margin = new Thickness(10, 0, 0, 0)
                });

                dateStackPanel.Children.Add(new TextBlock
                {
                    Text = $"End Date: {med._end.ToString("dd/MM/yyyy")}",
                    Foreground = Brushes.GhostWhite,
                    FontFamily = new FontFamily("Bahnschrift Condensed"),
                    FontSize = 20,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    TextAlignment = TextAlignment.Right,
                    Margin = new Thickness(270, 0, 0, 0)
                });

                StackPanel notesStackPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    VerticalAlignment = VerticalAlignment.Bottom
                };

                if (!string.IsNullOrEmpty(med._notes))
                {
                    notesStackPanel.Children.Add(new TextBlock
                    {
                        Text = $"Notes: {med._notes}",
                        Foreground = Brushes.LightSlateGray,
                        FontFamily = new FontFamily("Bahnschrift Condensed"),
                        FontSize = 20,
                        Margin = new Thickness(10, 5, 0, 0),
                    });
                }
                else
                {
                    notesStackPanel.Children.Add(new TextBlock
                    {
                        Text = $"",
                        Margin = new Thickness(10, 5, 0, 0),
                    });
                }

                treatmentStackPanel.Children.Add(borderStackPanel);
                treatmentStackPanel.Children.Add(notesStackPanel);
                treatmentStackPanel.Children.Add(dateStackPanel);

                border.Child = treatmentStackPanel;
                medicationStackPanel.Children.Add(border);
            }

            ScrollMeds.Content = medicationStackPanel;
        }
    }
}
