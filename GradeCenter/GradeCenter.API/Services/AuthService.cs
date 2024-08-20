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

        public async Task<TokensResponse> Login(User user)
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

            var user = await _userManager.FindByIdAsync(token.UserId);

            if (user == null)
                return null;

            // Generate new tokens
            var tokens = await GenerateTokens(user);

            // Remove old refresh token from database
            _context.RefreshTokens.Remove(token);

            await _context.SaveChangesAsync();

            return new TokensResponse(tokens.AccessToken, tokens.RefreshToken);
        }

        private async Task<TokensResponse> GenerateTokens(User user)
        {
            // Get user role
            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            if (role == null)
                throw new Exception("Cannot generate tokens for a user without a role");

            // Generate tokens
            string accessToken = await _tokenService.GenerateAccessToken(user, role);
            string refreshToken = _tokenService.GenerateRefreshToken();

            await SaveRefreshTokenToDb(user.Id, refreshToken);

            return new TokensResponse(accessToken, refreshToken);
        }

        private async Task SaveRefreshTokenToDb(string userId, string refreshToken)
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
