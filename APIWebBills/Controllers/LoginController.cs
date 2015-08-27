using APIWebBills.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Storage.v1;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Http;


namespace APIWebBills.Controllers
{
    public class LoginController : ApiController
    {
        // GET api/login
        public IEnumerable<string> Get()
        {
            sendPicture();
            return new string[] { "value1", "value2" };
        }

        [HttpPost]
        public string Post([FromBody]LoginClass user)
        {
            //return "response " + user.userName + ", " + user.userPsswd;
            // Pobranie uzytkownika
            string activeUser = "";
            using (SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connection"].ConnectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("SELECT active FROM ACCOUNT WHERE nick = '" + user.userName + "' AND password = '" + user.userPsswd + "'" + " AND active = 1", con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                activeUser += reader.GetValue(i);
                            }
                        }
                    }
                }
            }
            if (activeUser == "1") // user is existing
            {
                return "Status: 1 Code: user exist";
            }
            else if (activeUser == "0") // account is inactive
            {
                return "Status: 0 Code: user account was deleted";
            }
            else // user is not existing
            {
                return "Status: -1 Code: user is not existing";
            }

            //var json = JsonConvert.SerializeObject(test);
        }

        //// PUT api/logins/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/login/5
        //public void Delete(int id)
        //{
        //}

        private void sendPicture()
        {
            String serviceAccountEmail = "1059077874089-potutnud3blg0bt2cmoj4321f6d28ghq@developer.gserviceaccount.com";

            string filePath = Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data/MyFirstProject-db5185b5746d.p12");
            var certificate = new X509Certificate2(filePath, "notasecret", X509KeyStorageFlags.Exportable);

            ServiceAccountCredential credential = new ServiceAccountCredential(
                new ServiceAccountCredential.Initializer(serviceAccountEmail)
                {
                    Scopes = new[] { Google.Apis.Storage.v1.StorageService.Scope.DevstorageFullControl }
                }.FromCertificate(certificate));

            Google.Apis.Storage.v1.StorageService ss = new Google.Apis.Storage.v1.StorageService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "My First Project",
            });

            var fileobj = new Google.Apis.Storage.v1.Data.Object()
            {
                Bucket = "keepguarantee",
                Name = "file"
            };

            //FileStream fileStream = null;
            //var dir = Directory.GetCurrentDirectory();
            //var path = Path.Combine(dir, "test.png");
            //fileStream = new FileStream(path, FileMode.Open);

            //var insmedia = new ObjectsResource.InsertMediaUpload(ss, fileobj, "keepguarantee", fileStream, "image/jpeg");
            //insmedia.Upload();
        }
    }
}