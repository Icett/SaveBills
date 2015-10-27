using APIWebBills.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Storage.v1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;

namespace APIWebBills.Controllers
{
    public class RegisterController : ApiController
    {
        // GET api/register
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/register
        [HttpPost]
        public string Post([FromBody]LoginClass user)
        {
            using (SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connection"].ConnectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("SELECT nick FROM ACCOUNT WHERE nick = '" + user.userName + "'", con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            return "Status: 0 Code: User exist";
                        }
                    }
                }

                string sqlInsert = "INSERT INTO Account (nick, password, mail, country, active, premium)" +
                                                        "values (@nick, @psswd, @mail, @country, '1', '0')";
                SqlCommand cmd = new SqlCommand(sqlInsert, con);
                cmd.Parameters.Add("@nick", SqlDbType.VarChar);
                cmd.Parameters.Add("@psswd", SqlDbType.VarChar);
                cmd.Parameters.Add("@mail", SqlDbType.VarChar);
                cmd.Parameters.Add("@country", SqlDbType.VarChar);
                cmd.Parameters["@nick"].Value = user.userName;
                cmd.Parameters["@psswd"].Value = user.userPsswd;


                if (user.userMail != "")
                    cmd.Parameters["@mail"].Value = user.userMail;
                else
                    cmd.Parameters["@mail"].Value = "EMPTY";


                if (user.userCountry != null)
                    cmd.Parameters["@country"].Value = user.userCountry;
                else
                    cmd.Parameters["@country"].Value = "EMPTY";


                int numberOfRecords = cmd.ExecuteNonQuery();

                if (numberOfRecords == 1)
                {
                    return "Status: 1 Code: User was add successfuly";
                }
                else
                    return "Status: -1 Code: User wasn't add to database";

            }
        }
    }
}