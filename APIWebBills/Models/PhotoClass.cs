using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIWebBills.Models
{
    public class PhotoClass
    {
        public int PhotoId { get; set; }
        public string PhotoName { get; set; }
        public string PhotoDate { get; set; }
        public DateTime Guarantee { get; set; }
        public string PhotoInfo { get; set; }
        public string UserName { get; set; }
        public byte[] ImageBytes { get; set; }
        public DateTime AddedDate { get; set; }
        public int Price { get; set; }
    }
}
