using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Configuration;
using System.Web.Http;
using APIWebBills.Models;

namespace APIWebBills.Controllers
{
    public class InfoController : ApiController
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

        [HttpPost]
        public HttpResponseMessage Post([FromBody]PhotoClass user)
        {
            string sql = "";
            using (SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connection"].ConnectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("SELECT name,date,guarantee,info FROM Photo WHERE Nick = '" + user.userName + "' AND name = '" + user.photoName + "'", con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                if (reader[i].ToString() != "")
                                    sql += reader[i].ToString() + ",";
                                else
                                {
                                    sql += "null" + ",";
                                }
                            }
                        }

                    }
                }
            }
            if (sql.Length > 0)
            {
                sql = sql.Remove(sql.Length - 1, 1);
            }

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StringContent(sql);
            return result;
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