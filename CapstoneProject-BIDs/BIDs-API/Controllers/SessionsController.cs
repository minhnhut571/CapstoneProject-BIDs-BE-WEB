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

namespace BIDs_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly ISessionService _SessionService;

        public SessionsController(ISessionService SessionService)
        {
            _SessionService = SessionService;
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
                await _SessionService.UpdateSession(updateSessionRequest);
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
                return Ok(await _SessionService.AddNewSession(createSessionRequest));
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
                await _SessionService.DeleteSession(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
