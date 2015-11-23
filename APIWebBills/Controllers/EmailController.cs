using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Configuration;
using System.Web.Http;
using APIWebBills.Models;

namespace APIWebBills.Controllers
{
    public class EmailController : ApiController
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
            HttpResponseMessage resp = new HttpResponseMessage();
            try
            {

                MailMessage mail = new MailMessage();

                mail.From = new MailAddress("inzy.proj@tlen.pl");
                mail.To.Add("karolpyrek1@o2.pl");

                mail.Subject = "Projekt inzynierski - zdjęcie";
                mail.Body = "Zdjęcie";

                //Get some binary data
                byte[] data = { 0, 0 };
                using (SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connection"].ConnectionString))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("SELECT ImageBytes FROM Photo WHERE Nick = '" + user.UserName + "' AND name = '" + user.PhotoName + "'", con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    data = (byte[])reader[0];
                                }
                            }

                        }
                    }
                }

                MemoryStream ms = new MemoryStream(data);

                mail.Attachments.Add(new Attachment(ms, "example.jpg", "image/jpeg"));

                //send the message
                SmtpClient smtp = new SmtpClient();
                smtp.Credentials = new System.Net.NetworkCredential("inzy.proj@tlen.pl", "inz123");
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Host = "poczta.o2.pl";
                smtp.Send(mail);
                resp.StatusCode = HttpStatusCode.OK;
                resp.Content = new StringContent("Mail wysłany");
            }
            catch (Exception ex)
            {
                resp.StatusCode = HttpStatusCode.InternalServerError;
                resp.Content = new StringContent("Błąd + " + Convert.ToString(ex));
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