using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace APIWebBills.Models
{
    public class LoginClass
    {
        public int userID { get; set; }
        public string userName { get; set; }
        [Required(ErrorMessage = "This field is required !")]
        public string userPsswd { get; set; }
        public string userMail { get; set; }
        public string userCountry { get; set; }
        public bool premium { get; set; }

        #region SZYFROWANIE
        private string Crypt_The_Password(string psswd, string apiSalt)
        {
            string hash;
            psswd = psswd + apiSalt;

            using (MD5 md5Hash = MD5.Create())
            {
                hash = GetMd5Hash(md5Hash, psswd);
            }

            hash = apiSalt + hash;
            hash = GenerateSha512(hash);
            return hash;
        }

        public string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public string GenerateSha512(string value)
        {
            byte[] buff = SHA512.Create().ComputeHash(Encoding.Default.GetBytes(value));

            return buff.Aggregate<byte, string>(null, (current, buf) => current + buf.ToString("x2"));
        }

        public string Sha1HashStringForUtf8String(string s)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(s);

            var sha1 = SHA1.Create();
            byte[] hashBytes = sha1.ComputeHash(bytes);

            return HexStringFromBytes(hashBytes);
        }


        public string HexStringFromBytes(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }
        #endregion
    }
}
