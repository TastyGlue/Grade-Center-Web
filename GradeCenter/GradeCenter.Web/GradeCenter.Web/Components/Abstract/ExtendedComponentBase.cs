namespace GradeCenter.Web.Components.Abstract
{
    public class ExtendedComponentBase<T> : ComponentBase
    {
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
        [Inject] public TokenService TokenService { get; set; } = default!;
        [Inject] public ProtectedLocalStorage LocalStorage { get; set; } = default!;
        [Inject] public IHttpClientFactory HttpClientFactory { get; set; } = default!;
        [Inject] public ISnackbar Snackbar { get; set; } = default!;
        [Inject] public IDialogService DialogService { get; set; } = default!;
        [Inject] public IJSRuntime Js { get; set; } = default!;

        public JsonSerializerOptions CaseInsensitiveJson
            => new() { PropertyNameCaseInsensitive = true };

        private DialogOptions DefaultConfirmationOptions { get; set; } = new() { BackdropClick = true, CloseButton = true };

        public HttpClient CreateApiClient(string? accessToken = null)
        {
            var client = HttpClientFactory.CreateClient(Constants.API_CLIENT_NAME);

            if (client is null)
                NavigationManager.NavigateTo("/error");

            if (accessToken is not null)
                client!.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

            return client!;
        }

        public Dictionary<string, string> EnumToDict<TEnum>() where TEnum : Enum
        {
            var dict = new Dictionary<string, string>();

            foreach (var item in Enum.GetValues(typeof(TEnum)))
            {
                string itemString = item.ToString() ?? "";
                dict.Add(itemString, ToTitleCase(itemString));
            }

            return dict;
        }

        public static string ToTitleCase(string value)
        {
            TextInfo info = CultureInfo.CurrentCulture.TextInfo;
            return info.ToTitleCase(value.ToLower());
        }

        public void Notify(string message, Severity severity, int duration = 5000)
        {
            Snackbar.Add(message, severity, config => { config.VisibleStateDuration = duration; });
        }

        public Task<IDialogReference> ShowConfirmationDialog(string header, string content, string button, Color color)
        {
            var parameters = new DialogParameters<ConfirmationDialog>
            {
                { x => x.ContentText, content},
                { x => x.ButtonText, button},
                { x => x.Color, color}
            };

            return DialogService.ShowAsync<ConfirmationDialog>(header, parameters, DefaultConfirmationOptions);
        }
    }
}
