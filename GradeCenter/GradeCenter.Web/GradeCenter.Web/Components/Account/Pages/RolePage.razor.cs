namespace GradeCenter.Web.Components.Account.Pages
{
    public partial class RolePage : ExtendedComponentBase<RolePage>
    {
        [Parameter] public string AccessToken { get; set; } = default!;
        [Parameter] public string RefreshToken { get; set; } = default!;

        public IEnumerable<string> Roles { get; set; } = [];
        public string SelectedRole { get; set; } = default!;
        public bool IsLoaded { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            AccessToken = Encoding.UTF8.GetString(Convert.FromBase64String(AccessToken));
            RefreshToken = Encoding.UTF8.GetString(Convert.FromBase64String(RefreshToken));

            if (!TokenService.ValidateToken(AccessToken))
                NavigationManager.NavigateTo("/account/login");

            Roles = TokenService.ParseClaimsFromJwt(AccessToken)
                .Where(x => x.Type == "role")
                .Select(x => ToTitleCase(x.Value))
                .ToList();

            if (Roles.Count() < 2)
                await DirectLogin();

            IsLoaded = true;
        }

        private async Task DirectLogin()
        {
            if (!TokenService.ValidateToken(AccessToken))
                NavigationManager.NavigateTo("/account/login");

            await LocalStorage.SetAsync("accessToken", AccessToken);
            await LocalStorage.SetAsync("refreshToken", AccessToken);

            NavigationManager.NavigateTo("/", forceLoad: true);
        }

        private async Task ContinueHandler()
        {
            var client = CreateApiClient(AccessToken);
            var result = await client.GetAsync($"api/auth/role/{SelectedRole}");
            var content = await result.Content.ReadAsStringAsync();

            if (result.IsSuccessStatusCode)
            {
                var tokens = JsonSerializer.Deserialize<TokensResponse>(content, CaseInsensitiveJson);

                if (tokens is null)
                {
                    string errorMessage = "There occured an application error";
                    Notify(errorMessage, Severity.Error);
                    NavigationManager.NavigateTo("/account/login");
                    return;
                }

                await LocalStorage.SetAsync("accessToken", tokens.AccessToken);
                await LocalStorage.SetAsync("refreshToken", tokens.RefreshToken);

                NavigationManager.NavigateTo("/", forceLoad: true);
            }
            else
            {
                Notify(content, Severity.Error);
                NavigationManager.NavigateTo("/account/login");
            }
        }

        private void BackHandler()
        {
            NavigationManager.NavigateTo("/account/login");
        }
    }
}
