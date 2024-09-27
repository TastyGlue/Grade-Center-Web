namespace GradeCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthService _authService;

        public AuthController(UserManager<User> userManager, IAuthService authService)
        {
            _userManager = userManager;
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokensResponse>> Login([FromBody] LoginRequest request)
        {
            // Find user with given email
            var user = await _userManager.FindByEmailAsync(request.Email.ToUpper());

            // Check if user exists
            if (user == null)
                return NotFound("Couldn't find user with these credentials");

            // Check if user is active
            if (!user.IsActive)
                return NotFound("Your account is deactivated. If you think this is a mistake - contact your school administrator");

            // Check if password is valid
            bool isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid)
                return NotFound("Couldn't find user with these credentials");

            try
            {
                var result = await _authService.Login(user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpGet("refresh")]
        public async Task<ActionResult<TokensResponse>> Refresh([FromQuery] string refreshToken)
        {
            var result = await _authService.RefreshToken(refreshToken);

            // Check if refresh token process was successful
            if (result == null)
                return Forbid();

            return Ok(result);
        }
    }
}
