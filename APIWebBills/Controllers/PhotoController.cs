using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Http;

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
        public void Post([FromBody]string value)
        {
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