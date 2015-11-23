using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.UI.WebControls.WebParts;
using APIWebBills.Models;
using Newtonsoft.Json;

namespace APIWebBills.Controllers
{
    public class ListController : ApiController
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
        [HttpPost]
        public HttpResponseMessage Post([FromBody]PhotoClass user)
        {
            List<string> listTitle1 = new List<string>();
            HttpResponseMessage resp = new HttpResponseMessage();

            if (user != null)
            {
                using (SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connection"].ConnectionString))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("SELECT name FROM Photo WHERE Nick = '"+ user.UserName+"'", con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    listTitle1.Add(Convert.ToString(reader.GetValue(i)));
                                }
                            }
                        }
                    }
                }
                resp.Content = new StringContent(JsonConvert.SerializeObject(listTitle1));
                resp.StatusCode = HttpStatusCode.OK;
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