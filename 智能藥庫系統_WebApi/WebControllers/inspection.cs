using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SQLUI;
using Basic;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Configuration;

namespace 智慧藥庫系統_WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class inspectionController : Controller
    {
        public class returnData
        {
            private List<object> _data = new List<object>();
            private int _code = 0;
            private string _result = "";

            public List<object> Data { get => _data; set => _data = value; }
            public int Code { get => _code; set => _code = value; }
            public string Result { get => _result; set => _result = value; }
        }
        //[HttpGet]
        //public string Get()
        //{

        //}
    }
}
