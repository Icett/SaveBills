﻿using APIWebBills.Models;
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
    public class LoginController : ApiController
    {
        // GET api/login
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/login/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/login
        [HttpPost]
        public string Post([FromBody]LoginClass user)
        {
            return "response " + user.userName + ", " + user.userPsswd;
            // Pobranie uzytkownika
            //string test = "";
            //using (SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connection"].ConnectionString))
            //{
            //    con.Open();
            //    using (SqlCommand command = new SqlCommand("SELECT * FROM ACCOUNT", con))
            //    {
            //        using (SqlDataReader reader = command.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {
            //                for (int i = 0; i < reader.FieldCount; i++)
            //                {
            //                    test += reader.GetValue(i);
            //                }
            //            }
            //        }
            //    }
            //}
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
    }
}