namespace GradeCenter.API.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly byte[] securityKey;
        private readonly int tokenExpirationInMinutes;

        public TokenService(IConfiguration config, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _config = config;
            _roleManager = roleManager;

            // Get values from config file
            securityKey = Encoding.UTF8.GetBytes(_config["JwtSettings:SecurityKey"]!);
            tokenExpirationInMinutes = int.Parse(_config["JwtSettings:TokenExpirationInMinutes"]!);
        }

        public string GenerateAccessToken(User user, IList<string> roles)
        {
            // Make a list of claims included in the token
            var claims = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.FullName),
                new(ClaimTypes.Email, user.Email!)
            };

            // Add roles to claims
            claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)));

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

        public TokenContent? GetTokenContentFromAuthHeader(StringValues authHeader)
        {
            // Get the token string
            var tokenString = authHeader.ToString().Split(' ')[1];
            var token = new JwtSecurityToken(tokenString);

            // Get claims from token
            var claims = token.Claims.ToList();

            // Get the values of the claims
            var userId = claims.FirstOrDefault(x => x.Type == "nameid")?.Value;
            if (userId == null)
                return null;

            var fullName = claims.FirstOrDefault(x => x.Type == "unique_name")?.Value;
            if (fullName == null)
                return null;

            var email = claims.FirstOrDefault(x => x.Type == "email")?.Value;
            if (email == null)
                return null;

            var roleString = claims.FirstOrDefault(x => x.Type == "role")?.Value;
            if (roleString == null)
                return null;

            // Check if role exists
            if (!Enum.TryParse(roleString.ToUpper(), out Roles role))
                return null;

            return new TokenContent()
            {
                UserId = new Guid(userId),
                Email = email,
                FullName = fullName,
                Role = role
            };
        }
    }
}
