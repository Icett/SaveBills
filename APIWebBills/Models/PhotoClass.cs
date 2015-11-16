using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIWebBills.Models
{
    public class PhotoClass
    {
        public int photoID { get; set; }
        public string photoName { get; set; }
        public string photoDate { get; set; }
        public DateTime guarantee { get; set; }
        public string photoInfo { get; set; }
        public string userName { get; set; }
        public byte[] ImageBytes { get; set; }
        public DateTime AddedDate { get; set; }
        public int price { get; set; }
    }
}
