using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Configuration;
using System.Web.Http;
using APIWebBills.Models;

namespace APIWebBills.Controllers
{
    public class InfoController : ApiController
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
            string sql = "";
            using (SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connection"].ConnectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("SELECT name,date,Guarantee,info, id, Price FROM Photo WHERE Nick = '" + user.UserName + "' AND name = '" + user.PhotoName + "'", con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                if (reader[i].ToString() != "")
                                    sql += reader[i].ToString() + ",";
                                else
                                {
                                    sql += "null" + ",";
                                }
                            }
                        }

                    }
                }
            }
            if (sql.Length > 0)
            {
                sql = sql.Remove(sql.Length - 1, 1);
            }

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StringContent(sql);
            return result;
        }

        // PUT api/<controller>/5
        [HttpPut]
        public HttpResponseMessage Put([FromBody]PhotoClass user)
        {
            HttpResponseMessage resp = new HttpResponseMessage();
            using (SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connection"].ConnectionString))
            {
                con.Open();
                string sqlInsert = "";

                sqlInsert = "UPDATE Photo SET info = @info, date = @date, Guarantee = @Guarantee, Price = @Price, name = @name WHERE id = @id";

                
                SqlCommand cmd = new SqlCommand(sqlInsert, con);
                cmd.Parameters.Add("@info", SqlDbType.VarChar);
                cmd.Parameters["@info"].Value = user.PhotoInfo;
                cmd.Parameters.Add("@date", SqlDbType.Date);
                cmd.Parameters["@date"].Value = user.AddedDate;
                cmd.Parameters.Add("@Guarantee", SqlDbType.Date);
                cmd.Parameters["@Guarantee"].Value = user.Guarantee;
                cmd.Parameters.Add("@name", SqlDbType.VarChar);
                cmd.Parameters["@name"].Value = user.PhotoName;
                cmd.Parameters.Add("@id", SqlDbType.Int);
                cmd.Parameters["@id"].Value = user.PhotoId;
                cmd.Parameters.Add("@Price", SqlDbType.Int);
                cmd.Parameters["@Price"].Value = user.Price;


                int numberOfRecords = cmd.ExecuteNonQuery();
            }
            return resp;
        }

        [HttpDelete]
        public HttpResponseMessage Delete([FromBody]PhotoClass user)
        {
            HttpResponseMessage resp = new HttpResponseMessage();

            return resp;
        }
    }
}