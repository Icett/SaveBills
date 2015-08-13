using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIWebBills.Models
{
    class SettingsClass
    {
        public int userID { get; set; }
        public bool storeOnPhone { get; set; }
        public string theme { get; set; }
        public string language { get; set; }
    }
}
