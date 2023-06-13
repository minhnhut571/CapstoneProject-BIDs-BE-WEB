using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data_Access.Entities;
using Business_Logic.Modules.SessionModule.Interface;
using Business_Logic.Modules.SessionModule.Request;
using BIDs_API.SignalR;
using Microsoft.AspNetCore.SignalR;
using Business_Logic.Modules.SessionModule.Request;

namespace BIDs_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly ISessionService _SessionService;
        private readonly IHubContext<SessionHub> _hubSessionContext;

        public SessionsController(ISessionService SessionService
            , IHubContext<SessionHub> hubSessionContext)
        {
            _SessionService = SessionService;
            _hubSessionContext = hubSessionContext;
        }

        // GET api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Session>>> GetSessionsForAdmin()
        {
            try
            {
                var response = await _SessionService.GetAll();
                if (response == null)
                {
                    return NotFound();
                }
                return Ok(response);
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Session>> GetSessionByID([FromRoute] Guid? id)
        {
            var Session = await _SessionService.GetSessionByID(id);

            if (Session == null)
            {
                return NotFound();
            }

            return Session;
        }

        // GET api/<ValuesController>/abc
        [HttpGet("by_name/{name}")]
        public async Task<ActionResult<Session>> GetSessionByName([FromRoute] string name)
        {
            var Session = await _SessionService.GetSessionByName(name);

            if (Session == null)
            {
                return NotFound();
            }

            return Session;
        }

        // PUT api/<ValuesController>/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutSession([FromBody] UpdateSessionRequest updateSessionRequest)
        {
            try
            {
                var session = await _SessionService.UpdateSession(updateSessionRequest);
                await _hubSessionContext.Clients.All.SendAsync("ReceiveSessionUpdate", session);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<ValuesController>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Session>> PostSession([FromBody] CreateSessionRequest createSessionRequest)
        {
            try
            {
                var Session = await _SessionService.AddNewSession(createSessionRequest);
                await _hubSessionContext.Clients.All.SendAsync("ReceiveSessionAdd", Session);
                return Ok(Session);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSession([FromRoute] Guid? id)
        {
            try
            {
                var Session = await _SessionService.DeleteSession(id);
                await _hubSessionContext.Clients.All.SendAsync("ReceiveSessionDelete", Session);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
