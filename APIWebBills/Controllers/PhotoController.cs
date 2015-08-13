using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public string Get(int id)
        {
            return "value";
        }

        // POST api/photo
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
    }
}