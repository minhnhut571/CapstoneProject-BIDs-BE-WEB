using Microsoft.AspNetCore.Mvc;
using Data_Access.Entities;
using Business_Logic.Modules.UserModule.Interface;
using Business_Logic.Modules.UserModule.Request;
using AutoMapper;
using Business_Logic.Modules.UserModule.Response;

namespace BIDs_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public readonly IMapper _mapper;

        public UsersController(IUserService userService
            , IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        // GET api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetUsersForAdmin()
        {
            try
            {
                var list = await _userService.GetAll();
                var response = list.Select
                           (
                             emp => _mapper.Map<User, UserResponse>(emp)
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

        // GET api/<ValuesController>
        [HttpGet("get-active")]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetUsersActive()
        {
            try
            {
                var list = await _userService.GetUsersIsActive();
                var response = list.Select
                           (
                             emp => _mapper.Map<User, UserResponse>(emp)
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

        // GET api/<ValuesController>
        [HttpGet("get-waitting")]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetUsersWaitting()
        {
            try
            {
                var list = await _userService.GetUsersIsWaitting();
                var response = list.Select
                           (
                             emp => _mapper.Map<User, UserResponse>(emp)
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

        // GET api/<ValuesController>
        [HttpGet("get-ban")]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetUsersBan()
        {
            try
            {
                var list = await _userService.GetUsersIsBan();
                var response = list.Select
                           (
                             emp => _mapper.Map<User, UserResponse>(emp)
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
        public async Task<ActionResult<UserResponse>> GetUserByID([FromRoute] Guid id)
        {
            var user = _mapper.Map<UserResponse>(await _userService.GetUserByID(id));

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // GET api/<ValuesController>/abc
        [HttpGet("by_name/{name}")]
        public async Task<ActionResult<UserResponse>> GetUserByName([FromRoute] string name)
        {
            var user = _mapper.Map<UserResponse>(await _userService.GetUserByName(name)); ;

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // GET api/<ValuesController>/abc
        [HttpGet("by_account_name/{name}")]
        public async Task<ActionResult<UserResponse>> GetUserByAccountName([FromRoute] string name)
        {
            var user = _mapper.Map<UserResponse>(await _userService.GetUserByAccountName(name)); ;

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT api/<ValuesController>/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutUser([FromBody] UpdateUserRequest updateUserRequest)
        {
            try
            {
                await _userService.UpdateUser(updateUserRequest);
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
        public async Task<ActionResult<UserResponse>> PostUser([FromBody] CreateUserRequest createUserRequest)
        {
            try
            {
                return Ok(_mapper.Map<UserResponse>(await _userService.AddNewUser(createUserRequest)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //// DELETE api/<ValuesController>/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        //{
        //    try
        //    {
        //        await _userService.DeleteUser(id);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //private bool UserExists(Guid id)
        //{
        //    return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        //}
    }
}
