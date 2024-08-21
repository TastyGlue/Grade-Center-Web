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

        [HttpGet]
        public IActionResult TestDateOnly()
        {
            var dateonly = new DateOnly(2014, 4, 20);
            return Ok(dateonly);
        }
    }
}
