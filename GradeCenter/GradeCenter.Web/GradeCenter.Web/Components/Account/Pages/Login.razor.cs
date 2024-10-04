namespace GradeCenter.Web.Components.Account.Pages
{
    public partial class Login : ExtendedComponentBase<Login>
    {
        public MudForm FormRef { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string? ErrorMessage { get; set; } = null;

        bool IsPasswordVisible = false;
        InputType PasswordInputType = InputType.Password;
        string PasswordIcon = Icons.Material.Sharp.VisibilityOff;

        bool isLoginSuccess = false;
        TokensResponse Tokens = new();

        public async Task ValidSubmit()
        {
            var loginRequest = new LoginRequest(Email, Password);
            var client = CreateApiClient();

            var request = await client.PostAsJsonAsync("api/auth/login", loginRequest);
            var content = await request.Content.ReadAsStringAsync();

            if (request.IsSuccessStatusCode)
            {
                var tokens = JsonSerializer.Deserialize<TokensResponse>(content, CaseInsensitiveJson);

                if (tokens is null)
                {
                    ErrorMessage = "An application error occurred";
                    return;
                }

                Tokens.AccessToken = tokens.AccessToken;
                Tokens.RefreshToken = tokens.RefreshToken;
                isLoginSuccess = true;
            }
            else
            {
                ErrorMessage = content;
            }
        }

        private async Task LoginClickHandler()
        {
            ErrorMessage = null;

            await FormRef.Validate();

            if (FormRef.IsValid)
                await ValidSubmit();
        }

        private void PasswordVisibilityHandler()
        {
            if (IsPasswordVisible)
            {
                PasswordIcon = Icons.Material.Filled.VisibilityOff;
                PasswordInputType = InputType.Password;
            }
            else
            {
                PasswordIcon = Icons.Material.Filled.Visibility;
                PasswordInputType = InputType.Text;
            }

            IsPasswordVisible = !IsPasswordVisible;
        }

        private IEnumerable<string> PasswordStrength(string pw)
        {
            if (string.IsNullOrWhiteSpace(pw))
            {
                yield return "Password is required";
                yield break;
            }
            if (pw.Length < 8)
                yield return "Password must be at least of length 8";
            if (!CapitalLetter().IsMatch(pw))
                yield return "Password must contain at least one capital letter";
            if (!LowercaseLetter().IsMatch(pw))
                yield return "Password must contain at least one lowercase letter";
            if (!Digit().IsMatch(pw))
                yield return "Password must contain at least one digit";
        }

        private IEnumerable<string> EmailValidity(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                yield return "Email is required";
                yield break;
            }
            if (!EmailFormat().IsMatch(email))
                yield return "Email is invalid";
        }

        #region Regex
        [GeneratedRegex(@"[A-Z]")]
        private static partial Regex CapitalLetter();
        [GeneratedRegex(@"[a-z]")]
        private static partial Regex LowercaseLetter();
        [GeneratedRegex(@"[0-9]")]
        private static partial Regex Digit();
        [GeneratedRegex("^[a-zA-Z0-9._%±]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,}$")]
        private static partial Regex EmailFormat();
        #endregion
    }
}
