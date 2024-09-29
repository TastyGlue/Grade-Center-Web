namespace GradeCenter.Web.Components.Account.Pages
{
    public partial class Register : ExtendedComponentBase<Register>
    {
        public MudForm FormRef { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string Password { get; set; } = default!;
        public DateTime? DateOfBirth { get; set; }
        public string? ErrorMessage { get; set; } = null;
        public bool IsRegisterSuccess { get; set; } = false;

        public async Task ValidSubmit()
        {
            var newUser = new AddUserRequest()
            {
                Email = Email,
                FullName = FullName,
                Password = Password,
                DateOfBirth = DateOfBirth!.Value
            };
            
            var client = CreateApiClient();

            var request = await client.PostAsJsonAsync("api/user", newUser);
            var content = await request.Content.ReadAsStringAsync();

            if (request.IsSuccessStatusCode)
                IsRegisterSuccess = true;
            else
                ErrorMessage = content;
        }

        private async Task RegisterClickHandler()
        {
            ErrorMessage = null;

            await FormRef.Validate();

            if (FormRef.IsValid)
                await ValidSubmit();
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

        private IEnumerable<string> FullNameValidity(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                yield return "Full Name is required";
                yield break;
            }
            if (fullName.Split(" ").Length < 2)
            {
                yield return "Full Name must contain at least two names";
                yield break;
            }
        }

        private string PasswordMatch(string arg)
        {
            if (arg != Password)
                return "Passwords don't match";
            return null;
        }

        private string DateTimeValidity(DateTime? arg)
        {
            if (arg is null)
                return "Date of Birth is required";
            return null;
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
