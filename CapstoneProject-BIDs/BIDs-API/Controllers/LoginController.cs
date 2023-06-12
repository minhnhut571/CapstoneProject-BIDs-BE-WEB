using Business_Logic.Modules.LoginModule.InterFace;
using Business_Logic.Modules.LoginModule.Request;
using Data_Access.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Business_Logic.Modules.StaffModule.Interface;
using Business_Logic.Modules.UserModule.Interface;
using Business_Logic.Modules.RoleModule.Interface;

namespace BIDs_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _LoginService;
        private readonly IConfiguration _configuration;
        private readonly IStaffService _staffService;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        public LoginController(ILoginService LoginService
            , IConfiguration configuration
            , IStaffService staffService
            , IUserService userService
            , IRoleService roleService)
        {
            _LoginService = LoginService;
            _configuration = configuration;
            _staffService = staffService;
            _userService = userService;
            _roleService = roleService;
        }


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

                    new Claim("UserName", string.Join(",", login.AccountName))

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

                    new Claim("StaffName", string.Join(",", login.AccountName)),
                    new Claim("RoleID", string.Join(",", result.RoleId)),

                }),
                Expires = expiry,
                SigningCredentials = creds
            };
            var token = jwtToken.CreateToken(tokenDescription);
            return Ok(new LoginRespone { Successful = true, Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

        [HttpPost("decrypttoken-staff")]
        public async Task<IActionResult> DecryptTokenForStaff([FromHeader]string token)
        {
            if (token != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                //Decode JWT
                var claims = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var readToken = claims;
                var respone = readToken.Claims;
                var staffName = "";
                int roleId = new int();
                foreach (var x in respone)
                {
                    switch (x.Type)
                    {
                        case "StaffName":
                            staffName = x.Value;
                            break;
                        case "RoleID":
                            roleId = int.Parse(x.Value);
                            break;
                    }
                }
                var staff = await _staffService.GetStaffByAccountName(staffName);
                var role = await _roleService.GetRoleByID(roleId);
                return Ok(new
                {
                    Staff = staff,
                    Role = role
                });
            }
            return BadRequest();
        }

        [HttpPost("decrypttoken-user")]
        public async Task<IActionResult> DecryptTokenForUser([FromHeader] string token)
        {
            
            if (token != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                //Decode JWT
                var claims = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var readToken = claims;
                var respone = readToken.Claims;
                var username = "";
                foreach (var x in respone)
                {
                    switch (x.Type)
                    {
                        case "UserName":
                            username = x.Value;
                            break;
                    }
                }
                var user = await _userService.GetUserByAccountName(username);
                return Ok(new
                {
                    User = user,
                });
            }
            return BadRequest();
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
