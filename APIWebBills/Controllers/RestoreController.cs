using APIWebBills.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Configuration;
using System.Web.Http;

namespace APIWebBills.Controllers
{
    public class RestoreController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/<controller>
        [HttpPost]
        public string Post([FromBody]LoginClass user)
        {
            using (SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connection"].ConnectionString))
            {
                con.Open();
                string sqlCommand = "SELECT active FROM ACCOUNT WHERE nick = '" + user.UserName + "' OR mail = '" + user.UserMail + "'";

                if (user.UserMail != "")
                    sqlCommand = "SELECT active FROM ACCOUNT WHERE mail = '" + user.UserMail + "'";

                if (user.UserName != "")
                    sqlCommand = "SELECT active FROM ACCOUNT WHERE nick = '" + user.UserName + "'";

                using (SqlCommand command = new SqlCommand(sqlCommand, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            return "Status: -1 Code: User doesn't exist"; 
                        }
                    }
                }

                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                var random = new Random();
                var result = new string(
                    Enumerable.Repeat(chars, 8)
                              .Select(s => s[random.Next(s.Length)])
                              .ToArray());

                string sqlInsert = "";

                if (user.UserMail != "")
                    sqlInsert = "UPDATE Account SET password = @psswd WHERE mail = @mail";

                if (user.UserName != "")
                    sqlInsert = "UPDATE Account SET password = @psswd WHERE nick = @nick";

                
                SqlCommand cmd = new SqlCommand(sqlInsert, con);
                cmd.Parameters.Add("@psswd", SqlDbType.VarChar);
                
                cmd.Parameters["@psswd"].Value = result;
                if (user.UserMail != "")
                {
                    cmd.Parameters.Add("@mail", SqlDbType.VarChar);
                    cmd.Parameters["@mail"].Value = user.UserMail;
                }
                
                if (user.UserName != "")
                {
                    cmd.Parameters.Add("@nick", SqlDbType.VarChar);
                    cmd.Parameters["@nick"].Value = user.UserName;
                }

                int numberOfRecords = cmd.ExecuteNonQuery();
                
                if (numberOfRecords == 1 && user.UserMail.Length > 3)
                {
                    MailMessage o = new MailMessage("karolpyrek1@tlen.pl", user.UserMail, "Keep Guarantee: Reset your password", "Thanks for using my app! Here is your new password: " + result);
                    NetworkCredential netCred = new NetworkCredential("karolpyrek1@tlen.pl", "qazxc284");
                    SmtpClient smtpobj = new SmtpClient("poczta.o2.pl", 587);
                    smtpobj.EnableSsl = true;
                    smtpobj.Credentials = netCred;
                    smtpobj.Send(o);

                    
                    return "Status: 1 Code: password was sent to an e-mail (if exist)";
                }
                    
                else
                    return "Status: 0 Code: Couldn't change the password";
            }
        }
    }
}