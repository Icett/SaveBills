using APIWebBills.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;

namespace APIWebBills.Controllers
{
    public class DeleteController : ApiController
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
                string sqlInsert = "UPDATE Account SET active = @active WHERE nick = @nick";


                SqlCommand cmd = new SqlCommand(sqlInsert, con);
                cmd.Parameters.Add("@nick", SqlDbType.VarChar);
                cmd.Parameters.Add("@active", SqlDbType.VarChar);
                cmd.Parameters["@nick"].Value = user.userName;
                cmd.Parameters["@active"].Value = '0';

                int numberOfRecords = cmd.ExecuteNonQuery();

                if (numberOfRecords == 1)
                    return "Status: 1 Code: User was delete";
                else
                    return "Status: 0 Code: Couldn't delete the user";
            }
         }
    }
}