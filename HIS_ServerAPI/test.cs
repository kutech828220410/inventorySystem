using Basic;
using HIS_DB_Lib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyOffice;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS_ServerAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class testController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            var localIpAddress = HttpContext.Connection.LocalIpAddress?.ToString();
            var localPort = HttpContext.Connection.LocalPort;
            var protocol = HttpContext.Request.IsHttps ? "https" : "http";
            returnData returnData = new returnData();
            returnData.Code = 200;
            returnData.Result = $"Api test sucess!{protocol}://{localIpAddress}:{localPort}";

            return returnData.JsonSerializationt(true);
        }
     
    }
}
