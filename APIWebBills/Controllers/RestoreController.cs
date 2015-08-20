using APIWebBills.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
                using (SqlCommand command = new SqlCommand("SELECT active FROM ACCOUNT WHERE nick = '" + user.userName + "' OR mail = '" + user.userMail + "'", con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            return "Status: -1 Code: User doesn't exist"; // Code: User exist, password was sent to an e-mail (if exist)";
                        }
                    }
                }

                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                var random = new Random();
                var result = new string(
                    Enumerable.Repeat(chars, 8)
                              .Select(s => s[random.Next(s.Length)])
                              .ToArray());

                string sqlInsert = "UPDATE Account SET password = @psswd WHERE mail = @mail OR nick = @nick";
                SqlCommand cmd = new SqlCommand(sqlInsert, con);
                cmd.Parameters.Add("@psswd", SqlDbType.VarChar);
                cmd.Parameters.Add("@mail", SqlDbType.VarChar);
                cmd.Parameters.Add("@nick", SqlDbType.VarChar);
                cmd.Parameters["@psswd"].Value = result;
                if(user.userMail != null)
                    cmd.Parameters["@mail"].Value = user.userMail;
                if(user.userName != null)
                    cmd.Parameters["@nick"].Value = user.userName;
                int numberOfRecords = cmd.ExecuteNonQuery();

                if (numberOfRecords == 1)
                    return "Status: 1 Code: password was sent to an e-mail (if exist)";
                else
                    return "Status: 0 Code: Couldn't change the password";
            }
        }
    }
}