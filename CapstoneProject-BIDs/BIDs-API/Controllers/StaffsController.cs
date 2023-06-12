using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data_Access.Entities;
using Business_Logic.Modules.StaffModule.Interface;
using Business_Logic.Modules.StaffModule.Request;
using Business_Logic.Modules.StaffModule.Response;
using AutoMapper;

namespace BIDs_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffsController : ControllerBase
    {
        private readonly IStaffService _StaffService;
        private readonly IMapper _mapper;

        public StaffsController(IStaffService StaffService
            , IMapper mapper)
        {
            _StaffService = StaffService;
            _mapper = mapper;
        }

        // GET api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffResponse>>> GetStaffsForAdmin()
        {
            try
            {
                
                var list = await _StaffService.GetAll();
                var response = list.Select
                           (
                             emp => _mapper.Map<Staff, StaffResponse>(emp)
                           );
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
        public async Task<ActionResult<StaffResponse>> GetStaffByID([FromRoute] Guid id)
        {
            var Staff = _mapper.Map<StaffResponse>( await _StaffService.GetStaffByID(id));

            if (Staff == null)
            {
                return NotFound();
            }

            return Staff;
        }

        // GET api/<ValuesController>/abc
        [HttpGet("by_name/{name}")]
        public async Task<ActionResult<StaffResponse>> GetStaffByName([FromRoute] string name)
        {
            var Staff = _mapper.Map<StaffResponse>(await _StaffService.GetStaffByName(name));

            if (Staff == null)
            {
                return NotFound();
            }

            return Staff;
        }

        [HttpGet("by_account_name/{name}")]
        public async Task<ActionResult<StaffResponse>> GetStaffByAccountName([FromRoute] string name)
        {
            var Staff = _mapper.Map<StaffResponse>(await _StaffService.GetStaffByAccountName(name));

            if (Staff == null)
            {
                return NotFound();
            }

            return Staff;
        }

        // PUT api/<ValuesController>/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutStaff([FromBody] UpdateStaffRequest updateStaffRequest)
        {
            try
            {
                await _StaffService.UpdateStaff(updateStaffRequest);
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
        public async Task<ActionResult<StaffResponse>> PostStaff([FromBody] CreateStaffRequest createStaffRequest)
        {
            try
            {
                return Ok(_mapper.Map<StaffResponse>(await _StaffService.AddNewStaff(createStaffRequest)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff([FromRoute] Guid id)
        {
            try
            {
                await _StaffService.DeleteStaff(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ValuesController>/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("accept_user/{AcceptID}")]
        public async Task<IActionResult> AcceptAccountCreate([FromRoute] Guid AcceptID)
        {
            try
            {
                await _StaffService.AcceptCreateAccount(AcceptID);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ValuesController>/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("deny_user/{DenyID}")]
        public async Task<IActionResult> DenyAccountCreate([FromRoute] Guid DenyID)
        {
            try
            {
                await _StaffService.DenyCreate(DenyID);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ValuesController>/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=212375
        [HttpPut("ban/{BanID}")]
        public async Task<IActionResult> BanUser([FromRoute] Guid BanID)
        {
            try
            {
                await _StaffService.BanUser(BanID);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ValuesController>/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("unban/{UnbanID}")]
        public async Task<IActionResult> UnbanUser([FromRoute] Guid UnbanID)
        {
            try
            {
                await _StaffService.UnbanUser(UnbanID);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //private bool StaffExists(Guid id)
        //{
        //    return (_context.Staffs?.Any(e => e.StaffId == id)).GetValueOrDefault();
        //}
    }
}
