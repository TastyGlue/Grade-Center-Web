namespace GradeCenter.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly GradeCenterDbContext _context;
        private readonly IConfiguration _config;
        private readonly int refreshTokenExpirationInDays;

        public AuthService(UserManager<User> userManager, ITokenService tokenService, GradeCenterDbContext context, IConfiguration config)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _context = context;
            _config = config;

            // Get value from config file
            refreshTokenExpirationInDays = int.Parse(_config["JwtSettings:RefreshTokenExpirationInDays"]!);
        }

        public async Task<TokensResponse> Login(User user, string? role = null)
        {
            // Remove old tokens for user
            var oldTokens = await _context.RefreshTokens.Where(x => x.UserId == user.Id).ToArrayAsync();
            _context.RefreshTokens.RemoveRange(oldTokens);

            await _context.SaveChangesAsync();

            // Generate new tokens for user
            var tokens = await GenerateTokens(user);
            return tokens;
        }

        public async Task<TokensResponse?> RefreshToken(string refreshToken)
        {
            // Find refresh token in database
            var token = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == refreshToken);

            // Check if refresh token is valid
            if (token == null)
                return null;

            if (token.IsUsed)
                return null;

            if (DateTime.UtcNow > token.ExpireOn)
                return null;

            var user = await _userManager.FindByIdAsync(token.UserId.ToString());

            if (user == null)
                return null;

            // Generate new tokens
            var tokens = await GenerateTokens(user);

            // Remove old refresh token from database
            _context.RefreshTokens.Remove(token);

            await _context.SaveChangesAsync();

            return tokens;
        }

        private async Task<TokensResponse> GenerateTokens(User user, string? role = null)
        {
            // Get user role
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Count == 0)
                throw new Exception("Cannot generate tokens for a user without a role");

            if (role is not null)
                roles = new List<string> { role };

            // Generate tokens
            string accessToken = _tokenService.GenerateAccessToken(user, roles);
            string refreshToken = _tokenService.GenerateRefreshToken();

            await SaveRefreshTokenToDb(user.Id, refreshToken);

            return new()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        private async Task SaveRefreshTokenToDb(Guid userId, string refreshToken)
        {
            // Add refresh token to database
            _context.RefreshTokens.Add(new()
            {
                UserId = userId,
                Token = refreshToken,
                IsUsed = false,
                ExpireOn = DateTime.UtcNow.AddDays(refreshTokenExpirationInDays)
            });

            await _context.SaveChangesAsync();
        }
    }
}
