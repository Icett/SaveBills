using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIWebBills.Models
{
    class SettingsClass
    {
        public int UserId { get; set; }
        public bool StoreOnPhone { get; set; }
        public string Theme { get; set; }
        public string Language { get; set; }
    }
}
