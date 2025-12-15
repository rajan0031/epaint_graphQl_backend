
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyGraphqlApp.dtos;
using MyGraphqlApp.Model;
using MyGraphqlApp.Utils;

namespace MyGraphqlApp.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtUtils _jwtUtils;



        public UsersController(IUserService userService, JwtUtils jwtUtils)
        {
            _userService = userService;
            _jwtUtils = jwtUtils;
        }

        [Authorize]
        [HttpGet("all")]
        public ActionResult<List<User>> GetAllUsers()
        {


            var userDetails = _jwtUtils.ExtractLoggedInUserDetails();
            // Console.WriteLine("get the user details", userDetails);
            Console.WriteLine(userDetails.Id);
            Console.WriteLine(userDetails.Name);
            Console.WriteLine(userDetails.Role);
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {

            var user = await _userService.getUserById(id);
            return Ok(user);


        }


        [HttpPost]
        public async Task<ActionResult<User>> CreateUser([FromBody] User user)
        {
            var createdUser = await _userService.CreateUserAsync(user.Name, user.UserName, user.Email, user.Password, user.PhoneNumber, user.Role);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, [FromBody] User user)
        {
            var updatedUser = await _userService.UpdateUserAsync(id, user.Name, user.UserName, user.Email, user.PhoneNumber, user.Role);
            if (updatedUser == null)
                return NotFound(new { message = "User not found" });

            return Ok(updatedUser);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var deleted = await _userService.DeleteUserAsync(id);
            if (!deleted)
                return NotFound(new { message = "User not found" });

            return NoContent();
        }

        [HttpPost("login")]
        public async Task<UserDto.LoginResponse> LoginUser(UserDto.loginDto loginDto)
        {
            var result = await _userService.loginUser(loginDto);
            return result;
        }


        [HttpPost("changepassword")]
        public string changePassword(UserDto.ChangePasswordDto changePasswordDto)
        {
            return _userService.changePassword(changePasswordDto);
        }
    }

}
