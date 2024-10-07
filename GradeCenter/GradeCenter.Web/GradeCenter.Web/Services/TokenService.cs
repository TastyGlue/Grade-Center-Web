namespace GradeCenter.Web.Services
{
    public class TokenService
    {
        private readonly IOptionsMonitor<JwtSettings> _jwtSettingsMonitor;
        private readonly ProtectedLocalStorage _localStorage;
        private readonly NavigationManager _navigationManager;

        public TokenService(IOptionsMonitor<JwtSettings> jwtSettingsMonitor, ProtectedLocalStorage localStorage, NavigationManager navigationManager)
        {
            _jwtSettingsMonitor = jwtSettingsMonitor;
            _localStorage = localStorage;
            _navigationManager = navigationManager;
        }

        public bool ValidateToken(string token)
        {
            var jwtSettings = _jwtSettingsMonitor.CurrentValue;  // Get the current settings
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSettings.SecurityKey);

            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero  // Set clock skew to zero to avoid delay in expiration validation
                };

                // This validates the token and throws an exception if invalid
                tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                // If we get here, the token is valid
                return true;
            }
            catch
            {
                // Token is invalid (could be expired, tampered with, etc.)
                return false;
            }
        }

        public IEnumerable<Claim> ParseClaimsFromJwt(string token)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtHandler.ReadJwtToken(token);
            return jwtToken.Claims;
        }

        public async Task<string> GetToken()
        {
            var token = (await _localStorage.GetAsync<string>("accessToken")).Value;
            if (token is null)
            {
                _navigationManager.NavigateTo("/account/login", forceLoad: true);
                return string.Empty;
            }

            return token;
        }
    }
}
