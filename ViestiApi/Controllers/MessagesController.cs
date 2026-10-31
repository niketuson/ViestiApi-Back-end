using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViestiApi.Middleware;
using ViestiApi.Models;
using ViestiApi.Services;

namespace ViestiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        //private readonly MessageContext _context;
        private readonly IMessageService _messageService;
        private readonly IUserAuthenticationService _userAuthenticationService;
        public MessagesController(IMessageService service, IUserAuthenticationService authService)
        {
            _messageService = service;
            _userAuthenticationService = authService;
        }

        // GET: api/Messages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetMessages()
        {
            return Ok (await _messageService.GetMessagesAsync());
        }

        // GET: api/Messages/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<MessageDTO>> GetMessage(long id)
        {
            // onko minun viesti
            string username = this.User.FindFirst(ClaimTypes.Name).Value;
            if (!await _userAuthenticationService.CanReadMessage(username, id))
            {
                return Unauthorized();
            }

            var message = await _messageService.GetMessageAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            return message;
        }

        //Hae minulle lähetetyt viestit
        [HttpGet("received/{username}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetMyMessages(string username)
        {
            string authUser = this.User.FindFirst(ClaimTypes.Name).Value;
            if (username != authUser)
            {
                return Unauthorized();
            }
            return Ok(await _messageService.GetMyReceivedMessages(username));

        }

        // PUT: api/Messages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutMessage(long id, MessageDTO message)
        {
            
            if (id != message.Id)
            {
                return BadRequest();
            }

            //Tarkistetaan onko oma viesti
            string username = this.User.FindFirst(ClaimTypes.Name).Value;
            if (!await _userAuthenticationService.IsMyMessage(username, id))
            {
                return Unauthorized();
            }


            bool result = await _messageService.UpdateMessageAsync(message);
            
            if(!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Messages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Message>> PostMessage(MessageDTO message)
        {
            string username = this.User.FindFirst(ClaimTypes.Name).Value;
            if (username != message.SenderID)
            {
                return Unauthorized();
            }

            MessageDTO? newMessage = await _messageService.CreateMessageAsync(message);
            if(newMessage == null)
            {
                return Problem();

            }

            return CreatedAtAction("GetMessage", new { id = newMessage.Id }, newMessage);
        }

        // DELETE: api/Messages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(long id)
        {
            string username = this.User.FindFirst(ClaimTypes.Name).Value;
            if (!await _userAuthenticationService.IsMyMessage(username, id))
            {
                return Unauthorized();
            }

            bool result = await _messageService.DeleteMessageAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
