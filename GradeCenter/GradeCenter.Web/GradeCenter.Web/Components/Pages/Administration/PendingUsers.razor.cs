namespace GradeCenter.Web.Components.Pages.Administration
{
    public partial class PendingUsers : ExtendedComponentBase<PendingUsers>
    {
        public List<PendingUserDto> PendingUsersList { get; set; } = [];
        public HashSet<PendingUserDto> SelectedPendingUsers { get; set; } = [];
        private string SearchString { get; set; } = default!;
        private bool IsLoading { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {
            var token = await TokenService.GetToken();
            var client = CreateApiClient(token);

            var request = await client.GetAsync("api/user/pending");
            if (request.IsSuccessStatusCode)
                PendingUsersList = (await request.Content.ReadFromJsonAsync<List<PendingUserDto>>(CaseInsensitiveJson) ?? [])
                    .OrderByDescending(x => x.CreatedOn).ToList();
            else
                Notify("Couldn't retrieve data from server", Severity.Error);

            IsLoading = false;
        }

        private Func<PendingUserDto, bool> QuickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(SearchString))
                return true;

            if (x.FullName.Contains(SearchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.Email.Contains(SearchString, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        };
    }
}
