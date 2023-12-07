using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myPetCare
{
    public partial class Users
    {

        public Users() 
        {
            //this.Loggers = new HashSet<Logger>();
        }

        public int IdUser { get; set; }
        public string username { get; set; }
        public string parola { get; set; }

       // public virtual ICollection<Logger> Loggers { get; set; }
    }
}
