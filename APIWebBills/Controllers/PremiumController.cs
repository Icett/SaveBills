using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;
using APIWebBills.Models;

namespace APIWebBills.Controllers
{
    public class PremiumController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]LoginClass user)
        {
            HttpResponseMessage resp = new HttpResponseMessage();

            using (SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connection"].ConnectionString))
            {
                con.Open();
                string sqlInsert = "";

                sqlInsert = "UPDATE Account SET Premium = @Premium WHERE nick = @nick";


                SqlCommand cmd = new SqlCommand(sqlInsert, con);
                cmd.Parameters.Add("@nick", SqlDbType.VarChar);
                cmd.Parameters["@nick"].Value = user.UserName;
                cmd.Parameters.Add("@Premium", SqlDbType.SmallInt);
                cmd.Parameters["@Premium"].Value = 1;
              


                int numberOfRecords = cmd.ExecuteNonQuery();
                resp.StatusCode = numberOfRecords > 0 ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;
            }
            return resp;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}