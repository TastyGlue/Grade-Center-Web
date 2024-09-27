namespace GradeCenter.Web.Components.Abstract
{
    public class ExtendedComponentBase<T> : ComponentBase
    {
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;

        [Inject] public IHttpClientFactory HttpClientFactory { get; set; } = default!;

        public JsonSerializerOptions CaseInsensitiveJson
            => new() { PropertyNameCaseInsensitive = true };

        public HttpClient CreateApiClient(string? accessToken = null)
        {
            var client = HttpClientFactory.CreateClient(Constants.API_CLIENT_NAME);

            if (client is null)
                NavigationManager.NavigateTo("/error");

            if (accessToken is not null)
                client!.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

            return client!;
        }
    }
}
