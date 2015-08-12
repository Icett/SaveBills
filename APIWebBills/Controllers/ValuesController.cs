using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;
using Newtonsoft.Json;

namespace APIWebBills.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value23" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            string test = "";
            using (SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connection"].ConnectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM ACCOUNT", con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                test += reader.GetValue(i);
                            }
                        }
                    }
                }
            }

            var json = JsonConvert.SerializeObject(test);

            return json;
        }


        // POST api/values
        [HttpPost]
        public string Post([FromBody]string value)
        {
            return "weszlo: " + value;
        }
        
        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        //public string Logowanie()
        //{
        //    HttpWebRequest request = WebRequest.Create(Variables.apiLink + "/api/login") as HttpWebRequest;
        //    if (request != null)
        //    {
        //        request.Method = "POST";
        //        request.ContentType = "application/json";

        //        try
        //        {
        //            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
        //            {
        //                string sendLogPss = new JavaScriptSerializer().Serialize(new
        //                {
        //                    login = _usrname,
        //                    passwordHash = _password
        //                });

        //                streamWriter.Write(sendLogPss);
        //                streamWriter.Flush();
        //                streamWriter.Close();

        //                var httpResponse = (HttpWebResponse)request.GetResponse();
        //                string result;
        //                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        //                {
        //                    result = streamReader.ReadToEnd();
        //                }
        //                var myObject = JsonConvert.DeserializeObject<DeserializeLogout>(result);


        //                if (myObject.Status == "ok")
        //                {
        //                    string data = myObject.last_pass_modified;
        //                    _apiKey = myObject.api_key;

        //                    DateTime myDate = DateTime.Parse(data);
        //                    if ((DateTime.Now - myDate).TotalDays > 29)
        //                        return "Wymagana zmiana hasla";

        //                    Variables.PoleUserId = myObject.wpisywacz_id;

        //                }
        //                else
        //                {
        //                    switch (myObject.code)
        //                    {
        //                        case "1": return "Nie poprawna metoda requestu";
        //                        case "2": return "Nie poprawny typ requestu";
        //                        case "3": return "Nie poprawne naglowki (ApiSign)";
        //                        case "4": return "Dostep zabroniony (ApiKey)";
        //                        case "5": return "Dostep zabroniony (ApiSign)";
        //                        case "6": return "Nie poprawne naglowki (ApiSecret)";
        //                        case "10": return "Nie poprawny login";
        //                        case "11": return "Nie poprawne haslo";
        //                        case "12": return "Brak uprawnien";
        //                    }

        //                }
        //                return "OK";


        //            }
        //        }
        //        catch (WebException ex)
        //        {
        //            Variables.write_to_log_file("[API][Error] Connection error : " + Convert.ToString(ex));
        //            return "Blad polaczenia z internetem. Sprawdź połączenie internetowe i zaloguj się ponownie.";
        //        }
        //    }
        //    return "Blad polaczenia z internetem.";
        //}
    }
}