namespace GradeCenter.Web.Services
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedLocalStorage _localStorage;
        private readonly IOptionsMonitor<JwtSettings> _jwtSettingsMonitor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly NavigationManager _navigationManager;
        private readonly string _tokenKey = "accessToken";
        private readonly string _refreshTokenKey = "refreshToken";

        public CustomAuthStateProvider(ProtectedLocalStorage localStorage, IOptionsMonitor<JwtSettings> jwtSettingsMonitor, IHttpClientFactory httpClientFactory, NavigationManager navigationManager)
        {
            _localStorage = localStorage;
            _jwtSettingsMonitor = jwtSettingsMonitor;
            _httpClientFactory = httpClientFactory;
            _navigationManager = navigationManager;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var accessToken = (await _localStorage.GetAsync<string>(_tokenKey)).Value;

            var identity = new ClaimsIdentity();

            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                if (ValidateToken(accessToken))
                {
                    var claims = ParseClaimsFromJwt(accessToken);
                    identity = new ClaimsIdentity(claims, "jwtAuth");
                }
                else
                {
                    // Attempt to refresh the token
                    var refreshToken = (await _localStorage.GetAsync<string>(_refreshTokenKey)).Value;

                    if (await TryRefreshToken(accessToken, refreshToken))
                    {
                        accessToken = (await _localStorage.GetAsync<string>(_tokenKey)).Value; // Get new token
                        var claims = ParseClaimsFromJwt(accessToken!);
                        identity = new ClaimsIdentity(claims, "jwtAuth");
                    }
                    else
                    {
                        // Redirect to login if refresh token is also expired
                        _navigationManager.NavigateTo("/account/login");
                    }
                }
            }

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        private bool ValidateToken(string token)
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

        private IEnumerable<Claim> ParseClaimsFromJwt(string token)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtHandler.ReadJwtToken(token);
            return jwtToken.Claims;
        }

        private async Task<bool> TryRefreshToken(string accessToken, string? refreshToken)
        {
            if (refreshToken is null)
                return false;

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(accessToken);

            // Check if the token is expired
            var expiryDateUnix = long.Parse(jwtToken.Claims.First(c => c.Type == JwtRegisteredClaimNames.Exp).Value);
            var expiryDate = DateTimeOffset.FromUnixTimeSeconds(expiryDateUnix);

            // Check if the refresh token expiration is still valid
            if (expiryDate.AddDays(_jwtSettingsMonitor.CurrentValue.RefreshTokenExpirationInDays) >= DateTime.UtcNow)
            {
                // Call the API to refresh the token
                var httpClient = _httpClientFactory.CreateClient(Constants.API_CLIENT_NAME);
                var response = await httpClient.GetAsync($"api/auth/refresh?refreshToken={Uri.EscapeDataString(refreshToken)}");

                if (response.IsSuccessStatusCode)
                {
                    var tokens = await response.Content.ReadFromJsonAsync<TokensResponse>(new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    if (tokens is null || tokens.AccessToken is null)
                        return false;

                    // Store the new tokens in LocalStorage
                    await _localStorage.SetAsync(_tokenKey, tokens.AccessToken);
                    await _localStorage.SetAsync(_refreshTokenKey, tokens.RefreshToken);

                    return true; // Successfully refreshed token
                }
            }

            return false;
        }
    }
}
