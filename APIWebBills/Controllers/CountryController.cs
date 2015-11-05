using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;

namespace APIWebBills.Controllers
{
    public class CountryController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/<controller>
        [HttpPost]
        public HttpResponseMessage Post()
        {
            HttpResponseMessage resultw = new HttpResponseMessage();
            try
            {
                country objectCountry = new country();

                using (SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connection"].ConnectionString))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("SELECT countryName FROM countries", con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    objectCountry.listCountry.Add(Convert.ToString(reader.GetValue(i)));
                                }
                            }
                        }
                    }
                }
                string jsonCountries = JsonConvert.SerializeObject(objectCountry);

                if (objectCountry.listCountry.Any())
                {
                    resultw.StatusCode = HttpStatusCode.OK;
                    resultw.Content = new StringContent(jsonCountries);
                    return resultw;
                }
                else
                {
                    resultw.StatusCode = HttpStatusCode.NotFound;
                    resultw.Content = new StringContent("");
                    return resultw;
                }
            }
            catch (Exception ex)
            {
                resultw.StatusCode = HttpStatusCode.InternalServerError;
                resultw.Content = new StringContent(ex.ToString());
                return resultw;
            }
        }

        public class country
        {
            public List<string> listCountry = new List<string>();
        }
    }
}