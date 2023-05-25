using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HIS_WebApi
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IHubContext<MessageHub> _hubContext;

        public MessageController(IHubContext<MessageHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] string message)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", message);
            return Ok();
        }
    }
}
