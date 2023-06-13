using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data_Access.Entities;
using Business_Logic.Modules.RoleModule.Interface;
using Business_Logic.Modules.RoleModule.Request;
using BIDs_API.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace BIDs_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _RoleService;
        private readonly IHubContext<RoleHub> _hubRoleContext;
        public RolesController(IRoleService RoleService
            , IHubContext<RoleHub> hubRoleContext)
        {
            _RoleService = RoleService;
            _hubRoleContext = hubRoleContext;
        }

        // GET api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRolesForAdmin()
        {
            try
            {
                var response = await _RoleService.GetAll();
                if(response == null)
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
        public async Task<ActionResult<Role>> GetRoleByID([FromRoute] int id)
        {
            var Role = await _RoleService.GetRoleByID(id);

            if (Role == null)
            {
                return NotFound();
            }

            return Role;
        }

        // GET api/<ValuesController>/abc
        [HttpGet("by_name/{name}")]
        public async Task<ActionResult<Role>> GetRoleByName([FromRoute] string name)
        {
            var Role = await _RoleService.GetRoleByName(name);

            if (Role == null)
            {
                return NotFound();
            }

            return Role;
        }

        // PUT api/<ValuesController>/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutRole([FromBody] UpdateRoleRequest updateRoleRequest)
        {
            try
            {
                var Role = await _RoleService.UpdateRole(updateRoleRequest);
                await _hubRoleContext.Clients.All.SendAsync("ReceiveRoleUpdate", Role);
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
        public async Task<ActionResult<Role>> PostRole([FromBody] CreateRoleRequest createRoleRequest)
        {
            try
            {
                var Role = await _RoleService.AddNewRole(createRoleRequest);
                await _hubRoleContext.Clients.All.SendAsync("ReceiveRoleAdd", Role);
                return Ok(Role);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole([FromRoute] int id)
        {
            try
            {
                var Role = await _RoleService.DeleteRole(id);
                await _hubRoleContext.Clients.All.SendAsync("ReceiveRoleDelete", Role);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
