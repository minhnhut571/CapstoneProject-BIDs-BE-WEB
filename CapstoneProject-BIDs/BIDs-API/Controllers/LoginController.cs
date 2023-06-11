using Business_Logic.Modules.LoginModule.InterFace;
using Business_Logic.Modules.LoginModule.Request;
using Data_Access.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace BIDs_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _LoginService;
        private readonly IConfiguration _configuration;
        //private readonly SignInManager<User> _signInManager;
        public LoginController(ILoginService LoginService
            //, SignInManager<User> signInManager
            , IConfiguration configuration)
        {
            _LoginService = LoginService;
            //_signInManager = signInManager;
            _configuration = configuration;
        }

        //// POST api/<ValuesController>
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        //{
        //    try
        //    {
        //        return Ok(_LoginService.Login(loginRequest));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpPost("login-user")]
        public async Task<IActionResult> LoginUser([FromBody] LoginRequest login)
        {
            var result = _LoginService.LoginUser(login);

            if(result == false)
                return BadRequest(new LoginRespone { Successful = false, Error = "Sai tài khoản hoặc mật khẩu"});

            var jwtToken = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddHours(Convert.ToInt32(2));

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                 {
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),

                    new Claim("UserId", string.Join(",", login.AccountName))

                }),
                Expires = expiry,
                SigningCredentials = creds
            };
            var token = jwtToken.CreateToken(tokenDescription);
            return Ok(new LoginRespone { Successful = true, Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

        [HttpPost("login-staff")]
        public async Task<IActionResult> LoginStaff([FromBody] LoginRequest login)
        {
            var result = _LoginService.LoginStaff(login);

            if (result == null)
                return BadRequest(new LoginRespone { Successful = false, Error = "Sai tài khoản hoặc mật khẩu" });

            var jwtToken = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddHours(Convert.ToInt32(2));

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                 {
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),

                    new Claim("UserId", string.Join(",", login.AccountName)),
                    new Claim("BranchIds", string.Join(",", result.RoleId)),

                }),
                Expires = expiry,
                SigningCredentials = creds
            };
            var token = jwtToken.CreateToken(tokenDescription);
            return Ok(new LoginRespone { Successful = true, Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

        //[HttpPost("decrypttoken")]
        //public async Task<IActionResult> DecryptToken(TokenModel tokenmodel)
        //{
        //    var token = tokenmodel.AccessToken;
        //    if (token != null)
        //    {
        //        var readToken = _LoginService.EncrypToken(token);
        //        var respone = readToken.Claims;
        //        var accountID = new Guid();
        //        foreach (var x in respone)
        //        {
        //            accountID = Guid.Parse(x.Value);
        //        }
        //        var userPermissions = await _permissionService.ShowPermission(userId);
        //        var user = await _userService.GetUserById(userId);
        //        user.UserPermission = (ICollection<Core.Display.DisplayPermission>)userPermissions;
        //        return Ok(new
        //        {
        //            User = user,
        //            BranchIds = branchIds,
        //        });
        //    }
        //    return BadRequest();
        //}

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
