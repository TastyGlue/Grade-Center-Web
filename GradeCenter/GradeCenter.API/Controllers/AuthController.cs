namespace GradeCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;

        public AuthController(UserManager<User> userManager, IAuthService authService, ITokenService tokenService)
        {
            _userManager = userManager;
            _authService = authService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokensResponse>> Login([FromBody] LoginRequest request)
        {
            // Find user with given email
            var user = await _userManager.FindByEmailAsync(request.Email.ToUpper());

            // Check if user exists
            if (user == null)
                return NotFound("Incorrect email or password");

            // Check if user has any roles
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Count == 0)
                return Unauthorized("Your register request has not been reviewed yet");

            // Check if user is active
            if (!user.IsActive)
                return Forbid("Your account is deactivated");

            // Check if password is valid
            bool isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid)
                return NotFound("Incorrect email or password");

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

        [Authorize]
        [HttpGet("role/{role}")]
        public async Task<IActionResult> LoginRole(string role)
        {
            var authHeader = HttpContext.Request.Headers.Authorization;

            var token = _tokenService.GetTokenContentFromAuthHeader(authHeader);
            if (token is null)
                return BadRequest("An unexpected error occurred");

            var user = await _userManager.FindByIdAsync(token.UserId.ToString());
            if (user is null)
                return BadRequest("An unexpected error occurred");

            try
            {
                var result = await _authService.Login(user, role);
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
