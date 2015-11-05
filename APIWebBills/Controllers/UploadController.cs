using APIWebBills.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
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



        [HttpGet]
        public HttpResponseMessage ReturnBytes(int id)
        {
            byte[] binaryString;
            using (SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connection"].ConnectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("SELECT ImageBytes FROM PHOTO WHERE Nick = 'KAROLE'", con))
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
            HttpResponseMessage resultw = new HttpResponseMessage(HttpStatusCode.OK);

            return resultw;


        }

        [HttpDelete]
        public HttpResponseMessage DeleteImage(int id)
        {
            HttpResponseMessage resultw = new HttpResponseMessage(HttpStatusCode.OK);

            return resultw;
        }


        // POST api/images
        [HttpPost]
        public string PostImage([FromBody]PhotoClass Image)
        {
            if (Image != null)
            {
                using (SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connection"].ConnectionString))
                {
                    con.Open();
                   

                    string sqlInsert = "INSERT INTO Photo (Nick, ImageBytes)" +
                                                            "values (@Nick, @ImageBytes)";

                    SqlCommand cmd = new SqlCommand(sqlInsert, con);
                    cmd.Parameters.Add("@Nick", SqlDbType.VarChar);
                    cmd.Parameters.Add("@ImageBytes", SqlDbType.VarBinary);
                    cmd.Parameters["@Nick"].Value = Image.userName;
                    cmd.Parameters["@ImageBytes"].Value = Image.ImageBytes;

                    int numberOfRecords = cmd.ExecuteNonQuery();

                    if (numberOfRecords == 1)
                    {
                        return "Status: 1 Code: Photo was add successfuly";
                    }
                    else
                        return "Status: 0 Code: Photo wasn't add to database" + Image.ImageBytes + " nick: " + Image.userName;
                }
            }

            return "Status: -1 Code: Photo was null";
        }

    }
}