namespace GradeCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // Admin only
        [HttpPost("add")]
        public async Task<IActionResult> AddUser([FromBody] AddUserRequest user)
        {
            try
            {
                var result = await _userService.AddUser(user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        // Authorize
        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            if (request.NewPassword != request.ConfirmPassword)
                return BadRequest("\"Confirm Password\" does not match \"Password\"");

            var result = await _userService.ChangePassword(request);

            return result ? Ok() : BadRequest("Password change failed");
        }
    }
}
