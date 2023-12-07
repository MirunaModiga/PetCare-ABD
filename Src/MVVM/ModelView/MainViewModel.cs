using myPetCare.Core;
using myPetCare.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myPetCare.MVVM.ViewModel
{
     class MainViewModel : ObservableObject
     {
        private int _loggedInUserID;

        public int LoggedInUserID
        {
            get { return _loggedInUserID; }
            set
            {
                _loggedInUserID = value;
                OnPropertyChanged();
            }
        }

        baza_PetCareDataContext context = new baza_PetCareDataContext();

        public ObservableCollection<MessageModel> Messages { get; set; }

        public ObservableCollection<ContactModel> Contacts { get; set; }

        /* Commands */

        public RelayCommand SendCommand { get; set; }

        private ContactModel _selectedContact;

        public ContactModel SelectedContact
        {
            get { return _selectedContact; }
            set 
            {
                _selectedContact = value; 
                OnPropertyChanged();

                //Console.WriteLine($"SelectedContact set to: {value?.Username}");
                LoadMessagesForSelectedContact();
            }
        }

        public string _message;

        public string Message
        {
            get { return _message; }
            set 
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {

        }

        public MainViewModel(int IDuser_connected) 
        {
            LoggedInUserID = IDuser_connected; 

            Messages = new ObservableCollection<MessageModel>();
            Contacts = new ObservableCollection<ContactModel>();

            string my_usertype = (from u in context.Users
                                  where u.IDUser == LoggedInUserID
                                  select u._usertype).FirstOrDefault();
            string image="";
            if(my_usertype == "Owner")
            {
                image = "\\images\\icons8-doctor.png";
            }
            else
            {
                image = "\\images\\icons8-male-user.png";
            }

            // contactele sunt ownerii daca eu sunt doctor si invers

            var users = (from u in context.Users
                         where u.IDUser != LoggedInUserID && u._usertype != my_usertype  
                         select u._FullName).ToList();

            for (int i = 0; i < users.Count(); i++)
            {
                Contacts.Add(new ContactModel
                {
                    Username = $"{users[i]}",
                    ImageSource = image,
                    Messages = Messages
                });
            }

            SendCommand = new RelayCommand(
            o =>
            {
                Messages.Add(new MessageModel
                {
                    Message = Message,
                    ImageSource = image,
                    UsernameColor = "#409aff",
                    Username = "You",
                    Time = DateTime.Now,
                    isNativeOrigin = false,
                    FirstMessage = true
                });
                Message = "";
                //Console.WriteLine("SendCommand executed");

                if (SelectedContact != null)
                {

                    int IDreceiver = (from u in context.Users
                                      where u._FullName == SelectedContact.Username
                                      select u.IDUser).FirstOrDefault();
                    //Console.WriteLine($"IDReceiver is : {IDreceiver}");
                    try
                    {
                        //Console.WriteLine("Trying insertion to database");

                        var new_message = new Message
                        {
                            _SenderID = LoggedInUserID,
                            _ReceiverID = IDreceiver,
                            _messageContent = Messages.Last().Message,
                            _timestamp = DateTime.Now 
                        };

                        context.Messages.InsertOnSubmit(new_message);
                        context.SubmitChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error adding message to database: {ex.Message}");
                    }
                }
            }, null);

        }

        private void LoadMessagesForSelectedContact()
        {
            //Console.WriteLine("LoadMessagesForSelectedContact called");

            int IDreceiver = (from u in context.Users
                              where u._FullName == SelectedContact.Username
                              select u.IDUser).FirstOrDefault();

           // Console.WriteLine($"IDReceiver new is : {IDreceiver}");

            // Incarcă mesajele din baza de date pentru contactul selectat
            var messagesForContact = from msg in context.Messages
                                     where (msg._SenderID == LoggedInUserID && msg._ReceiverID == IDreceiver) ||
                                           (msg._SenderID == IDreceiver && msg._ReceiverID == LoggedInUserID)
                                     select new MessageModel
                                     {
                                         Username = msg._SenderID == LoggedInUserID ? "You" : SelectedContact.Username,
                                         UsernameColor = "#409aff", 
                                         ImageSource = "/images/user.png",
                                         Message = msg._messageContent,
                                         Time = msg._timestamp,
                                         isNativeOrigin = msg._SenderID != LoggedInUserID,
                                         FirstMessage = true
                                     };

            foreach (var messageModel in messagesForContact)
            {
                Messages.Add(messageModel);
            }
        }
    }
}
