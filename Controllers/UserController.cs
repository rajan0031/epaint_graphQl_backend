using Microsoft.AspNetCore.Mvc;
using MyGraphqlApp.Interface;
using MyGraphqlApp.Model;

namespace MyGraphqlApp.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        
        [HttpGet("all")]
        public ActionResult<List<User>> GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            try
            {
                var user = await _userService.getUserById(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

 
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser([FromBody] User user)
        {
            var createdUser = await _userService.CreateUserAsync(user.Name,user.UserName, user.Email,user.Password,user.PhoneNumber, user.Role);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, [FromBody] User user)
        {
            var updatedUser = await _userService.UpdateUserAsync(id, user.Name, user.UserName,user.Email,user.PhoneNumber, user.Role);
            if (updatedUser == null)
                return NotFound(new { message = "User not found" });

            return Ok(updatedUser);
        }

      
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var deleted = await _userService.DeleteUserAsync(id);
            if (!deleted)
                return NotFound(new { message = "User not found" });

            return NoContent();
        }
    }
}
