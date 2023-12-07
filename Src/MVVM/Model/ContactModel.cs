using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myPetCare.MVVM.Model
{
    class ContactModel
    {
        public string Username { get; set; }

        public string ImageSource { get; set; }

        public ObservableCollection<MessageModel> Messages { get; set; }

        public string LastMessage => Messages.Any() ? Messages.Last().Message : "See messages";

    }
}
