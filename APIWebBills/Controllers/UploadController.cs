using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace APIWebBills.Controllers
{
    public class UploadController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        public async Task<HttpResponseMessage> Post()
        {
            Stream requestStream = await this.Request.Content.ReadAsStreamAsync();
            byte[] byteArray = null;
            using (MemoryStream ms = new MemoryStream())
            {
                await requestStream.CopyToAsync(ms);
                byteArray = ms.ToArray();
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}