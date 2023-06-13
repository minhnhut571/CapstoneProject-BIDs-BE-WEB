using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data_Access.Entities;
using Business_Logic.Modules.SessionDetailModule.Interface;
using Business_Logic.Modules.SessionDetailModule.Request;
using BIDs_API.SignalR;
using Microsoft.AspNetCore.SignalR;
using Business_Logic.Modules.SessionDetailModule.Request;

namespace BIDs_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionDetailsController : ControllerBase
    {
        private readonly ISessionDetailService _SessionDetailService;
        private readonly IHubContext<SessionDetailHub> _hubSessionDetailContext;

        public SessionDetailsController(ISessionDetailService SessionDetailService
            , IHubContext<SessionDetailHub> hubSessionDetailContext)
        {
            _SessionDetailService = SessionDetailService;
            _hubSessionDetailContext = hubSessionDetailContext;
        }

        // GET api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SessionDetail>>> GetSessionDetailsForAdmin()
        {
            try
            {
                var response = await _SessionDetailService.GetAll();
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
        public async Task<ActionResult<SessionDetail>> GetSessionDetailByID([FromRoute] Guid? id)
        {
            var SessionDetail = await _SessionDetailService.GetSessionDetailByID(id);

            if (SessionDetail == null)
            {
                return NotFound();
            }

            return SessionDetail;
        }

        // GET api/<ValuesController>/abc
        [HttpGet("by_user/{id}")]
        public async Task<ActionResult<IEnumerable<SessionDetail>>> GetSessionDetailByUser([FromRoute] Guid? id)
        {
            try
            {
                var response = await _SessionDetailService.GetSessionDetailByUser(id);
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

        // GET api/<ValuesController>/abc
        [HttpGet("by_session/{id}")]
        public async Task<ActionResult<IEnumerable<SessionDetail>>> GetSessionDetailBySession([FromRoute] Guid? id)
        {
            try
            {
                var response = await _SessionDetailService.GetSessionDetailBySession(id);
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

        // PUT api/<ValuesController>/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutSessionDetail([FromBody] UpdateSessionDetailRequest updateSessionDetailRequest)
        {
            try
            {
                var SessionDetail = await _SessionDetailService.UpdateSessionDetail(updateSessionDetailRequest);
                await _hubSessionDetailContext.Clients.All.SendAsync("ReceiveSessionDetailUpdate", SessionDetail);
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
        public async Task<ActionResult<SessionDetail>> PostSessionDetail([FromBody] CreateSessionDetailRequest createSessionDetailRequest)
        {
            try
            {
                var SessionDetail = await _SessionDetailService.AddNewSessionDetail(createSessionDetailRequest);
                await _hubSessionDetailContext.Clients.All.SendAsync("ReceiveSessionDetailAdd", SessionDetail);
                return Ok(SessionDetail);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSessionDetail([FromRoute] Guid? id)
        {
            try
            {
                var SessionDetail = await _SessionDetailService.DeleteSessionDetail(id);
                await _hubSessionDetailContext.Clients.All.SendAsync("ReceiveSessionDetailDelete", SessionDetail);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
