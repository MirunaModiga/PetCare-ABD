using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testnou
{
    public partial class Logger
    {
        public int IdLogger { get; set; }
        public int IdUser { get; set; }
        public System.DateTime Timp { get; set; }
        public string Actiune { get; set; }

        public virtual Users Useri { get; set; }
    }
}
