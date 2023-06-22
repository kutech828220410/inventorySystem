using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Cors;
using HIS_DB_Lib;
using Basic;
namespace HIS_ServerAPI
{
  
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors]
    public class MessageController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public MessageController(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(returnData returnData)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage","" , returnData.JsonSerializationt());
            return Ok();
        }
    }
}
