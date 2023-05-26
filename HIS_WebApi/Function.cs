using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HIS_WebApi
{

   static class Method
    {

        static public void  GetLocalUrl()
        {
            //var localIpAddress = HttpContext.Connection.LocalIpAddress?.ToString();
            //var localPort = HttpContext.Connection.LocalPort;
            //var protocol = HttpContext.Request.IsHttps ? "https" : "http";
            //return $"{protocol}://{localIpAddress}:{localPort}";
        }
    }
}
