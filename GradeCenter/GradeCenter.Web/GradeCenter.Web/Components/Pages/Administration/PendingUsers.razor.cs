namespace GradeCenter.Web.Components.Pages.Administration
{
    public partial class PendingUsers : ExtendedComponentBase<PendingUsers>
    {
        public ObservableCollection<PendingUserDto> PendingUsersList { get; set; } = [];
        public HashSet<PendingUserDto> SelectedPendingUsers { get; set; } = [];
        private string SearchString { get; set; } = default!;
        private bool IsLoading { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {
            var token = await TokenService.GetToken();
            var client = CreateApiClient(token);

            var request = await client.GetAsync("api/user/pending");
            if (request.IsSuccessStatusCode)
                PendingUsersList = new((await request.Content.ReadFromJsonAsync<List<PendingUserDto>>(CaseInsensitiveJson) ?? [])
                    .OrderByDescending(x => x.CreatedOn).ToList());
            else
                Notify("Couldn't retrieve data from server", Severity.Error);

            IsLoading = false;
        }

        private async Task RejectHandler()
        {
            string header = "Reject requests?";
            string content = "Are you sure you want to reject these requests?\n\n";

            foreach (var selectedPendingUser in SelectedPendingUsers)
            {
                content += $"{selectedPendingUser.FullName} - {selectedPendingUser.Role}\n";
            }

            var dialog = await ShowConfirmationDialog(header, content, "Reject", Color.Error);
            var result = await dialog.Result;

            if (result != null && !result.Canceled)
            {
                var token = await TokenService.GetToken();
                var client = CreateApiClient(token);

                var selectedIds = SelectedPendingUsers.Select(x => x.Id);

                var request = await client.PostAsJsonAsync("api/user/pending/delete", selectedIds);

                if (request.IsSuccessStatusCode)
                {
                    foreach (var selected in SelectedPendingUsers)
                    {
                        PendingUsersList.Remove(selected);
                    }

                    SelectedPendingUsers.Clear();

                    Notify("Requests rejected successfully", Severity.Success);
                }
                else
                {
                    string error = await request.Content.ReadAsStringAsync();
                    await Js.InvokeVoidAsync("console.log", error);

                    Notify("An error occurred trying to reject requests", Severity.Error);
                }
            }
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
