using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using APIWebBills.Models;

namespace APIWebBills.Controllers
{
    public class PhotoController : ApiController
    {
        // GET api/photo
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/photo/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/photo
        [HttpPost]
        public HttpResponseMessage Post([FromBody]PhotoClass user)
        {
            HttpResponseMessage resp = new HttpResponseMessage();

            byte[] binaryString;
            using (SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connection"].ConnectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("SELECT ImageBytes FROM Photo WHERE Nick = '" + user.userName + "' AND name = '" + user.photoName+"'", con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                binaryString = (byte[])reader[0];

                                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                                result.Content = new ByteArrayContent(binaryString);
                                result.Content.Headers.ContentType =
                                    new MediaTypeHeaderValue("application/octet-stream");

                                return result;
                            }
                        }

                    }
                }
            }




            return resp;
        }

        // PUT api/photo/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/photo/5
        public void Delete(int id)
        {
        }


        private void sendPicture(string userName, string photoName)
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
                Name = userName + "/" + photoName
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