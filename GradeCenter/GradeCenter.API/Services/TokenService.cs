namespace GradeCenter.API.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly byte[] securityKey;
        private readonly int tokenExpirationInMinutes;
        private readonly int refreshTokenExpirationInDays;

        public TokenService(IConfiguration config, RoleManager<IdentityRole> roleManager)
        {
            _config = config;
            _roleManager = roleManager;

            // Get values from config file
            securityKey = Encoding.UTF8.GetBytes(_config["JwtSettings: SecurityKey"]!);
            tokenExpirationInMinutes = int.Parse(_config["JwtSettings:TokenExpirationInMinutes"]!);
            refreshTokenExpirationInDays = int.Parse(_config["JwtSettings:RefreshTokenExpirationInDays"]!);
        }

        public async Task<string> GenerateAccessToken(User user, string roleName)
        {
            // Make a list of claims included in the token
            var claims = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Name, user.FullName),
                new(ClaimTypes.Email, user.Email!)
            };

            // Check if role exists
            var role = await _roleManager.FindByNameAsync(roleName.ToUpper());
            if (role == null)
                throw new Exception("Couldn't find role");

            // Add role to claims
            claims.Add(new(ClaimTypes.Role, roleName.ToLower()));

            // Set the key and algorithm for token signing
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(securityKey),
                SecurityAlgorithms.HmacSha256);

            // Set the description for token creation
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(tokenExpirationInMinutes),
                SigningCredentials = signingCredentials
            };

            // Create token
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }

        public string GenerateRefreshToken()
            => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
}
