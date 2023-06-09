using Business_Logic.Modules.LoginModule.InterFace;
using Business_Logic.Modules.LoginModule.Request;
using Business_Logic.Modules.RoleModule.Interface;
using Business_Logic.Modules.RoleModule.Request;
using Data_Access.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BIDs_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _LoginService;

        public LoginController(ILoginService LoginService)
        {
            _LoginService = LoginService;
        }

        // POST api/<ValuesController>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                return Ok(_LoginService.Login(loginRequest));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ValuesController>/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("reset-password/{email}")]
        public async Task<IActionResult> ResetPassword([FromRoute] string email)
        {
            try
            {
                await _LoginService.ResetPassword(email);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ValuesController>/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("send-email/{email}")]
        public async Task<IActionResult> sendemail([FromRoute] string email)
        {
            try
            {
                await _LoginService.sendemail(email);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
